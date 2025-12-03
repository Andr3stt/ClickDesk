# 📁 SHARED - Arquivos Compartilhados do Clickdesk

Esta pasta contém todos os arquivos base e documentação do sistema de design padronizado do Clickdesk.

---

## 📂 Estrutura de Arquivos

### 🎨 Estilos Base
- **`styles/clickdesk-base.css`** - CSS global com todas as variáveis, componentes e estilos reutilizáveis

### 📄 Templates
- **`template-base.html`** - Template HTML completo para criar novas telas
- **`paleta-cores.html`** - Visualização interativa da paleta de cores e componentes

### 📚 Documentação
- **`README-DESIGN-SYSTEM.md`** - Manual completo do sistema de design
- **`PADRONIZACAO-COMPLETA.md`** - Resumo da implementação e checklist
- **`README.md`** - Este arquivo (índice)

---

## 🚀 Início Rápido

### Para criar uma nova tela:

1. **Copie o template base**
   ```html
   <!-- No <head> da sua página -->
   <link rel="stylesheet" href="../shared/styles/clickdesk-base.css" />
   ```

2. **Use a estrutura padrão**
   Veja `template-base.html` para exemplo completo

3. **Consulte a documentação**
   Abra `README-DESIGN-SYSTEM.md` para ver todos os componentes

4. **Visualize a paleta**
   Abra `paleta-cores.html` no navegador para ver todas as cores e componentes

---

## 📖 Guias Disponíveis

### 🎨 Design System
**Arquivo:** `README-DESIGN-SYSTEM.md`

**Conteúdo:**
- Paleta de cores completa
- Tipografia (fontes, tamanhos, pesos)
- Layout e estrutura
- Componentes (botões, cards, inputs, badges)
- Responsividade
- Acessibilidade
- Como usar em novas telas
- Checklist de padronização

### ✅ Resumo de Implementação
**Arquivo:** `PADRONIZACAO-COMPLETA.md`

**Conteúdo:**
- O que foi criado
- Padrões estabelecidos
- Telas já padronizadas
- Próximos passos
- Como usar

### 🎨 Paleta Visual
**Arquivo:** `paleta-cores.html`

**Conteúdo:**
- Visualização de todas as cores
- Exemplos de tipografia
- Todos os componentes (botões, badges, cards)
- Border radius e shadows
- Demonstrações interativas

---

## 🎯 Componentes Disponíveis

### Layout
- `.app-shell` - Container principal
- `.sidebar` - Sidebar de navegação
- `.content` - Área de conteúdo
- `.topbar` - Barra superior
- `.main-area` - Área principal
- `.container` - Container de conteúdo

### Botões
- `.btn .btn-primary` - Laranja
- `.btn .btn-secondary` - Cinza
- `.icon-btn` - Apenas ícone
- `.btn-mini` - Pequeno

### Cards
- `.card` - Padrão
- `.card-soft` - Fundo suave
- `.card-title` - Título

### Inputs
- `.input` - Input/Select/Textarea
- `.label` - Label

### Badges
- `.badge .status-open` - Verde
- `.badge .status-progress` - Amarelo
- `.badge .status-waiting` - Azul
- `.badge .status-closed` - Cinza

### Prioridades
- `.prio-flag .p1` - P1 (vermelho)
- `.prio-flag .p2` - P2 (amarelo)
- `.prio-flag .p3` - P3 (verde)

### Outros
- `.chip-toggle` - Chip toggleável
- `.icon-stroke` - Ícone SVG
- `.page-title` - Título de página
- `.page-title--accent` - Com linha laranja

---

## 🎨 Paleta de Cores

### Cores Principais
```
Background:    #EDE6D9
Surface:       #F5EFE6
Surface 2:     #F7F3EC
Brand:         #F28A1A (laranja)
Texto:         #1E2A22
Texto muted:   #6F6F6F
Bordas:        #D5D0C7
```

### Status
```
✅ Sucesso:    #0F8C4D (verde)
⚠️ Atenção:    #A56400 (amarelo)
ℹ️ Info:       #2855B2 (azul)
❌ Erro:       #A12016 (vermelho)
```

---

## 📱 Responsividade

### Desktop (> 1080px)
- Sidebar: 240px
- Grid: 4 colunas
- Labels completas

### Tablet (760px - 1080px)
- Sidebar: 240px
- Grid: 2 colunas

### Mobile (< 760px)
- Sidebar: 84px (ícones)
- Grid: 1 coluna
- Labels ocultas

---

## ✅ Checklist para Nova Tela

- [ ] Importa `../shared/styles/clickdesk-base.css`
- [ ] Usa estrutura `app-shell`
- [ ] Sidebar presente
- [ ] Topbar presente
- [ ] Título com `page-title--accent`
- [ ] Usa componentes base
- [ ] Usa variáveis CSS
- [ ] Links de navegação corretos
- [ ] Responsivo
- [ ] Acessível

---

## 📞 Suporte

Para dúvidas sobre o sistema de design:

1. Consulte `README-DESIGN-SYSTEM.md`
2. Abra `paleta-cores.html` para visualizar
3. Use `template-base.html` como referência
4. Veja `PADRONIZACAO-COMPLETA.md` para resumo

---

## 🔄 Manutenção

### Para adicionar nova cor:
1. Adicione em `:root` no `clickdesk-base.css`
2. Use padrão: `--categoria-nome`
3. Atualize documentação

### Para adicionar novo componente:
1. Adicione no `clickdesk-base.css`
2. Adicione exemplo no `template-base.html`
3. Atualize `paleta-cores.html`
4. Documente no README

---

**Clickdesk Design System** - Sistema de padronização completo! 🎨✨
