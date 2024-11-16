using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;
using Microsoft.ML.Data;

namespace PowerNow.API.Controllers
{
    public class DadosEnergiaSolar
    {
        [LoadColumn(0)] public string Data { get; set; } // Data da medição
        [LoadColumn(1)] public string Hora { get; set; } // Hora da medição
        [LoadColumn(2)] public float PrevisaoGeracao { get; set; } // Previsão de geração (kWh)
        [LoadColumn(3)] public float GeracaoReal { get; set; } // Geração real (kWh)
        [LoadColumn(4)] public float Temperatura { get; set; } // Temperatura (°C)
        [LoadColumn(5)] public float IrradiacaoSolar { get; set; } // Irradiação solar (W/m²)
        [LoadColumn(6)] public float VelocidadeVento { get; set; } // Velocidade do vento (m/s)
        [LoadColumn(7)] public float UmidadeRelativa { get; set; } // Umidade relativa (%)
    }

    public class RecomendacaoEnergiaSolar
    {
        [ColumnName("Score")]
        public float PontuacaoRecomendacao { get; set; }
        [ColumnName("GeracaoReal")]
        public float GeracaoReal { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class EnergiaSolarController : ControllerBase
    {
        private readonly string caminhoModelo = Path.Combine(Environment.CurrentDirectory, "wwwroot", "MLModels", "ModeloEnergiaSolar.zip");
        private readonly string caminhoTreinamento = Path.Combine(Environment.CurrentDirectory, "Data", "geracao_energia_previsao.csv");
        private readonly MLContext mlContext;

        public EnergiaSolarController()
        {
            mlContext = new MLContext();

            if (!System.IO.File.Exists(caminhoModelo))
            {
                Console.WriteLine("Modelo não encontrado. Iniciando treinamento...");
                TreinarModelo();
            }
        }

        [HttpGet("recomendar/{temperatura}/{irradiacao}/{vento}/{umidade}")]
        public IActionResult Recomendar(float temperatura, float irradiacao, float vento, float umidade)
        {
            if (!System.IO.File.Exists(caminhoModelo))
            {
                return BadRequest("O modelo ainda não foi treinado.");
            }

            ITransformer modelo;
            using (var stream = new FileStream(caminhoModelo, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                modelo = mlContext.Model.Load(stream, out var modeloSchema);
            }

            var engineRecomendacao = mlContext.Model.CreatePredictionEngine<DadosEnergiaSolar, RecomendacaoEnergiaSolar>(modelo);

            var recomendacao = engineRecomendacao.Predict(new DadosEnergiaSolar
            {
                Temperatura = temperatura,
                IrradiacaoSolar = irradiacao,
                VelocidadeVento = vento,
                UmidadeRelativa = umidade
            });

            return Ok(new
            {
                score = recomendacao.PontuacaoRecomendacao,
                geracaoReal = recomendacao.GeracaoReal,
                recomendacao = GetStatusRecomendacao(recomendacao.PontuacaoRecomendacao)
            });
        }

        private void TreinarModelo()
        {
            var pastaModelo = Path.GetDirectoryName(caminhoModelo);
            if (!Directory.Exists(pastaModelo))
            {
                Directory.CreateDirectory(pastaModelo);
                Console.WriteLine($"Diretório criado: {pastaModelo}");
            }

            IDataView dadosTreinamento = mlContext.Data.LoadFromTextFile<DadosEnergiaSolar>(
                path: caminhoTreinamento, hasHeader: true, separatorChar: ',');

            var pipeline = mlContext.Transforms.CopyColumns(outputColumnName: "Label", inputColumnName: nameof(DadosEnergiaSolar.GeracaoReal))
                .Append(mlContext.Transforms.Concatenate("Features", nameof(DadosEnergiaSolar.Temperatura), nameof(DadosEnergiaSolar.IrradiacaoSolar), nameof(DadosEnergiaSolar.VelocidadeVento), nameof(DadosEnergiaSolar.UmidadeRelativa)))
                .Append(mlContext.Regression.Trainers.FastTree());

            var modelo = pipeline.Fit(dadosTreinamento);

            mlContext.Model.Save(modelo, dadosTreinamento.Schema, caminhoModelo);
            Console.WriteLine($"Modelo treinado e salvo em: {caminhoModelo}");
        }

        private string GetStatusRecomendacao(float pontuacao)
        {
            switch (Math.Round(pontuacao, 1))
            {
                case >= 4:
                    return "Alta eficiência";
                case >= 3:
                    return "Eficiência média";
                default:
                    return "Baixa eficiência";
            }
        }
    }
}
