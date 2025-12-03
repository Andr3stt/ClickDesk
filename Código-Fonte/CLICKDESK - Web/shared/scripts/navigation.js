// Script de navegação global para o ClickDesk
// Corrige automaticamente todos os links com espaços nos caminhos

(function() {
  'use strict';

  // Função auxiliar para navegar com segurança
  window.navigateTo = function(path) {
    window.location.href = encodeURI(path);
  };

  // Função para abrir detalhes de chamado
  window.openTicketDetails = function(ticketId) {
    // Para usuário comum
    window.location.href = encodeURI(`../14.1. TELA DETALHES CHAMADO/meu-chamado.html?id=${ticketId}`);
  };

  // Função para abrir ticket (admin)
  window.openTicket = function(ticketId) {
    window.location.href = encodeURI(`../14. TELA DE MEU CHAMADO/detalhes-chamado.html?id=${ticketId}`);
  };

  // Interceptar cliques em links
  document.addEventListener('click', function(e) {
    const link = e.target.closest('a');
    if (!link || !link.href) return;
    
    // Se o href tem espaços e não está encodado
    const href = link.getAttribute('href');
    if (href && href.includes(' ') && !href.includes('%20')) {
      e.preventDefault();
      window.location.href = encodeURI(href);
    }
  });

  // Interceptar submissão de formulários com action contendo espaços
  document.addEventListener('submit', function(e) {
    const form = e.target;
    if (!form || !form.action) return;
    
    const action = form.getAttribute('action');
    if (action && action.includes(' ') && !action.includes('%20')) {
      e.preventDefault();
      form.action = encodeURI(action);
      form.submit();
    }
  });

})();
