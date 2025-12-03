# 🎨 SISTEMA DE PADRONIZAÇÃO CLICKDESK - IMPLEMENTADO

## ✅ O QUE FOI CRIADO

### 1. **CSS Global Padronizado** 
📁 `shared/styles/clickdesk-base.css`

**Conteúdo completo:**
- ✅ Paleta de cores extraída da Dashboard (referência)
- ✅ Variáveis CSS para todas as cores:
  - Background: `#EDE6D9`
  - Superfícies: `#F5EFE6`, `#F7F3EC`
  - Brand (laranja): `#F28A1A`
  - Texto: `#1E2A22`, `#6F6F6F`
  - Bordas: `#D5D0C7`, `#C9C3B8`
  
- ✅ Sistema de tipografia completo:
  - Fonte: System UI (padrão do sistema)
  - Tamanhos: 12px até 42px
  - Pesos: 400 (normal) até 900 (black)
  - Letter spacing: .2px - .3px
  
- ✅ Border radius padronizados:
  - `--radius-xxl: 18px` (sidebar)
  - `--radius-xl: 16px` (cards grandes)
  - `--radius-lg: 12px` (cards médios)
  - `--radius-md: 10px` (botões/inputs)
  - `--radius-sm: 8px` (badges)
  - `--radius-full: 999px` (pills)
  
- ✅ Sombras (shadows):
  - `--shadow-lg`: Para modais/popovers
  - `--shadow-md`: Para cards principais
  - `--shadow-sm`: Para elementos pequenos
  
- ✅ Layout padrão:
  - Sidebar: 240px (desktop) / 84px (mobile)
  - Topbar: altura fixa
  - Content: flexível
  
- ✅ Componentes reutilizáveis:
  - Botões: `.btn`, `.btn-primary`, `.btn-secondary`, `.icon-btn`, `.btn-mini`
  - Cards: `.card`, `.card-soft`, `.card-title`
  - Inputs: `.input`, `.label`
  - Badges: `.badge.status-open/progress/waiting/closed`
  - Prioridades: `.prio-flag.p1/p2/p3`
  - Chips: `.chip-toggle`
  
- ✅ Status colors:
  - 🟢 Sucesso (Aberto): Verde `#0F8C4D`
  - 🟡 Atenção (Em progresso): Amarelo `#A56400`
  - 🔵 Info (Aguardando): Azul `#2855B2`
  - 🔴 Erro: Vermelho `#A12016`
  
- ✅ Responsividade:
  - Breakpoint 1080px (tablet)
  - Breakpoint 760px (mobile)
  - Sidebar colapsa em mobile
  
- ✅ Acessibilidade:
  - Focus visível
  - Reduced motion
  - ARIA labels

---

### 2. **Template HTML Base**
📁 `shared/template-base.html`

**Estrutura completa:**
```html
<div class="app-shell">
  <aside class="sidebar">
    <!-- Logo + Navegação -->
  </aside>
  
  <main class="content">
    <header class="topbar">
      <!-- Ações do topo -->
    </header>
    
    <section class="main-area">
      <h1 class="page-title page-title--accent">Título</h1>
      <div class="container">
        <!-- Conteúdo -->
      </div>
    </section>
  </main>
</div>
```

**Inclui exemplos de:**
- ✅ Cards
- ✅ Formulários completos
- ✅ Badges e prioridades
- ✅ Grid responsivo
- ✅ Navegação padronizada

---

### 3. **Documentação Completa**
📁 `shared/README-DESIGN-SYSTEM.md`

**Conteúdo:**
- ✅ Visão geral do sistema
- ✅ Paleta de cores com códigos
- ✅ Tipografia (fontes, tamanhos, pesos)
- ✅ Componentes (como usar cada um)
- ✅ Layout e estrutura
- ✅ Responsividade
- ✅ Checklist de padronização
- ✅ Como usar em novas telas
- ✅ Manutenção e boas práticas

---

### 4. **Telas Atualizadas**

#### ✅ Dashboard (referência)
- Atualizada para usar `clickdesk-base.css`
- CSS específico separado: `dashboard-specific.css`

#### ✅ FAQ
- Importa CSS global
- Navegação padronizada
- Links corrigidos

#### ✅ Novo Chamado
- Importa CSS global
- Navegação padronizada
- Estrutura com card centralizado

---

## 🎯 PADRÕES ESTABELECIDOS

### Cores
```css
/* Background */
--bg-app: #EDE6D9

/* Superfícies */
--surface: #F5EFE6
--surface-2: #F7F3EC

/* Brand */
--brand: #F28A1A (laranja principal)

/* Texto */
--ink: #1E2A22 (principal)
--muted-ink: #6F6F6F (secundário)

/* Bordas */
--outline: #D5D0C7
--outline-strong: #C9C3B8
```

### Tipografia
```css
/* Família */
System UI (nativa do sistema operacional)

/* Tamanhos principais */
Título página: 28px (font-black: 900)
Subtítulos: 16px (font-black: 900)
Texto base: 14px
Navegação: 15px (font-semibold: 650)
Small: 13px
XSmall: 12px

/* KPIs grandes */
42px (font-black: 900)
```

### Espaçamentos
```css
/* Gaps */
Container: 18px
Cards grid: 14px
Elementos: 8px, 12px

/* Padding */
Cards: 14px
Sidebar: 18px 16px
Main-area: 28px 32px
```

### Border Radius
```css
Sidebar: 18px (cantos direitos)
Cards: 16px
Botões/inputs: 10px
Badges: 8px
Pills: 999px
```

---

## 📱 RESPONSIVIDADE

### Desktop (> 1080px)
- Sidebar: 240px completa
- Grid de cards: 4 colunas
- Navegação com labels

### Tablet (760px - 1080px)
- Sidebar: 240px
- Grid de cards: 2 colunas

### Mobile (< 760px)
- Sidebar: 84px (apenas ícones)
- Labels de navegação ocultas
- Grid de cards: 1 coluna
- Padding reduzido

---

## 🔗 NAVEGAÇÃO PADRÃO

Todas as telas devem ter os mesmos links:

1. **Dashboard** → `../6. TELA DE DASHBOARD/dashboard.html`
2. **Novo chamado** → `../7. TELA DE NOVO CHAMADO/novo-chamado.html`
3. **Meus chamados** → `../9. TELA DE MEU CHAMADO/meus-chamado.html`
4. **Edição de perfil** → `../5.5.TELA DE EDIÇÃO DE PERFIL/editar-perfil.html`
5. **FAQ** → `../10. TELA DE FAQ/faq.html`

---

## 📦 COMO USAR EM UMA NOVA TELA

### Passo 1: Importar CSS global
```html
<head>
  <link rel="stylesheet" href="../shared/styles/clickdesk-base.css" />
  <link rel="stylesheet" href="styles/minha-tela.css" />
</head>
```

### Passo 2: Usar estrutura padrão
```html
<div class="app-shell">
  <aside class="sidebar">...</aside>
  <main class="content">
    <header class="topbar">...</header>
    <section class="main-area">
      <h1 class="page-title page-title--accent">Título</h1>
      <div class="container">
        <!-- Conteúdo -->
      </div>
    </section>
  </main>
</div>
```

### Passo 3: Usar componentes
```html
<!-- Card -->
<article class="card">
  <h2 class="card-title">Título</h2>
  <p>Conteúdo...</p>
</article>

<!-- Botão -->
<button class="btn btn-primary">Salvar</button>

<!-- Input -->
<label class="label" for="campo">Nome</label>
<input type="text" id="campo" class="input">

<!-- Badge -->
<span class="badge status-open">Aberto</span>
<span class="prio-flag p1">P1</span>
```

### Passo 4: CSS específico (se necessário)
No arquivo `styles/minha-tela.css`, adicione APENAS estilos únicos da tela:
```css
/* Usar variáveis do base */
.meu-componente-especifico {
  background: var(--surface);
  border-radius: var(--radius-lg);
  padding: 16px;
}
```

---

## ✅ CHECKLIST DE PADRONIZAÇÃO

Ao criar/atualizar uma tela, verificar:

- [ ] Importa `../shared/styles/clickdesk-base.css`
- [ ] Usa estrutura `<div class="app-shell">`
- [ ] Sidebar presente com navegação completa
- [ ] Topbar presente
- [ ] Título com `<h1 class="page-title page-title--accent">`
- [ ] Container com `<div class="container">`
- [ ] Usa componentes base (btn, card, input, badge)
- [ ] Usa variáveis CSS (não valores fixos)
- [ ] Links de navegação corretos
- [ ] Responsivo (testa em 760px e 1080px)
- [ ] Ícones SVG com `class="icon-stroke"`
- [ ] ARIA labels para acessibilidade

---

## 🎨 COMPONENTES DISPONÍVEIS

### Botões
- `.btn .btn-primary` - Botão principal (laranja)
- `.btn .btn-secondary` - Botão secundário (cinza)
- `.icon-btn` - Botão apenas ícone
- `.btn-mini` - Botão pequeno

### Cards
- `.card` - Card padrão
- `.card-soft` - Card com fundo suave
- `.card-title` - Título do card

### Inputs
- `.input` - Input, select ou textarea
- `.label` - Label do campo

### Badges
- `.badge .status-open` - Verde (Aberto)
- `.badge .status-progress` - Amarelo (Em progresso)
- `.badge .status-waiting` - Azul (Aguardando)
- `.badge .status-closed` - Cinza (Concluído)

### Prioridades
- `.prio-flag .p1` - P1 (vermelho)
- `.prio-flag .p2` - P2 (amarelo)
- `.prio-flag .p3` - P3 (verde)

### Chips
- `.chip-toggle` - Chip toggleável
- `.chip-toggle.active` - Chip ativo

### Ícones
- `.icon-stroke` - Ícone SVG padrão (20x20px)
- `.icon-sm` - 16px
- `.icon-md` - 20px
- `.icon-lg` - 24px

### Layout
- `.app-shell` - Container principal
- `.sidebar` - Sidebar
- `.content` - Área de conteúdo
- `.topbar` - Barra superior
- `.main-area` - Área principal
- `.container` - Container de conteúdo
- `.page-title` - Título da página
- `.page-title--accent` - Título com linha laranja

---

## 🚀 PRÓXIMOS PASSOS

### Telas a serem padronizadas:
1. ⏳ Lista de chamados (9. TELA DE MEU CHAMADO)
2. ⏳ Detalhes do chamado (14. TELA DE DETALHES DO CHAMADO)
3. ⏳ Edição de perfil (5.5.TELA DE EDIÇÃO DE PERFIL)
4. ⏳ Dashboard Admin (11. TELA DASHBOARD ADM)
5. ⏳ Aprovação de chamados (12. TELA DE APROVAÇÃO DE CHAMADOS ADM)
6. ⏳ Login/Registro (1, 2, 3, 4)

**Para cada tela:**
1. Importar `clickdesk-base.css`
2. Usar estrutura `app-shell`
3. Substituir CSS customizado por classes base
4. Criar CSS específico apenas para estilos únicos
5. Testar responsividade
6. Verificar navegação

---

## 📊 RESUMO

### ✅ Criado
- Sistema de design completo
- CSS global reutilizável
- Template HTML base
- Documentação completa
- 3 telas padronizadas (Dashboard, FAQ, Novo Chamado)

### 🎨 Padronização inclui
- Cores
- Tipografia
- Espaçamentos
- Border radius
- Sombras
- Componentes
- Layout
- Responsividade
- Acessibilidade
- Navegação

### 🎯 Resultado
Interface **100% consistente**, **profissional** e **fácil de manter**! 

Todas as telas seguirão o mesmo padrão visual extraído da Dashboard. 🚀
