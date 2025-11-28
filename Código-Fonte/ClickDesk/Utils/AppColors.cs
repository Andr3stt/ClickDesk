using System.Drawing;

namespace ClickDesk.Utils
{
    /// <summary>
    /// Define a paleta de cores do sistema ClickDesk.
    /// Todas as cores seguem o design do sistema web para consistência visual.
    /// </summary>
    public static class AppColors
    {
        // ========================================
        // CORES PRIMÁRIAS
        // ========================================

        /// <summary>
        /// Cor primária (azul) - Botões principais, links, destaques
        /// Hex: #2563eb
        /// </summary>
        public static Color Primary => Color.FromArgb(37, 99, 235);

        /// <summary>
        /// Cor primária escura (hover)
        /// Hex: #1d4ed8
        /// </summary>
        public static Color PrimaryDark => Color.FromArgb(29, 78, 216);

        /// <summary>
        /// Cor primária clara (backgrounds sutis)
        /// Hex: #dbeafe
        /// </summary>
        public static Color PrimaryLight => Color.FromArgb(219, 234, 254);

        // ========================================
        // CORES DE STATUS
        // ========================================

        /// <summary>
        /// Sucesso (verde) - Operações bem-sucedidas, status resolvido
        /// Hex: #10b981
        /// </summary>
        public static Color Success => Color.FromArgb(16, 185, 129);

        /// <summary>
        /// Sucesso escuro (hover)
        /// Hex: #059669
        /// </summary>
        public static Color SuccessDark => Color.FromArgb(5, 150, 105);

        /// <summary>
        /// Sucesso claro (backgrounds)
        /// Hex: #d1fae5
        /// </summary>
        public static Color SuccessLight => Color.FromArgb(209, 250, 229);

        /// <summary>
        /// Perigo/Erro (vermelho) - Erros, exclusões, alertas críticos
        /// Hex: #ef4444
        /// </summary>
        public static Color Danger => Color.FromArgb(239, 68, 68);

        /// <summary>
        /// Perigo escuro (hover)
        /// Hex: #dc2626
        /// </summary>
        public static Color DangerDark => Color.FromArgb(220, 38, 38);

        /// <summary>
        /// Perigo claro (backgrounds)
        /// Hex: #fee2e2
        /// </summary>
        public static Color DangerLight => Color.FromArgb(254, 226, 226);

        /// <summary>
        /// Aviso (amarelo/laranja) - Alertas, status em andamento
        /// Hex: #f59e0b
        /// </summary>
        public static Color Warning => Color.FromArgb(245, 158, 11);

        /// <summary>
        /// Aviso escuro (hover)
        /// Hex: #d97706
        /// </summary>
        public static Color WarningDark => Color.FromArgb(217, 119, 6);

        /// <summary>
        /// Aviso claro (backgrounds)
        /// Hex: #fef3c7
        /// </summary>
        public static Color WarningLight => Color.FromArgb(254, 243, 199);

        /// <summary>
        /// Info (azul claro) - Informações, dicas
        /// Hex: #3b82f6
        /// </summary>
        public static Color Info => Color.FromArgb(59, 130, 246);

        /// <summary>
        /// Info claro (backgrounds)
        /// Hex: #e0f2fe
        /// </summary>
        public static Color InfoLight => Color.FromArgb(224, 242, 254);

        // ========================================
        // TONS DE CINZA
        // ========================================

        /// <summary>
        /// Cinza mais escuro - Textos principais, sidebar
        /// Hex: #111827
        /// </summary>
        public static Color Gray900 => Color.FromArgb(17, 24, 39);

        /// <summary>
        /// Cinza escuro - Backgrounds de sidebar, headers
        /// Hex: #1f2937
        /// </summary>
        public static Color Gray800 => Color.FromArgb(31, 41, 55);

        /// <summary>
        /// Cinza médio-escuro - Hover em menus
        /// Hex: #374151
        /// </summary>
        public static Color Gray700 => Color.FromArgb(55, 65, 81);

        /// <summary>
        /// Cinza médio - Bordas, separadores
        /// Hex: #4b5563
        /// </summary>
        public static Color Gray600 => Color.FromArgb(75, 85, 99);

        /// <summary>
        /// Cinza neutro - Texto secundário
        /// Hex: #6b7280
        /// </summary>
        public static Color Gray500 => Color.FromArgb(107, 114, 128);

        /// <summary>
        /// Cinza claro-médio - Placeholders
        /// Hex: #9ca3af
        /// </summary>
        public static Color Gray400 => Color.FromArgb(156, 163, 175);

        /// <summary>
        /// Cinza claro - Bordas de inputs
        /// Hex: #d1d5db
        /// </summary>
        public static Color Gray300 => Color.FromArgb(209, 213, 219);

        /// <summary>
        /// Cinza mais claro - Backgrounds alternativos
        /// Hex: #e5e7eb
        /// </summary>
        public static Color Gray200 => Color.FromArgb(229, 231, 235);

        /// <summary>
        /// Cinza quase branco - Backgrounds principais
        /// Hex: #f3f4f6
        /// </summary>
        public static Color Gray100 => Color.FromArgb(243, 244, 246);

        /// <summary>
        /// Branco levemente acinzentado
        /// Hex: #f9fafb
        /// </summary>
        public static Color Gray50 => Color.FromArgb(249, 250, 251);

        // ========================================
        // CORES AUXILIARES
        // ========================================

        /// <summary>
        /// Branco puro
        /// </summary>
        public static Color White => Color.White;

        /// <summary>
        /// Preto puro
        /// </summary>
        public static Color Black => Color.Black;

        /// <summary>
        /// Texto principal (escuro)
        /// </summary>
        public static Color TextPrimary => Gray900;

        /// <summary>
        /// Texto secundário (cinza)
        /// </summary>
        public static Color TextSecondary => Gray500;

        /// <summary>
        /// Texto em backgrounds escuros
        /// </summary>
        public static Color TextLight => Gray100;

        /// <summary>
        /// Background da sidebar
        /// </summary>
        public static Color SidebarBackground => Gray800;

        /// <summary>
        /// Background da área de conteúdo
        /// </summary>
        public static Color ContentBackground => Gray100;

        /// <summary>
        /// Background de cards
        /// </summary>
        public static Color CardBackground => White;

        /// <summary>
        /// Borda padrão
        /// </summary>
        public static Color Border => Gray300;

        /// <summary>
        /// Borda de inputs com foco
        /// </summary>
        public static Color BorderFocus => Primary;

        // ========================================
        // MÉTODOS AUXILIARES
        // ========================================

        /// <summary>
        /// Retorna a cor correspondente ao status do chamado.
        /// </summary>
        /// <param name="status">Status do chamado</param>
        /// <returns>Cor apropriada para o status</returns>
        public static Color GetStatusColor(string status)
        {
            switch (status?.ToUpper())
            {
                case "ABERTO": return Primary;
                case "EM_ANDAMENTO": return Warning;
                case "RESOLVIDO": return Success;
                case "FECHADO": return Gray500;
                case "ESCALADO": return Danger;
                default: return Gray500;
            }
        }

        /// <summary>
        /// Retorna a cor correspondente à severidade do chamado.
        /// </summary>
        /// <param name="severidade">Severidade do chamado</param>
        /// <returns>Cor apropriada para a severidade</returns>
        public static Color GetSeveridadeColor(string severidade)
        {
            switch (severidade?.ToUpper())
            {
                case "BAIXA": return Success;
                case "MEDIA": return Warning;
                case "ALTA": return Danger;
                case "CRITICA": return Color.FromArgb(124, 45, 18); // Vermelho escuro
                default: return Gray500;
            }
        }

        /// <summary>
        /// Retorna a cor correspondente ao papel do usuário.
        /// </summary>
        /// <param name="role">Papel do usuário</param>
        /// <returns>Cor apropriada para o papel</returns>
        public static Color GetRoleColor(string role)
        {
            switch (role?.ToUpper())
            {
                case "ADMIN": return Danger;
                case "TECH": return Warning;
                case "USER": return Primary;
                default: return Gray500;
            }
        }
    }
}
