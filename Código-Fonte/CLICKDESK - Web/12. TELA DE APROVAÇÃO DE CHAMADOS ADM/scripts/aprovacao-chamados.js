(function(){
  'use strict';

  // ===== PROTEÇÃO: Apenas técnicos/admin podem acessar =====
  // TEMPORARIAMENTE DESABILITADO PARA TESTES
  /*
  function verificarAcesso() {
    const userSession = localStorage.getItem('userSession');
    if (!userSession) {
      window.location.href = '../1. TELA DE LOGIN/login.html';
      return false;
    }
    try {
      const user = JSON.parse(userSession);
      if (user.tipo !== 'tecnico') {
        window.location.href = '../6. TELA DE DASHBOARD/dashboard.html';
        return false;
      }
    } catch(e) {
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

  // Mock de agentes (atribuição). Troque por GET /api/agents
  const agents = ['—', 'Sarah', 'Rafael', 'Aline', 'Julia'];

  // Mock de chamados pendentes (trocar por GET /api/admin/pending?query=&page=&pageSize=)
  const data = [
    { id:'1124', subject:'Lentidão no sistema', category:'Hardware', priority:'medium', date:'2025-05-24', assigned:'' },
    { id:'1332', subject:'Sem acesso ao sistema ERP', category:'Software', priority:'high', date:'2025-05-23', assigned:'Rafael' },
    { id:'1290', subject:'Erro ao enviar e-mail corporativo', category:'Software', priority:'low', date:'2025-05-21', assigned:'' },
    { id:'1277', subject:'VPN desconectando frequentemente', category:'Rede', priority:'medium', date:'2025-05-20', assigned:'Sarah' },
    { id:'1251', subject:'Troca de mouse com defeito', category:'Hardware', priority:'low', date:'2025-05-19', assigned:'' },
    { id:'1425', subject:'Impressora não conecta à rede', category:'Hardware', priority:'medium', date:'2025-05-18', assigned:'Aline' },
    { id:'1389', subject:'Sistema de backup falhando', category:'Software', priority:'high', date:'2025-05-17', assigned:'' },
    { id:'1456', subject:'Lentidão na internet', category:'Rede', priority:'medium', date:'2025-05-16', assigned:'Julia' },
    { id:'1478', subject:'Computador não liga', category:'Hardware', priority:'high', date:'2025-05-15', assigned:'' },
    { id:'1501', subject:'Erro no login do sistema', category:'Software', priority:'medium', date:'2025-05-14', assigned:'Rafael' },
    { id:'1523', subject:'Problema com projetor', category:'Hardware', priority:'low', date:'2025-05-13', assigned:'' },
    { id:'1567', subject:'Sistema muito lento pela manhã', category:'Software', priority:'medium', date:'2025-05-12', assigned:'Sarah' },
    { id:'1589', subject:'WiFi instável no 3º andar', category:'Rede', priority:'medium', date:'2025-05-11', assigned:'' },
    { id:'1612', subject:'Monitor piscando', category:'Hardware', priority:'low', date:'2025-05-10', assigned:'Aline' },
    { id:'1634', subject:'Falha crítica no servidor', category:'Software', priority:'high', date:'2025-05-09', assigned:'' }
  ];

  let filtered = [...data];
  let pageIndex = 1;
  let pageSize = 5;

  // Elements
  const chamadosGrid = $('#chamadosGrid');
  const searchInput = $('#searchInput');
  const pageSizeSel = $('#pageSize');
  const rangeInfo = $('#rangeInfo');
  const prevBtn = $('#prevBtn');
  const nextBtn = $('#nextBtn');
  const pageIndexEl = $('#pageIndex');

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
  const prioLabel = p => ({low:'Baixa', medium:'Média', high:'Alta'})[String(p||'').toLowerCase()] || (p||'—');

  function ticketDetailUrl(id){
    // Ajuste conforme a localização da sua tela de detalhes
    // Ex 1: na raiz: ../detalhes-chamado.html?id=ID
    // Ex 2: na pasta "7.5 DETALHES DO CHAMADO": ../7.5 DETALHES DO CHAMADO/detalhes-chamado.html?id=ID
    return `../detalhes-chamado.html?id=${encodeURIComponent(id)}`;
  }

  function renderAssignSelect(current, id){
    const opts = agents.map(a=>{
      const sel = a === (current || '—') ? 'selected' : '';
      const safe = escapeHtml(a);
      return `<option value="${safe}" ${sel}>${safe}</option>`;
    }).join('');
    return `<select class="assign-select" data-id="${escapeHtml(id)}">${opts}</select>`;
  }

  function render(){
    const total = filtered.length;
    const pages = Math.max(1, Math.ceil(total / pageSize));
    pageIndex = Math.min(Math.max(1, pageIndex), pages);
    const start = (pageIndex-1)*pageSize;
    const end = Math.min(start+pageSize, total);
    const slice = filtered.slice(start, end);

    chamadosGrid.innerHTML = slice.map(item=>{
      const statusClass = getStatusClass(item.priority);
      const statusText = getStatusText(item.priority);
      
      return `
        <div class="chamado-card" onclick="window.location.href='${ticketDetailUrl(item.id)}'">
          <div class="chamado-header">
            <div class="chamado-id">CD-${escapeHtml(item.id)}</div>
            <div class="chamado-status ${statusClass}">${statusText}</div>
          </div>
          
          <h3 class="chamado-titulo">${escapeHtml(item.subject)}</h3>
          
          <div class="chamado-descricao">
            Chamado de ${escapeHtml(item.category)} criado em ${fmtDate(item.date)}
          </div>
          
          <div class="chamado-meta">
            <div class="meta-item meta-categoria">
              📋 ${escapeHtml(item.category)}
            </div>
            <div class="meta-item">
              📅 ${fmtDate(item.date)}
            </div>
            <div class="meta-item">
              ⚡ ${prioLabel(item.priority)}
            </div>
            ${item.assigned ? `<div class="meta-item">👤 ${escapeHtml(item.assigned)}</div>` : ''}
          </div>
          
          <div class="chamado-acoes">
            <button class="btn btn-ver-detalhes" onclick="event.stopPropagation(); approveTicket('${escapeHtml(item.id)}')"
                    title="Aprovar chamado">
              ✓ Aprovar
            </button>
            <button class="btn btn-recusar" onclick="event.stopPropagation(); rejectTicket('${escapeHtml(item.id)}')"
                    title="Recusar chamado"
                    style="background: #dc3545; margin-left: 8px;">
              ✗ Recusar
            </button>
          </div>
        </div>
      `;
    }).join('');

    rangeInfo.textContent = total
      ? `Mostrando ${start+1}–${end} de ${total} entradas`
      : 'Nenhuma entrada';

    if(pageIndexEl) pageIndexEl.textContent = String(pageIndex);
    prevBtn.disabled = pageIndex<=1;
    nextBtn.disabled = pageIndex>=pages;
  }

  function getStatusClass(priority) {
    switch(priority) {
      case 'high': return 'status-em-progresso';
      case 'medium': return 'status-em-espera';
      case 'low': return 'status-aberto';
      default: return 'status-em-espera';
    }
  }

  function getStatusText(priority) {
    switch(priority) {
      case 'high': return 'URGENTE';
      case 'medium': return 'PENDENTE';
      case 'low': return 'NORMAL';
      default: return 'PENDENTE';
    }
  }

  function approveTicket(id) {
    if(confirm(`Aprovar chamado CD-${id}?`)) {
      // Aqui você faria a chamada para a API
      console.log('Aprovando chamado:', id);
      // Redirecionar para a tela de detalhes do chamado
      window.location.href = `../14. TELA DE DETALHES DO CHAMADO ADM/detalhes-chamado.html?id=${encodeURIComponent(id)}`;
    }
  }

  function rejectTicket(id) {
    if(confirm(`Recusar chamado CD-${id}?`)) {
      // Aqui você faria a chamada para a API
      console.log('Recusando chamado:', id);
      // Simular remoção da lista após recusa
      const index = data.findIndex(item => item.id === id);
      if (index !== -1) {
        data.splice(index, 1);
        applyFilters();
        render();
      }
    }
  }

  function applyFilters(){
    const q = (searchInput.value || '').trim().toLowerCase();
    if (!q){
      filtered = [...data];
    } else {
      filtered = data.filter(it=>{
        return `cd-${it.id}`.toLowerCase().includes(q)
            || String(it.id).toLowerCase().includes(q)
            || String(it.subject).toLowerCase().includes(q)
            || String(it.category).toLowerCase().includes(q);
      });
    }
    pageIndex = 1;
    render();
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

  // Menu do usuário
  function setupUserMenu(){
    const userMenuBtn = $('#userMenuBtn');
    const userDropdown = $('#userDropdown');
    const logoutBtn = $('#logoutBtn');

    const userSession = JSON.parse(localStorage.getItem('userSession') || '{}');
    const userName = userSession.name || 'Técnico';
    const userEmail = userSession.email || 'tecnico@email.com';
    
    if($('#userName')) $('#userName').textContent = userName;
    if($('#userEmail')) $('#userEmail').textContent = userEmail;
    
    const avatarUrl = `https://ui-avatars.com/api/?name=${encodeURIComponent(userName)}&background=F28A1A&color=fff&bold=true`;
    if($('#userAvatar')) $('#userAvatar').src = avatarUrl;
    if($('#userAvatarDropdown')) $('#userAvatarDropdown').src = avatarUrl;

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

  document.addEventListener('DOMContentLoaded', ()=>{
    pageSize = parseInt((pageSizeSel?.value||'5'),10) || 5;
    applyFilters();
    setupUserMenu();
  });
})();
  // ===== UPLOAD DE FOTO DE PERFIL =====
  function setupPhotoUpload() {
    const changePhotoBtn = $('#changePhotoBtn');
    const photoModal = $('#photoModal');
    const closePhotoModal = $('#closePhotoModal');
    const photoInput = $('#photoInput');
    const selectPhotoBtn = $('#selectPhotoBtn');
    const uploadPhotoBtn = $('#uploadPhotoBtn');
    const photoPreview = $('#photoPreview');
    const previewImg = $('#previewImg');
    const photoPlaceholder = $('#photoPlaceholder');

    let selectedFile = null;

    if (changePhotoBtn) {
      on(changePhotoBtn, 'click', () => {
        photoModal.removeAttribute('hidden');
        document.body.style.overflow = 'hidden';
      });
    }

    function closeModal() {
      photoModal.setAttribute('hidden', '');
      document.body.style.overflow = '';
      selectedFile = null;
      previewImg.style.display = 'none';
      photoPlaceholder.style.display = 'flex';
      uploadPhotoBtn.disabled = true;
    }

    if (closePhotoModal) on(closePhotoModal, 'click', closeModal);
    on(photoModal, 'click', (e) => { if (e.target.classList.contains('photo-modal-backdrop')) closeModal(); });
    if (selectPhotoBtn) on(selectPhotoBtn, 'click', () => { photoInput.click(); });
    if (photoPreview) on(photoPreview, 'click', () => { photoInput.click(); });

    if (photoInput) {
      on(photoInput, 'change', (e) => {
        const file = e.target.files[0];
        if (!file) return;
        if (!file.type.startsWith('image/')) { alert('Selecione uma imagem v�lida.'); return; }
        if (file.size > 5 * 1024 * 1024) { alert('Imagem deve ter no m�ximo 5MB.'); return; }
        selectedFile = file;
        const reader = new FileReader();
        reader.onload = (event) => {
          previewImg.src = event.target.result;
          previewImg.style.display = 'block';
          photoPlaceholder.style.display = 'none';
          uploadPhotoBtn.disabled = false;
        };
        reader.readAsDataURL(file);
      });
    }

    if (uploadPhotoBtn) {
      on(uploadPhotoBtn, 'click', () => {
        if (!selectedFile) return;
        const reader = new FileReader();
        reader.onload = (event) => {
          try {
            localStorage.setItem('userProfilePhoto', event.target.result);
            const userAvatar = $('#userAvatar');
            const userAvatarDropdown = $('#userAvatarDropdown');
            if (userAvatar) userAvatar.src = event.target.result;
            if (userAvatarDropdown) userAvatarDropdown.src = event.target.result;
            closeModal();
            alert('Foto atualizada com sucesso!');
          } catch (e) { alert('Erro ao salvar. Tente uma imagem menor.'); }
        };
        reader.readAsDataURL(selectedFile);
      });
    }
  }

  function loadSavedPhoto() {
    const savedPhoto = localStorage.getItem('userProfilePhoto');
    if (savedPhoto) {
      const userAvatar = $('#userAvatar');
      const userAvatarDropdown = $('#userAvatarDropdown');
      if (userAvatar) userAvatar.src = savedPhoto;
      if (userAvatarDropdown) userAvatarDropdown.src = savedPhoto;
    }
  }

  document.addEventListener('DOMContentLoaded', () => {
    setupPhotoUpload();
    loadSavedPhoto();
  });
