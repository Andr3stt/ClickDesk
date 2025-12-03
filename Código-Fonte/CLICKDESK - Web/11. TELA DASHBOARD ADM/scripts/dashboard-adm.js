(function(){
  'use strict';

  // ===== PROTEÇÃO: Apenas técnicos/admin podem acessar =====
  // TEMPORARIAMENTE DESABILITADO PARA TESTES
  /*
  function verificarAcesso() {
    const userSession = localStorage.getItem('userSession');
    if (!userSession) {
      // Não logado - redireciona para login
      window.location.href = '../1. TELA DE LOGIN/login.html';
      return false;
    }
    
    try {
      const user = JSON.parse(userSession);
      if (user.tipo !== 'tecnico') {
        // Usuário comum tentando acessar área de técnico - redireciona para dashboard normal
        window.location.href = '../6. TELA DE DASHBOARD/dashboard.html';
        return false;
      }
    } catch(e) {
      // Sessão inválida
      localStorage.removeItem('userSession');
      window.location.href = '../1. TELA DE LOGIN/login.html';
      return false;
    }
    return true;
  }

  if (!verificarAcesso()) return;
  */
  // ===== FIM DA PROTEÇÃO =====

  const $ = (s,r=document)=> r.querySelector(s);
  const $$ = (s,r=document)=> Array.from(r.querySelectorAll(s));
  const on = (el,ev,fn,opt)=> el && el.addEventListener(ev,fn,opt);

  const fmtDateTime = (iso)=>{
    try{
      const d=new Date(iso);
      const dd=String(d.getDate()).padStart(2,'0');
      const mm=String(d.getMonth()+1).padStart(2,'0');
      const yy=String(d.getFullYear()).slice(-2);
      const hh=String(d.getHours()).padStart(2,'0');
      const mi=String(d.getMinutes()).padStart(2,'0');
      return `${dd}/${mm}/${yy} ${hh}:${mi}`;
    }catch(_){ return iso; }
  };
  const shortDate = (iso)=>{
    try{
      const d=new Date(iso);
      const dd=String(d.getDate()).padStart(2,'0');
      const mm=String(d.getMonth()+1).padStart(2,'0');
      const yy=String(d.getFullYear()).slice(-2);
      return `${dd}/${mm}/${yy}`;
    }catch(_){ return iso; }
  };
  const escapeHtml = s => String(s).replace(/[&<>"']/g, m=>({'&':'&amp;','<':'&lt;','>':'&gt;','"':'&quot;',"'":'&#39;'}[m]));

  // Mocks (substitua por suas APIs)
  async function getAdminStats(range){
    try{
      const resp = await fetch(`/api/admin/stats?range=${encodeURIComponent(range)}`, { headers:{Accept:'application/json'} });
      if(!resp.ok) throw new Error();
      return await resp.json();
    }catch(_){
      return {
        total: 28, attended: 14, waiting: 6, inprogress: 8,
        deltas: { total: -5, attended: +2, waiting: +1, inprogress: -1 },
        pendingCount: 3,
        updatedAt: new Date().toISOString()
      };
    }
  }
  async function getPending(range){
    try{
      const resp = await fetch(`/api/admin/pending?range=${encodeURIComponent(range)}`, { headers:{Accept:'application/json'} });
      if(!resp.ok) throw new Error();
      return await resp.json();
    }catch(_){
      return [
        { id:'1234', status:'Em andamento', category:'Software', time:'2 horas', subject:'Problema no login do sistema', description:'Não consigo fazer login no sistema desde ontem. Aparece erro de autenticação.' },
        { id:'1124', status:'Aberto', category:'Hardware', time:'2 horas', subject:'Lentidão no computador', description:'O computador está muito lento, principalmente ao abrir programas.' },
        { id:'1274', status:'Aguardando', category:'Acesso', time:'2 horas', subject:'Sem acesso ao e-mail', description:'Não estou conseguindo acessar meu e-mail corporativo.' },
        { id:'1324', status:'Fechado', category:'Hardware', time:'2 horas', subject:'Impressora não imprime', description:'A impressora não está imprimindo os documentos.' },
      ];
    }
  }
  async function getActivities(range){
    try{
      const resp = await fetch(`/api/admin/activities?range=${encodeURIComponent(range)}`, { headers:{Accept:'application/json'} });
      if(!resp.ok) throw new Error();
      return await resp.json();
    }catch(_){
      return [
        { id:'CD-2299', text:'Ticket aprovado por você', when:'2025-10-19T12:10:00Z' },
        { id:'CD-2292', text:'Status alterado para Em progresso por Julia', when:'2025-10-19T09:50:00Z' },
        { id:'CD-2287', text:'Novo ticket criado por Daniel', when:'2025-10-18T17:30:00Z' },
      ];
    }
  }

  function setDelta(el, v){
    if(!el) return;
    if(v>0){ el.className='delta up'; el.textContent=`▲ ${v}%`; }
    else if(v<0){ el.className='delta down'; el.textContent=`▼ ${Math.abs(v)}%`; }
    else { el.className='delta neutral'; el.textContent='—'; }
  }

  function renderStats(s){
    $('#kpi-total').textContent = s.total;
    $('#kpi-attended').textContent = s.attended;
    $('#kpi-waiting').textContent = s.waiting;
    $('#kpi-inprogress').textContent = s.inprogress;
    setDelta($('#delta-total'), s.deltas?.total ?? 0);
    setDelta($('#delta-attended'), s.deltas?.attended ?? 0);
    setDelta($('#delta-waiting'), s.deltas?.waiting ?? 0);
    setDelta($('#delta-inprogress'), s.deltas?.inprogress ?? 0);

    $('#lastUpdatedToolbar').textContent = `Atualizado às ${fmtDateTime(s.updatedAt || new Date()).split(' ')[1]}`;
  }

  function renderPending(list){
    const grid = $('#pendingCardsGrid');
    const empty = $('#pendingEmpty');
    
    if (!list || list.length === 0) {
      if (empty) empty.removeAttribute('hidden');
      if (grid) grid.innerHTML = '';
      return;
    }
    
    if (empty) empty.setAttribute('hidden', '');
    
    grid.innerHTML = list.map(it => {
      const statusMap = {
        'Em andamento': { class: 'status-in-progress', label: 'EM ANDAMENTO' },
        'Aberto': { class: 'status-open', label: 'ABERTO' },
        'Aguardando': { class: 'status-waiting', label: 'AGUARDANDO' },
        'Fechado': { class: 'status-closed', label: 'FECHADO' },
        'Pendente': { class: 'status-pending', label: 'PENDENTE' }
      };
      
      const status = statusMap[it.status] || { class: 'status-pending', label: 'PENDENTE' };
      
      return `
      <div class="ticket-card" data-id="${escapeHtml(it.id)}">
        <div class="ticket-header">
          <div class="ticket-id">#${escapeHtml(it.id)}</div>
          <div class="ticket-status ${status.class}">${status.label}</div>
        </div>
        <div class="ticket-title">${escapeHtml(it.subject)}</div>
        <div class="ticket-description">${escapeHtml(it.description || '')}</div>
        <div class="ticket-footer">
          <div style="display: flex; align-items: center; gap: 12px;">
            <div class="ticket-category">${escapeHtml(it.category)}</div>
            <div class="ticket-meta">
              <svg viewBox="0 0 24 24" width="14" height="14" fill="none" stroke="currentColor" stroke-width="2">
                <circle cx="12" cy="12" r="10"/>
                <polyline points="12 6 12 12 16 14"/>
              </svg>
              Há ${escapeHtml(it.time || '2 horas')}
            </div>
          </div>
          <button class="ticket-btn" onclick="window.location.href='../14. TELA DE DETALHES DO CHAMADO ADM/detalhes-chamado.html?id=${escapeHtml(it.id)}'">
            VER DETALHES
            <svg viewBox="0 0 24 24" width="16" height="16" fill="none" stroke="currentColor" stroke-width="2.5">
              <path d="M5 12h14M12 5l7 7-7 7"/>
            </svg>
          </button>
        </div>
      </div>
      `;
    }).join('');
  }

  async function loadAll(range){
    const [stats, pending] = await Promise.all([
      getAdminStats(range), getPending(range)
    ]);
    renderStats(stats);
    renderPending(pending);
  }

  function bind(){
    on($('#rangeSelect'),'change', async e=>{ await loadAll(e.target.value||'7d'); });
    on($('#categoryTimeFilter'),'change', async e=>{ 
      // Aqui você pode implementar filtro específico para o gráfico de categorias
      await loadAll($('#rangeSelect').value||'7d'); 
    });
    on($('#statusTimeFilter'),'change', async e=>{ 
      // Aqui você pode implementar filtro específico para o gráfico de status
      await loadAll($('#rangeSelect').value||'7d'); 
    });
    
    // Bind para os cards de aprovação pendente
    on($('#pendingCardsGrid'),'click', async e=>{
      const btn = e.target.closest('button[data-act]');
      if(!btn) return;
      const id = btn.getAttribute('data-id');
      const act = btn.getAttribute('data-act'); // approve|reject
      // Integração real:
      // await fetch(`/api/admin/pending/${encodeURIComponent(id)}/${act}`, { method:'POST' });
      btn.closest('.recent-card')?.remove();
      
      // Se não há mais cards, mostrar mensagem vazia
      const remainingCards = $('#pendingCardsGrid').children.length;
      if (remainingCards === 0) {
        $('#pendingEmpty').removeAttribute('hidden');
      }
    });
  }

  function setupUserMenu() {
    const userAvatar = document.querySelector('.user-avatar');
    const userDropdown = document.querySelector('.user-dropdown');
    const dropdownName = document.querySelector('.user-dropdown-name');
    const dropdownEmail = document.querySelector('.user-dropdown-email');

    if (!userAvatar || !userDropdown) return;

    const userSession = JSON.parse(localStorage.getItem('userSession') || '{}');
    const userName = userSession.nome || 'Técnico';
    const userEmail = userSession.username || 'tecnico@clickdesk.com';

    // Atualizar informações do dropdown
    if (dropdownName) dropdownName.textContent = userName;
    if (dropdownEmail) dropdownEmail.textContent = userEmail;

    // Configurar avatar com iniciais
    const avatarUrl = `https://ui-avatars.com/api/?name=${encodeURIComponent(userName)}&background=F28A1A&color=fff&bold=true&size=88`;
    
    const avatarImg = userAvatar.querySelector('img');
    if (avatarImg) {
      avatarImg.src = avatarUrl;
      avatarImg.alt = userName;
    }

    // Toggle dropdown
    userAvatar.addEventListener('click', (e) => {
      e.stopPropagation();
      const isHidden = userDropdown.hasAttribute('hidden');
      if (isHidden) {
        userDropdown.removeAttribute('hidden');
      } else {
        userDropdown.setAttribute('hidden', '');
      }
    });

    // Fechar dropdown ao clicar fora
    document.addEventListener('click', (e) => {
      if (!userDropdown.hasAttribute('hidden') && !userDropdown.contains(e.target)) {
        userDropdown.setAttribute('hidden', '');
      }
    });

    // Ação de sair
    const logoutBtn = userDropdown.querySelector('.user-dropdown-item[onclick*="sair"]');
    if (logoutBtn) {
      logoutBtn.addEventListener('click', (e) => {
        e.preventDefault();
        localStorage.removeItem('userSession');
        window.location.href = '../1.%20TELA%20DE%20LOGIN/login.html';
      });
    }
  }

  function setupClickableCards() {
    // Tornar cards de aprovações pendentes clicáveis
    on($('#pendingCardsGrid'), 'click', (e) => {
      const card = e.target.closest('.recent-card');
      const btn = e.target.closest('button[data-act]');
      
      // Se clicou em um botão de ação, não redireciona
      if (btn) return;
      
      // Se clicou no card, redireciona para aprovações
      if (card) {
        window.location.href = '../12.%20TELA%20DE%20APROVA%C3%87%C3%83O%20DE%20CHAMADOS%20ADM/aprovacao-chamados.html';
      }
    });
    
    // Adicionar cursor pointer nos cards
    const style = document.createElement('style');
    style.textContent = `
      .recent-card { cursor: pointer; }
      .recent-card-actions { pointer-events: none; }
      .recent-card-actions .btn { pointer-events: auto; }
    `;
    document.head.appendChild(style);
  }

  function setupStickyTitle() {
    const pageTitle = document.querySelector('.page-title');
    
    if (!pageTitle) return;
    
    window.addEventListener('scroll', () => {
      if (window.scrollY > 10) {
        pageTitle.classList.add('scrolled');
      } else {
        pageTitle.classList.remove('scrolled');
      }
    });
  }

  // Botão de atualizar
  function setupRefreshButton() {
    const refreshBtn = $('#refreshBtn');
    if (!refreshBtn) return;
    
    on(refreshBtn, 'click', async () => {
      refreshBtn.classList.add('is-spinning');
      try {
        await loadAll($('#rangeSelect').value || '7d');
      } finally {
        setTimeout(() => {
          refreshBtn.classList.remove('is-spinning');
        }, 600);
      }
    });
  }

  document.addEventListener('DOMContentLoaded', async ()=>{
    await loadAll($('#rangeSelect').value||'7d');
    bind();
    setupUserMenu();
    setupStickyTitle();
    setupClickableCards();
    setupRefreshButton();
  });
})();