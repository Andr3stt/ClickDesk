using System.Drawing;

namespace ClickDesk.Forms
{
    internal static class ClickDeskColors
    {
        // Cores principais
        public static readonly Color Primary      = ColorTranslator.FromHtml("#FA8C2B");
        public static readonly Color PrimaryDark  = ColorTranslator.FromHtml("#E1780E");
        public static readonly Color Background   = ColorTranslator.FromHtml("#F5EFE6");
        public static readonly Color BackgroundApp = ColorTranslator.FromHtml("#EDE6D9");
        public static readonly Color CardBackground = ColorTranslator.FromHtml("#F5EFE6");
        public static readonly Color Brand        = ColorTranslator.FromHtml("#FA8C2B");
        public static readonly Color BrandDark    = ColorTranslator.FromHtml("#E1780E");
        public static readonly Color Info         = ColorTranslator.FromHtml("#2196F3");
        public static readonly Color SidebarBackground = ColorTranslator.FromHtml("#F5EFE6");
        public static readonly Color Page         = ColorTranslator.FromHtml("#EDE6D9");
        public static readonly Color Text         = ColorTranslator.FromHtml("#15241B");
        public static readonly Color TextPrimary = ColorTranslator.FromHtml("#15241B");
        public static readonly Color TextSecondary = ColorTranslator.FromHtml("#7C7C7C");
        public static readonly Color SubtleText   = ColorTranslator.FromHtml("#7C7C7C");
        public static readonly Color Border       = ColorTranslator.FromHtml("#CFCFCF");
        public static readonly Color Danger       = ColorTranslator.FromHtml("#D32F2F");
        public static readonly Color Success      = ColorTranslator.FromHtml("#0F8C4D");
        public static readonly Color SuccessLight = ColorTranslator.FromHtml("#E8F5E9");
        
        // Cores de superfície
        public static readonly Color Surface      = Color.White;
        public static readonly Color White        = Color.White;
        
        // Escala de cinza
        public static readonly Color Gray50       = ColorTranslator.FromHtml("#F9F9F9");
        public static readonly Color Gray100      = ColorTranslator.FromHtml("#F3F3F3");
        public static readonly Color Gray200      = ColorTranslator.FromHtml("#E8E8E8");
        public static readonly Color Gray300      = ColorTranslator.FromHtml("#E0E0E0");
        public static readonly Color Gray400      = ColorTranslator.FromHtml("#D1D1D1");
        public static readonly Color Gray500      = ColorTranslator.FromHtml("#9E9E9E");
        public static readonly Color Gray600      = ColorTranslator.FromHtml("#757575");
        public static readonly Color Gray700      = ColorTranslator.FromHtml("#616161");
        public static readonly Color Gray800      = ColorTranslator.FromHtml("#424242");
        public static readonly Color Gray900      = ColorTranslator.FromHtml("#212121");
        
        // Cores de texto
        public static readonly Color TextLight    = ColorTranslator.FromHtml("#757575");
        
        // Cores de alerta
        public static readonly Color Warning      = ColorTranslator.FromHtml("#FFA500");
        
        // Métodos utilitários
        public static Color GetStatusColor(string status)
        {
            if (status == null) return Gray500;
            
            switch (status.ToLower())
            {
                case "aberto": return Primary;
                case "em_andamento": return Warning;
                case "fechado": return Success;
                case "cancelado": return Danger;
                default: return Gray500;
            }
        }
        
        public static Color GetSeveridadeColor(string severidade)
        {
            if (severidade == null) return Gray500;
            
            switch (severidade.ToLower())
            {
                case "alta": return Danger;
                case "média": return Warning;
                case "baixa": return Success;
                default: return Gray500;
            }
        }
        
        public static Color GetRoleColor(string role)
        {
            if (role == null) return Gray500;
            
            switch (role.ToLower())
            {
                case "admin": return Danger;
                case "tecnico": return Primary;
                case "usuario": return Success;
                default: return Gray500;
            }
        }
    }
}
