using System;
using ClickDesk.Models;

namespace ClickDesk.Utils
{
    /// <summary>
    /// Gerenciador de sessão do usuário.
    /// Mantém os dados do usuário logado e o token JWT em memória.
    /// </summary>
    public static class SessionManager
    {
        // ========================================
        // PROPRIEDADES DE SESSÃO
        // ========================================

        /// <summary>
        /// Token JWT de autenticação
        /// </summary>
        public static string Token { get; private set; }

        /// <summary>
        /// Dados do usuário logado
        /// </summary>
        public static User CurrentUser { get; private set; }

        /// <summary>
        /// Data/hora do login
        /// </summary>
        public static DateTime? LoginTime { get; private set; }

        /// <summary>
        /// Tempo de expiração do token em segundos
        /// </summary>
        public static int TokenExpiresIn { get; private set; }

        // ========================================
        // PROPRIEDADES COMPUTADAS
        // ========================================

        /// <summary>
        /// Verifica se há um usuário logado
        /// </summary>
        public static bool IsLoggedIn => CurrentUser != null && !string.IsNullOrEmpty(Token);

        /// <summary>
        /// Verifica se o usuário é administrador
        /// </summary>
        public static bool IsAdmin => CurrentUser?.IsAdmin ?? false;

        /// <summary>
        /// Verifica se o usuário é técnico
        /// </summary>
        public static bool IsTech => CurrentUser?.IsTech ?? false;

        /// <summary>
        /// Verifica se o usuário tem acesso administrativo (Admin ou Tech)
        /// </summary>
        public static bool HasAdminAccess => CurrentUser?.HasAdminAccess ?? false;

        /// <summary>
        /// Retorna o nome do usuário para exibição
        /// </summary>
        public static string UserDisplayName => CurrentUser?.Nome ?? CurrentUser?.Username ?? "Usuário";

        /// <summary>
        /// Retorna a role do usuário formatada
        /// </summary>
        public static string UserRole => CurrentUser?.Role ?? "USER";

        // ========================================
        // MÉTODOS DE GERENCIAMENTO DE SESSÃO
        // ========================================

        /// <summary>
        /// Salva os dados da sessão após login bem-sucedido.
        /// </summary>
        /// <param name="token">Token JWT</param>
        /// <param name="user">Dados do usuário</param>
        /// <param name="expiresIn">Tempo de expiração em segundos (padrão: 1 hora)</param>
        public static void SaveSession(string token, User user, int expiresIn = 3600)
        {
            Token = token;
            CurrentUser = user;
            LoginTime = DateTime.Now;
            TokenExpiresIn = expiresIn;
        }

        /// <summary>
        /// Limpa todos os dados da sessão (logout).
        /// </summary>
        public static void ClearSession()
        {
            Token = null;
            CurrentUser = null;
            LoginTime = null;
            TokenExpiresIn = 0;
        }

        /// <summary>
        /// Atualiza os dados do usuário na sessão.
        /// </summary>
        /// <param name="user">Dados atualizados</param>
        public static void UpdateUser(User user)
        {
            if (user != null)
            {
                CurrentUser = user;
            }
        }

        /// <summary>
        /// Atualiza o token na sessão.
        /// </summary>
        /// <param name="token">Novo token</param>
        /// <param name="expiresIn">Tempo de expiração</param>
        public static void UpdateToken(string token, int expiresIn = 3600)
        {
            if (!string.IsNullOrEmpty(token))
            {
                Token = token;
                LoginTime = DateTime.Now;
                TokenExpiresIn = expiresIn;
            }
        }

        /// <summary>
        /// Verifica se o token está expirado.
        /// </summary>
        /// <returns>True se expirado, False se válido</returns>
        public static bool IsTokenExpired()
        {
            if (!LoginTime.HasValue || TokenExpiresIn <= 0)
            {
                return true;
            }

            DateTime expirationTime = LoginTime.Value.AddSeconds(TokenExpiresIn);
            return DateTime.Now > expirationTime;
        }

        /// <summary>
        /// Retorna o tempo restante do token em segundos.
        /// </summary>
        /// <returns>Segundos restantes (0 se expirado)</returns>
        public static int GetTokenTimeRemaining()
        {
            if (!LoginTime.HasValue || TokenExpiresIn <= 0)
            {
                return 0;
            }

            DateTime expirationTime = LoginTime.Value.AddSeconds(TokenExpiresIn);
            TimeSpan remaining = expirationTime - DateTime.Now;

            return remaining.TotalSeconds > 0 ? (int)remaining.TotalSeconds : 0;
        }

        /// <summary>
        /// Verifica se o usuário tem uma determinada permissão.
        /// </summary>
        /// <param name="requiredRole">Role necessária</param>
        /// <returns>True se tem permissão</returns>
        public static bool HasRole(string requiredRole)
        {
            if (CurrentUser == null || string.IsNullOrEmpty(requiredRole))
            {
                return false;
            }

            string userRole = CurrentUser.Role?.ToUpper() ?? "";
            string required = requiredRole.ToUpper();

            // Admin tem acesso a tudo
            if (userRole == "ADMIN")
            {
                return true;
            }

            // Tech tem acesso a TECH e USER
            if (userRole == "TECH" && (required == "TECH" || required == "USER"))
            {
                return true;
            }

            // Verifica igualdade exata
            return userRole == required;
        }
    }
}
