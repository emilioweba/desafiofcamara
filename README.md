### 1. INTRODUÇÃO
#### 1.1 Objetivo 
Este documento descreve como funciona a arquitetura do website criado por Emílio Weba através do desafio proposto como parte do processo seletivo da FCamara.
#### 1.2 Escopo 
Este website contém apenas uma página com 3 botões. Um para gerar um token que expira em 60 segundos e os outros 2 botões populam uma tabela com dados. Para que os 2 botões funcionem, o token deve estar ativo. 

### 2. TECNOLOGIA
A seguir irei detalhar as principais tecnologias utilizadas no desenvolvimento do projeto. Em suma, utilizei ASP.NET 4.5, C# 5.0, WCF, Web API, Visual Studio 2013, Entity Framework 6, MVC, jQuery, Knockout.js, Microsoft Azure, Ajax e SQL Server 2012.
#### 2.1 WCF
Foi criado um contrato que contém 2 endpoints: 
- **GenerateToken**

Neste endpoint é gerado um novo token randomicamente.
- **ValidateToken**

Neste endpoint é verificado a data que o token foi criado com a data atual do servidor. Caso seja menor que 60 segundos é retornado um HttpStatusCode.Accepted. Caso contrário retorna-se HttpStatusCode.Unauthorized.

#### 2.2 Web API
Foi criado um serviço que recebe um token e consulta o endpoint do WCF **ValidateToken** para verificar se o token está ativo. Caso esteja, usando Entity Framework, consulto o banco de dados SQL Server que está hospedado na nuvem (Azure) e retorno a lista de produtos.

#### 2.3 Como de fato funciona o Front-End
Ao clicar em **Gerar Token**, um método javascript é acionado. Neste método faço um $.get() no serviço WCF **GenerateToken** (que encontra-se neste link: http://www.emilioweba.com/Home/GenerateToken) e o novo token é exibido na View, além de iniciar-se uma contagem regressiva começando em 60 segundos.

Com o token válido, ao clicar em Buscar Produtos MVC, o serviço da Web API é chamado através de uma chamada XMLHttpRequest passando o token como parâmetro no link http://www.emilioweba.com/api/product/. Caso menos de 60 segundos tenham se passado, os produtos são retornados para a View e, usando Knockout.js, a tabela é populada dinamicamente e exibida para o usuário.

Com o token válido, ao clicar em Buscar Produtos Ajax, uma chamada $.ajax é realizada para o mesmo serviço Web API e também uso Knockout para popular a tabela.

Em ambos os casos, caso tenha-se passado mais de 60 segundos, uma mensagem é exibida na View pedindo ao usuário para que gere um novo token. 

É possível acessar a página proposta no seguinte link:  http://www.emilioweba.com/Home/Index. O site e o banco de dados estão hospedados na plataforma Microsoft Azure. O código está disponível em https://github.com/emilioweba/desafiofcamara.
