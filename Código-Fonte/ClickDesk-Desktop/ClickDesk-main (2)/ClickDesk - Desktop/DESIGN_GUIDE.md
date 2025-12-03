# 📋 Guia de Padronização Visual - ClickDesk WinForms

## 🎯 Objetivo
Padronizar todos os formulários do projeto ClickDesk usando apenas **C# puro**, **GDI+** e **controles personalizados**, replicando o visual do sistema web sem HTML/CSS/JavaScript.

---

## 📦 Componentes Criados

### 1. **StyleManager.cs** (Utils)
Gerenciador centralizado de estilos contendo:
- ✅ Paleta de cores replicando o sistema web
- ✅ Definições de fontes padrão
- ✅ Constantes de espaçamento (padding, margin, border-radius)
- ✅ Métodos auxiliares para aplicar estilos

**Localização:** `Utils/StyleManager.cs`

**Uso:**
```csharp
// Cores
Color primaryColor = StyleManager.Colors.Primary;
Color successColor = StyleManager.Colors.Success;

// Fontes
Font titleFont = StyleManager.Fonts.Title;
Font bodyFont = StyleManager.Fonts.Body;

// Espaçamento
int padding = StyleManager.Spacing.PaddingLarge;
int borderRadius = StyleManager.Spacing.BorderRadiusMedium;
```

---

### 2. **RoundedPanel.cs** (Controls)
Painel com bordas arredondadas, sombra e efeitos modernos.

**Propriedades Principais:**
- `BorderRadius` - Raio das bordas (padrão: 8px)
- `ShowShadow` - Exibir sombra (padrão: true)
- `ShowBorder` - Exibir borda (padrão: true)
- `BorderColor` - Cor da borda
- `ShadowDepth` - Profundidade da sombra

**Exemplo de Uso:**
```csharp
RoundedPanel panel = new RoundedPanel
{
    BackColor = StyleManager.Colors.Background,
    BorderRadius = 8,
    ShowShadow = true,
    Padding = new Padding(16),
    Size = new Size(300, 200),
    Location = new Point(10, 10)
};
this.Controls.Add(panel);
```

---

### 3. **RoundedButton.cs** (Controls)
Botão com bordas arredondadas, efeitos hover/click e cores personalizáveis.

**Propriedades Principais:**
- `BorderRadius` - Raio das bordas
- `HoverColor` - Cor ao passar mouse
- `ClickColor` - Cor ao clicar
- Herda todas as propriedades do Button

**Exemplo de Uso:**
```csharp
RoundedButton btnSave = new RoundedButton
{
    Text = "Salvar",
    BackColor = StyleManager.Colors.Primary,
    HoverColor = StyleManager.Colors.PrimaryDark,
    ClickColor = StyleManager.Colors.PrimaryDark,
    Size = new Size(120, 40),
    Location = new Point(10, 10)
};
this.Controls.Add(btnSave);
```

---

### 4. **InputText.cs** (Controls)
TextBox estilizado com bordas arredondadas, efeito foco e placeholder.

**Propriedades Principais:**
- `Placeholder` - Texto de dica
- `BorderRadius` - Raio das bordas
- `BorderColor` - Cor da borda
- `FocusedBorderColor` - Cor da borda ao focar
- `UsePasswordChar` - Mostrar como senha
- `MaxLength` - Máximo de caracteres

**Exemplo de Uso:**
```csharp
InputText inputEmail = new InputText
{
    Placeholder = "Digite seu email",
    Size = new Size(300, 40),
    Location = new Point(10, 10),
    MaxLength = 100
};
this.Controls.Add(inputEmail);
string email = inputEmail.Text;
```

---

### 5. **StyledCard.cs** (Controls)
Card/Container para agrupar conteúdo com título opcional.

**Propriedades Principais:**
- `Title` - Título do card
- `ShowTitle` - Exibir título
- `BorderRadius` - Raio das bordas
- `ShowShadow` - Exibir sombra
- `BorderColor` - Cor da borda

**Exemplo de Uso:**
```csharp
StyledCard card = new StyledCard
{
    Title = "Informações Pessoais",
    ShowTitle = true,
    BackColor = StyleManager.Colors.Background,
    Size = new Size(400, 300),
    Location = new Point(10, 10)
};
this.Controls.Add(card);
```

---

## 🎨 Paleta de Cores Disponíveis

### Cores Primárias
```csharp
StyleManager.Colors.Primary         // #4287F5 - Azul principal
StyleManager.Colors.PrimaryDark     // #326EC8 - Azul escuro
StyleManager.Colors.PrimaryLight    // #649BFF - Azul claro
```

### Cores de Status
```csharp
StyleManager.Colors.Success    // #2ECC71 - Verde
StyleManager.Colors.Warning    // #F1C40F - Amarelo
StyleManager.Colors.Danger     // #E74C3C - Vermelho
StyleManager.Colors.Info       // #3498DB - Azul informação
```

### Cores Neutras
```csharp
StyleManager.Colors.Light      // #F2F5FA - Muito claro
StyleManager.Colors.DarkGray   // #95A5A6 - Cinza
StyleManager.Colors.Dark       // #34495E - Escuro
```

### Cores de Texto
```csharp
StyleManager.Colors.TextPrimary    // #2C3E50 - Texto principal
StyleManager.Colors.TextSecondary  // #7F8C8D - Texto secundário
StyleManager.Colors.TextInverted   // #FFFFFF - Texto invertido
```

---

## 🔤 Fontes Disponíveis

```csharp
StyleManager.Fonts.Title          // 28px Bold - Para títulos principais
StyleManager.Fonts.Heading1       // 22px Bold - Para títulos seções
StyleManager.Fonts.Heading2       // 18px Bold - Para subtítulos
StyleManager.Fonts.Heading3       // 16px Bold - Para pequenos títulos

StyleManager.Fonts.BodyLarge      // 14px Regular - Corpo grande
StyleManager.Fonts.Body           // 12px Regular - Corpo padrão
StyleManager.Fonts.BodySmall      // 10px Regular - Corpo pequeno

StyleManager.Fonts.Label          // 11px Regular - Labels
StyleManager.Fonts.LabelBold      // 11px Bold - Labels em negrito

StyleManager.Fonts.Button         // 11px Bold - Botões
StyleManager.Fonts.ButtonSmall    // 10px Regular - Botões pequenos
```

---

## 📐 Espaçamento Padronizado

```csharp
StyleManager.Spacing.XSmall       // 4px
StyleManager.Spacing.Small        // 8px
StyleManager.Spacing.Medium       // 16px
StyleManager.Spacing.Large        // 24px
StyleManager.Spacing.XLarge       // 32px
StyleManager.Spacing.XXLarge      // 48px

// Padding/Margin
StyleManager.Spacing.PaddingSmall   // 8px
StyleManager.Spacing.PaddingMedium  // 12px
StyleManager.Spacing.PaddingLarge   // 16px

// Border Radius
StyleManager.Spacing.BorderRadiusSmall    // 4px
StyleManager.Spacing.BorderRadiusMedium   // 8px
StyleManager.Spacing.BorderRadiusLarge    // 12px
StyleManager.Spacing.BorderRadiusXL       // 16px
```

---

## 📝 Padrão de Layout Recomendado

```
┌─────────────────────────────────────────────┐
│          HEADER (RoundedPanel)              │  80px
├──────────────┬──────────────────────────────┤
│              │                              │
│  SIDE PANEL  │  CONTENT PANEL               │
│  (Menu)      │  (RoundedPanel)              │
│  280px       │                              │
│              │  ┌──────────────────────┐    │
│              │  │  StyledCard          │    │
│              │  │  - Title             │    │
│              │  │  - Inputs/Controls   │    │
│              │  └──────────────────────┘    │
│              │                              │
└──────────────┴──────────────────────────────┘
```

---

## 🚀 Implementação Passo a Passo

### Passo 1: Criar Novo Formulário Estilizado

```csharp
public partial class FormNovoFormulario : Form
{
    private RoundedPanel headerPanel;
    private RoundedPanel contentPanel;

    public FormNovoFormulario()
    {
        InitializeComponent();
        StyleForm();
        CreateLayout();
    }

    private void StyleForm()
    {
        // Configurar formulário base
        Text = "Meu Formulário";
        StartPosition = FormStartPosition.CenterScreen;
        ClientSize = new Size(1200, 700);
        BackColor = StyleManager.Colors.BackgroundAlt;
        Font = StyleManager.Fonts.Body;
    }

    private void CreateLayout()
    {
        // Criar header
        headerPanel = new RoundedPanel
        {
            BackColor = StyleManager.Colors.Primary,
            Dock = DockStyle.Top,
            Height = 80,
            Padding = new Padding(24)
        };
        Controls.Add(headerPanel);

        // Criar área de conteúdo
        contentPanel = new RoundedPanel
        {
            BackColor = StyleManager.Colors.BackgroundAlt,
            Dock = DockStyle.Fill,
            Padding = new Padding(StyleManager.Spacing.Large)
        };
        Controls.Add(contentPanel);

        // Adicionar controles ao contentPanel
        AddControls();
    }

    private void AddControls()
    {
        // Adicionar seus controles aqui
    }
}
```

### Passo 2: Usar Controles Estilizados

```csharp
private void AddControls()
{
    // Card
    StyledCard card = new StyledCard
    {
        Title = "Dados",
        ShowTitle = true,
        Location = new Point(0, 0),
        Size = new Size(500, 300)
    };
    contentPanel.Controls.Add(card);

    // Input
    InputText input = new InputText
    {
        Location = new Point(16, 60),
        Size = new Size(card.Width - 32, 40)
    };
    card.Controls.Add(input);

    // Botão
    RoundedButton btn = new RoundedButton
    {
        Text = "Enviar",
        Location = new Point(16, input.Bottom + 16),
        Size = new Size(120, 40),
        BackColor = StyleManager.Colors.Primary
    };
    card.Controls.Add(btn);
}
```

---

## ✅ Checklist de Refatoração

Para cada formulário existente:

- [ ] Adicionar header com `RoundedPanel`
- [ ] Adicionar side panel com menu
- [ ] Substituir `Button` por `RoundedButton`
- [ ] Substituir `TextBox` por `InputText`
- [ ] Substituir `Panel` por `RoundedPanel` ou `StyledCard`
- [ ] Aplicar cores do `StyleManager.Colors`
- [ ] Aplicar fontes do `StyleManager.Fonts`
- [ ] Usar espaçamento do `StyleManager.Spacing`
- [ ] Validar alinhamento visual
- [ ] Testar responsividade

---

## 🔧 Métodos Auxiliares Disponíveis

```csharp
// Aplicar cor com transparência
Color transparent = StyleManager.GetColorWithAlpha(
    StyleManager.Colors.Primary, 
    128  // alpha 0-255
);

// Centralizar controle
StyleManager.CenterControl(button, this);

// Aplicar efeito hover
StyleManager.ApplyHoverEffect(
    panel, 
    normalColor, 
    hoverColor
);

// Redimensionar botão com padding automático
StyleManager.AutoSizeButton(button, 100, 40);
```

---

## 📌 Notas Importantes

1. **Sem HTML/CSS/JS**: Tudo é implementado em C# puro com GDI+
2. **Compat ibilidade**: Todos os controles herdam de `Control`
3. **Performance**: Usar `DoubleBuffered = true` para suavidade
4. **Acessibilidade**: Suporta Tab, Enter, Escape padrão do Windows
5. **DPI Scaling**: Responde automaticamente ao zoom do SO

---

## 🎓 Exemplos Completos

Veja `FormStyledExample.cs` na pasta `Forms/Examples/` para um exemplo completo de implementação.

---

## 📞 Suporte

Para adicionar novos componentes ou modificar estilos, edite `StyleManager.cs` e todos os controles usarão as novas configurações automaticamente.

