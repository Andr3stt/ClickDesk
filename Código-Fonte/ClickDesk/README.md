# ClickDesk - Sistema Desktop de Helpdesk

![ClickDesk Logo](../../Assets/clickdesk_logo.jpeg)

## 📋 Sobre o Projeto

O **ClickDesk Desktop** é uma aplicação Windows Forms desenvolvida em C# (.NET Framework 4.8) que integra com a API REST do sistema ClickDesk para gerenciamento de chamados de suporte técnico (helpdesk).

### ✨ Principais Funcionalidades

- 🔐 **Autenticação JWT** - Login seguro com token
- 📊 **Dashboard** - Estatísticas e visão geral dos chamados
- 🤖 **Integração com IA** - Resolução automática de chamados
- 📝 **Gestão de Chamados** - CRUD completo de tickets
- 📚 **FAQ** - Base de conhecimento pesquisável
- 👤 **Perfil** - Gerenciamento de dados do usuário
- 👥 **Administração** - Funcionalidades exclusivas para Admin/Tech

## 🚀 Instalação

### Pré-requisitos

- Windows 7 ou superior
- .NET Framework 4.8
- Visual Studio 2019/2022 (para desenvolvimento)

### Passos de Instalação

1. **Clone o repositório:**
```bash
git clone https://github.com/Andr3stt/ClickDesk.git
cd ClickDesk/Código-Fonte/ClickDesk
```

2. **Restaure os pacotes NuGet:**
```bash
nuget restore ClickDesk.sln
```
ou via Visual Studio: Clique com botão direito na solução → Restaurar Pacotes NuGet

3. **Configure a API:**
   - Abra o arquivo `App.config`
   - Altere a URL da API conforme necessário:
```xml
<appSettings>
  <add key="ApiBaseUrl" value="http://localhost:8080"/>
</appSettings>
```

4. **Compile e execute:**
   - Abra `ClickDesk.sln` no Visual Studio
   - Pressione F5 para executar

## 🎮 Como Usar

### Login

1. Abra a aplicação
2. Informe usuário e senha
3. Clique em "ENTRAR"

### Criar Chamado

1. No dashboard, clique em "Novo Chamado"
2. Preencha título, descrição e categoria
3. A IA tentará resolver automaticamente
4. Se não resolvido, será escalado para um técnico

### Visualizar Chamados

- **Meus Chamados**: Lista seus tickets
- **Todos os Chamados** (Admin/Tech): Lista todos os tickets do sistema

### FAQ

1. Acesse o menu "FAQ"
2. Use a busca para encontrar respostas
3. Clique em uma FAQ para ver detalhes

## 🏗️ Estrutura do Projeto

```
ClickDesk/
├── Forms/
│   ├── Auth/
│   │   ├── FormLogin.cs
│   │   └── FormRegistro.cs
│   ├── Dashboard/
│   │   ├── FormDashboard.cs
│   │   └── FormDashboardAdmin.cs
│   ├── Chamados/
│   │   ├── FormNovoChamado.cs
│   │   ├── FormMeusChamados.cs
│   │   ├── FormDetalhesChamado.cs
│   │   ├── FormListaChamados.cs
│   │   └── FormAprovarChamados.cs
│   ├── FAQ/
│   │   ├── FormFAQ.cs
│   │   └── FormFAQAdmin.cs
│   └── Perfil/
│       ├── FormPerfil.cs
│       ├── FormEditarPerfil.cs
│       └── FormCriarUsuario.cs
├── Models/
│   ├── User.cs
│   ├── Chamado.cs
│   ├── FAQ.cs
│   ├── ApiResponse.cs
│   ├── LoginRequest.cs
│   ├── RegisterRequest.cs
│   └── ChamadoRequest.cs
├── Services/API/
│   ├── ApiConfig.cs
│   ├── ApiService.cs
│   ├── AuthService.cs
│   ├── ChamadoService.cs
│   ├── FAQService.cs
│   └── UserService.cs
├── Utils/
│   ├── AppColors.cs
│   ├── SessionManager.cs
│   └── UIHelper.cs
├── Properties/
├── App.config
├── Program.cs
├── ClickDesk.csproj
└── ClickDesk.sln
```

## 🎨 Paleta de Cores

| Cor | Hex | Uso |
|-----|-----|-----|
| Primary | #2563eb | Botões principais, links |
| Success | #10b981 | Sucesso, resolvido |
| Danger | #ef4444 | Erro, exclusão |
| Warning | #f59e0b | Alerta, em andamento |
| Gray900 | #111827 | Texto principal |
| Gray800 | #1f2937 | Sidebar |
| Gray100 | #f3f4f6 | Backgrounds |

## 🔒 Níveis de Acesso

| Role | Permissões |
|------|------------|
| USER | Dashboard, Meus Chamados, Novo Chamado, FAQ, Perfil |
| TECH | Todas de USER + Todos os Chamados, Aprovar Chamados |
| ADMIN | Todas de TECH + Gerenciar FAQ, Criar Usuários |

## 📡 Endpoints da API

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| POST | /auth/login | Autenticação |
| POST | /auth/register | Registro |
| GET | /api/chamados | Listar chamados |
| POST | /api/chamados | Criar chamado |
| POST | /api/chamados/{id}/feedback | Enviar feedback |
| GET | /api/faqs | Listar FAQ |
| POST | /api/ia/assist | Assistência IA |

## 🛠️ Tecnologias Utilizadas

- **Linguagem**: C# 7.3
- **Framework**: .NET Framework 4.8
- **UI**: Windows Forms
- **Serialização**: Newtonsoft.Json 13.0.3
- **HTTP**: System.Net.Http

## 🐛 Resolução de Problemas

### "Erro de conexão com o servidor"
- Verifique se a API está rodando
- Confira a URL no App.config

### "Sessão expirada"
- Faça login novamente
- O token JWT expira após 1 hora

### "Acesso negado"
- Verifique se você tem permissão para a operação
- Contate o administrador

## 👥 Equipe

- **André Barbosa** - Product Owner
- **Vinicius Fagundes** - Scrum Master
- **Erika Cordeiro** - Dev Team
- **Kaique Uchoa** - Dev Team

## 📄 Licença

Este projeto é de uso acadêmico. Todos os direitos reservados © 2024 ClickDesk Team.

## 📞 Suporte

Em caso de dúvidas ou problemas, entre em contato com a equipe através do repositório GitHub.
