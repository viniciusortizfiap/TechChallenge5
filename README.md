# Introdução 
Esse projeto tem como objetivo criar um <u>Sistema de Gestão de Investimentos</u>, utilizando-se da arquitetura limpa e 
focando na separação de preocupações e independência entre UI, regras de negócio e infraestrutura, 
utilizando testes automatizados executados por meio de pipeline.

O sistema utiliza o SQLite para armazenar seus dados.

# Detalhes da arquitetura

Para ler sobre a arquitetura de forma detalhada, [clique aqui](Detalhes_arquitetura.docx)

# Funcionalidades

O sistema irá:

1. <b>Cadastrar novos usuários:</b><br>
    O sistema é capaz de cadastrar/alterar os dados de usuários que podem ter investimentos, com Nome, Email e Senha.<br>
    Para segurança, a senha é armazenada como um Hash SHA256.
2. <b>Cadastrar Ativos</b><br>
    O sistema faz o cadastro/alteração de Ativos em que o usuário pode investir, com Nome, Tipo do ativo (ex: Ações, Títulos, Criptomoedas) e seu código de negociação<br>
3. <b>Criação de Portfolios:</b><br>
    Após logado no sistema, o usuário pode criar um ou mais portfólios para seus ativos, que será usado para a compra e venda dos mesmos<br>
4. <b>Realizar Transações:</b><br>
    O usuário logado e com um portifólio, pode fazer transações de compra e venda de ativos, para adicioná-los ou retirá-los de seu portifólio.

# Execução do Projeto
1. Instale o .NET 8.
2. Abra o projeto em uma IDE (Visual Studio Code ou Visual Studio 2022).
3. Configure o projeto TechChallenge5 como o projeto de inicialização (Set as Start Project).
4. Execute o projeto de API (dotnet run ou use o botão de start do Visual Studio).

# Tecnologias usadas no projeto
1. .NET 8
2. Entity Framework 8.0.8
3. AutoMapper
4. SQLite
5. Dependency Injection
6. Swagger
