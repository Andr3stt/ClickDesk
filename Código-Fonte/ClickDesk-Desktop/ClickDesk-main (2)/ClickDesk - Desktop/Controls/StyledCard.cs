using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ClickDesk.Utils;

namespace ClickDesk.Controls
{
    /// <summary>
    /// Painel estilizado tipo Card para exibir conteúdo em containers modernos.
    /// Implementado 100% em C# com GDI+.
    /// </summary>
    public class StyledCard : Panel
    {
        private int _borderRadius = 8;
        private int _shadowDepth = 4;
        private bool _showShadow = true;
        private Color _borderColor = StyleManager.Colors.BorderLight;
        private int _borderThickness = 1;
        private string _title = "";
        private Font _titleFont = StyleManager.Fonts.Heading3;
        private bool _showTitle = false;

        [Category("Appearance")]
        [Description("Título do card")]
        public string Title
        {
            get => _title;
            set { _title = value; Invalidate(); }
        }

        [Category("Appearance")]
        [Description("Mostrar título do card")]
        public bool ShowTitle
        {
            get => _showTitle;
            set { _showTitle = value; Invalidate(); }
        }

        [Category("Appearance")]
        [Description("Raio das bordas arredondadas")]
        public int BorderRadius
        {
            get => _borderRadius;
            set { _borderRadius = value; Invalidate(); }
        }

        [Category("Appearance")]
        [Description("Profundidade da sombra")]
        public int ShadowDepth
        {
            get => _shadowDepth;
            set { _shadowDepth = value; Invalidate(); }
        }

        [Category("Appearance")]
        [Description("Mostrar sombra")]
        public bool ShowShadow
        {
            get => _showShadow;
            set { _showShadow = value; Invalidate(); }
        }

        [Category("Appearance")]
        [Description("Cor da borda")]
        public Color BorderColor
        {
            get => _borderColor;
            set { _borderColor = value; Invalidate(); }
        }

        [Category("Appearance")]
        [Description("Espessura da borda")]
        public int BorderThickness
        {
            get => _borderThickness;
            set { _borderThickness = value; Invalidate(); }
        }

        public StyledCard()
        {
            DoubleBuffered = true;
            BackColor = StyleManager.Colors.Background;
            ForeColor = StyleManager.Colors.TextPrimary;
            Padding = new Padding(StyleManager.Spacing.PaddingLarge);
            Size = new Size(300, 200);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            // Calcular área utilizável
            Rectangle contentArea = ClientRectangle;

            // Desenhar sombra
            if (_showShadow)
            {
                DrawShadow(e.Graphics, contentArea);
            }

            // Criar caminho arredondado
            GraphicsPath path = CreateRoundedRectangle(contentArea, _borderRadius);

            // Preencher com background
            using (Brush brush = new SolidBrush(BackColor))
            {
                e.Graphics.FillPath(brush, path);
            }

            // Desenhar borda
            if (_borderThickness > 0)
            {
                using (Pen pen = new Pen(_borderColor, _borderThickness))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            }

            // Desenhar título se ativado
            if (_showTitle && !string.IsNullOrEmpty(_title))
            {
                DrawTitle(e.Graphics, contentArea);
            }

            path.Dispose();

            // Desenhar conteúdo dos controles
            base.OnPaint(e);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // Não pintar background padrão
        }

        private void DrawTitle(Graphics graphics, Rectangle area)
        {
            Rectangle titleArea = new Rectangle(
                area.Left + Padding.Left,
                area.Top + Padding.Top,
                area.Width - Padding.Left - Padding.Right,
                30
            );

            // Desenhar linha separadora abaixo do título
            using (Pen linePen = new Pen(StyleManager.Colors.BorderLight, 1))
            {
                graphics.DrawLine(
                    linePen,
                    titleArea.Left,
                    titleArea.Bottom + 8,
                    titleArea.Right,
                    titleArea.Bottom + 8
                );
            }

            TextRenderer.DrawText(
                graphics,
                _title,
                _titleFont,
                titleArea,
                ForeColor,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter
            );
        }

        private void DrawShadow(Graphics graphics, Rectangle area)
        {
            Rectangle shadowRect = new Rectangle(
                area.X,
                area.Y + _shadowDepth,
                area.Width,
                area.Height
            );

            GraphicsPath shadowPath = CreateRoundedRectangle(shadowRect, _borderRadius);

            using (Brush shadowBrush = new SolidBrush(StyleManager.Colors.GetColorWithAlpha(Color.Black, 15)))
            {
                graphics.FillPath(shadowBrush, shadowPath);
            }

            shadowPath.Dispose();
        }

        private GraphicsPath CreateRoundedRectangle(Rectangle bounds, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;

            if (diameter > bounds.Width) diameter = bounds.Width;
            if (diameter > bounds.Height) diameter = bounds.Height;

            path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
            path.AddArc(bounds.X + bounds.Width - diameter, bounds.Y, diameter, diameter, 270, 90);
            path.AddArc(bounds.X + bounds.Width - diameter, bounds.Y + bounds.Height - diameter, diameter, diameter, 0, 90);
            path.AddArc(bounds.X, bounds.Y + bounds.Height - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();

            return path;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }
    }
}
