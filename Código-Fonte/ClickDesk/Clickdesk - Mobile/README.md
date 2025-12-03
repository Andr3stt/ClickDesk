# 📱 ClickDesk Mobile - React Native

Aplicação mobile React Native com Expo para o sistema de gerenciamento de chamados ClickDesk.

## 🚀 Tecnologias

- **React Native** 0.81.5
- **Expo** ~54.0.0
- **React Navigation** 6.x
- **Axios** 1.6.0
- **AsyncStorage** 1.19.0
- **Expo Vector Icons** 15.0.3

## 📦 Instalação

### Pré-requisitos
- Node.js 18+ instalado
- npm ou yarn
- Expo CLI (`npm install -g expo-cli`)
- Expo Go app no seu dispositivo móvel (Android/iOS)

### Passos

```bash
# Instalar dependências
npm install

# ou com yarn
yarn install
```

## ⚙️ Configuração

1. Copie o arquivo `.env.example` para `.env`:
```bash
cp .env.example .env
```

2. Configure a URL da API Backend no arquivo `.env`:
```env
REACT_APP_API_URL=http://localhost:8080
REACT_APP_TIMEOUT=30000
```

> **Nota**: Para testar no dispositivo físico, use o IP da sua máquina ao invés de `localhost`.

## 🏃 Executar

```bash
# Iniciar o servidor Expo
npm start

# Executar no Android
npm run android

# Executar no iOS
npm run ios

# Executar no navegador web
npm run web
```

Escaneie o QR code com o aplicativo Expo Go para testar no dispositivo.

## 📚 Estrutura do Projeto (100% PT-BR)

```
src/
├── configuracao/              # Configurações globais
│   ├── constantes.js         # Endpoints, enums e constantes da API
│   └── ambiente.js           # Configurações de ambiente
│
├── servicos/                 # Camada de serviços
│   ├── api/                  # Serviços de API
│   │   ├── clienteHttp.js    # Cliente Axios configurado com JWT
│   │   ├── autenticacaoService.js  # Serviço de autenticação
│   │   ├── chamadosService.js      # Serviço de chamados
│   │   ├── faqService.js           # Serviço de FAQs
│   │   └── iaService.js            # Serviço de IA
│   └── utilitarios/          # Funções utilitárias
│       ├── armazenamentoLocal.js   # AsyncStorage helpers
│       ├── validadores.js          # Validações de formulário
│       └── formatadores.js         # Formatação de dados
│
├── telas/                    # Telas da aplicação
│   ├── autenticacao/         # Telas de autenticação
│   │   ├── TelaLogin.js
│   │   ├── TelaRegistro.js
│   │   ├── TelaTermos.js
│   │   ├── TelaLogout.js
│   │   └── TelaEsqueciSenha.js
│   ├── usuario/              # Telas de usuário
│   │   ├── TelaDashboard.js
│   │   ├── TelaMeusChamados.js
│   │   ├── TelaDetalhesChamado.js
│   │   ├── TelaNovoChamado.js
│   │   ├── TelaEditarPerfil.js
│   │   └── TelaCriarPerfil.js
│   ├── administrador/        # Telas de administrador
│   │   ├── TelaDashboardAdmin.js
│   │   ├── TelaChamadosAdmin.js
│   │   ├── TelaFAQAdmin.js
│   │   └── TelaAprovacaoChamados.js
│   └── compartilhadas/       # Telas compartilhadas
│       ├── TelaFAQ.js
│       └── TelaListaChamados.js
│
├── componentes/              # Componentes reutilizáveis
│   ├── comum/               # Componentes básicos
│   ├── layout/              # Componentes de layout
│   └── chamados/            # Componentes específicos de chamados
│
├── contextos/               # Context API
├── hooks/                   # Custom Hooks
├── navegacao/               # Configuração de navegação
│   └── NavegadorPrincipal.js
│
├── estilos/                 # Estilos globais
│   ├── global.js           # Estilos reutilizáveis
│   ├── cores.js            # Paleta de cores
│   └── temas.js            # Temas da aplicação
│
├── modelos/                 # Modelos de dados
│   ├── Usuario.js
│   ├── Chamado.js
│   └── Enums.js
│
├── App.js                   # Componente raiz
└── index.js                 # Ponto de entrada
```

## 📡 Endpoints da API Backend

### Autenticação
| Método | Endpoint | Descrição |
|--------|----------|-----------|
| POST | `/auth/register` | Registrar novo usuário |
| POST | `/auth/verify-email` | Verificar email |
| POST | `/auth/set-password` | Definir senha |
| POST | `/auth/login` | Fazer login |

### Chamados
| Método | Endpoint | Descrição |
|--------|----------|-----------|
| GET | `/api/chamados` | Listar todos os chamados |
| POST | `/api/chamados` | Criar novo chamado |
| GET | `/api/chamados/meus` | Listar meus chamados |
| GET | `/api/chamados/tecnico` | Listar chamados do técnico |
| GET | `/api/chamados/pendentes` | Listar chamados pendentes |
| PUT | `/api/chamados/{id}/status` | Atualizar status |
| POST | `/api/chamados/{id}/feedback` | Enviar feedback |

### FAQ
| Método | Endpoint | Descrição |
|--------|----------|-----------|
| GET | `/api/faqs` | Listar FAQs |
| GET | `/api/faqs/search` | Buscar FAQs |

### IA
| Método | Endpoint | Descrição |
|--------|----------|-----------|
| POST | `/api/ia/assist` | Obter assistência da IA |

## 🔑 Autenticação

O sistema utiliza **JWT (JSON Web Token)** para autenticação:
- Token armazenado com **AsyncStorage** (React Native)
- Token enviado automaticamente em todas as requisições via interceptor Axios
- Logout automático em caso de token inválido (401)

## 👥 Papéis de Usuário (Roles)

| Role | Descrição | Acesso |
|------|-----------|--------|
| **USER** | Usuário comum | Dashboard, Criar/Ver chamados, FAQ |
| **TECH** | Técnico de suporte | Atender chamados, Gerenciar chamados |
| **ADMIN** | Administrador | Acesso total ao sistema |

## 📝 Status de Chamados

| Status | Descrição |
|--------|-----------|
| `ABERTO` | Chamado recém-criado |
| `EM_ATENDIMENTO` | Em atendimento por técnico |
| `AGUARDANDO` | Aguardando resposta |
| `RESOLVIDO_AUTOMATICO` | Resolvido pela IA |
| `RESOLVIDO` | Resolvido pelo técnico |
| `FECHADO` | Chamado finalizado |
| `ESCALADO` | Escalado para nível superior |

## 🎯 Severidades

| Severidade | Cor | Descrição |
|------------|-----|-----------|
| `BAIXA` | 🟢 Verde | Baixa prioridade |
| `MEDIA` | 🟡 Amarelo | Prioridade média |
| `ALTA` | 🟠 Laranja | Alta prioridade |
| `CRITICA` | 🔴 Vermelho | Crítico - atenção imediata |

## 📂 Categorias de Chamados

| Categoria | Ícone | Descrição |
|-----------|-------|-----------|
| `SOFTWARE` | 💻 | Problemas em aplicações |
| `HARDWARE` | 🖥️ | Falhas de equipamento |
| `REDES` | 🌐 | Conexão e infraestrutura |
| `TREINAMENTO` | 🎓 | Dúvidas e capacitação |
| `OUTROS` | 📋 | Outros assuntos |

## 🎨 Paleta de Cores

- **Primary**: `#E67E22` (Laranja)
- **Background**: `#EDE6D9` (Bege)
- **Text**: `#2C3E50` (Azul escuro)
- **Success**: `#27AE60` (Verde)
- **Error**: `#E74C3C` (Vermelho)
- **Warning**: `#F39C12` (Amarelo)

## 🔧 Migração da Estrutura Antiga

As telas foram reorganizadas da seguinte forma:

| Arquivo Antigo | Novo Arquivo | Localização |
|----------------|--------------|-------------|
| `LoginScreen.js` | `TelaLogin.js` | `telas/autenticacao/` |
| `RegisterScreen.js` | `TelaRegistro.js` | `telas/autenticacao/` |
| `DashboardScreen.js` | `TelaDashboard.js` | `telas/usuario/` |
| `AdminDashboardScreen.js` | `TelaDashboardAdmin.js` | `telas/administrador/` |
| `FAQScreen.js` | `TelaFAQ.js` | `telas/compartilhadas/` |

## 🐛 Troubleshooting

### Erro de conexão com API
- Verifique se o backend está rodando
- Use o IP da máquina ao invés de `localhost` no dispositivo físico
- Verifique as configurações de firewall

### Erro no AsyncStorage
```bash
npx expo install @react-native-async-storage/async-storage
```

### Erro no Expo
```bash
# Limpar cache
expo start -c
```

## 📄 Licença

Este projeto é parte do sistema ClickDesk e está sujeito às políticas da organização.

## 👨‍💻 Desenvolvimento

Para contribuir com o projeto:
1. Siga a estrutura de pastas em PT-BR
2. Use os componentes e estilos globais
3. Adicione comentários em português
4. Mantenha a consistência de código

---

**Desenvolvido com ❤️ pela equipe ClickDesk**
