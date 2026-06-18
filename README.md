# Fluxo completo da API

## 1. Inicialização da aplicação

Antes de utilizar qualquer funcionalidade da API, execute o endpoint responsável por criar todos os registros necessários para o funcionamento do sistema.

### Endpoint

```http
POST /AccessConfig/start-app
```

### Objetivo

Cria automaticamente os registros iniciais da aplicação:

- Roles
- Tipos de log
- Usuários padrão
- Logins
- Produtos genéricos

Após sua execução a aplicação estará pronta para utilização.

---

# 2. Cadastro de usuários

## Endpoint

```http
POST /AccessRegister/create-user
```

Este endpoint cria um novo usuário juntamente com seu login.

---

## Cadastro de Administrador

### Request

```json
{
  "fullName": "dados iniciais adm",
  "nickname": "inicio-adm",
  "email": "inicio@teste.com",
  "password": "dados123",
  "roleId": 1
}
```

### Response

```json
{
  "userId": 4,
  "fullName": "dados iniciais adm",
  "nickname": "inicio-adm",
  "email": "inicio@teste.com",
  "password": "dados123",
  "roleId": "ADMIN"
}
```

---

## Cadastro de Vendedor

### Request

```json
{
  "fullName": "dados iniciais seller",
  "nickname": "inicio-sel",
  "email": "inicio-sell@teste.com",
  "password": "dados123",
  "roleId": 2
}
```

### Response

```json
{
  "userId": 5,
  "fullName": "dados iniciais seller",
  "nickname": "inicio-sel",
  "email": "inicio-sell@teste.com",
  "password": "dados123",
  "roleId": "SELLER"
}
```

---

# 3. Login

## Endpoint

```http
POST /AccessRegister/login
```

Este endpoint autentica o usuário e registra o login no sistema.

Quando o envio de e-mails estiver habilitado, um token JWT será enviado automaticamente para o e-mail cadastrado.

Atualmente o disparo de e-mails encontra-se temporariamente bloqueado, portanto o token JWT é retornado diretamente na resposta da API.

---

## Request

```json
{
  "nickname": "inicio-adm",
  "password": "dados123"
}
```

---

## Response (Administrador)

```text
Token gerado: eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiI0IiwidW5pcXVlX25hbWUiOiJpbmljaW8tYWRtIiwicm9sZSI6IkFETUlOIiwibmJmIjoxNzgxODAwNDUzLCJleHAiOjE3ODE4MjkyNTMsImlhdCI6MTc4MTgwMDQ1M30.UQTtq6BfKzal6YEiXQquBSgAhV1AnqV5lb9Jpj89Jvg
```

---

## Response (Vendedor)

```text
Token gerado: eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiI1IiwidW5pcXVlX25hbWUiOiJpbmljaW8tc2VsIiwicm9sZSI6IlNFTExFUiIsIm5iZiI6MTc4MTgwMDUwNiwiZXhwIjoxNzgxODI5MzA2LCJpYXQiOjE3ODE4MDA1MDZ9.OlcYUH6dQURCbne5mDlMoyxj0ePtObwdpTkXn9RHeUU
```

---

## Resposta esperada quando o envio de e-mails estiver habilitado

```text
Acabamos de enviar um token secreto para o seu e-mail. Com ele, você poderá acessar sua conta com segurança.
```

---

## Autenticação

Após obter o token JWT, todas as requisições protegidas deverão conter o seguinte header:

```http
Authorization: Bearer <TOKEN>
```

---

# 4. Produtos iniciais

Após a execução do endpoint:

```http
POST /AccessConfig/start-app
```

a aplicação cadastra automaticamente **10 produtos genéricos**, permitindo que pedidos possam ser criados imediatamente.

Os próximos exemplos demonstram o cadastro de novos produtos além daqueles gerados automaticamente.

---

# 5. Cadastro de produtos

## Endpoint

```http
POST /Product/create-product
```

---

## Primeiro produto

### Request

```json
{
  "productName": "Golden Leash",
  "productDescription": "Coleira de ouro para tiges",
  "price": 9.99,
  "stockQuantity": 10
}
```

### Response

```json
{
  "productId": 11,
  "productName": "Golden Leash",
  "productDescription": "Coleira de ouro para tiges",
  "price": 9.99,
  "stockQuantity": 10
}
```

---

## Segundo produto

### Request

```json
{
  "productName": "Interactive Cat Laser Toy",
  "productDescription": "Brinquedo laser automático para gatos, com temporizador e modos de movimento.",
  "price": 24.99,
  "stockQuantity": 10
}
```

### Response

```json
{
  "productId": 12,
  "productName": "Interactive Cat Laser Toy",
  "productDescription": "Brinquedo laser automático para gatos, com temporizador e modos de movimento.",
  "price": 24.99,
  "stockQuantity": 10
}
```

---

# 6. Cadastro de estoque

## Endpoint

```http
POST /Stock/add-stock
```

---

## Adicionar estoque ao primeiro produto

### Request

```json
{
  "productId": 11,
  "quantity": 4,
  "invoiceNumber": "123456"
}
```

### Response

```json
{
  "stockMovementId": 1,
  "productId": 11,
  "productName": "Golden Leash",
  "quantity": 4,
  "invoiceNumber": "123456",
  "movementDate": "2026-06-18T17:28:30.6913888Z",
  "movementType": "Entrada"
}
```

---

## Adicionar estoque ao segundo produto

### Request

```json
{
  "productId": 12,
  "quantity": 40,
  "invoiceNumber": "58545625"
}
```

### Response

```json
{
  "stockMovementId": 4,
  "productId": 12,
  "productName": "Interactive Cat Laser Toy",
  "quantity": 40,
  "invoiceNumber": "58545625",
  "movementDate": "2026-06-18T17:42:42.4122006Z",
  "movementType": "Entrada"
}
```

---

# 7. Atualização de produto

## Endpoint

```http
PUT /Product/update-product
```

### Request

```json
{
  "productId": 12,
  "productName": "Interactive Cat Laser Toy",
  "productDescription": "Brinquedo laser automático para gatos, com temporizador e modos de movimento.",
  "price": 24.99,
  "stockQuantity": 45
}
```

### Response

```json
{
  "productId": 12,
  "productName": "Interactive Cat Laser Toy",
  "productDescription": "Brinquedo laser automático para gatos, com temporizador e modos de movimento.",
  "price": 24.99,
  "stockQuantity": 45
}
```

---

# 8. Criação de pedidos

## Endpoint

```http
POST /Order/create-order
```

Este endpoint cria uma nova comanda utilizando os produtos cadastrados e disponíveis em estoque.

---

### Request

```json
{
  "customerDocument": "12345678901",
  "sellerName": "jonas",
  "items": [
    {
      "productId": 1,
      "quantity": 7
    }
  ]
}
```

### Response

```json
{
  "orderId": 4,
  "customerDocument": "12345678901",
  "sellerName": "jonas",
  "totalAmount": 209.93,
  "createdAt": "2026-06-18T19:45:53.8539625Z",
  "items": [
    {
      "productId": 1,
      "productName": "Dog Food",
      "quantity": 7,
      "unitPrice": 29.99,
      "subtotal": 209.93
    }
  ]
}
```

---

# Fluxo completo de utilização

1. Executar `POST /AccessConfig/start-app`.
2. Criar usuários utilizando `POST /AccessRegister/create-user`.
3. Realizar login utilizando `POST /AccessRegister/login`.
4. Copiar o token JWT retornado.
5. Informar o token no header `Authorization: Bearer <TOKEN>`.
6. Cadastrar novos produtos.
7. Adicionar estoque aos produtos.
8. Atualizar produtos quando necessário.
9. Criar pedidos utilizando `POST /Order/create-order`.