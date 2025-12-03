# 📡 Documentação de Endpoints - API ClickDesk

Documentação completa dos endpoints da API Backend Java/Spring Boot utilizados pelo aplicativo mobile.

---

## 🔧 Configuração

### Base URL
```
http://localhost:8080
```

Para dispositivo físico, use o IP da máquina:
```
http://192.168.x.x:8080
```

### Headers Padrão
```http
Content-Type: application/json
Authorization: Bearer {jwt_token}
```

---

## 🔐 Autenticação

### 1. Registrar Novo Usuário
```http
POST /auth/register
```

**Body:**
```json
{
  "username": "joao.silva",
  "email": "joao@example.com",
  "nome": "João Silva"
}
```

**Response (201):**
```json
{
  "mensagem": "Usuário registrado. Verifique seu email.",
  "userId": 1
}
```

**Códigos de Status:**
- `201` - Usuário criado com sucesso
- `400` - Dados inválidos
- `409` - Usuário já existe

---

### 2. Verificar Email
```http
POST /auth/verify-email
```

**Body:**
```json
{
  "email": "joao@example.com",
  "codigo": "123456"
}
```

**Response (200):**
```json
{
  "mensagem": "Email verificado com sucesso",
  "token": "temporary_token"
}
```

---

### 3. Definir Senha
```http
POST /auth/set-password
```

**Headers:**
```http
Authorization: Bearer {temporary_token}
```

**Body:**
```json
{
  "senha": "SenhaSegura123!",
  "confirmarSenha": "SenhaSegura123!"
}
```

**Response (200):**
```json
{
  "mensagem": "Senha definida com sucesso"
}
```

---

### 4. Login
```http
POST /auth/login
```

**Body:**
```json
{
  "username": "joao.silva",
  "senha": "SenhaSegura123!"
}
```

**Response (200):**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "tipo": "Bearer",
  "usuario": {
    "id": 1,
    "username": "joao.silva",
    "email": "joao@example.com",
    "nome": "João Silva",
    "role": "USER"
  }
}
```

**Códigos de Status:**
- `200` - Login bem-sucedido
- `401` - Credenciais inválidas
- `403` - Usuário inativo

---

## 🎫 Chamados

### 1. Listar Todos os Chamados (Admin/Tech)
```http
GET /api/chamados
```

**Query Parameters:**
- `pagina` (opcional) - Número da página (default: 0)
- `tamanho` (opcional) - Itens por página (default: 20)
- `status` (opcional) - Filtrar por status
- `severidade` (opcional) - Filtrar por severidade

**Response (200):**
```json
{
  "conteudo": [
    {
      "id": 1,
      "titulo": "Problema no sistema",
      "descricao": "O sistema está lento",
      "categoria": "SOFTWARE",
      "severidade": "MEDIA",
      "status": "ABERTO",
      "usuarioId": 1,
      "tecnicoId": null,
      "dataCriacao": "2024-12-03T10:00:00",
      "dataAtualizacao": "2024-12-03T10:00:00"
    }
  ],
  "totalPaginas": 5,
  "totalElementos": 95,
  "paginaAtual": 0
}
```

---

### 2. Criar Novo Chamado
```http
POST /api/chamados
```

**Body:**
```json
{
  "titulo": "Problema no sistema",
  "descricao": "Descrição detalhada do problema",
  "categoria": "SOFTWARE",
  "severidade": "MEDIA"
}
```

**Response (201):**
```json
{
  "id": 1,
  "titulo": "Problema no sistema",
  "descricao": "Descrição detalhada do problema",
  "categoria": "SOFTWARE",
  "severidade": "MEDIA",
  "status": "ABERTO",
  "usuarioId": 1,
  "dataCriacao": "2024-12-03T10:00:00"
}
```

**Códigos de Status:**
- `201` - Chamado criado
- `400` - Dados inválidos
- `401` - Não autenticado

---

### 3. Listar Meus Chamados
```http
GET /api/chamados/meus
```

**Response (200):**
```json
[
  {
    "id": 1,
    "titulo": "Meu problema",
    "status": "ABERTO",
    "severidade": "MEDIA",
    "dataCriacao": "2024-12-03T10:00:00"
  }
]
```

---

### 4. Listar Chamados do Técnico
```http
GET /api/chamados/tecnico
```

**Apenas para usuários com role TECH ou ADMIN**

**Response (200):**
```json
[
  {
    "id": 1,
    "titulo": "Chamado atribuído",
    "status": "EM_ATENDIMENTO",
    "usuario": {
      "nome": "João Silva"
    }
  }
]
```

---

### 5. Listar Chamados Pendentes
```http
GET /api/chamados/pendentes
```

**Apenas para TECH/ADMIN**

Lista chamados sem técnico atribuído.

**Response (200):**
```json
[
  {
    "id": 2,
    "titulo": "Chamado sem atribuição",
    "status": "ABERTO",
    "severidade": "ALTA",
    "dataCriacao": "2024-12-03T11:00:00"
  }
]
```

---

### 6. Buscar Chamado por ID
```http
GET /api/chamados/{id}
```

**Response (200):**
```json
{
  "id": 1,
  "titulo": "Problema no sistema",
  "descricao": "Descrição completa",
  "categoria": "SOFTWARE",
  "severidade": "MEDIA",
  "status": "ABERTO",
  "usuario": {
    "id": 1,
    "nome": "João Silva"
  },
  "tecnico": null,
  "resolucao": null,
  "feedback": null,
  "dataCriacao": "2024-12-03T10:00:00",
  "dataAtualizacao": "2024-12-03T10:00:00"
}
```

---

### 7. Atualizar Status do Chamado
```http
PUT /api/chamados/{id}/status
```

**Body:**
```json
{
  "status": "EM_ATENDIMENTO",
  "resolucao": "Estou trabalhando no problema"
}
```

**Response (200):**
```json
{
  "id": 1,
  "status": "EM_ATENDIMENTO",
  "resolucao": "Estou trabalhando no problema",
  "dataAtualizacao": "2024-12-03T11:00:00"
}
```

**Status Permitidos:**
- `ABERTO` → `EM_ATENDIMENTO`
- `EM_ATENDIMENTO` → `RESOLVIDO`
- `RESOLVIDO` → `FECHADO`
- `*` → `ESCALADO` (apenas admin)

---

### 8. Enviar Feedback
```http
POST /api/chamados/{id}/feedback
```

**Body:**
```json
{
  "feedback": "Problema resolvido rapidamente!",
  "avaliacao": 5
}
```

**Response (200):**
```json
{
  "mensagem": "Feedback registrado com sucesso"
}
```

---

## ❓ FAQ

### 1. Listar FAQs
```http
GET /api/faqs
```

**Query Parameters:**
- `categoria` (opcional) - Filtrar por categoria

**Response (200):**
```json
[
  {
    "id": 1,
    "pergunta": "Como resetar minha senha?",
    "resposta": "Clique em 'Esqueci a senha' na tela de login...",
    "categoria": "AUTENTICACAO",
    "visualizacoes": 150,
    "util": 120,
    "dataCriacao": "2024-12-01T10:00:00"
  }
]
```

---

### 2. Buscar FAQs
```http
GET /api/faqs/search
```

**Query Parameters:**
- `q` (obrigatório) - Termo de busca

**Response (200):**
```json
[
  {
    "id": 1,
    "pergunta": "Como resetar senha?",
    "resposta": "...",
    "relevancia": 0.95
  }
]
```

---

### 3. Criar FAQ (Admin)
```http
POST /api/faqs
```

**Body:**
```json
{
  "pergunta": "Como criar um chamado?",
  "resposta": "Para criar um chamado, clique em...",
  "categoria": "CHAMADOS"
}
```

**Response (201):**
```json
{
  "id": 2,
  "pergunta": "Como criar um chamado?",
  "resposta": "Para criar um chamado, clique em...",
  "categoria": "CHAMADOS"
}
```

---

### 4. Atualizar FAQ (Admin)
```http
PUT /api/faqs/{id}
```

**Body:**
```json
{
  "pergunta": "Pergunta atualizada",
  "resposta": "Resposta atualizada",
  "categoria": "CHAMADOS"
}
```

---

### 5. Deletar FAQ (Admin)
```http
DELETE /api/faqs/{id}
```

**Response (204):**
```
No Content
```

---

## 🤖 Inteligência Artificial

### 1. Obter Assistência da IA
```http
POST /api/ia/assist
```

**Body:**
```json
{
  "mensagem": "Meu computador não liga",
  "contexto": {
    "categoria": "HARDWARE"
  }
}
```

**Response (200):**
```json
{
  "resposta": "Vamos tentar alguns passos para resolver:\n1. Verifique se o cabo de energia está conectado\n2. ...",
  "confianca": 0.85,
  "sugestoes": [
    {
      "titulo": "Verificar alimentação",
      "descricao": "..."
    }
  ],
  "faqsRelacionados": [1, 3, 5]
}
```

---

## 📋 Enums e Constantes

### Status do Chamado
```javascript
ABERTO              // Recém-criado
EM_ATENDIMENTO      // Técnico atribuído
AGUARDANDO          // Aguardando informações
RESOLVIDO_AUTOMATICO // Resolvido pela IA
RESOLVIDO           // Resolvido pelo técnico
FECHADO             // Finalizado
ESCALADO            // Escalado
```

### Severidade
```javascript
BAIXA     // Prioridade baixa
MEDIA     // Prioridade média
ALTA      // Prioridade alta
CRITICA   // Atenção imediata
```

### Categoria
```javascript
SOFTWARE      // Problemas em aplicações
HARDWARE      // Falhas de equipamento
REDES         // Conexão/infraestrutura
TREINAMENTO   // Dúvidas/capacitação
OUTROS        // Outros assuntos
```

### Roles de Usuário
```javascript
USER   // Usuário comum
TECH   // Técnico de suporte
ADMIN  // Administrador
```

---

## 🔒 Autenticação e Autorização

### Fluxo de Autenticação
1. Usuário faz login → recebe JWT token
2. Token é armazenado no AsyncStorage
3. Token é enviado em todas as requisições via header `Authorization: Bearer {token}`
4. Backend valida token e permissões

### Requisitos por Endpoint

| Endpoint | Autenticação | Role Mínimo |
|----------|--------------|-------------|
| `POST /auth/login` | ❌ Não | - |
| `POST /auth/register` | ❌ Não | - |
| `GET /api/chamados/meus` | ✅ Sim | USER |
| `POST /api/chamados` | ✅ Sim | USER |
| `GET /api/chamados` | ✅ Sim | TECH |
| `PUT /api/chamados/{id}/status` | ✅ Sim | TECH |
| `POST /api/faqs` | ✅ Sim | ADMIN |
| `DELETE /api/faqs/{id}` | ✅ Sim | ADMIN |

---

## 🐛 Códigos de Erro Comuns

| Código | Significado | Solução |
|--------|-------------|---------|
| 400 | Bad Request | Verificar formato dos dados enviados |
| 401 | Unauthorized | Token inválido ou expirado - fazer login novamente |
| 403 | Forbidden | Usuário não tem permissão - verificar role |
| 404 | Not Found | Recurso não encontrado |
| 409 | Conflict | Conflito (ex: usuário já existe) |
| 500 | Server Error | Erro no servidor - contatar suporte |

---

## 🧪 Testando a API

### Com cURL
```bash
# Login
curl -X POST http://localhost:8080/auth/login \
  -H "Content-Type: application/json" \
  -d '{"username":"admin","senha":"admin123"}'

# Listar chamados (com token)
curl -X GET http://localhost:8080/api/chamados/meus \
  -H "Authorization: Bearer {seu_token}"
```

### Com Postman
1. Importe a collection disponível
2. Configure a variável `{{baseUrl}}`
3. Faça login para obter o token
4. Token será usado automaticamente nas próximas requisições

---

## 📞 Suporte

Para dúvidas sobre a API:
- Consulte a documentação do Backend
- Verifique os logs do servidor
- Entre em contato com a equipe de backend

---

**Versão da API**: 1.0.0  
**Última Atualização**: Dezembro 2024
