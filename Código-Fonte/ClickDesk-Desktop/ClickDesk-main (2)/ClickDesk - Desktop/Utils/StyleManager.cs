using System.Drawing;

namespace ClickDesk.Utils
{
    public static class StyleManager
    {
        // ===== CORES GLOBAIS (baseadas no CSS que uso no Web) =====

        // Fundo geral da aplicação
        public static Color BgPage         = ColorTranslator.FromHtml("#EDE6D9");   // --bg-app
        // Card principal / superfícies
        public static Color Card           = ColorTranslator.FromHtml("#F5EFE6");   // --surface
        public static Color CardSoft       = ColorTranslator.FromHtml("#F7F3EC");   // --surface-2

        // Texto
        public static Color TextStrong     = ColorTranslator.FromHtml("#1E2A22");   // --ink
        public static Color TextSecondary  = ColorTranslator.FromHtml("#262B26");   // --ink-secondary
        public static Color TextSubtle     = ColorTranslator.FromHtml("#6F6F6F");   // --muted-ink

        // Acentos
        public static Color Accent         = ColorTranslator.FromHtml("#F28A1A");   // --brand
        public static Color AccentDark     = ColorTranslator.FromHtml("#E1780E");   // --brand-600;
        public static Color AccentLight    = ColorTranslator.FromHtml("#FFEAD6");   // --brand-light;

        // Inputs
        public static Color InputBg        = Color.White;
        public static Color InputBorder    = ColorTranslator.FromHtml("#D5D0C7");   // --outline

        // Bordas / linhas
        public static Color BorderStrong   = ColorTranslator.FromHtml("#C9C3B8");   // --outline-strong
        public static Color BorderSoft     = ColorTranslator.FromHtml("#EDE5DA");   // --outline-soft

        // ===== FONTES (mapeando o que outros arquivos podem usar) =====

        // Tenta Poppins; se não tiver, cai para Segoe UI
        private static Font GetFont(float size, FontStyle style = FontStyle.Regular)
        {
            try
            {
                return new Font("Poppins", size, style);
            }
            catch
            {
                return new Font("Segoe UI", size, style);
            }
        }

        // Fonte base / conteúdo
        public static Font FontRegular     => GetFont(11f, FontStyle.Regular);      // antes: FontBase
        public static Font FontBold        => GetFont(11f, FontStyle.Bold);         // antes: FontBaseStrong
        public static Font FontRegularStrong => GetFont(11f, FontStyle.Bold);       // alias para compatibilidade

        // Títulos de seções / cards
        public static Font FontSectionTitle => GetFont(13f, FontStyle.Bold);

        // Título grande de login (Login Clickdesk)
        public static Font FontLoginTitle  => GetFont(22f, FontStyle.Bold);        // antes: FontLG

        // Logo / branding (Clickdesk grande do lado esquerdo)
        public static Font FontBigLogo     => GetFont(36f, FontStyle.Bold);        // antes: Font5XL / Font3XL

        // ===== RADII PADRÕES (úteis para alguns forms) =====

        public static int RadiusXL         = 22;   // cards grandes / containers
        public static int RadiusMD         = 12;   // botões, inputs
        public static int RadiusSM         = 8;    // badges, pequenos elementos
    }
}
