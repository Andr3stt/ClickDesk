// Mock de dados de chamados
const mockTickets = [
  {
    id: '0156',
    title: 'Sistema lento durante acesso ao módulo de vendas',
    status: 'aberto',
    category: 'Sistema',
    priority: 'média',
    date: '15/01/2025',
    time: '14:30',
    tech: 'Carlos Silva'
  },
  {
    id: '0155',
    title: 'Erro ao gerar relatório mensal de vendas',
    status: 'em-progresso',
    category: 'Relatórios',
    priority: 'alta',
    date: '15/01/2025',
    time: '11:20',
    tech: 'Ana Santos'
  },
  {
    id: '0154',
    title: 'Problema de conexão com impressora de etiquetas',
    status: 'resolvido',
    category: 'Hardware',
    priority: 'baixa',
    date: '14/01/2025',
    time: '16:45',
    tech: 'João Pereira'
  },
  {
    id: '0153',
    title: 'Usuário não consegue fazer login no sistema',
    status: 'aberto',
    category: 'Acesso',
    priority: 'alta',
    date: '14/01/2025',
    time: '09:15',
    tech: 'Maria Costa'
  },
  {
    id: '0152',
    title: 'Solicitação de nova licença de software',
    status: 'em-progresso',
    category: 'Software',
    priority: 'média',
    date: '13/01/2025',
    time: '13:00',
    tech: 'Carlos Silva'
  },
  {
    id: '0151',
    title: 'Computador não liga após atualização',
    status: 'resolvido',
    category: 'Hardware',
    priority: 'alta',
    date: '13/01/2025',
    time: '10:30',
    tech: 'Ana Santos'
  },
  {
    id: '0150',
    title: 'E-mail não está sincronizando corretamente',
    status: 'aberto',
    category: 'Sistema',
    priority: 'baixa',
    date: '12/01/2025',
    time: '15:20',
    tech: 'João Pereira'
  },
  {
    id: '0149',
    title: 'Solicitar instalação de novo software de design',
    status: 'em-progresso',
    category: 'Software',
    priority: 'média',
    date: '12/01/2025',
    time: '08:45',
    tech: 'Maria Costa'
  }
];


// Elementos DOM
const statusSelect = document.getElementById('statusSelect');
const categorySelect = document.getElementById('categorySelect');
const refreshBtn = document.getElementById('refreshBtn');
const ticketsGrid = document.getElementById('ticketsGrid');

// KPI Elements
const totalTicketsEl = document.getElementById('kpi-total');
const openTicketsEl = document.getElementById('kpi-open');
const progressTicketsEl = document.getElementById('kpi-progress');
const resolvedTicketsEl = document.getElementById('kpi-resolved');

// Função para atualizar KPIs
function updateKPIs(tickets) {
  const total = tickets.length;
  const open = tickets.filter(t => t.status === 'aberto').length;
  const inProgress = tickets.filter(t => t.status === 'em-progresso').length;
  const resolved = tickets.filter(t => t.status === 'resolvido').length;

  if (totalTicketsEl) totalTicketsEl.textContent = total;
  if (openTicketsEl) openTicketsEl.textContent = open;
  if (progressTicketsEl) progressTicketsEl.textContent = inProgress;
  if (resolvedTicketsEl) resolvedTicketsEl.textContent = resolved;
}

// Função para obter badge de status
function getStatusBadge(status) {
  const badges = {
    'aberto': { class: 'badge badge-aberto', text: 'Aberto' },
    'em-progresso': { class: 'badge badge-em-progresso', text: 'Em Andamento' },
    'resolvido': { class: 'badge badge-resolvido', text: 'Resolvido' }
  };
  const badge = badges[status] || badges['aberto'];
  return `<span class="${badge.class}">${badge.text}</span>`;
}

// Função para renderizar chamados
function renderTickets(tickets) {
  if (!ticketsGrid) return;
  
  if (tickets.length === 0) {
    ticketsGrid.innerHTML = '<div class="no-tickets">Nenhum chamado encontrado</div>';
    return;
  }

  ticketsGrid.innerHTML = tickets.map(ticket => `
    <div class="ticket-card">
      <div class="ticket-header">
        <h3 class="ticket-id">#${ticket.id}</h3>
        <div class="ticket-badges">
          ${getStatusBadge(ticket.status)}
        </div>
      </div>
      <p class="ticket-title">${ticket.title}</p>
      <div class="ticket-meta">
        <span class="ticket-meta-item">
          <span class="ticket-category-badge">${ticket.category}</span>
        </span>
        <span class="ticket-meta-item">
          <svg width="14" height="14" fill="currentColor" viewBox="0 0 16 16">
            <path d="M8 3.5a.5.5 0 0 0-1 0V9a.5.5 0 0 0 .252.434l3.5 2a.5.5 0 0 0 .496-.868L8 8.71V3.5z"/>
            <path d="M8 16A8 8 0 1 0 8 0a8 8 0 0 0 0 16zm7-8A7 7 0 1 1 1 8a7 7 0 0 1 14 0z"/>
          </svg>
          Há ${ticket.time}
        </span>
      </div>
      <div class="ticket-footer">
        <div class="ticket-tech">
          <svg width="14" height="14" fill="currentColor" viewBox="0 0 16 16">
            <path d="M3 14s-1 0-1-1 1-4 6-4 6 3 6 4-1 1-1 1H3zm5-6a3 3 0 1 0 0-6 3 3 0 0 0 0 6z"/>
          </svg>
          ${ticket.tech}
        </div>
        <button class="ticket-details-btn" onclick="openTicket('${ticket.id}'); event.stopPropagation();">
          VER DETALHES
          <svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
            <path d="M5 12h14M12 5l7 7-7 7"/>
          </svg>
        </button>
      </div>
    </div>
  `).join('');
}

// Função para filtrar chamados
function filterTickets() {
  if (!statusSelect || !categorySelect) return;
  
  const statusFilter = statusSelect.value;
  const categoryFilter = categorySelect.value;

  let filtered = mockTickets;

  if (statusFilter && statusFilter !== '') {
    // Mapear valores do select para status dos tickets
    const statusMap = {
      'open': 'aberto',
      'progress': 'em-progresso',
      'waiting': 'aguardando',
      'closed': 'resolvido'
    };
    const mappedStatus = statusMap[statusFilter] || statusFilter;
    filtered = filtered.filter(t => t.status === mappedStatus);
  }

  if (categoryFilter && categoryFilter !== '') {
    filtered = filtered.filter(t => t.category === categoryFilter);
  }

  updateKPIs(mockTickets); // KPIs sempre mostram todos
  renderTickets(filtered);
}

// Função para abrir detalhes do chamado
function openTicket(ticketId) {
  window.location.href = encodeURI(`../7.5 DETALHES DO CHAMADO/detalhes-chamado.html?id=${ticketId}`);
}

// Função de refresh
function refreshData() {
  if (!refreshBtn) return;
  
  refreshBtn.classList.add('is-spinning');
  
  setTimeout(() => {
    filterTickets();
    refreshBtn.classList.remove('is-spinning');
  }, 900);
}

// Setup do menu de usuário
function setupUserMenu() {
  const userAvatar = document.querySelector('.user-avatar');
  const userDropdown = document.querySelector('.user-dropdown');
  const dropdownName = document.querySelector('.user-dropdown-name');
  const dropdownEmail = document.querySelector('.user-dropdown-email');

  if (!userAvatar || !userDropdown) return;

  const userSession = JSON.parse(localStorage.getItem('userSession') || '{}');
  const userName = userSession.nome || 'Usuário';
  const userEmail = userSession.username || 'usuario@email.com';

  if (dropdownName) dropdownName.textContent = userName;
  if (dropdownEmail) dropdownEmail.textContent = userEmail;

  const avatarUrl = `https://ui-avatars.com/api/?name=${encodeURIComponent(userName)}&background=F28A1A&color=fff&bold=true&size=88`;
  
  const avatarImg = userAvatar.querySelector('img');
  if (avatarImg) {
    avatarImg.src = avatarUrl;
    avatarImg.alt = userName;
  }

  userAvatar.addEventListener('click', (e) => {
    e.stopPropagation();
    const isHidden = userDropdown.hasAttribute('hidden');
    if (isHidden) {
      userDropdown.removeAttribute('hidden');
    } else {
      userDropdown.setAttribute('hidden', '');
    }
  });

  document.addEventListener('click', (e) => {
    if (!userDropdown.hasAttribute('hidden') && !userDropdown.contains(e.target)) {
      userDropdown.setAttribute('hidden', '');
    }
  });

  const logoutBtn = userDropdown.querySelector('.user-dropdown-item[onclick*="sair"]');
  if (logoutBtn) {
    logoutBtn.addEventListener('click', (e) => {
      e.preventDefault();
      localStorage.removeItem('userSession');
      window.location.href = '../1.%20TELA%20DE%20LOGIN/login.html';
    });
  }
}

// Event Listeners
if (statusSelect) statusSelect.addEventListener('change', filterTickets);
if (categorySelect) categorySelect.addEventListener('change', filterTickets);
if (refreshBtn) refreshBtn.addEventListener('click', refreshData);

// Inicialização
document.addEventListener('DOMContentLoaded', () => {
  updateKPIs(mockTickets);
  renderTickets(mockTickets);
  setupUserMenu();
});