(function(){
  'use strict';

  // ===== PROTEÇÃO DE ACESSO DESABILITADA PARA TESTES =====
  
  const $ = (s,r=document)=> r.querySelector(s);
  const $$ = (s,r=document)=> Array.from(r.querySelectorAll(s));
  const on = (el,ev,fn,opt)=> el && el.addEventListener(ev,fn,opt);

  // Mock de chamados do usuário (baseado na tela 9)
  const data = [
    { id:'1234', subject:'Problema no login do sistema', status:'progress', category:'Software', priority:'medium', date:'2025-05-19', rating:3,
      name:'João Silva', department:'TI', description:'Não consigo acessar o sistema com minhas credenciais. Mensagem: "Usuário inválido".',
      attachments:[ { name:'print-erro.png', url:'https://picsum.photos/seed/erro1/1200/800', mime:'image/png' } ] },
    
    { id:'1124', subject:'Lentidão no computador', status:'waiting', category:'Hardware', priority:'medium', date:'2025-05-20', rating:0, 
      name:'Maria Santos', department:'Comercial', description:'Computador muito lento ao iniciar e abrir aplicações como Excel.' },
    
    { id:'1224', subject:'Sem acesso à rede', status:'closed', category:'Rede', priority:'high', date:'2025-04-09', rating:4, 
      name:'Pedro Costa', department:'Financeiro', description:'Sem acesso à rede desde ontem às 16h. Não consigo acessar sistemas internos.' },
    
    { id:'1244', subject:'Rede Wi-Fi instável', status:'progress', category:'Rede', priority:'high', date:'2025-05-19', rating:0, 
      name:'Ana Oliveira', department:'RH', description:'Quedas frequentes de conexão Wi-Fi no 3º andar.' },
    
    { id:'1114', subject:'Erro ao exportar relatório', status:'open', category:'Software', priority:'low', date:'2025-05-18', rating:0, 
      name:'Carlos Lima', department:'Vendas', description:'Exportação de relatórios para CSV falha com erro 502.' },

    { id:'1456', subject:'Monitor piscando', status:'open', category:'Hardware', priority:'low', date:'2025-05-17', rating:0, 
      name:'Fernanda Reis', department:'Marketing', description:'Monitor do lado direito fica piscando a cada 10 minutos.' },

    { id:'1789', subject:'Impressora não funciona', status:'waiting', category:'Hardware', priority:'medium', date:'2025-05-16', rating:0, 
      name:'Ricardo Mendes', department:'Operações', description:'Impressora HP do 2º andar não está imprimindo. Led vermelho aceso.' },

    { id:'1890', subject:'Sistema ERP lento', status:'progress', category:'Software', priority:'high', date:'2025-05-15', rating:0, 
      name:'Juliana Silva', department:'Contabilidade', description:'Sistema ERP demora mais de 5 minutos para carregar relatórios.' },

    { id:'1945', subject:'Teclado com teclas travadas', status:'closed', category:'Hardware', priority:'low', date:'2025-05-14', rating:5, 
      name:'Roberto Alves', department:'Logística', description:'Várias teclas do teclado não funcionam (A, S, Enter).' },

    { id:'2001', subject:'Email não sincroniza', status:'open', category:'Software', priority:'medium', date:'2025-05-13', rating:0, 
      name:'Carla Ferreira', department:'Atendimento', description:'Outlook não está sincronizando emails há 2 dias.' },

    { id:'2156', subject:'Webcam sem imagem', status:'waiting', category:'Hardware', priority:'low', date:'2025-05-12', rating:0, 
      name:'Eduardo Santos', department:'TI', description:'Webcam integrada do notebook não mostra imagem em videoconferências.' },

    { id:'2234', subject:'Acesso negado ao servidor', status:'progress', category:'Rede', priority:'high', date:'2025-05-11', rating:0, 
      name:'Patrícia Lima', department:'Administração', description:'Não consigo acessar pasta compartilhada no servidor. Erro de permissão.' },

    { id:'2345', subject:'Backup automático falhou', status:'open', category:'Software', priority:'medium', date:'2025-05-10', rating:0, 
      name:'Marcos Oliveira', department:'TI', description:'Rotina de backup automático falhou nos últimos 3 dias consecutivos.' },

    { id:'2456', subject:'Mouse sem fio desconecta', status:'closed', category:'Hardware', priority:'low', date:'2025-05-09', rating:4, 
      name:'Sandra Costa', department:'Vendas', description:'Mouse sem fio desconecta a cada 15 minutos. Já tentei trocar pilhas.' },

    { id:'2567', subject:'VPN não conecta', status:'progress', category:'Rede', priority:'high', date:'2025-05-08', rating:0, 
      name:'André Silva', department:'Comercial', description:'VPN corporativa não consegue estabelecer conexão desde esta manhã.' }
  ];

  let filtered = [...data];
  let pageIndex = 1;
  let pageSize = 5;

  // Elements
  const chamadosLista = $('#chamadosLista');
  const searchInput = $('#searchInput');
  const pageSizeSel = $('#pageSize');
  const rangeInfo = $('#rangeInfo');
  const prevBtn = $('#prevBtn');
  const nextBtn = $('#nextBtn');
  const pageIndexEl = $('#pageIndex');
  const backBtn = $('#backBtn');
  const refreshBtn = $('#refreshBtn');

  // Utils
  const escapeHtml = s => String(s).replace(/[&<>"']/g, m=>({'&':'&amp;','<':'&lt;','>':'&gt;','"':'&quot;',"'":'&#39;'}[m]));
  const fmtDate = iso => {
    try{
      const d = new Date(iso);
      const dd = String(d.getDate()).padStart(2,'0');
      const mm = String(d.getMonth()+1).padStart(2,'0');
      const yy = String(d.getFullYear()).slice(-2);
      return `${dd}/${mm}/${yy}`;
    }catch(_){ return iso; }
  };
  const statusLabel = s => ({open:'Aberto', progress:'Em progresso', waiting:'Em espera', closed:'Fechado'})[String(s||'').toLowerCase()] || (s||'—');
  const prioLabel = p => ({low:'Baixa', medium:'Média', high:'Alta'})[String(p||'').toLowerCase()] || (p||'—');

  function getStatusClass(status) {
    switch(status) {
      case 'progress': return 'status-em-progresso';
      case 'waiting': return 'status-em-espera';
      case 'closed': return 'status-fechado';
      case 'open': return 'status-aberto';
      default: return 'status-em-espera';
    }
  }

  function ticketDetailUrl(id){
    return encodeURI(`../14. TELA DE MEU CHAMADO/detalhes-chamado.html?id=${id}`);
  }

  function render(){
    const total = filtered.length;
    const pages = Math.max(1, Math.ceil(total / pageSize));
    pageIndex = Math.min(Math.max(1, pageIndex), pages);
    const start = (pageIndex-1)*pageSize;
    const end = Math.min(start+pageSize, total);
    const slice = filtered.slice(start, end);

    chamadosLista.innerHTML = slice.map(item=>{
      const statusClass = getStatusClass(item.status);
      const statusText = statusLabel(item.status);
      const priorityText = prioLabel(item.priority);
      
      return `
        <div class="chamado-card">
          <div class="chamado-header">
            <div class="chamado-id">CD-${escapeHtml(item.id)}</div>
            <div class="chamado-status ${statusClass}">${statusText}</div>
          </div>
          <h3 class="chamado-titulo">${escapeHtml(item.subject)}</h3>
          <p class="chamado-descricao">Chamado de ${escapeHtml(item.category)} criado em ${fmtDate(item.date)}</p>
          <div class="chamado-meta">
            <div class="meta-item">
              <svg viewBox="0 0 24 24" width="14" height="14" fill="none" stroke="currentColor" stroke-width="2">
                <rect x="3" y="3" width="18" height="18" rx="2" ry="2"/>
                <circle cx="8.5" cy="8.5" r="1.5"/>
                <path d="M21 15l-5-5L5 21"/>
              </svg>
              ${escapeHtml(item.category)}
            </div>
            <div class="meta-item">
              <svg viewBox="0 0 24 24" width="14" height="14" fill="none" stroke="currentColor" stroke-width="2">
                <rect x="3" y="4" width="18" height="18" rx="2" ry="2"/>
                <line x1="16" y1="2" x2="16" y2="6"/>
                <line x1="8" y1="2" x2="8" y2="6"/>
                <line x1="3" y1="10" x2="21" y2="10"/>
              </svg>
              ${fmtDate(item.date)}
            </div>
            <div class="meta-item">
              <svg viewBox="0 0 24 24" width="14" height="14" fill="none" stroke="currentColor" stroke-width="2">
                <circle cx="12" cy="12" r="10"/>
                <path d="M12 6v6l4 2"/>
              </svg>
              ${priorityText}
            </div>
          </div>
          <div class="chamado-acoes">
            <button class="btn btn-aprovar" onclick="alert('Aprovar chamado CD-${escapeHtml(item.id)}')">
              ✓ APROVAR
            </button>
            <button class="btn btn-recusar" onclick="alert('Recusar chamado CD-${escapeHtml(item.id)}')">
              ✕ RECUSAR
            </button>
          </div>
        </div>
      `;
    }).join('');

    updatePagination(total, pages);
  }

  function getStatusClass(status) {
    const statusMap = {
      'open': 'status-aberto',
      'progress': 'status-em-progresso', 
      'waiting': 'status-aguardando',
      'closed': 'status-fechado'
    };
    return statusMap[status] || 'status-aberto';
  }

  function getPriorityClass(priority) {
    const priorityMap = {
      'high': 'alta',
      'medium': 'media',
      'low': 'baixa'
    };
    return priorityMap[priority] || priority;
  }

  function updatePagination(total, pages) {
    const start = (pageIndex-1)*pageSize;
    const end = Math.min(start+pageSize, total);
    
    rangeInfo.textContent = total
      ? `Mostrando ${start+1}-${end} de ${total} entradas`
      : 'Nenhuma entrada';

    if(pageIndexEl) pageIndexEl.textContent = String(pageIndex);
    prevBtn.disabled = pageIndex<=1;
    prevBtn.disabled = pageIndex<=1;
    nextBtn.disabled = pageIndex>=pages;
  }

  function applyFilters(){
    const q = (searchInput.value || '').trim().toLowerCase();
    const statusFilter = $('#statusFilter');
    const priorityFilter = $('#priorityFilter');
    const categoryFilter = $('#categoryFilter');
    
    const selectedStatus = statusFilter ? statusFilter.value : '';
    const selectedPriority = priorityFilter ? priorityFilter.value : '';
    const selectedCategory = categoryFilter ? categoryFilter.value : '';

    filtered = data.filter(item => {
      // Filtro de busca
      let matchesSearch = true;
      if (q) {
        matchesSearch = `cd-${item.id}`.toLowerCase().includes(q)
                    || String(item.id).toLowerCase().includes(q)
                    || String(item.subject).toLowerCase().includes(q)
                    || String(item.category).toLowerCase().includes(q)
                    || String(item.name).toLowerCase().includes(q)
                    || String(item.department).toLowerCase().includes(q);
      }

      // Filtro de status
      let matchesStatus = true;
      if (selectedStatus) {
        // Mapear status do filtro para os valores dos dados
        const statusMap = {
          'aberto': 'open',
          'andamento': 'progress',
          'aguardando': 'waiting',
          'fechado': 'closed'
        };
        matchesStatus = item.status === (statusMap[selectedStatus] || selectedStatus);
      }

      // Filtro de prioridade
      let matchesPriority = true;
      if (selectedPriority) {
        // Mapear prioridade do filtro para os valores dos dados
        const priorityMap = {
          'alta': 'high',
          'media': 'medium',
          'baixa': 'low'
        };
        matchesPriority = item.priority === (priorityMap[selectedPriority] || selectedPriority);
      }

      // Filtro de categoria
      let matchesCategory = true;
      if (selectedCategory) {
        // Mapear categoria do filtro para os valores dos dados
        const categoryMap = {
          'tecnico': 'Software',
          'administrativo': 'Administrativo',
          'financeiro': 'Financeiro',
          'comercial': 'Comercial'
        };
        matchesCategory = item.category.toLowerCase() === selectedCategory.toLowerCase() 
                       || item.category === (categoryMap[selectedCategory] || selectedCategory);
      }

      return matchesSearch && matchesStatus && matchesPriority && matchesCategory;
    });

    pageIndex = 1;
    render();
    
    // Mostrar/ocultar estado vazio
    const emptyState = $('#emptyState');
    const chamadosLista = $('#chamadosLista');
    
    if (filtered.length === 0) {
      if (emptyState) emptyState.hidden = false;
      if (chamadosLista) chamadosLista.style.display = 'none';
    } else {
      if (emptyState) emptyState.hidden = true;
      if (chamadosLista) chamadosLista.style.display = 'flex';
    }
  }

  // Eventos
  on(searchInput,'input', ()=>{
    clearTimeout(searchInput._t);
    searchInput._t = setTimeout(applyFilters, 160);
  });
  on(pageSizeSel,'change', ()=>{
    pageSize = parseInt(pageSizeSel.value,10) || 5;
    pageIndex = 1;
    render();
  });
  on(prevBtn,'click', ()=>{ pageIndex--; render(); });
  on(nextBtn,'click', ()=>{ pageIndex++; render(); });

  // Botões de ação
  on(backBtn,'click', ()=>{
    window.location.href = '../9. TELA DE MEU CHAMADO/meus-chamado.html';
  });
  on(refreshBtn,'click', ()=>{
    applyFilters(); // Recarrega a lista
  });

  // Menu do usuário
  function setupUserMenu(){
    const userMenuBtn = $('#userMenuBtn');
    const userDropdown = $('#userDropdown');
    const logoutBtn = $('#logoutBtn');

    const userSession = JSON.parse(localStorage.getItem('userSession') || '{}');
    const userName = userSession.name || 'Usuário';
    const userEmail = userSession.email || 'usuario@email.com';
    
    if($('#userName')) $('#userName').textContent = userName;
    if($('#userEmail')) $('#userEmail').textContent = userEmail;

    if(userMenuBtn){
      on(userMenuBtn, 'click', (e)=>{
        e.stopPropagation();
        const isHidden = userDropdown.hasAttribute('hidden');
        if(isHidden) userDropdown.removeAttribute('hidden');
        else userDropdown.setAttribute('hidden', '');
      });
    }

    on(document, 'click', (e)=>{
      if(!userDropdown.hasAttribute('hidden') && !userDropdown.contains(e.target)){
        userDropdown.setAttribute('hidden', '');
      }
    });

    if(logoutBtn){
      on(logoutBtn, 'click', ()=>{
        localStorage.removeItem('userSession');
        window.location.href = '../1. TELA DE LOGIN/login.html';
      });
    }
  }

  // Upload de foto
  function setupPhotoUpload(){
    const photoModal = $('#photoModal');
    const changePhotoBtn = $('#changePhotoBtn');
    const closePhotoModal = $$('#closePhotoModal');
    const selectPhotoBtn = $('#selectPhotoBtn');
    const photoInput = $('#photoInput');
    const uploadPhotoBtn = $('#uploadPhotoBtn');
    const previewImg = $('#previewImg');
    const photoPlaceholder = $('#photoPlaceholder');

    if(changePhotoBtn){
      on(changePhotoBtn, 'click', ()=>{
        photoModal.removeAttribute('hidden');
      });
    }

    closePhotoModal.forEach(btn => {
      on(btn, 'click', ()=>{
        photoModal.setAttribute('hidden', '');
      });
    });

    if(selectPhotoBtn){
      on(selectPhotoBtn, 'click', ()=>{
        photoInput.click();
      });
    }

    if(photoInput){
      on(photoInput, 'change', (e)=>{
        const file = e.target.files[0];
        if(file){
          const reader = new FileReader();
          reader.onload = (e)=>{
            previewImg.src = e.target.result;
            previewImg.style.display = 'block';
            photoPlaceholder.style.display = 'none';
            uploadPhotoBtn.disabled = false;
          };
          reader.readAsDataURL(file);
        }
      });
    }

    if(uploadPhotoBtn){
      on(uploadPhotoBtn, 'click', ()=>{
        // Aqui seria feito o upload real
        photoModal.setAttribute('hidden', '');
        // Reset
        previewImg.style.display = 'none';
        photoPlaceholder.style.display = 'flex';
        uploadPhotoBtn.disabled = true;
        photoInput.value = '';
      });
    }
  }

  // Inicialização
  function init(){
    setupUserMenu();
    setupPhotoUpload();
    setupFilters();
    setupAvatar();
    applyFilters(); // Renderiza a lista inicial
    updateStats(); // Atualiza estatísticas
  }

  // Configurar filtros
  function setupFilters() {
    const statusFilter = $('#statusFilter');
    const priorityFilter = $('#priorityFilter');
    const categoryFilter = $('#categoryFilter');
    const searchInput = $('#searchInput');
    const pageSize = $('#pageSize');

    // Event listeners para os filtros
    if (statusFilter) on(statusFilter, 'change', applyFilters);
    if (priorityFilter) on(priorityFilter, 'change', applyFilters);
    if (categoryFilter) on(categoryFilter, 'change', applyFilters);
    if (searchInput) on(searchInput, 'input', applyFilters);
    if (pageSize) on(pageSize, 'change', applyFilters);
  }

  // Configurar avatar dinâmico
  function setupAvatar() {
    const userSession = JSON.parse(localStorage.getItem('userSession') || '{}');
    const tecnicoName = userSession.nome || 'Técnico';
    const userPhotoUrl = userSession.foto || localStorage.getItem('userPhoto');

    setupUserAvatar(tecnicoName, userPhotoUrl);

    // Atualizar informações no dropdown
    const dropdownName = $('#userName');
    const dropdownEmail = $('#userEmail');
    if (dropdownName) dropdownName.textContent = tecnicoName;
    if (dropdownEmail) dropdownEmail.textContent = userSession.username || 'tecnico@empresa.com';
  }

  function setupUserAvatar(userName, photoUrl) {
    const userPhoto = $('#userPhoto');
    const userInitials = $('#userInitials');
    const userSvg = $('#userSvg');

    if (photoUrl) {
      // Tentar carregar a foto
      userPhoto.src = photoUrl;
      userPhoto.style.display = 'block';
      userInitials.style.display = 'none';
      userSvg.style.display = 'none';
      
      userPhoto.onload = function() {
        console.log('Foto de perfil carregada');
      };
      
      userPhoto.onerror = function() {
        this.style.display = 'none';
        showUserInitials(userName);
      };
    } else {
      showUserInitials(userName);
    }
  }

  function showUserInitials(userName) {
    const userPhoto = $('#userPhoto');
    const userInitials = $('#userInitials');
    const userSvg = $('#userSvg');
    
    userPhoto.style.display = 'none';
    userSvg.style.display = 'none';
    
    // Gerar iniciais
    const initials = userName
      .split(' ')
      .slice(0, 2)
      .map(name => name.charAt(0))
      .join('')
      .toUpperCase();
    
    userInitials.textContent = initials;
    userInitials.style.display = 'flex';
  }

  // Atualizar estatísticas
  function updateStats() {
    const total = data.length;
    const open = data.filter(item => item.status === 'open' || item.status === 'progress').length;
    const closed = data.filter(item => item.status === 'closed').length;

    const totalElement = $('#totalTickets');
    const openElement = $('#openTickets');
    const closedElement = $('#closedTickets');

    if (totalElement) totalElement.textContent = total;
    if (openElement) openElement.textContent = open;
    if (closedElement) closedElement.textContent = closed;
  }

  // Executar quando DOM estiver pronto
  if(document.readyState === 'loading'){
    document.addEventListener('DOMContentLoaded', init);
  } else {
    init();
  }

})();