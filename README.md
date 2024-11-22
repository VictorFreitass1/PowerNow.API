  # PowerNow.APIII

## Integrantes
- RM98695  - Breno Giacoppini Câmara   
- RM551744 - Jaqueline Martins dos Santos   
- RM97510  - Mariana Bastos Esteves   
- RM551155 - Matheus Oliveira Macedo   
- RM99982  - Victor Freitas Silva 

## Visão Geral
A **PowerNow.API** é uma aplicação desenvolvida com o intuito de analisar a geração de energia de painéis solares, aproveitando o crescimento exponencial do mercado de energia solar no Brasil. A API tem como principal objetivo fornecer recomendações personalizadas, com base em dados geográficos e climáticos, para maximizar a captação e o uso da energia gerada. 

O Brasil tem se destacado no setor de energia solar, como evidenciado pelos números recentes apresentados pela Agência Nacional de Energia Elétrica (Aneel) e pelo Portal Solar. Durante o primeiro semestre de 2024, o país adicionou 6,9 GW de energia solar à sua matriz elétrica, o que representa um grande avanço na utilização de fontes renováveis. Com base nesses dados, a **PowerNow** oferece uma solução que integra inteligência artificial (IA) para fornecer previsões sobre a geração de energia e recomendações para otimizar o uso de energia solar.

A grande inovação da aplicação é o uso de IA para prever a quantidade de energia que será gerada nos próximos dias ou meses, com base em informações climáticas e geográficas. Isso permite a recomendação personalizada de ações para aumentar a eficiência no uso dos sistemas fotovoltaicos, como o desligamento de rastreadores solares em dias com baixa incidência de luz. 

## Funcionalidades
- **Análise de Geração de Energia**: A API realiza a análise da energia gerada pelos painéis solares, levando em consideração fatores climáticos e geográficos.
- **Previsões de Geração**: Com o uso de inteligência artificial, a API prevê a quantidade de energia que será gerada nos próximos dias, fornecendo informações valiosas para os usuários.
- **Recomendações de Uso**: Através de IA, a API sugere ações para otimizar o uso da energia gerada, como o ajuste de sistemas de rastreamento solar.

## Estrutura de Camadas

- **Presentation Layer (Camada de Apresentação)**: Utiliza ASP.NET Core para expor os endpoints da API e garantir uma interface limpa e eficiente para os consumidores.
- **Application Layer (Camada de Aplicação)**: Contém a lógica de negócios e coordena as operações entre as diferentes camadas.
- **Domain Layer (Camada de Domínio)**: Define as entidades de domínio e as regras de negócios centrais.
- **Infrastructure Layer (Camada de Infraestrutura)**: Gerencia as integrações com serviços externos, como banco de dados e APIs externas.
- **Test Layer (Camada de Testes)**: Implementação de testes unitários e de integração para garantir a confiabilidade da aplicação.

## Clean Code 

- **Clean Code** é um conjunto de boas práticas e princípios para escrever códigos legíveis, simples, e fáceis de manter facilitando a manutenção e evolução do sistema. Alguns dos princípios básicos incluem:
- **Nomes significativos**: Os nomes de variáveis, métodos, classes e outros elementos do código devem ser descritivos e deixar claro seu propósito.
- **Funções pequenas e de única responsabilidade**: Funções devem ser curtas e fazer apenas uma coisa. Elas devem ser simples de entender e ter um único propósito.
- **Remoção de código Morto**: Código não utilizado ou comentado deve ser removido para evitar confusão e manter o código limpo. 
- **Tratamento de erros claro**: Exceções e validações ajudam a evitar comportamentos inesperados e facilitam a identificação de problemas.
- **Evite Dependências Ocultas**: O código deve ser claro sobre quais dados ele precisa. Depender de variáveis globais ou de comportamentos que não são explícitos pode gerar bugs difíceis de identificar. 

## SOLID
  
- **SRP (Single Responsibility Principle)**: Cada classe tem uma única responsabilidade.
- **OCP (Open/Closed Principle)**: Módulos são abertos para extensão e fechados para modificação.
- **LSP (Liskov Substitution Principle)**: Classe e interface específica para casos de uso.
- **ISP (Dependency Inversion Principle)**: Princípio da segregação de interfaces.
- **DIP (Dependency Inversion Principle)**: Inversão de dependências torna o sistema mais modular.

## Testes Unitários
- Testes unitários foram implementados nas camadas `ApplicationService` e `Repository` para verificar o funcionamento correto e a integridade dos dados, aumentando a robustez e confiabilidade da API.

## Design Patterns Utilizados

### 1. Singleton
O padrão Singleton foi utilizado para garantir que algumas classes críticas, como a de conexão com o banco de dados, tenham apenas uma instância ao longo da execução, economizando recursos.

### 2. Microservices
A API adota uma arquitetura de microservices para oferecer escalabilidade e modularidade. Cada serviço opera de forma independente, promovendo uma estrutura resiliente e fácil de manter.

### 3. Onion Architecture
Estrutura de desacoplamento entre camadas, proporcionando uma base sólida para o crescimento da aplicação.

## Tecnologias Utilizadas

- **Oracle Database**: Usado para armazenar e gerenciar os dados.
- **ASP.NET Core**: Framework para a criação da API.
- **OpenAPI/Swagger**: Para a documentação interativa da API.
- **ML.NET**: Para as funcionalidades de inteligência artificial e previsão de geração de energia.
- **xUnit**: Para implementação de testes automatizados.
- **ViaCEP API**: Para consultas de CEP.

## Requisitos

- **.NET SDK 8.0**
- **Visual Studio 2022 ou Visual Studio Code**
- **Oracle Database**
- **Ferramenta de gerenciamento de dependências**

## Instruções para Executar a API

### 1. Clone o repositório:
```
git clone <link-do-repositorio>
```

### 2. Navegue até a pasta do projeto:
```
cd PowerNow.API
```

### 3. Restaure os pacotes NuGet:
```
dotnet restore
```

### 4. Configure a string de conexão com o banco de dados ORACLE no arquivo appsettings.json:
```
"ConnectionStrings": {
  "Oracle": "Data Source=<oracle-db-url>;User Id=<username>;Password=<password>;"
}
```

### 5. Execute a aplicação:
```
dotnet run
```

### 6. Acesse a documentação da API gerada pelo Swagger:
```
Após executar a API, navegue até http://localhost:<porta>/swagger para visualizar e interagir com a documentação.
```

### 7. No caso de erro no banco de dados: Se houver inconsistências entre o código e o banco de dados, você pode gerar e aplicar migrations para corrigir a estrutura do banco.
```
Remove-Migration
```
```
Add-Migration <nome-da-migração>
```
```
Update-Database
```

## Testando a API
A **PowerNow** utiliza o Swagger para expor os endpoints de forma interativa. Abra a URL fornecida após executar a API e você verá a documentação da API com opções para testar cada endpoint.

## Contato
Para qualquer dúvida ou sugestão, entre em contato com victor.fsilva45@gmail.com
