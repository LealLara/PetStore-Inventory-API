# PetStore Inventory API - Guia de QA

Este projeto e uma API .NET 8 para controle de inventario de pet shop. A aplicacao organiza o dominio em quatro camadas principais:

- `PetStore.Inventory.Api`: controllers, DTOs HTTP e configuracao da API.
- `PetStore.Inventory.Application`: servicos de aplicacao, orquestracao de regras e contratos.
- `PetStore.Inventory.Domain`: modelos de negocio, entidades, validacoes e constantes.
- `PetStore.Inventory.Infrastructure`: `DbContext` e repositorios de persistencia.

## Objetivo de qualidade

O foco de QA e garantir que as regras de negocio principais sejam verificadas por testes unitarios e que os fluxos criticos da API sejam documentados para execucao manual, automatizada e regressiva.

A suite atual cobre:

- cadastro, consulta, atualizacao e remocao logica de produtos via servicos e controllers;
- criacao de pedidos, calculo de total e baixa de estoque;
- adicao de estoque e registro de movimentacao;
- criacao inicial de roles, usuarios, logins, produtos e tipos de log;
- validacoes FluentValidation de produto, usuario, estoque, role e tipo de log;
- login com hash de senha, validacao de credenciais e geracao de JWT;
- envio de e-mail de login via mock de repositorio;
- mapeamento de DTOs HTTP para requests de aplicacao;
- respostas HTTP esperadas para cenarios 200, 400, 401, 404 e 500 em controllers representativos.

## Como executar os testes

Na raiz da solucao:

```powershell
dotnet test PetStore.Inventory.Tests\PetStore.Inventory.Tests.csproj
```

Com cobertura Cobertura:

```powershell
dotnet test PetStore.Inventory.Tests\PetStore.Inventory.Tests.csproj --collect:"XPlat Code Coverage"
```

O relatorio XML sera criado em:

```text
PetStore.Inventory.Tests\TestResults\<execucao>\coverage.cobertura.xml
```

Resultado validado em 18/06/2026:

```text
Total: 54 testes
Aprovados: 54
Falhas: 0
Ignorados: 0
Cobertura de linhas: 49,33%
Cobertura de branches: 27,15%
```

Observacao de QA: a cobertura numerica inclui camadas que exigem testes de integracao, como repositorios EF Core/SQLite, `AppDbContext` e bootstrap em `Program.cs`. A suite adicionada e unitária e usa mocks para evitar banco, e-mail real e rede.

## Estrutura dos testes

```text
PetStore.Inventory.Tests
|-- Controllers
|   `-- ControllerTests.cs
|-- Domain
|   `-- DomainAndDtoTests.cs
`-- Services
    `-- CoreServicesTests.cs
```

### `CoreServicesTests`

Cobre regras de aplicacao com mocks de repositorios e servicos dependentes:

- `ProductServices`
- `UserServices`
- `OrderServices`
- `RoleServices`
- `LogTypeServices`
- `StockServices`
- `LoginServices`
- `AuthenticationServices`
- `EmailServices`
- `AccessConfigServices`
- `AccessRegisterServices`

### `DomainAndDtoTests`

Cobre regras simples e conversoes:

- `ProductModel.AddStock`
- `ProductModel.RemoveStock`
- DTOs de usuario, produto e pedido
- validadores de produto, usuario e estoque
- criacao de logins padrao com senha hasheada

### `ControllerTests`

Cobre a traducao de resultado dos servicos para HTTP:

- `OkObjectResult`
- `BadRequestObjectResult`
- `UnauthorizedObjectResult`
- `NotFoundObjectResult`
- `ObjectResult` com status `500`

## Matriz de cenarios de QA

| Area | Cenario | Resultado esperado |
| --- | --- | --- |
| Inicializacao | Executar `AccessConfig/start-app` pela primeira vez | roles, tipos de log, usuarios, logins e produtos padrao criados |
| Inicializacao | Executar `start-app` novamente | erro informando que registros iniciais ja existem |
| Usuario | Criar usuario com role valida e e-mail unico | usuario criado |
| Usuario | Criar usuario antes do seed de roles | erro orientando iniciar a aplicacao |
| Usuario | Criar usuario com e-mail duplicado | erro de e-mail em uso |
| Usuario | Buscar usuario por ID invalido | erro de argumento |
| Produto | Criar produto com nome, preco e estoque validos | produto persistido |
| Produto | Criar produto com estoque zero | falha de validacao |
| Produto | Atualizar produto existente | produto atualizado |
| Produto | Atualizar produto com ID zero | erro de ID obrigatorio |
| Estoque | Adicionar estoque a produto existente | estoque incrementado e movimento registrado |
| Estoque | Adicionar estoque a produto inexistente | erro de produto nao encontrado |
| Pedido | Criar pedido com item valido | total calculado e estoque reduzido |
| Pedido | Criar pedido com produto inexistente | erro de produto nao encontrado |
| Pedido | Criar pedido sem estoque suficiente | erro de estoque insuficiente |
| Login | Login com senha correta | mensagem com token gerado |
| Login | Login com senha incorreta | retorno nulo/nao autorizado |
| E-mail | Gerar e-mail de login com repositorio OK | mensagem de sucesso |

## Execucao manual da API

### Subir com Docker

Na raiz da solucao:

```powershell
docker compose up --build
```

A API ficara disponivel em:

```text
http://localhost:8080
```

O banco SQLite do container e salvo no volume `petstore-inventory-data`, usando a string de conexao `Data Source=/data/PetStore.db`.

Para buildar a imagem manualmente:

```powershell
docker build -t petstore-inventory-api -f PetStore.Inventory.Api\Dockerfile .
```

Para executar a imagem manualmente:

```powershell
docker run --rm -p 8080:8080 -e ASPNETCORE_ENVIRONMENT=Docker -e "ConnectionStrings__DefaultConnection=Data Source=/data/PetStore.db" -v petstore-inventory-data:/data petstore-inventory-api
```

### Subir a API

```powershell
dotnet run --project PetStore.Inventory.Api\PetStore.Inventory.Api.csproj
```

Ambientes locais costumam expor Swagger em uma URL semelhante a:

```text
https://localhost:<porta>/swagger
```

Consulte tambem:

```text
PetStore.Inventory.Api\Properties\launchSettings.json
```

### Ordem sugerida de teste manual

1. Executar `POST /AccessConfig/start-app`.
2. Fazer login com um usuario padrao.
3. Copiar o token retornado e enviar como `Authorization: Bearer <token>`.
4. Consultar produtos.
5. Adicionar estoque a um produto.
6. Criar um pedido consumindo o produto.
7. Consultar estoque e historico de movimentacoes.
8. Validar respostas de erro com IDs inexistentes, estoque insuficiente e payloads invalidos.

## Endpoints por area

### Acesso e autenticacao

| Metodo | Rota | Objetivo |
| --- | --- | --- |
| `POST` | `/AccessConfig/start-app` | cria registros iniciais |
| `POST` | `/AccessRegister/create-user` | cria usuario e login |
| `POST` | `/AccessRegister/login` | autentica usuario |
| `DELETE` | `/AccessRegister/delete-account` | remove login e usuario |
| `POST` | `/Login/login` | autentica usuario |
| `GET` | `/Login/get-all-logins` | lista logins |

### Produtos

| Metodo | Rota | Objetivo |
| --- | --- | --- |
| `GET` | `/Product/get-all-products` | lista produtos |
| `POST` | `/Product/create-product` | cria produto |
| `GET` | `/Product/get-products-filtered-by-id` | busca por ID |
| `GET` | `/Product/get-products-filtered-by-string` | busca por texto |
| `PUT` | `/Product/update-product` | atualiza produto |
| `DELETE` | `/Product/remove-product` | remove produto |

### Estoque

| Metodo | Rota | Objetivo |
| --- | --- | --- |
| `POST` | `/Stock/add-stock` | adiciona estoque |
| `GET` | `/Stock/get-stock-movements` | lista movimentos por produto |
| `GET` | `/Stock/get-product-stock` | consulta estoque atual |

### Pedidos

| Metodo | Rota | Objetivo |
| --- | --- | --- |
| `POST` | `/Order/create-order` | cria pedido |
| `GET` | `/Order/get-all-orders` | lista pedidos |
| `GET` | `/Order/get-order-by-id` | busca pedido por ID |
| `GET` | `/Order/get-orders-by-seller` | busca pedidos por vendedor |

### Cadastros auxiliares

| Metodo | Rota | Objetivo |
| --- | --- | --- |
| `GET` | `/Role/get-all` | lista roles |
| `POST` | `/Role/create-role` | cria role |
| `GET` | `/LogType/get-all` | lista tipos de log |
| `POST` | `/LogType/create-log-type` | cria tipo de log |
| `GET` | `/User/get-all-users` | lista usuarios |
| `GET` | `/User/get-all-filtered-by-role-id` | filtra usuarios por role |
| `GET` | `/User/get-all-filtered-by-string` | filtra usuarios por texto |
| `GET` | `/User/get-all-filtered-by-user-id` | busca usuario por ID |
| `POST` | `/User/create-user` | cria usuario |

## Exemplos de payloads

### Criar produto

```json
{
  "productName": "Racao premium",
  "productDescription": "Pacote para caes adultos",
  "price": 99.9,
  "stockQuantity": 10
}
```

### Adicionar estoque

```json
{
  "productId": 1,
  "quantity": 5,
  "invoiceNumber": "NF-2026-001"
}
```

### Criar pedido

```json
{
  "customerDocument": "12345678900",
  "sellerName": "Ana",
  "items": [
    {
      "productId": 1,
      "quantity": 2
    }
  ]
}
```

### Login

```json
{
  "nickname": "admin",
  "password": "senha"
}
```

## Checklist de regressao

- Confirmar que o projeto compila sem erros.
- Rodar `dotnet test` antes de cada entrega.
- Rodar cobertura quando houver mudanca em regra de negocio.
- Validar que nenhum teste depende de ordem de execucao.
- Validar que nenhum teste usa banco real, e-mail real ou token real externo.
- Conferir que alteracoes em DTOs atualizam testes de mapeamento.
- Conferir que alteracoes em validadores atualizam casos positivos e negativos.
- Conferir que alteracoes em roles/autorizacao sao refletidas em testes manuais com token JWT.

## Riscos e recomendacoes

- Repositorios e `AppDbContext` devem receber testes de integracao com SQLite isolado ou container.
- `Program.cs` deve receber teste de smoke/integracao para validar injecao de dependencia, Swagger, autenticacao JWT e migrations.
- Controllers adicionais podem ganhar testes por metodo quando houver padrao de erro especifico ou contrato publico mais rigido.
- A solucao `.slnx` nao foi usada como fonte unica de validacao porque a execucao direta do projeto de testes revelou o estado real da compilacao.
- Senhas, chaves JWT e configuracoes sensiveis devem ser movidas para secrets/variaveis de ambiente em ambientes de QA e CI.
