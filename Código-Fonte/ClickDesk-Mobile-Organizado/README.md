# 📱 ClickDesk Mobile - Aplicação Organizada

![ClickDesk Logo](../../Assets/clickdesk_logo.jpeg)

## 📋 Sobre o Projeto

O **ClickDesk Mobile** é uma aplicação React Native desenvolvida com Expo para gerenciamento de chamados de suporte técnico (helpdesk). Esta é a **versão organizada e profissional** do projeto, com estrutura escalável e código limpo.

### ✨ Principais Funcionalidades

- 🔐 **Autenticação JWT** - Login seguro com token
- 📊 **Dashboard** - Estatísticas e visão geral dos chamados
- 🤖 **Integração com IA** - Resolução automática de chamados
- 📝 **Gestão de Chamados** - CRUD completo de tickets
- 📚 **FAQ** - Base de conhecimento pesquisável
- 👤 **Perfil** - Gerenciamento de dados do usuário
- 👥 **Administração** - Funcionalidades exclusivas para Admin/Tech

---

## 🏗️ Estrutura do Projeto

```
ClickDesk-Mobile-Organizado/
├── src/
│   ├── configuracao/              # Configurações e constantes
│   │   ├── constantes.js          # Endpoints + Enums da API
│   │   └── ambiente.js            # Configurações por ambiente
│   │
│   ├── servicos/
│   │   ├── api/                   # Serviços de comunicação com API
│   │   │   ├── clienteHttp.js     # Axios configurado com JWT
│   │   │   ├── autenticacaoService.js
│   │   │   ├── chamadosService.js
│   │   │   ├── faqService.js
│   │   │   └── iaService.js
│   │   └── utilitarios/           # Utilitários gerais
│   │       ├── armazenamentoLocal.js
│   │       ├── validadores.js
│   │       └── formatadores.js
│   │
│   ├── telas/                     # Telas da aplicação
│   │   ├── autenticacao/          # Telas de autenticação
│   │   │   ├── TelaLogin.js
│   │   │   ├── TelaRegistro.js
│   │   │   ├── TelaTermos.js
│   │   │   └── TelaLogout.js
│   │   ├── usuario/               # Telas do usuário comum
│   │   │   ├── TelaDashboard.js
│   │   │   ├── TelaMeusChamados.js
│   │   │   ├── TelaDetalhesChamado.js
│   │   │   └── TelaNovoChamado.js
│   │   ├── administrador/         # Telas do administrador
│   │   │   ├── TelaDashboardAdmin.js
│   │   │   ├── TelaChamadosAdmin.js
│   │   │   └── TelaFAQAdmin.js
│   │   └── compartilhadas/        # Telas compartilhadas
│   │       ├── TelaFAQ.js
│   │       └── TelaListaChamados.js
│   │
│   ├── componentes/               # Componentes reutilizáveis
│   │   ├── comum/                 # Componentes comuns
│   │   │   ├── Botao.js
│   │   │   ├── Input.js
│   │   │   └── Cartao.js
│   │   ├── layout/                # Componentes de layout
│   │   │   ├── Cabecalho.js
│   │   │   ├── MenuLateral.js
│   │   │   └── Rodape.js
│   │   └── chamados/              # Componentes de chamados
│   │       ├── CartaoChamado.js
│   │       ├── ListaChamados.js
│   │       └── FormularioChamado.js
│   │
│   ├── navegacao/                 # Configuração de navegação
│   │   ├── NavegadorPrincipal.js
│   │   ├── NavegadorAutenticacao.js
│   │   └── RotaProtegida.js
│   │
│   ├── contextos/                 # Context API para estado global
│   │   ├── ContextoAutenticacao.js
│   │   ├── ContextoChamados.js
│   │   └── ContextoTema.js
│   │
│   ├── hooks/                     # Hooks customizados
│   │   ├── useAutenticacao.js
│   │   ├── useChamados.js
│   │   ├── useAPI.js
│   │   └── useFormulario.js
│   │
│   ├── modelos/                   # Modelos de dados
│   │   ├── Usuario.js
│   │   ├── Chamado.js
│   │   ├── FAQ.js
│   │   └── Enums.js
│   │
│   ├── estilos/                   # Estilos e temas
│   │   ├── cores.js
│   │   ├── temas.js
│   │   └── global.js
│   │
│   ├── utils/                     # Utilitários diversos
│   │   └── helpers.js
│   │
│   ├── App.js                     # Componente principal
│   └── index.js                   # Ponto de entrada
│
├── assets/                        # Recursos estáticos
│   ├── images/
│   ├── icons/
│   └── fonts/
│
├── .env.example                   # Exemplo de variáveis de ambiente
├── .gitignore                     # Arquivos ignorados pelo Git
├── package.json                   # Dependências do projeto
├── app.json                       # Configurações do Expo
├── babel.config.js                # Configuração do Babel
├── metro.config.js                # Configuração do Metro Bundler
├── README.md                      # Este arquivo
└── GUIA_MIGRACAO.md              # Guia de migração da versão antiga
```

---

## 🚀 Instalação

### Pré-requisitos

- **Node.js** 14.x ou superior
- **npm** ou **yarn**
- **Expo CLI** (instalar globalmente: `npm install -g expo-cli`)
- **Expo Go** (app no celular) ou emulador Android/iOS

### Passos de Instalação

1. **Clone o repositório:**
```bash
git clone https://github.com/Andr3stt/ClickDesk.git
cd ClickDesk/Código-Fonte/ClickDesk-Mobile-Organizado
```

2. **Instale as dependências:**
```bash
npm install
# ou
yarn install
```

3. **Configure as variáveis de ambiente:**
```bash
cp .env.example .env
```

Edite o arquivo `.env` e configure a URL da API:
```env
REACT_APP_API_URL=http://seu-servidor:8080
```

4. **Inicie o servidor de desenvolvimento:**
```bash
npm start
# ou
yarn start
```

5. **Execute no dispositivo:**
- **Android:** Pressione `a` no terminal ou escaneie o QR code com Expo Go
- **iOS:** Pressione `i` no terminal ou escaneie o QR code com a câmera
- **Web:** Pressione `w` no terminal

---

## 📡 Configuração da API

A aplicação se comunica com a API Backend do ClickDesk. Configure a URL da API no arquivo `.env`:

```env
REACT_APP_API_URL=http://localhost:8080
```

### Endpoints Disponíveis

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| **Autenticação** |
| POST | `/auth/login` | Realizar login |
| POST | `/auth/register` | Registrar novo usuário |
| POST | `/auth/logout` | Realizar logout |
| **Chamados** |
| GET | `/api/chamados` | Listar todos os chamados |
| GET | `/api/chamados/meus` | Listar meus chamados |
| GET | `/api/chamados/{id}` | Obter chamado específico |
| POST | `/api/chamados` | Criar novo chamado |
| PUT | `/api/chamados/{id}` | Atualizar chamado |
| PUT | `/api/chamados/{id}/status` | Atualizar status |
| POST | `/api/chamados/{id}/feedback` | Enviar feedback |
| **FAQ** |
| GET | `/api/faqs` | Listar FAQs |
| GET | `/api/faqs/search?q=termo` | Buscar FAQs |
| POST | `/api/faqs` | Criar FAQ (Admin) |
| **IA** |
| POST | `/api/ia/assist` | Solicitar assistência da IA |

---

## 🎨 Identidade Visual

### Cores Principais

| Cor | Hex | Uso |
|-----|-----|-----|
| Background | `#EDE6D9` | Fundo da aplicação |
| Cards | `#F5EFE6` | Fundo dos cards |
| Brand (Laranja) | `#F28A1A` | Ações principais |
| Texto Principal | `#1E2A22` | Texto principal |
| Texto Secundário | `#6B7280` | Texto secundário |

### Paleta Semântica

| Cor | Hex | Uso |
|-----|-----|-----|
| Primary | `#2563eb` | Botões principais |
| Success | `#10b981` | Sucesso, resolvido |
| Danger | `#ef4444` | Erro, exclusão |
| Warning | `#f59e0b` | Alerta, em andamento |

---

## 🔒 Níveis de Acesso

| Role | Permissões |
|------|------------|
| **USER** | Dashboard, Meus Chamados, Novo Chamado, FAQ, Perfil |
| **TECH** | Todas de USER + Todos os Chamados, Aprovar Chamados |
| **ADMIN** | Todas de TECH + Gerenciar FAQ, Criar Usuários |

---

## 🛠️ Tecnologias Utilizadas

- **React Native** 0.72.6 - Framework mobile
- **Expo** ~49.0 - Plataforma de desenvolvimento
- **React Navigation** 6.x - Navegação
- **Axios** 1.6.2 - Cliente HTTP
- **AsyncStorage** 1.18.2 - Armazenamento local
- **Expo Vector Icons** 13.0 - Ícones

---

## 📝 Scripts Disponíveis

```bash
# Iniciar servidor de desenvolvimento
npm start

# Executar no Android
npm run android

# Executar no iOS
npm run ios

# Executar no navegador
npm run web

# Limpar cache
expo start -c
```

---

## 🐛 Resolução de Problemas

### Erro: "Unable to resolve module"
```bash
# Limpe o cache do Metro Bundler
expo start -c
```

### Erro: "Network Error"
- Verifique se a API está rodando
- Confirme a URL no arquivo `.env`
- Certifique-se de que o dispositivo está na mesma rede

### Erro: "Token expirado"
- Faça login novamente
- O token JWT expira após 60 minutos

### Erro ao instalar dependências
```bash
# Remova node_modules e reinstale
rm -rf node_modules
npm install
```

---

## 📚 Documentação Adicional

- **[GUIA_MIGRACAO.md](GUIA_MIGRACAO.md)** - Como migrar da versão antiga
- **[API Backend](../ClickDesk/README.md)** - Documentação da API

---

## 👥 Equipe

- **André Barbosa** - Product Owner
- **Vinicius Fagundes** - Scrum Master
- **Erika Cordeiro** - Dev Team
- **Kaique Uchoa** - Dev Team

---

## 📄 Licença

Este projeto é de uso acadêmico. Todos os direitos reservados © 2024 ClickDesk Team.

---

## 📞 Suporte

Em caso de dúvidas ou problemas:
1. Consulte a documentação acima
2. Verifique o [GUIA_MIGRACAO.md](GUIA_MIGRACAO.md)
3. Entre em contato através do repositório GitHub

---

## 🔄 Diferenças da Versão Antiga

Esta versão organizada oferece:

✅ **Estrutura profissional e escalável**
✅ **Serviços de API completos e documentados**
✅ **Sistema de autenticação JWT implementado**
✅ **Validadores e formatadores reutilizáveis**
✅ **Sistema de temas e estilos globais**
✅ **Modelos de dados com métodos utilitários**
✅ **Código 100% comentado em PT-BR**
✅ **Organização por funcionalidade**

---

**Versão:** 1.0.0  
**Última Atualização:** Dezembro 2024
