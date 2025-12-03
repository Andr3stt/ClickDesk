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

### ✅ Verificação Automatizada

Para verificar se tudo está configurado corretamente:

**No Windows (PowerShell):**
```powershell
.\verify-build.ps1
```

**No Linux/Mac (apenas verificação de configuração):**
```bash
bash verify-config.sh
```

Para mais detalhes, consulte: [BUILD_VERIFICATION_README.md](BUILD_VERIFICATION_README.md)

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
│   │   ├── FormLogin.cs           # Tela de login
│   │   ├── FormRegistro.cs        # Criação de nova conta
│   │   ├── FormTermosUso.cs       # Termos de uso e privacidade
│   │   └── FormRecuperarSenha.cs  # Recuperação de senha
│   ├── Dashboard/
│   │   ├── FormDashboard.cs       # Dashboard do usuário
│   │   └── FormDashboardAdmin.cs  # Dashboard administrativo (Tech/Admin)
│   ├── Chamados/
│   │   ├── FormNovoChamado.cs           # Criar novo chamado
│   │   ├── FormMeusChamados.cs          # Lista de chamados do usuário
│   │   ├── FormDetalhesChamado.cs       # Detalhes do chamado (usuário)
│   │   ├── FormDetalhesChamadoTecnico.cs # Detalhes do chamado (técnico - edição)
│   │   ├── FormListaChamados.cs         # Lista de todos os chamados
│   │   └── FormAprovarChamados.cs       # Aprovação de chamados
│   ├── FAQ/
│   │   ├── FormFAQ.cs             # Base de conhecimento (usuário)
│   │   └── FormFAQAdmin.cs        # Gerenciamento de FAQ (admin)
│   └── Perfil/
│       ├── FormPerfil.cs          # Visualização do perfil
│       ├── FormEditarPerfil.cs    # Edição do perfil
│       └── FormCriarUsuario.cs    # Criação de usuário (admin)
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
│   ├── ClickDeskStyles.cs
│   ├── SessionManager.cs
│   └── UIHelper.cs
├── Properties/
├── App.config
├── Program.cs
├── ClickDesk.csproj
└── ClickDesk.sln
```

## 🎨 Identidade Visual

### Cores Principais
| Cor | Hex | Uso |
|-----|-----|-----|
| Background | #EDE6D9 | Background da aplicação |
| Cards | #F5EFE6 | Surface/cards |
| Brand (Laranja) | #F28A1A | Ações principais |
| Texto | #1E2A22 | Texto principal |

### Paleta Secundária
| Cor | Hex | Uso |
|-----|-----|-----|
| Primary | #2563eb | Botões principais, links |
| Success | #10b981 | Sucesso, resolvido |
| Danger | #ef4444 | Erro, exclusão |
| Warning | #f59e0b | Alerta, em andamento |
| Gray900 | #111827 | Texto principal |
| Gray800 | #1f2937 | Sidebar |
| Gray100 | #f3f4f6 | Backgrounds |

### Espaçamentos
| Elemento | Valor | Uso |
|----------|-------|-----|
| Main area horizontal | 32px | Padding horizontal da área principal |
| Main area vertical | 28px | Padding vertical da área principal |
| Cards padding | 14px | Padding interno dos cards |
| Gap entre cards | 14px | Espaçamento entre cards |

### Border Radius
| Elemento | Valor |
|----------|-------|
| Sidebar | 18px (cantos direitos) |
| Cards | 16px |
| Botões | 10px |

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
- **UI**: Windows Forms + Siticone.Desktop.UI 2.1.1
- **Serialização**: Newtonsoft.Json 13.0.3
- **HTTP**: System.Net.Http
- **Tema**: Sistema dark/light mode com ThemeManager

## 🎨 UI Moderna com Siticone

O projeto utiliza o framework **Siticone.Desktop.UI** para criar uma interface moderna:

- ✨ Componentes com design flat e moderno
- 🌓 Suporte a temas claro e escuro
- 🎯 Animações e transições suaves
- 📱 Visual responsivo e profissional

**Forms Modernizados:**
- FormLogin - Tela de autenticação
- FormRegistro - Cadastro de usuários
- FormDashboard - Painel principal

Para modernizar novos formulários, consulte: [QUICK_MODERNIZATION_TEMPLATE.md](QUICK_MODERNIZATION_TEMPLATE.md)

## 🐛 Resolução de Problemas

### "The type or namespace name 'Siticone' could not be found"
- Execute: `nuget restore ClickDesk.sln`
- Ou use o script: `.\verify-build.ps1`
- Adicione: `using Siticone.Desktop.UI.WinForms;`

### "Erro de conexão com o servidor"
- Verifique se a API está rodando
- Confira a URL no App.config

### "Sessão expirada"
- Faça login novamente
- O token JWT expira após 1 hora

### "Acesso negado"
- Verifique se você tem permissão para a operação
- Contate o administrador

### "ClickDeskStyles não existe"
- Verifique se `using ClickDesk.Utils;` está presente
- Recompile: Build → Rebuild Solution

### "Cannot connect to API"
- Verifique se a API está rodando
- Confirme URL em `App.config`

Para mais problemas de build, veja: [BUILD_VERIFICATION_README.md](BUILD_VERIFICATION_README.md)

## 👥 Equipe

- **André Barbosa** - Product Owner
- **Vinicius Fagundes** - Scrum Master
- **Erika Cordeiro** - Dev Team
- **Kaique Uchoa** - Dev Team

## 📚 Documentação Adicional

- **[BUILD_VERIFICATION_README.md](BUILD_VERIFICATION_README.md)** - Guia de verificação de build
- **[SITICONE_SETUP_GUIDE.md](SITICONE_SETUP_GUIDE.md)** - Setup completo do Siticone
- **[SITICONE_AUDIT_COMPLETE_REPORT.md](SITICONE_AUDIT_COMPLETE_REPORT.md)** - Relatório de auditoria
- **[MODERNIZATION_GUIDE.md](MODERNIZATION_GUIDE.md)** - Guia de modernização de UI
- **[MANUAL_TECNICO.md](MANUAL_TECNICO.md)** - Manual técnico completo

## 📄 Licença

Este projeto é de uso acadêmico. Todos os direitos reservados © 2024 ClickDesk Team.

## 📞 Suporte

Em caso de dúvidas ou problemas:
1. Consulte a documentação acima
2. Execute os scripts de verificação
3. Entre em contato através do repositório GitHub
