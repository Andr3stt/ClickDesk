// ===========================
// Sistema de Autenticação
// Verifica se o usuário está logado e tem permissão para acessar a página
// ===========================

(function() {
  'use strict';

  // Verifica se há uma sessão ativa
  function getUserSession() {
    try {
      const session = localStorage.getItem('userSession');
      return session ? JSON.parse(session) : null;
    } catch (e) {
      return null;
    }
  }

  // Salva informações do usuário
  function saveUserSession(data) {
    localStorage.setItem('userSession', JSON.stringify(data));
  }

  // Remove a sessão (logout)
  function clearUserSession() {
    localStorage.removeItem('userSession');
  }

  // Verifica se o usuário tem permissão para acessar a página
  function checkPageAccess(requiredType) {
    const session = getUserSession();
    
    // Se não há sessão, redireciona para login
    if (!session) {
      redirectToLogin();
      return false;
    }

    // Se a página requer um tipo específico e o usuário não tem
    if (requiredType && session.tipo !== requiredType) {
      redirectToDashboard(session.tipo);
      return false;
    }

    return true;
  }

  // Redireciona para o login
  function redirectToLogin() {
    // Evita loop infinito
    if (window.location.pathname.includes('login.html')) return;
    
    window.location.href = getRelativePath('../1. TELA DE LOGIN/login.html');
  }

  // Redireciona para o dashboard apropriado
  function redirectToDashboard(userType) {
    if (userType === 'tecnico') {
      window.location.href = getRelativePath('../11. TELA DASHBOARD ADM/dashboard-adm.html');
    } else {
      window.location.href = getRelativePath('../6. TELA DE DASHBOARD/dashboard.html');
    }
  }

  // Calcula caminho relativo baseado na estrutura de pastas
  function getRelativePath(path) {
    // Esta função assume que estamos sempre dentro de uma pasta numerada
    // Ex: "1. TELA DE LOGIN", "6. TELA DE DASHBOARD", etc.
    return path;
  }

  // Configuração de logout para todos os botões de sair
  function setupLogoutButtons() {
    const logoutButtons = document.querySelectorAll('#logoutBtn, [data-logout]');
    logoutButtons.forEach(btn => {
      btn.addEventListener('click', (e) => {
        e.preventDefault();
        if (confirm('Deseja realmente sair?')) {
          clearUserSession();
          redirectToLogin();
        }
      });
    });
  }

  // Exibe nome do usuário em elementos com [data-user-name]
  function displayUserInfo() {
    const session = getUserSession();
    if (!session) return;

    // Exibir nome do usuário
    const nameElements = document.querySelectorAll('[data-user-name]');
    nameElements.forEach(el => {
      el.textContent = session.nome || session.username;
    });

    // Exibir tipo de usuário
    const typeElements = document.querySelectorAll('[data-user-type]');
    typeElements.forEach(el => {
      el.textContent = session.tipo === 'tecnico' ? 'Técnico' : 'Usuário';
    });
  }

  // Exportar funções globalmente
  window.ClickdeskAuth = {
    getUserSession,
    saveUserSession,
    clearUserSession,
    checkPageAccess,
    redirectToLogin,
    redirectToDashboard,
    setupLogoutButtons,
    displayUserInfo
  };

  // Auto-executar ao carregar a página
  document.addEventListener('DOMContentLoaded', () => {
    setupLogoutButtons();
    displayUserInfo();
  });

})();
