using System.Drawing;
using System.Drawing.Drawing2D;

namespace ClickDesk.Utils
{
    /// <summary>
    /// Define estilos, espaçamentos e métodos auxiliares para a identidade visual do ClickDesk.
    /// Segue o design do sistema web para consistência visual.
    /// </summary>
    public static class ClickDeskStyles
    {
        // ===== BORDER RADIUS =====
        public const int RadiusXXL = 18; // Sidebar
        public const int RadiusXL = 16;  // Cards
        public const int RadiusLG = 12;  // Botões grandes
        public const int RadiusMD = 10;  // Botões normais
        public const int RadiusSM = 8;   // Inputs

        // ===== SIDEBAR =====
        public const int SidebarWidth = 260;

        // ===== FONTES =====
        public static Font FontLG => new Font("Segoe UI", 10F);
        public static Font Font5XL => new Font("Segoe UI", 28F, FontStyle.Bold);

        // ===== ESPAÇAMENTOS (baseado no sistema web) =====
        public const int PaddingMainArea = 32; // Horizontal
        public const int PaddingMainAreaVertical = 28; // Vertical
        public const int PaddingCard = 14;
        public const int GapContainer = 18;
        public const int GapGrid = 14;

        /// <summary>
        /// Cria GraphicsPath para bordas arredondadas em todos os cantos.
        /// </summary>
        public static GraphicsPath GetRoundedRectangle(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            var path = new GraphicsPath();

            // Canto superior esquerdo
            path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);

            // Canto superior direito
            path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90);

            // Canto inferior direito
            path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);

            // Canto inferior esquerdo
            path.AddArc(bounds.X, bounds.Bottom - diameter, diameter, diameter, 90, 90);

            path.CloseFigure();

            return path;
        }

        /// <summary>
        /// Cria GraphicsPath para bordas arredondadas apenas nos cantos direitos (para sidebar)
        /// </summary>
        public static GraphicsPath GetRoundedRectangleRight(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            var path = new GraphicsPath();

            // Canto superior esquerdo (reto)
            path.AddLine(bounds.X, bounds.Y, bounds.Right - radius, bounds.Y);

            // Canto superior direito (arredondado)
            path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90);

            // Lado direito
            path.AddLine(bounds.Right, bounds.Y + radius, bounds.Right, bounds.Bottom - radius);

            // Canto inferior direito (arredondado)
            path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);

            // Canto inferior esquerdo (reto)
            path.AddLine(bounds.Right - radius, bounds.Bottom, bounds.X, bounds.Bottom);

            // Lado esquerdo
            path.AddLine(bounds.X, bounds.Bottom, bounds.X, bounds.Y);

            path.CloseFigure();

            return path;
        }
    }

    /// <summary>
    /// Define a paleta de cores alternativa do ClickDesk (estilo web).
    /// </summary>
    public static class ClickDeskColors
    {
        // ===== BACKGROUNDS =====
        /// <summary>Background da aplicação - Hex: #EDE6D9</summary>
        public static Color BackgroundApp => Color.FromArgb(237, 230, 217);

        /// <summary>Surface (cards) - Hex: #F5EFE6</summary>
        public static Color Surface => Color.FromArgb(245, 239, 230);

        // ===== BRAND =====
        /// <summary>Brand (Laranja) - Hex: #F28A1A</summary>
        public static Color Brand => Color.FromArgb(242, 138, 26);

        /// <summary>Brand Dark (hover) - Hex: #D97706</summary>
        public static Color BrandDark => Color.FromArgb(217, 119, 6);

        // ===== TEXTO =====
        /// <summary>Texto principal - Hex: #1E2A22</summary>
        public static Color TextPrimary => Color.FromArgb(30, 42, 34);

        /// <summary>Texto secundário - Hex: #6B7280</summary>
        public static Color TextSecondary => Color.FromArgb(107, 114, 128);

        // ===== BORDAS =====
        /// <summary>Borda padrão - Hex: #E5E7EB</summary>
        public static Color Border => Color.FromArgb(229, 231, 235);
    }
}
