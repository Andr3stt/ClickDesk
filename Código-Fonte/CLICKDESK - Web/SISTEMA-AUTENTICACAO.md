# Sistema de Autenticação Clickdesk

## 📋 Visão Geral

O sistema diferencia dois tipos de usuários:
- **Técnico/Administrador**: Acessa o Dashboard ADM e gerencia chamados
- **Usuário Comum**: Acessa o Dashboard normal e cria/acompanha seus chamados

## 🆕 Registro de Novos Usuários

Agora é possível criar novos usuários através da tela de registro!

### Como Registrar
1. Acesse `2. TELA DE REGISTRO/registro.html`
2. Preencha os campos:
   - **Usuário**: Escolha um nome único
   - **Senha**: Mínimo 8 caracteres
   - **Confirmar senha**: Digite a mesma senha
   - **E-mail**: E-mail válido
   - **Tipo de conta**: Escolha entre:
     - **Usuário** (para criar e acompanhar chamados)
     - **Técnico** (para atender e gerenciar chamados)
3. Clique em "Registrar"
4. Faça login com suas credenciais

### Armazenamento
- Os usuários registrados são salvos no **localStorage**
- Formato: `registeredUsers` contém todos os usuários cadastrados
- O tipo escolhido no registro define qual dashboard o usuário acessará

## 🔑 Credenciais de Teste (Padrão do Sistema)

### Técnicos/Administradores
- **Usuário:** `admin` | **Senha:** `admin123`
- **Usuário:** `tecnico` | **Senha:** `tecnico123`
- **Usuário:** `suporte` | **Senha:** `suporte123`

### Usuários Comuns
- **Usuário:** `usuario` | **Senha:** `usuario123`
- **Usuário:** `cliente` | **Senha:** `cliente123`

## 🚀 Como Funciona

### 1. Registro
Na tela de registro (`2. TELA DE REGISTRO/registro.html`):
- Usuário preenche seus dados
- **Escolhe o tipo de conta**: Usuário ou Técnico
- Sistema valida os dados
- Salva no localStorage com o tipo selecionado
- Redireciona para o login

### 2. Login
Ao fazer login em `1. TELA DE LOGIN/login.html`:
- O sistema verifica credenciais padrão E usuários registrados
- Identifica o tipo de usuário (técnico ou usuário)
- Salva a sessão no localStorage
- Redireciona para o dashboard apropriado:
  - **Técnico** → `11. TELA DASHBOARD ADM/dashboard-adm.html`
  - **Usuário** → `6. TELA DE DASHBOARD/dashboard.html`

### 2. Proteção de Rotas
Para proteger uma página e verificar permissões, adicione no início do HTML:

```html
<script src="../shared/scripts/auth-check.js"></script>
<script>
  // Para páginas que requerem estar logado (qualquer tipo)
  ClickdeskAuth.checkPageAccess();

  // Para páginas exclusivas de técnicos
  ClickdeskAuth.checkPageAccess('tecnico');

  // Para páginas exclusivas de usuários comuns
  ClickdeskAuth.checkPageAccess('usuario');
</script>
```

### 3. Logout
O sistema detecta automaticamente botões de logout:
- Elementos com `id="logoutBtn"`
- Elementos com atributo `data-logout`

Exemplo:
```html
<button id="logoutBtn">Sair</button>
<!-- ou -->
<button data-logout>Sair</button>
```

### 4. Exibir Informações do Usuário
Para mostrar o nome do usuário logado:
```html
<span data-user-name></span>
```

Para mostrar o tipo:
```html
<span data-user-type></span>
```

## 📂 Estrutura de Arquivos

```
shared/
└── scripts/
    └── auth-check.js        # Sistema de autenticação

1. TELA DE LOGIN/
└── scripts/
    └── login.js             # Lógica de login com diferenciação

6. TELA DE DASHBOARD/        # Dashboard para usuários comuns
└── dashboard.html

11. TELA DASHBOARD ADM/      # Dashboard para técnicos/admins
└── dashboard-adm.html
```

## 🔄 Fluxo de Navegação

### Usuário Comum
```
Login → Dashboard → Novo Chamado
                 → Meus Chamados
                 → Editar Perfil
                 → FAQ
```

### Técnico/Admin
```
Login → Dashboard ADM → Aprovação de Chamados
                     → Meus Chamados ADM
                     → Gerenciar Usuários
```

## 💡 Personalização

### Adicionar Usuários via Código
Para adicionar novos usuários padrão, edite `1. TELA DE LOGIN/scripts/login.js`:

```javascript
const usuariosPadrao = {
  'novoUsuario': { 
    senha: 'senha123', 
    tipo: 'usuario', // ou 'tecnico'
    nome: 'Nome Completo' 
  }
};
```

### Adicionar Usuários via Interface
Basta acessar a tela de registro e preencher o formulário!

## 📊 Estrutura de Dados

### localStorage - registeredUsers
```json
{
  "usuario1": {
    "username": "usuario1",
    "senha": "senha123",
    "email": "usuario@email.com",
    "tipo": "usuario",
    "nome": "Usuario1",
    "registradoEm": "2025-10-29T..."
  },
  "tecnico1": {
    "username": "tecnico1",
    "senha": "senha123",
    "email": "tecnico@email.com",
    "tipo": "tecnico",
    "nome": "Tecnico1",
    "registradoEm": "2025-10-29T..."
  }
}
```

### localStorage - userSession
```json
{
  "username": "usuario1",
  "nome": "Usuario1",
  "tipo": "usuario",
  "loginTime": "2025-10-29T..."
}
```

## 🔐 Segurança

⚠️ **IMPORTANTE**: Este é um sistema de demonstração!

Para produção:
- Implementar autenticação via API backend
- Usar tokens JWT ou sessões server-side
- Criptografar senhas
- Adicionar proteção CSRF
- Implementar rate limiting
- Usar HTTPS
