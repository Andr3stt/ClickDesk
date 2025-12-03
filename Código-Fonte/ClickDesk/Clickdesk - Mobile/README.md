# 📱 ClickDesk Mobile - React

Aplicação mobile React para o sistema de gerenciamento de chamados ClickDesk.

## 🚀 Tecnologias

- React 18
- Axios
- React Router DOM

## 📦 Instalação

```bash
npm install
```

## ⚙️ Configuração

1. Copie o arquivo `.env.example` para `.env`
2. Configure a URL da API Backend:

```env
REACT_APP_API_URL=http://localhost:8080
```

## 🏃 Executar

```bash
npm start
```

## 📚 Estrutura do Projeto

```
src/
├── configuracao/      # Constantes e configurações
├── servicos/          # Serviços de API
├── componentes/       # Componentes React
├── paginas/          # Páginas/Telas
├── modelos/          # Modelos de dados
├── contextos/        # Context API
├── hooks/            # Custom Hooks
└── rotas/            # Configuração de rotas
```

## 📡 Endpoints Disponíveis

### Autenticação
- `POST /auth/register` - Registro
- `POST /auth/verify-email` - Verificação de email
- `POST /auth/set-password` - Definir senha
- `POST /auth/login` - Login

### Chamados
- `GET /api/chamados` - Listar todos
- `POST /api/chamados` - Criar novo
- `GET /api/chamados/meus` - Meus chamados
- `PUT /api/chamados/{id}/status` - Atualizar status
- `POST /api/chamados/{id}/feedback` - Enviar feedback

### FAQ
- `GET /api/faqs` - Listar FAQs
- `GET /api/faqs/search` - Buscar FAQs

### IA
- `POST /api/ia/assist` - Assistência por IA

## 🔑 Autenticação

O sistema utiliza JWT (JSON Web Token) para autenticação. O token é armazenado no localStorage e enviado automaticamente em todas as requisições.

## 👥 Papéis de Usuário

- **USER** - Usuário comum
- **TECH** - Técnico de suporte
- **ADMIN** - Administrador

## 📝 Status de Chamados

- ABERTO
- EM_ATENDIMENTO
- AGUARDANDO
- RESOLVIDO_AUTOMATICO
- RESOLVIDO
- FECHADO
- ESCALADO

## 🎯 Severidades

- BAIXA
- MEDIA
- ALTA
- CRITICA

## 📂 Categorias

- SOFTWARE - Problema em Aplicações
- HARDWARE - Falha de Equipamento
- REDES - Conexão e Infraestrutura
- TREINAMENTO - Dúvidas e Capacitação
- OUTROS - Outros Assuntos
