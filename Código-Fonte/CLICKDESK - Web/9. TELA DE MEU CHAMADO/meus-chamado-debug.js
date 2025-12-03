// Meus Chamados - Script principal

const chamados = [
  { 
    id: '1234', 
    number: '#1234', 
    title: 'Problema no login do sistema', 
    description: 'Não consigo fazer login no sistema desde ontem. Aparece erro de autenticação.',
    status: 'progress', 
    category: 'Software', 
    priority: 'high',
    date: '15/01/2024'
  },
  { 
    id: '1124', 
    number: '#1124', 
    title: 'Lentidão no computador', 
    description: 'O computador está muito lento, principalmente ao abrir programas.',
    status: 'open', 
    category: 'Hardware', 
    priority: 'medium',
    date: '14/01/2024'
  },
  { 
    id: '1224', 
    number: '#1224', 
    title: 'Sem acesso ao e-mail', 
    description: 'Não estou conseguindo acessar meu e-mail corporativo.',
    status: 'waiting', 
    category: 'Acesso', 
    priority: 'urgent',
    date: '13/01/2024'
  },
  { 
    id: '1324', 
    number: '#1324', 
    title: 'Impressora não imprime', 
    description: 'A impressora não está imprimindo os documentos.',
    status: 'closed', 
    category: 'Hardware', 
    priority: 'low',
    date: '12/01/2024'
  }
];

// Função para obter a classe de status
function getStatusClass(status) {
  const statusMap = {
    'open': 'open',
    'progress': 'progress',
    'waiting': 'waiting',
    'closed': 'closed'
  };
  return statusMap[status] || 'open';
}

// Função para obter o texto do status
function getStatusText(status) {
  const statusText = {
    'open': 'Aberto',
    'progress': 'Em Andamento',
    'waiting': 'Aguardando',
    'closed': 'Fechado'
  };
  return statusText[status] || 'Aberto';
}

// Função para renderizar os cards
function renderTickets(tickets) {
  const grid = document.getElementById('ticketsGrid');
  const emptyState = document.getElementById('emptyState');
  
  if (!grid) return;
  
  if (tickets.length === 0) {
    grid.innerHTML = '';
    if (emptyState) emptyState.hidden = false;
    return;
  }
  
  if (emptyState) emptyState.hidden = true;
  
  const cardsHtml = tickets.map(ticket => `
    <article class="ticket-card">
      <div class="ticket-header">
        <span class="ticket-id">${ticket.number}</span>
        <span class="ticket-status ${getStatusClass(ticket.status)}">${getStatusText(ticket.status)}</span>
      </div>
      <h3 class="ticket-title">${ticket.title}</h3>
      <p class="ticket-description">${ticket.description}</p>
      <div class="ticket-footer">
        <div class="ticket-meta">
          <span class="ticket-category">${ticket.category}</span>
          <span class="ticket-time">Há 2 horas</span>
        </div>
        <button class="ticket-btn" onclick="window.location.href=encodeURI('../14.1. TELA DETALHES CHAMADO/meu-chamado.html?id=${ticket.id}')">
          VER DETALHES
          <svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
            <path d="M5 12h14M12 5l7 7-7 7"/>
          </svg>
        </button>
      </div>
    </article>
  `).join('');
  
  grid.innerHTML = cardsHtml;
}

// Atualizar contadores
function updateCounters() {
  const total = chamados.length;
  const open = chamados.filter(t => t.status === 'open').length;
  const progress = chamados.filter(t => t.status === 'progress').length;
  const resolved = chamados.filter(t => t.status === 'closed').length;
  
  document.getElementById('totalCount').textContent = total;
  document.getElementById('openCount').textContent = open;
  document.getElementById('progressCount').textContent = progress;
  document.getElementById('resolvedCount').textContent = resolved;
}

// Inicialização
document.addEventListener('DOMContentLoaded', function() {
  console.log('Meus Chamados carregado');
  
  // Renderizar tickets
  renderTickets(chamados);
  
  // Atualizar contadores
  updateCounters();
  
  // Event listener para o botão de refresh
  const refreshBtn = document.getElementById('refreshBtn');
  if (refreshBtn) {
    refreshBtn.addEventListener('click', function() {
      console.log('Atualizando...');
      renderTickets(chamados);
      updateCounters();
    });
  }
  
  // Filtros
  const statusFilter = document.getElementById('statusFilter');
  const categoryFilter = document.getElementById('categoryFilter');
  
  function applyFilters() {
    let filtered = [...chamados];
    
    const statusValue = statusFilter.value;
    const categoryValue = categoryFilter.value;
    
    if (statusValue) {
      filtered = filtered.filter(t => t.status === statusValue);
    }
    
    if (categoryValue) {
      filtered = filtered.filter(t => t.category.toLowerCase() === categoryValue.toLowerCase());
    }
    
    renderTickets(filtered);
  }
  
  if (statusFilter) {
    statusFilter.addEventListener('change', applyFilters);
  }
  
  if (categoryFilter) {
    categoryFilter.addEventListener('change', applyFilters);
  }
  
  // Menu do usuário
  const userMenuBtn = document.getElementById('userMenuBtn');
  const userDropdown = document.getElementById('userDropdown');
  
  if (userMenuBtn && userDropdown) {
    userMenuBtn.addEventListener('click', function(e) {
      e.stopPropagation();
      userDropdown.hidden = !userDropdown.hidden;
    });
    
    document.addEventListener('click', function() {
      userDropdown.hidden = true;
    });
  }
  
  // Botão de logout
  const logoutBtn = document.getElementById('logoutBtn');
  if (logoutBtn) {
    logoutBtn.addEventListener('click', function() {
      window.location.href = '../1. TELA DE LOGIN/login.html';
    });
  }
});