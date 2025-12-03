# 🎨 CLICKDESK - Sistema de Design Padronizado

## 📋 Visão Geral

Este documento descreve o **sistema de design padronizado** do Clickdesk, baseado na tela de Dashboard como referência oficial.

---

## 🗂️ Arquivos Base

### 1. **clickdesk-base.css** (CSS Global)
📁 `shared/styles/clickdesk-base.css`

Contém todas as variáveis CSS e estilos base reutilizáveis:
- ✅ Paleta de cores completa
- ✅ Tipografia (tamanhos, pesos, fontes)
- ✅ Border radius padronizados
- ✅ Shadows
- ✅ Layout (sidebar, topbar, content)
- ✅ Componentes base (botões, inputs, cards, badges)
- ✅ Responsividade
- ✅ Acessibilidade

### 2. **template-base.html** (HTML Template)
📁 `shared/template-base.html`

Template HTML completo com:
- ✅ Estrutura app-shell
- ✅ Sidebar com navegação
- ✅ Topbar
- ✅ Área de conteúdo
- ✅ Exemplos de componentes

---

## 🎨 Paleta de Cores

### Cores Principais
```css
--bg-app: #EDE6D9          /* Background da aplicação */
--surface: #F5EFE6          /* Cards e painéis */
--surface-2: #F7F3EC        /* Topbar */
--brand: #F28A1A            /* Laranja principal */
--ink: #1E2A22              /* Texto principal */
--muted-ink: #6F6F6F       /* Texto secundário */
--outline: #D5D0C7          /* Bordas */
```

### Status Colors
```css
✅ Sucesso/Aberto:    #0F8C4D (fundo: #E6F7EF)
⚠️ Atenção/Progresso: #A56400 (fundo: #FFF4D6)
ℹ️ Info/Aguardando:   #2855B2 (fundo: #E8F0FF)
❌ Erro:              #A12016 (fundo: #FFE3E0)
```

### Prioridades
```css
P1 (Alta):    Vermelho  (#FFE3E0 / #A12016)
P2 (Média):   Amarelo   (#FFF4D6 / #A56400)
P3 (Baixa):   Verde     (#E6F7EF / #0F8C4D)
```

---

## 📝 Tipografia

### Família de Fontes
```css
--font-system: system-ui, -apple-system, "Segoe UI", Roboto...
--font-mono: ui-monospace, Menlo, Monaco, Consolas...
```

### Tamanhos
```css
--font-xs: 12px    /* Badges, tags */
--font-sm: 13px    /* Texto pequeno */
--font-base: 14px  /* Texto padrão */
--font-md: 15px    /* Navegação */
--font-lg: 16px    /* Subtítulos */
--font-3xl: 28px   /* Títulos de página */
--font-5xl: 42px   /* KPIs grandes */
```

### Pesos
```css
--font-semibold: 650   /* Navegação */
--font-bold: 700
--font-extrabold: 800  /* Títulos de cards */
--font-black: 900      /* Títulos principais */
```

---

## 🧩 Componentes

### Botões
```html
<!-- Primário -->
<button class="btn btn-primary">Salvar</button>

<!-- Secundário -->
<button class="btn btn-secondary">Cancelar</button>

<!-- Ícone -->
<button class="icon-btn">
  <svg class="icon-stroke">...</svg>
</button>

<!-- Mini -->
<button class="btn-mini">Limpar</button>
```

### Cards
```html
<article class="card">
  <h2 class="card-title">Título</h2>
  <p>Conteúdo...</p>
</article>

<!-- Card suave -->
<article class="card card-soft">...</article>
```

### Inputs
```html
<div>
  <label class="label" for="campo">Nome</label>
  <input type="text" id="campo" class="input" placeholder="Digite...">
</div>

<select class="input">...</select>
<textarea class="input"></textarea>
```

### Badges
```html
<!-- Status -->
<span class="badge status-open">Aberto</span>
<span class="badge status-progress">Em progresso</span>
<span class="badge status-waiting">Aguardando</span>
<span class="badge status-closed">Concluído</span>

<!-- Prioridades -->
<span class="prio-flag p1">P1</span>
<span class="prio-flag p2">P2</span>
<span class="prio-flag p3">P3</span>
```

### Chips/Toggles
```html
<button class="chip-toggle active">Opção 1</button>
<button class="chip-toggle">Opção 2</button>
```

---

## 📐 Layout

### Estrutura Base
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

### Sidebar
- Largura: `240px` (desktop) / `84px` (mobile)
- Background: Gradiente `#F2EEE7` → `#EFEAE2`
- Border radius: `18px` (direita)

### Grid Responsivo
```html
<!-- 4 colunas (desktop) / 2 (tablet) / 1 (mobile) -->
<div style="display: grid; grid-template-columns: repeat(auto-fit, minmax(250px, 1fr)); gap: 14px;">
  <div class="card">...</div>
  <div class="card">...</div>
</div>
```

---

## 🚀 Como Usar em uma Nova Tela

### 1. Copie o template base
```html
<!doctype html>
<html lang="pt-BR">
<head>
  <meta charset="utf-8" />
  <title>Clickdesk — Sua Página</title>
  
  <!-- CSS GLOBAL -->
  <link rel="stylesheet" href="../shared/styles/clickdesk-base.css" />
  
  <!-- CSS ESPECÍFICO (opcional) -->
  <link rel="stylesheet" href="styles/sua-pagina.css" />
</head>
```

### 2. Use a estrutura padrão
- Sempre use `<div class="app-shell">`
- Sempre inclua sidebar e topbar
- Use `<section class="main-area">` para conteúdo
- Use `<h1 class="page-title page-title--accent">` para título

### 3. Use componentes do CSS base
- Não recrie estilos de botões, inputs, cards
- Use as classes do `clickdesk-base.css`
- Crie CSS específico APENAS para estilos únicos da página

### 4. Mantenha as variáveis CSS
```css
/* ✅ BOM - usa variáveis */
.meu-elemento {
  color: var(--ink);
  background: var(--surface);
  border-radius: var(--radius-lg);
}

/* ❌ RUIM - valores fixos */
.meu-elemento {
  color: #1E2A22;
  background: #F5EFE6;
  border-radius: 12px;
}
```

---

## 📱 Responsividade

### Breakpoints
```css
@media (max-width: 1080px) {
  /* Tablet */
}

@media (max-width: 760px) {
  /* Mobile */
  /* Sidebar vira ícones (84px) */
  /* nav-label oculta */
}
```

---

## ✅ Checklist de Padronização

Ao criar/atualizar uma tela:

- [ ] Importa `clickdesk-base.css`
- [ ] Usa estrutura `app-shell`
- [ ] Sidebar com navegação completa
- [ ] Topbar presente
- [ ] Título com `page-title page-title--accent`
- [ ] Usa variáveis CSS (não valores fixos)
- [ ] Usa componentes base (btn, card, input, badge)
- [ ] Responsivo (testa em mobile)
- [ ] Acessibilidade (aria-labels, semântica)

---

## 📚 Exemplos de Telas Padronizadas

✅ **Dashboard** - Referência oficial  
✅ **FAQ** - Em processo  
✅ **Novo Chamado** - Em processo  

---

## 🔄 Manutenção

### Para adicionar nova cor
1. Adicione em `:root` no `clickdesk-base.css`
2. Use padrão de nomenclatura: `--categoria-nome`
3. Documente aqui no README

### Para adicionar novo componente
1. Adicione no `clickdesk-base.css`
2. Adicione exemplo no `template-base.html`
3. Documente aqui

---

## 🎯 Objetivo

**Manter consistência visual em TODAS as telas do Clickdesk:**
- Mesmas cores
- Mesmas fontes e tamanhos
- Mesmos espaçamentos
- Mesmos componentes
- Mesma experiência de usuário

**Resultado:** Interface profissional, coesa e fácil de manter! 🚀
