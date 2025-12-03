# Manual Técnico - ClickDesk Desktop

## 📋 Visão Geral da Arquitetura

O ClickDesk Desktop segue uma arquitetura em camadas, separando responsabilidades entre Interface (Forms), Lógica de Negócio (Services) e Dados (Models).

```
┌─────────────────────────────────────────────────────────┐
│                    INTERFACE (Forms)                     │
│  Login │ Dashboard │ Chamados │ FAQ │ Perfil            │
├─────────────────────────────────────────────────────────┤
│                    UTILITÁRIOS (Utils)                   │
│     AppColors │ SessionManager │ UIHelper               │
├─────────────────────────────────────────────────────────┤
│                    SERVIÇOS (Services)                   │
│   ApiService │ AuthService │ ChamadoService │ FAQService│
├─────────────────────────────────────────────────────────┤
│                    MODELOS (Models)                      │
│    User │ Chamado │ FAQ │ Requests │ Responses          │
├─────────────────────────────────────────────────────────┤
│                    API REST (Externa)                    │
│              http://localhost:8080                      │
└─────────────────────────────────────────────────────────┘
```

## 🔧 Componentes Principais

### 1. Camada de Interface (Forms)

#### FormLogin
- **Responsabilidade**: Autenticação do usuário
- **Fluxo**: Coleta credenciais → AuthService.LoginAsync → Redireciona para Dashboard
- **Validações**: Campos obrigatórios, tratamento de erros

#### FormDashboard / FormDashboardAdmin
- **Responsabilidade**: Tela principal com estatísticas
- **Componentes**: Sidebar, Cards de estatísticas, DataGridView de chamados
- **Herança**: FormDashboardAdmin herda de FormDashboard, adicionando funcionalidades admin

#### FormNovoChamado
- **Responsabilidade**: Criação de chamados com integração IA
- **Fluxo**: 
  1. Usuário preenche dados
  2. POST /api/chamados
  3. Se IA resolver → Mostra solução e pede feedback
  4. Se não resolver → Escala para técnico

### 2. Camada de Serviços (Services/API)

#### ApiService
```csharp
// Classe estática para comunicação HTTP
public static class ApiService
{
    // HttpClient reutilizável
    private static readonly HttpClient _httpClient;
    
    // Métodos genéricos
    public static async Task<T> GetAsync<T>(string url);
    public static async Task<TResponse> PostAsync<TRequest, TResponse>(string url, TRequest data);
    public static async Task<TResponse> PutAsync<TRequest, TResponse>(string url, TRequest data);
    public static async Task<T> DeleteAsync<T>(string url);
    
    // Gerenciamento de token
    public static void SetAuthToken(string token);
    public static void ClearAuthToken();
}
```

#### AuthService
```csharp
// Serviço de autenticação
public static class AuthService
{
    public static async Task<LoginResponse> LoginAsync(string username, string password);
    public static async Task<ApiResponse<User>> RegisterAsync(RegisterRequest request);
    public static async Task LogoutAsync();
    public static bool IsAuthenticated();
}
```

#### ChamadoService
```csharp
// Serviço de chamados
public static class ChamadoService
{
    public static async Task<List<Chamado>> ListarTodosAsync();
    public static async Task<List<Chamado>> ListarMeusAsync();
    public static async Task<Chamado> ObterAsync(int id);
    public static async Task<ChamadoResponse> CriarAsync(ChamadoRequest request);
    public static async Task<Chamado> EnviarFeedbackAsync(int id, FeedbackRequest feedback);
    public static async Task<DashboardStats> ObterEstatisticasAsync();
}
```

### 3. Camada de Modelos (Models)

#### User
```csharp
public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Nome { get; set; }
    public string Role { get; set; }  // USER, TECH, ADMIN
    public string Departamento { get; set; }
    public string Telefone { get; set; }
    
    // Propriedades computadas
    public bool IsAdmin => Role?.ToUpper() == "ADMIN";
    public bool IsTech => Role?.ToUpper() == "TECH";
}
```

#### Chamado
```csharp
public class Chamado
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public string Categoria { get; set; }
    public string Status { get; set; }      // ABERTO, EM_ANDAMENTO, RESOLVIDO, FECHADO, ESCALADO
    public string Severidade { get; set; }  // BAIXA, MEDIA, ALTA, CRITICA
    public bool ResolvidoPorIA { get; set; }
    public string SolucaoIA { get; set; }
    public List<Comentario> Comentarios { get; set; }
}
```

### 4. Utilitários (Utils)

#### SessionManager
```csharp
// Gerenciador de sessão do usuário
public static class SessionManager
{
    public static string Token { get; private set; }
    public static User CurrentUser { get; private set; }
    public static bool IsLoggedIn => CurrentUser != null && !string.IsNullOrEmpty(Token);
    
    public static void SaveSession(string token, User user, int expiresIn = 3600);
    public static void ClearSession();
    public static bool IsTokenExpired();
}
```

#### AppColors
```csharp
// Paleta de cores da aplicação
public static class AppColors
{
    public static Color Primary => Color.FromArgb(37, 99, 235);      // #2563eb
    public static Color Success => Color.FromArgb(16, 185, 129);    // #10b981
    public static Color Danger => Color.FromArgb(239, 68, 68);      // #ef4444
    public static Color Warning => Color.FromArgb(245, 158, 11);    // #f59e0b
    
    public static Color GetStatusColor(string status);
    public static Color GetSeveridadeColor(string severidade);
}
```

#### UIHelper
```csharp
// Utilitário para criação de componentes UI
public static class UIHelper
{
    public static Button CreatePrimaryButton(string text, int width, int height);
    public static Button CreateSuccessButton(string text, int width, int height);
    public static Button CreateDangerButton(string text, int width, int height);
    
    public static void StyleDataGridView(DataGridView dgv);
    
    public static void ShowError(string message);
    public static void ShowSuccess(string message);
    public static bool ShowConfirmation(string message);
}
```

## 🔄 Fluxos de Dados

### Fluxo de Autenticação
```
1. Usuário → FormLogin → txtUsername, txtPassword
2. BtnLogin_Click → ValidarCampos
3. AuthService.LoginAsync(username, password)
4. ApiService.PostAsync → POST /auth/login
5. Resposta → LoginResponse { Token, User }
6. ApiService.SetAuthToken(token)
7. SessionManager.SaveSession(token, user)
8. Abrir FormDashboard ou FormDashboardAdmin
```

### Fluxo de Criação de Chamado com IA
```
1. Usuário → FormNovoChamado → Preenche dados
2. BtnEnviar_Click → ValidarCampos
3. ChamadoService.CriarAsync(request)
4. ApiService.PostAsync → POST /api/chamados
5. API processa com IA
6. Resposta → ChamadoResponse
   ├─ ResolvidoPorIA = true → MostrarSolucaoIA → SolicitarFeedback
   │   ├─ Útil → FeedbackRequest { SolucaoUtil: true } → Fechar chamado
   │   └─ Não útil → FeedbackRequest { EscalarParaTecnico: true } → Escalar
   └─ ResolvidoPorIA = false → Chamado criado normalmente
```

## 🔌 Integração com API

### Configuração
```xml
<!-- App.config -->
<appSettings>
  <add key="ApiBaseUrl" value="http://localhost:8080"/>
</appSettings>
```

### Headers HTTP
```
Content-Type: application/json
Authorization: Bearer {token}
```

### Tratamento de Erros
```csharp
try
{
    var response = await ApiService.GetAsync<List<Chamado>>(url);
}
catch (ApiException ex)
{
    // Status 401 → Sessão expirada, redirecionar para login
    // Status 403 → Sem permissão
    // Status 404 → Não encontrado
    // Status 500 → Erro do servidor
}
```

## 🎨 Padrões de UI

### Estilização de Botões
```csharp
// Botão primário
button.BackColor = AppColors.Primary;       // Azul
button.ForeColor = AppColors.White;
button.FlatStyle = FlatStyle.Flat;
button.FlatAppearance.BorderSize = 0;
button.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
```

### Estilização de DataGridView
```csharp
// Cabeçalho
dgv.ColumnHeadersDefaultCellStyle.BackColor = AppColors.Gray800;
dgv.ColumnHeadersDefaultCellStyle.ForeColor = AppColors.White;
dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

// Células
dgv.DefaultCellStyle.SelectionBackColor = AppColors.PrimaryLight;
dgv.AlternatingRowsDefaultCellStyle.BackColor = AppColors.Gray50;
```

### Layout Padrão
```
┌────────────────────────────────────────────────────────────┐
│                         FORM                                │
│ ┌─────────┬────────────────────────────────────────────┐   │
│ │ SIDEBAR │                CONTENT                      │   │
│ │  260px  │                                             │   │
│ │         │  ┌──────────────────────────────────────┐  │   │
│ │ [Logo]  │  │  Título                              │  │   │
│ │ [User]  │  │  Subtítulo                           │  │   │
│ │         │  ├──────────────────────────────────────┤  │   │
│ │ [Menu]  │  │  Cards de estatísticas               │  │   │
│ │         │  ├──────────────────────────────────────┤  │   │
│ │         │  │  DataGridView                        │  │   │
│ │         │  │                                      │  │   │
│ │ [Sair]  │  └──────────────────────────────────────┘  │   │
│ └─────────┴────────────────────────────────────────────┘   │
└────────────────────────────────────────────────────────────┘
```

## 🔒 Segurança

### Armazenamento de Token
- Token JWT armazenado apenas em memória (SessionManager)
- Token não persiste após fechar aplicação
- Token enviado em todos os requests via header Authorization

### Validação de Permissões
```csharp
// Verificar permissão antes de exibir funcionalidades
if (SessionManager.IsAdmin)
{
    // Mostrar opções de admin
}

if (SessionManager.HasAdminAccess) // Admin ou Tech
{
    // Mostrar opções administrativas
}
```

## 🔧 Extensibilidade

### Adicionar Nova Tela
1. Criar Form em `Forms/{Categoria}/`
2. Adicionar Designer.cs e recursos
3. Registrar no menu da sidebar
4. Adicionar navegação

### Adicionar Novo Endpoint
1. Criar método em Service apropriado
2. Adicionar URL em ApiConfig
3. Criar DTOs em Models se necessário
4. Usar ApiService para chamada HTTP

### Adicionar Nova Cor
```csharp
// Em Utils/AppColors.cs
public static Color NovaCorr => Color.FromArgb(R, G, B);
```

## 📦 Dependências

| Pacote | Versão | Uso |
|--------|--------|-----|
| Newtonsoft.Json | 13.0.3 | Serialização JSON |
| System.Net.Http | - | Cliente HTTP |
| System.Windows.Forms | - | Interface gráfica |

## 🧪 Testes

### Teste Manual - Login
1. Iniciar aplicação
2. Inserir credenciais válidas
3. Verificar redirecionamento para dashboard
4. Verificar token armazenado

### Teste Manual - Chamado com IA
1. Criar chamado com descrição clara
2. Verificar se IA tenta resolver
3. Testar feedback positivo e negativo
4. Verificar status do chamado

## 📝 Convenções de Código

### Nomenclatura
- Forms: `Form{Nome}.cs`
- Models: Nome em PascalCase
- Services: `{Domínio}Service.cs`
- Variáveis: camelCase
- Constantes: PascalCase
- Propriedades: PascalCase

### Comentários
```csharp
/// <summary>
/// Descrição do método/classe.
/// </summary>
/// <param name="nome">Descrição do parâmetro</param>
/// <returns>Descrição do retorno</returns>
```

### Async/Await
- Todos os métodos de API são async
- Usar await para chamadas
- Tratar exceções com try/catch
