# AstroBot - Sistema de Autenticação

Um sistema de autenticação robusto e moderno construído com .NET 8, focando em segurança e boas práticas de desenvolvimento.

## O que é?

O AstroBot é um projeto backend que implementa um sistema completo de autenticação, incluindo:
- Login com JWT (JSON Web Tokens)
- Registro de novos usuários
- Recuperação de senha
- Respostas padronizadas para melhor integração
- Proteção contra ataques de força bruta
- Validação de e-mail único
- Documentação Swagger/OpenAPI integrada

## Tecnologias Utilizadas

- .NET 8
- Entity Framework Core 8.0.0
- SQL Server
- JWT (com Microsoft.IdentityModel.Tokens 8.12.0)
- BCrypt.Net-Next 4.0.3
- Swagger/OpenAPI (Swashbuckle.AspNetCore 6.6.2)

## Requisitos

- .NET 8 SDK
- SQL Server (Local ou Remoto)
- Visual Studio 2022 ou VS Code

## Começando

1. Clone o repositório
```bash
git clone https://github.com/seu-usuario/AstroBot.git
cd AstroBot
```

2. Configure sua string de conexão em `appsettings.json`:

3. Configure suas chaves JWT em `appsettings.json`:

4. Rode as migrations para criar o banco de dados:
```bash
dotnet ef database update
```

5. Inicie o projeto:
```bash
dotnet run --project AstroBot.WebApi
```

A API estará disponível em:
- HTTP: http://localhost:5000
- HTTPS: https://localhost:5001
- Swagger UI: http://localhost:5000/swagger

## Endpoints da API

### Autenticação

#### POST /api/auth/register
Cria uma nova conta.
```json
{
  "email": "seu@email.com",
  "password": "suaSenha123"
}
```
Resposta de sucesso:
```json
{
  "statusCode": 200,
  "data": {
    "message": "User registered successfully"
  },
  "errorMessage": null
}
```

#### POST /api/auth/login
Faz login e retorna um token JWT.
```json
{
  "email": "seu@email.com",
  "password": "suaSenha123"
}
```
Resposta de sucesso:
```json
{
  "statusCode": 200,
  "data": {
    "token": "seu.token.jwt"
  },
  "errorMessage": null
}
```

#### POST /api/auth/reset-password
Redefine a senha de uma conta existente.
```json
{
  "email": "seu@email.com",
  "newPassword": "novaSenha123"
}
```
Resposta de sucesso:
```json
{
  "statusCode": 200,
  "data": {
    "message": "Password reset successfully"
  },
  "errorMessage": null
}
```

## Estrutura do Projeto

O projeto segue Clean Architecture para manter o código organizado e testável:

```
AstroBot/
├── Domain/          # Entidades e regras de negócio
│   ├── Entities/    # Classes de domínio
│   └── Interfaces/  # Contratos do domínio
├── Application/     # Casos de uso e DTOs
│   ├── DTOs/        # Objetos de transferência de dados
│   ├── Interfaces/  # Contratos da aplicação
│   └── Services/    # Implementação dos casos de uso
├── Infrastructure/  # Implementações externas
│   └── Services/    # Serviços de infraestrutura (JWT, etc)
├── Persistence/     # Acesso a dados
│   ├── Context/     # Contexto do EF Core
│   ├── Mappings/    # Configurações de entidades
│   └── Repositories/# Implementações dos repositórios
└── WebApi/          # Controllers e configuração
    ├── Controllers/ # Endpoints da API
    └── Program.cs   # Configuração da aplicação
```

## Respostas da API

Todas as respostas seguem o mesmo padrão:

### Sucesso
```json
{
  "statusCode": 200,
  "data": { ... },
  "errorMessage": null
}
```

### Erro
```json
{
  "statusCode": 400,
  "data": null,
  "errorMessage": "Mensagem detalhando o erro"
}
```

## Segurança

- Senhas são hasheadas com BCrypt
- Tokens JWT com expiração de 1 hora
- Validação de e-mail único
- Proteção contra ataques de força bruta
- HTTPS habilitado por padrão
- Headers de autenticação via Bearer token
- Swagger UI protegido com autenticação JWT

## Licença

Este projeto está licenciado sob a licença MIT - veja o arquivo [LICENSE](LICENSE) para mais detalhes.