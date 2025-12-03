// Detalhes do Chamado - JavaScript
(function() {
  'use strict';

  // Variáveis globais
  let currentTicket = null;
  let isLoading = false;

  // Verificar autenticação
  document.addEventListener('DOMContentLoaded', function() {
    if (typeof AuthUtils !== 'undefined') {
      // Verificar se o usuário está logado
      if (!AuthUtils.isLoggedIn()) {
        console.log('Usuário não está logado, redirecionando...');
        AuthUtils.redirectTo('login');
        return;
      }
      
      // Verificar se precisa completar o fluxo de registro
      if (!AuthUtils.hasAcceptedTerms()) {
        console.log('Termos não aceitos, redirecionando...');
        AuthUtils.redirectTo('terms');
        return;
      }
      
      if (!AuthUtils.hasCreatedProfile()) {
        console.log('Perfil não criado, redirecionando...');
        AuthUtils.redirectTo('profile');
        return;
      }
      
      console.log('Autenticação OK, carregando detalhes do chamado');
    }
    
    // Inicializar funcionalidades da página
    loadUserInfo();
    initializeTicketDetails();
    initializeEventListeners();
    loadTicketFromURL();
  });

  // Carregar informações do usuário
  function loadUserInfo() {
    if (typeof AuthUtils !== 'undefined' && AuthUtils.isLoggedIn()) {
      const user = AuthUtils.getCurrentUser();
      const userName = user?.username || 'Usuário';
      
      const userNameElements = document.querySelectorAll('.user-name');
      const userRoleElements = document.querySelectorAll('.user-role');
      const userAvatars = document.querySelectorAll('.user-avatar');
      
      userNameElements.forEach(el => el.textContent = userName);
      userRoleElements.forEach(el => el.textContent = 'Colaborador');
      userAvatars.forEach(el => el.textContent = userName.charAt(0).toUpperCase());
    }
  }

  // Inicializar detalhes do chamado
  function initializeTicketDetails() {
    // Carregar dados mock ou da URL
    const ticketId = getTicketIdFromURL();
    if (ticketId) {
      loadTicketDetails(ticketId);
    } else {
      loadMockTicketDetails();
    }
  }

  // Obter ID do chamado da URL
  function getTicketIdFromURL() {
    const urlParams = new URLSearchParams(window.location.search);
    return urlParams.get('id') || urlParams.get('ticket');
  }

  // Carregar dados do chamado da URL
  function loadTicketFromURL() {
    const ticketId = getTicketIdFromURL();
    if (ticketId) {
      console.log('Carregando chamado:', ticketId);
      // Aqui seria feita a chamada para API real
      // loadTicketDetails(ticketId);
    }
  }

  // Carregar detalhes do chamado (mock)
  function loadMockTicketDetails() {
    currentTicket = {
      id: '#12345',
      title: 'Problema com acesso ao sistema',
      status: 'open',
      priority: 'medium',
      author: 'João Silva',
      assignee: 'Maria Santos',
      category: 'Suporte Técnico',
      createdAt: '01/11/2025 10:30',
      updatedAt: '01/11/2025 14:22',
      description: `Estou enfrentando dificuldades para acessar o sistema desde ontem. Quando tento fazer login, o sistema retorna erro "Usuário ou senha inválidos", mesmo digitando as credenciais corretas.

Já tentei:
• Limpar cache do navegador
• Testar em navegador diferente
• Resetar senha pelo formulário

O problema persiste e preciso acessar urgentemente para finalizar relatórios do mês.`,
      attachments: [
        {
          name: 'screenshot-erro.png',
          size: '245 KB',
          url: '#'
        }
      ],
      comments: [
        {
          author: 'João Silva',
          avatar: 'JS',
          date: '01/11/2025 às 10:30',
          content: 'Chamado criado: Problema com acesso ao sistema'
        },
        {
          author: 'Maria Santos',
          avatar: 'MS',
          date: '01/11/2025 às 11:15',
          content: 'Olá João! Recebemos seu chamado e vamos investigar o problema. Pode confirmar qual navegador você está utilizando?'
        },
        {
          author: 'João Silva',
          avatar: 'JS',
          date: '01/11/2025 às 11:45',
          content: 'Estou usando Chrome versão 118. Também testei no Firefox e o problema persiste.'
        },
        {
          author: 'Maria Santos',
          avatar: 'MS',
          date: '01/11/2025 às 14:22',
          content: 'Identifiquei o problema. Houve uma atualização no sistema que afetou alguns usuários. Vou resetar sua conta e você receberá uma nova senha por email em alguns minutos.'
        }
      ]
    };

    updateTicketDisplay();
  }

  // Atualizar exibição do chamado
  function updateTicketDisplay() {
    if (!currentTicket) return;

    // Atualizar informações básicas
    updateElement('ticket-id', currentTicket.id);
    updateElement('ticket-title', currentTicket.title);
    updateElement('ticket-author', currentTicket.author);
    updateElement('ticket-date', currentTicket.createdAt);
    updateElement('ticket-category', currentTicket.category);
    updateElement('ticket-assignee', currentTicket.assignee);
    updateElement('ticket-updated', currentTicket.updatedAt);

    // Atualizar status
    const statusElement = document.getElementById('ticket-status');
    if (statusElement) {
      statusElement.textContent = getStatusText(currentTicket.status);
      statusElement.className = `status-badge status-${currentTicket.status}`;
    }

    // Atualizar prioridade
    const priorityElement = document.getElementById('ticket-priority');
    if (priorityElement) {
      priorityElement.textContent = getPriorityText(currentTicket.priority);
      priorityElement.className = `priority-badge priority-${currentTicket.priority}`;
    }

    // Atualizar descrição
    const descriptionElement = document.getElementById('ticket-description');
    if (descriptionElement) {
      descriptionElement.innerHTML = formatDescription(currentTicket.description);
    }

    // Atualizar anexos
    updateAttachments();

    // Atualizar comentários
    updateComments();

    // Atualizar selects
    updateSelects();
  }

  // Funções utilitárias
  function updateElement(id, value) {
    const element = document.getElementById(id);
    if (element) {
      element.textContent = value;
    }
  }

  function getStatusText(status) {
    const statusMap = {
      'open': 'Aberto',
      'in-progress': 'Em Andamento',
      'waiting': 'Aguardando',
      'resolved': 'Resolvido',
      'closed': 'Fechado'
    };
    return statusMap[status] || status;
  }

  function getPriorityText(priority) {
    const priorityMap = {
      'low': 'Baixa',
      'medium': 'Média',
      'high': 'Alta',
      'urgent': 'Urgente'
    };
    return priorityMap[priority] || priority;
  }

  function formatDescription(description) {
    return description
      .replace(/\n\n/g, '</p><p>')
      .replace(/\n•/g, '<br>•')
      .replace(/^(.+)$/, '<p>$1</p>');
  }

  // Atualizar anexos
  function updateAttachments() {
    const attachmentsContainer = document.getElementById('ticket-attachments');
    if (!attachmentsContainer || !currentTicket.attachments) return;

    attachmentsContainer.innerHTML = currentTicket.attachments.map(attachment => `
      <div class="attachment-item">
        <svg class="attachment-icon" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
          <path d="M13 2H6a2 2 0 0 0-2 2v16a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V9z"/>
          <polyline points="13,2 13,9 20,9"/>
        </svg>
        <div class="attachment-info">
          <span class="attachment-name">${attachment.name}</span>
          <span class="attachment-size">${attachment.size}</span>
        </div>
        <button class="attachment-download" aria-label="Baixar anexo" data-url="${attachment.url}">
          <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
            <path d="M21 15v4a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2v-4"/>
            <polyline points="7,10 12,15 17,10"/>
            <line x1="12" y1="15" x2="12" y2="3"/>
          </svg>
        </button>
      </div>
    `).join('');
  }

  // Atualizar comentários
  function updateComments() {
    const commentsContainer = document.getElementById('ticket-comments');
    if (!commentsContainer || !currentTicket.comments) return;

    commentsContainer.innerHTML = currentTicket.comments.map(comment => `
      <div class="comment-item">
        <div class="comment-author">
          <div class="author-avatar">${comment.avatar}</div>
          <div class="author-info">
            <span class="author-name">${comment.author}</span>
            <span class="comment-date">${comment.date}</span>
          </div>
        </div>
        <div class="comment-content">
          <p>${comment.content}</p>
        </div>
      </div>
    `).join('');
  }

  // Atualizar selects com valores atuais
  function updateSelects() {
    const statusSelect = document.getElementById('status-select');
    const prioritySelect = document.getElementById('priority-select');
    const assigneeSelect = document.getElementById('assignee-select');

    if (statusSelect) statusSelect.value = currentTicket.status;
    if (prioritySelect) prioritySelect.value = currentTicket.priority;
    if (assigneeSelect) assigneeSelect.value = getAssigneeValue(currentTicket.assignee);
  }

  function getAssigneeValue(assigneeName) {
    const assigneeMap = {
      'Maria Santos': 'maria',
      'Carlos Lima': 'carlos',
      'Ana Costa': 'ana'
    };
    return assigneeMap[assigneeName] || '';
  }

  // Inicializar event listeners
  function initializeEventListeners() {
    // Formulário de comentário
    const commentForm = document.getElementById('comment-form');
    if (commentForm) {
      commentForm.addEventListener('submit', handleCommentSubmit);
    }

    // Botão de anexar arquivo
    const attachButton = document.getElementById('attach-file');
    if (attachButton) {
      attachButton.addEventListener('click', handleAttachFile);
    }

    // Botões de ação
    const updateButton = document.getElementById('update-ticket');
    const resolveButton = document.getElementById('resolve-ticket');
    const closeButton = document.getElementById('close-ticket');

    if (updateButton) updateButton.addEventListener('click', handleUpdateTicket);
    if (resolveButton) resolveButton.addEventListener('click', handleResolveTicket);
    if (closeButton) closeButton.addEventListener('click', handleCloseTicket);

    // Downloads de anexos
    document.addEventListener('click', function(e) {
      if (e.target.closest('.attachment-download')) {
        handleAttachmentDownload(e);
      }
    });
  }

  // Handlers de eventos
  function handleCommentSubmit(e) {
    e.preventDefault();
    
    const commentTextarea = document.getElementById('new-comment');
    const commentText = commentTextarea.value.trim();
    
    if (!commentText) {
      alert('Por favor, digite um comentário.');
      return;
    }

    // Adicionar comentário
    addComment(commentText);
    
    // Limpar formulário
    commentTextarea.value = '';
    
    // Mostrar feedback
    showNotification('Comentário adicionado com sucesso!', 'success');
  }

  function addComment(content) {
    const user = AuthUtils.getCurrentUser();
    const userName = user?.username || 'Usuário';
    const userInitials = userName.split(' ').map(n => n[0]).join('').toUpperCase();
    
    const newComment = {
      author: userName,
      avatar: userInitials,
      date: new Date().toLocaleDateString('pt-BR') + ' às ' + new Date().toLocaleTimeString('pt-BR', { hour: '2-digit', minute: '2-digit' }),
      content: content
    };

    currentTicket.comments.push(newComment);
    updateComments();
    
    // Scroll para o novo comentário
    setTimeout(() => {
      const commentsContainer = document.getElementById('ticket-comments');
      commentsContainer.scrollTop = commentsContainer.scrollHeight;
    }, 100);
  }

  function handleAttachFile() {
    // Simular upload de arquivo
    const input = document.createElement('input');
    input.type = 'file';
    input.multiple = true;
    input.accept = '.pdf,.doc,.docx,.jpg,.jpeg,.png,.gif';
    
    input.onchange = function(e) {
      const files = Array.from(e.target.files);
      files.forEach(file => {
        console.log('Arquivo selecionado:', file.name);
        // Aqui seria implementado o upload real
      });
      
      if (files.length > 0) {
        showNotification(`${files.length} arquivo(s) selecionado(s). Upload em desenvolvimento.`, 'info');
      }
    };
    
    input.click();
  }

  function handleUpdateTicket() {
    const statusSelect = document.getElementById('status-select');
    const prioritySelect = document.getElementById('priority-select');
    const assigneeSelect = document.getElementById('assignee-select');

    // Atualizar dados do chamado
    if (statusSelect) currentTicket.status = statusSelect.value;
    if (prioritySelect) currentTicket.priority = prioritySelect.value;
    if (assigneeSelect) {
      const assigneeMap = {
        'maria': 'Maria Santos',
        'carlos': 'Carlos Lima',
        'ana': 'Ana Costa'
      };
      currentTicket.assignee = assigneeMap[assigneeSelect.value] || 'Sem responsável';
    }

    // Atualizar timestamp
    currentTicket.updatedAt = new Date().toLocaleDateString('pt-BR') + ' ' + new Date().toLocaleTimeString('pt-BR', { hour: '2-digit', minute: '2-digit' });

    // Atualizar display
    updateTicketDisplay();
    
    showNotification('Chamado atualizado com sucesso!', 'success');
  }

  function handleResolveTicket() {
    if (confirm('Tem certeza que deseja marcar este chamado como resolvido?')) {
      currentTicket.status = 'resolved';
      updateTicketDisplay();
      showNotification('Chamado marcado como resolvido!', 'success');
    }
  }

  function handleCloseTicket() {
    if (confirm('Tem certeza que deseja fechar este chamado? Esta ação não pode ser desfeita.')) {
      currentTicket.status = 'closed';
      updateTicketDisplay();
      showNotification('Chamado fechado com sucesso!', 'success');
    }
  }

  function handleAttachmentDownload(e) {
    e.preventDefault();
    const button = e.target.closest('.attachment-download');
    const url = button.dataset.url;
    
    if (url && url !== '#') {
      window.open(url, '_blank');
    } else {
      showNotification('Download de anexos em desenvolvimento.', 'info');
    }
  }

  // Função para mostrar notificações
  function showNotification(message, type = 'info') {
    // Criar elemento de notificação
    const notification = document.createElement('div');
    notification.className = `notification notification-${type}`;
    notification.innerHTML = `
      <div class="notification-content">
        <span class="notification-message">${message}</span>
        <button class="notification-close" onclick="this.parentElement.parentElement.remove()">×</button>
      </div>
    `;

    // Adicionar estilos se não existirem
    if (!document.getElementById('notification-styles')) {
      const styles = document.createElement('style');
      styles.id = 'notification-styles';
      styles.textContent = `
        .notification {
          position: fixed;
          top: 20px;
          right: 20px;
          z-index: 1000;
          max-width: 400px;
          padding: 1rem;
          border-radius: 8px;
          box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15);
          animation: slideInRight 0.3s ease-out;
        }
        .notification-success { background: #D1FAE5; color: #065F46; border-left: 4px solid #10B981; }
        .notification-info { background: #DBEAFE; color: #1E3A8A; border-left: 4px solid #3B82F6; }
        .notification-warning { background: #FEF3C7; color: #92400E; border-left: 4px solid #F59E0B; }
        .notification-error { background: #FEE2E2; color: #991B1B; border-left: 4px solid #EF4444; }
        .notification-content { display: flex; justify-content: space-between; align-items: center; }
        .notification-close { background: none; border: none; font-size: 1.25rem; cursor: pointer; opacity: 0.7; }
        .notification-close:hover { opacity: 1; }
        @keyframes slideInRight { from { transform: translateX(100%); opacity: 0; } to { transform: translateX(0); opacity: 1; } }
      `;
      document.head.appendChild(styles);
    }

    // Adicionar ao DOM
    document.body.appendChild(notification);

    // Remover automaticamente após 5 segundos
    setTimeout(() => {
      if (notification.parentElement) {
        notification.remove();
      }
    }, 5000);
  }

  // Função para carregar detalhes reais do chamado (API)
  function loadTicketDetails(ticketId) {
    isLoading = true;
    
    // Simular chamada de API
    console.log('Carregando detalhes do chamado:', ticketId);
    
    // Aqui seria feita a chamada real para a API
    // fetch(`/api/tickets/${ticketId}`)
    //   .then(response => response.json())
    //   .then(data => {
    //     currentTicket = data;
    //     updateTicketDisplay();
    //     isLoading = false;
    //   })
    //   .catch(error => {
    //     console.error('Erro ao carregar chamado:', error);
    //     showNotification('Erro ao carregar detalhes do chamado.', 'error');
    //     isLoading = false;
    //   });

    // Por enquanto, usar dados mock
    setTimeout(() => {
      loadMockTicketDetails();
      isLoading = false;
    }, 500);
  }

})();