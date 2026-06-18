# Fluxo completo de utilização da API

## 1. Inicializar a aplicação

### Criar os registros iniciais

**Endpoint**

```http
POST /AccessConfig/start-app
```

Este endpoint cria automaticamente:

- Roles padrão
- Tipos de log
- Usuários iniciais
- Logins
- Produtos genéricos

Após sua execução, a API estará pronta para uso.

---

## 2. Criar novos usuários

### Endpoint

```http
POST /AccessRegister/create-user
```

### Criar Administrador

#### Request

```json
{
  "fullName": "dados iniciais adm",
  "nickname": "inicio-adm",
  "email": "inicio@teste.com",
  "password": "dados123",
  "roleId": 1
}
```

#### Response

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

### Criar Vendedor

#### Request

```json
{
  "fullName": "dados iniciais seller",
  "nickname": "inicio-sel",
  "email": "inicio-sell@teste.com",
  "password": "dados123",
  "roleId": 2
}
```

#### Response

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

## 3. Realizar login

### Endpoint

```http
POST /AccessRegister/login
```

Responsável por autenticar o usuário e registrar o login no sistema.

Em ambiente normal, um e-mail contendo o token JWT é enviado ao usuário.

**Atualmente o envio de e-mail está temporariamente bloqueado**, portanto o token é retornado diretamente pela API.

### Request

```json
{
  "nickname": "inicio-adm",
  "password": "dados123"
}
```

### Token retornado (Administrador)

```text
Token gerado: eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiI0IiwidW5pcXVlX25hbWUiOiJpbmljaW8tYWRtIiwicm9sZSI6IkFETUlOIiwibmJmIjoxNzgxODAwNDUzLCJleHAiOjE3ODE4MjkyNTMsImlhdCI6MTc4MTgwMDQ1M30.UQTtq6BfKzal6YEiXQquBSgAhV1AnqV5lb9Jpj89Jvg
```

### Token retornado (Vendedor)

```text
Token gerado: eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiI1IiwidW5pcXVlX25hbWUiOiJpbmljaW8tc2VsIiwicm9sZSI6IlNFTExFUiIsIm5iZiI6MTc4MTgwMDUwNiwiZXhwIjoxNzgxODI5MzA2LCJpYXQiOjE3ODE4MDA1MDZ9.OlcYUH6dQURCbne5mDlMoyxj0ePtObwdpTkXn9RHeUU
```

### Mensagem esperada quando o envio de e-mail estiver habilitado

```text
Acabamos de enviar um token secreto para o seu e-mail. Com ele, você poderá acessar sua conta com segurança.
```

---

## Autenticação

Após obter o token, informe-o em todas as requisições protegidas.

```http
Authorization: Bearer <TOKEN>
```

---

## Produtos iniciais

Após executar o endpoint `/AccessConfig/start-app`, a aplicação cria automaticamente **10 produtos genéricos**.

Os exemplos abaixo demonstram a criação de novos produtos além desses registros iniciais.

---

## 4. Criar produtos

### Endpoint

```http
POST /Product/create-product
```

### Produto 1

#### Request

```json
{
  "productName": "Golden Leash",
  "productDescription": "Coleira de ouro para tiges",
  "price": 9.99,
  "stockQuantity": 10
}
```

#### Response

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

### Produto 2

#### Request

```json
{
  "productName": "Interactive Cat Laser Toy",
  "productDescription": "Brinquedo laser automático para gatos, com temporizador e modos de movimento.",
  "price": 24.99,
  "stockQuantity": 10
}
```

#### Response

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

## 5. Adicionar estoque

### Endpoint

```http
POST /Stock/add-stock
```

### Produto 11

#### Request

```json
{
  "productId": 11,
  "quantity": 4,
  "invoiceNumber": "123456"
}
```

#### Response

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

### Produto 12

#### Request

```json
{
  "productId": 12,
  "quantity": 40,
  "invoiceNumber": "58545625"
}
```

#### Response

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

## 6. Atualizar produto

### Endpoint

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

## 7. Criar pedido

### Endpoint

```http
POST /Order/create-order
```

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

## Fluxo resumido

1. Executar `POST /AccessConfig/start-app`.
2. Criar usuários utilizando `POST /AccessRegister/create-user`.
3. Fazer login em `POST /AccessRegister/login`.
4. Copiar o token JWT retornado.
5. Informar o token no header `Authorization: Bearer <TOKEN>`.
6. Cadastrar novos produtos.
7. Adicionar estoque aos produtos.
8. Atualizar produtos quando necessário.
9. Criar pedidos utilizando os produtos cadastrados.