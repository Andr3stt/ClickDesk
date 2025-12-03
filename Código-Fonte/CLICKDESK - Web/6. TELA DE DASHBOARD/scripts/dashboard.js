/* Clickdesk Dashboard (sem "Dias em aberto")
   - Título estilizado
   - "Situação" visível e ao lado do título do chamado
   - Itens de lista com 3 colunas (link | horário | prioridade), sem quebras
   - KPIs com valores e deltas (sem sparklines)
*/
(function(){
  'use strict';

  // ===== PROTEÇÃO: Apenas usuários comuns podem acessar =====
  function verificarAcesso() {
    const userSession = localStorage.getItem('userSession');
    if (!userSession) {
      // TEMPORÁRIO: Comentado para permitir teste
      // window.location.href = '../1. TELA DE LOGIN/login.html';
      // return false;
    }
    
    try {
      const user = JSON.parse(userSession);
      if (user.tipo === 'tecnico') {
        // TEMPORÁRIO: Comentado para permitir teste
        // window.location.href = '../11. TELA DASHBOARD ADM/dashboard-adm.html';
        // return false;
      }
    } catch(e) {
      // TEMPORÁRIO: Comentado para permitir teste
      // localStorage.removeItem('userSession');
      // window.location.href = '../1. TELA DE LOGIN/login.html';
      // return false;
    }
    return true;
  }

  if (!verificarAcesso()) return;
  // ===== FIM DA PROTEÇÃO =====

  const $ = (sel, root=document) => root.querySelector(sel);
  const $$ = (sel, root=document) => Array.from(root.querySelectorAll(sel));
  const on = (el, ev, fn, opt) => el && el.addEventListener(ev, fn, opt);
  const NS = 'http://www.w3.org/2000/svg';

  function ticketDetailUrl(id){
    try{
      const r = window.CLICKDESK_ROUTES?.ticketDetail;
      if (typeof r === 'function') return r(id);
      if (typeof r === 'string') return r.replace('{id}', encodeURIComponent(id));
    }catch(_){}
    return `/chamados/${encodeURIComponent(id)}`;
  }
  function toInt(v){ const n = Number(v); return Number.isFinite(n) ? n : 0; }
  function parseIso(x){ const d=new Date(x); return isNaN(d.getTime())? new Date(): d; }
  function toTitleCasePt(text){
    if(!text) return '';
    const low = new Set(['de','da','do','das','dos','e','em','no','na','nos','nas','ao','a','o','os','as','para','por','um','uma']);
    return text.toLowerCase().split(/\s+/).map((w,i)=> (i>0 && low.has(w))? w : w.charAt(0).toUpperCase()+w.slice(1)).join(' ');
  }
  function formatUpdated(iso){
    try{
      const d = parseIso(iso);
      const dateStr = d.toLocaleDateString('pt-BR', { day:'2-digit', month:'2-digit' });
      const timeStr = d.toLocaleTimeString('pt-BR', { hour:'2-digit', minute:'2-digit' });
      const tooltip = d.toLocaleString('pt-BR', { weekday:'long', day:'2-digit', month:'long', year:'numeric', hour:'2-digit', minute:'2-digit' });
      return { text: `${dateStr} • ${timeStr}`, tooltip: `Atualizado em ${tooltip}` };
    }catch(_){ return { text:'', tooltip:'' }; }
  }

  const state = {
    range: '7d',
    priorities: new Set(['p1','p2','p3']),
    recentAll: []
  };

  function normalizeItem(it){
    const p = String(it.priority||'p3').toLowerCase();
    return { ...it, priority: (p==='p1'||p==='p2'||p==='p3') ? p : 'p3', openedAt: it.openedAt || it.createdAt || it.updatedAt };
  }

  async function apiGetSummary(range){
    try{
      const resp = await fetch(`/api/dashboard/summary?range=${encodeURIComponent(range)}`, { headers:{ Accept:'application/json' } });
      if(!resp.ok) throw new Error('HTTP ' + resp.status);
      const d = await resp.json();
      const item = x => (typeof x==='number') ? { value:x, prev:x } : { value: toInt(x?.value), prev: toInt(x?.prev) };
      return { total:item(d.total), attended:item(d.attended), waiting:item(d.waiting), inProgress:item(d.inProgress) };
    }catch(_){
      return simSummary();
    }
  }
  async function apiGetRecent(range, priosCsv){
    try{
      const qs = new URLSearchParams({ limit:'20', range, priorities: priosCsv });
      const resp = await fetch(`/api/tickets/recent?${qs.toString()}`, { headers:{ Accept:'application/json' } });
      if(!resp.ok) throw new Error('HTTP ' + resp.status);
      const data = await resp.json();
      if (!Array.isArray(data)) throw new Error('Invalid payload');
      return data.map(normalizeItem);
    }catch(_){
      try{
        const resp = await fetch(`/api/tickets/recent?limit=50&range=${encodeURIComponent(range)}`, { headers:{ Accept:'application/json' } });
        if(!resp.ok) throw new Error('HTTP ' + resp.status);
        const data = await resp.json();
        state.recentAll = (Array.isArray(data)? data: []).map(normalizeItem);
      }catch(_){
        state.recentAll = simRecent().map(normalizeItem);
      }
      return filterClient(state.recentAll);
    }
  }

  function simSummary(){
    const mkVal = () => Math.floor(5 + Math.random()*15);
    const mkDelta = () => Math.floor(Math.random()*11)-5;
    const pack = () => { const prev = mkVal(); const v = Math.max(0, prev + mkDelta()); return { value: v, prev }; };
    return { total:pack(), attended:pack(), waiting:pack(), inProgress:pack() };
  }
  function simRecent(){
    const now=Date.now(), SEC_DAY=86400000;
    const mk=(i,t,desc,st,cat,od,um)=>({ 
      id:`#${1234-i}`, 
      title:t, 
      description:desc,
      status:st, 
      category:cat,
      openedAt:new Date(now-od*SEC_DAY).toISOString(), 
      updatedAt:new Date(now-um*60*1000).toISOString() 
    });
    return [
      mk(1,'Problema com impressora HP', 'A impressora não está reconhecendo o cartucho novo instalado...', 'open','Hardware',0,120),
      mk(2,'Acesso ao sistema financeiro', 'Preciso de acesso ao módulo de relatórios do sistema financeiro...', 'progress','Acesso',0,300),
      mk(3,'Conexão VPN instável', 'A VPN fica desconectando a cada 15 minutos...', 'closed','Rede',1,0),
    ];
  }

  function setDelta(el, value, prev){
    const pct = prev ? Math.round(((value - prev)/Math.max(1,prev))*100) : 0;
    el.textContent = pct>0? `▲ ${pct}%` : pct<0? `▼ ${Math.abs(pct)}%` : '—';
    el.classList.remove('up','down','neutral');
    el.classList.add(pct>0? 'up' : pct<0? 'down' : 'neutral');
  }
  function renderKPIs(s){
    const map = [
      { v:'#kpi-total', d:'#delta-total', data:s.total },
      { v:'#kpi-attended', d:'#delta-attended', data:s.attended },
      { v:'#kpi-waiting', d:'#delta-waiting', data:s.waiting },
      { v:'#kpi-inprogress', d:'#delta-inprogress', data:s.inProgress },
    ];
    map.forEach(({v,d,data})=>{
      const vEl=$(v), dEl=$(d);
      if (vEl) vEl.textContent = data.value;
      if (dEl) setDelta(dEl, data.value, data.prev);
    });
  }

  function bindFilters(){
    const chips = $('#prioChips');
    const clear = $('#clearFilters');

    on(chips,'click', async (e)=>{
      const btn = e.target.closest('.chip-toggle'); if(!btn) return;
      const pr = String(btn.getAttribute('data-prio')||'').toLowerCase(); if(!pr) return;
      if (state.priorities.has(pr) && state.priorities.size===1) return;
      if (state.priorities.has(pr)){ state.priorities.delete(pr); btn.classList.remove('active'); btn.setAttribute('aria-pressed','false'); }
      else { state.priorities.add(pr); btn.classList.add('active'); btn.setAttribute('aria-pressed','true'); }
      await loadRecents();
    });

    on(clear,'click', async ()=>{
      state.priorities = new Set(['p1','p2','p3']);
      $$('#prioChips .chip-toggle').forEach(b=>{ b.classList.add('active'); b.setAttribute('aria-pressed','true'); });
      await loadRecents();
    });
  }

  function filterClient(items){
    return items.filter(it=> state.priorities.has(String(it.priority||'').toLowerCase()));
  }

  function mapStatusLabel(s){
    switch((s||'').toLowerCase()){
      case 'open': return 'Aberto';
      case 'progress': return 'Em Andamento';
      case 'waiting': return 'Aguardando';
      case 'closed': return 'Resolvido';
      default: return s || '—';
    }
  }
  function mapStatusClass(s){
    switch((s||'').toLowerCase()){
      case 'open': return 'open';
      case 'progress': return 'progress';
      case 'waiting': return 'waiting';
      case 'closed': return 'closed';
      default: return 'open';
    }
  }

  function formatTimeAgo(isoString){
    try{
      const date = new Date(isoString);
      const now = new Date();
      const diffMs = now - date;
      const diffMinutes = Math.floor(diffMs / (1000 * 60));
      const diffHours = Math.floor(diffMs / (1000 * 60 * 60));

      if (diffMinutes < 60) {
        return diffMinutes <= 1 ? 'Agora' : `Há ${diffMinutes} minutos`;
      } else if (diffHours < 24) {
        return `Há ${diffHours} hora${diffHours > 1 ? 's' : ''}`;
      } else {
        const diffDays = Math.floor(diffMs / (1000 * 60 * 60 * 24));
        return `Há ${diffDays} dia${diffDays > 1 ? 's' : ''}`;
      }
    } catch(_) {
      return 'Ontem';
    }
  }

  function renderRecents(list){
    const container = $('#recentCardsGrid'), empty = $('#recentEmpty');
    container.innerHTML = '';
    if (!list?.length){ empty.hidden=false; return; }
    empty.hidden=true;

    list.forEach(ticket => {
      const card = document.createElement('div');
      card.className = 'recent-ticket-card';
      
      card.innerHTML = `
        <div class="recent-ticket-header">
          <span class="recent-ticket-id">${ticket.id}</span>
          <span class="recent-ticket-status ${mapStatusClass(ticket.status)}">${mapStatusLabel(ticket.status)}</span>
        </div>
        
        <h4 class="recent-ticket-title">${toTitleCasePt(ticket.title || '')}</h4>
        <p class="recent-ticket-description">${ticket.description || 'Sem descrição disponível'}</p>
        
        <div class="recent-ticket-footer">
          <div class="recent-ticket-meta">
            <span class="recent-ticket-category">${ticket.category || 'Geral'}</span>
            <span class="recent-ticket-time">${formatTimeAgo(ticket.updatedAt)}</span>
          </div>
          <a href="${ticketDetailUrl(ticket.id)}" class="recent-ticket-btn">
            Ver Detalhes
          </a>
        </div>
      `;
      
      container.appendChild(card);
    });
  }

  async function loadKPIs(){
    const s = await apiGetSummary(state.range);
    renderKPIs(s);
  }
  async function loadRecents(){
    const prCsv = Array.from(state.priorities).join(',');
    const data = await apiGetRecent(state.range, prCsv);
    renderRecents(data);
  }

  async function reload(){
    state.range = $('#rangeSelect')?.value || '7d';

    const btn = $('#refreshBtn');
    if (btn){
      btn.classList.add('is-spinning');
      btn.disabled = true;
      btn.setAttribute('aria-busy','true');
    }

    try{
      await Promise.all([ loadKPIs(), loadRecents() ]);
    } finally {
      if (btn){
        btn.classList.remove('is-spinning');
        btn.disabled = false;
        btn.removeAttribute('aria-busy');
      }
    }
  }

  function bindUI(){
    on($('#rangeSelect'),'change', reload);
    on($('#refreshBtn'),'click', reload);

    on($('#editProfileLink'),'click',(e)=>{ e.preventDefault(); try{ sessionStorage.setItem('showProfileModal','1'); }catch(_){} location.href='perfil-usuario.html'; });
    on($('#logoutBtn'),'click',()=>{ location.href='login.html'; });
  }

  // Menu do usuário
  function setupUserMenu(){
    const userMenuBtn = $('#userMenuBtn');
    const userDropdown = $('#userDropdown');
    const logoutBtn = $('#logoutBtn');

    // Carregar dados do usuário
    const userSession = JSON.parse(localStorage.getItem('userSession') || '{}');
    const userName = userSession.name || 'Usuário';
    const userEmail = userSession.email || 'usuario@email.com';
    
    // Atualizar elementos
    if($('#userName')) $('#userName').textContent = userName;
    if($('#userEmail')) $('#userEmail').textContent = userEmail;
    
    // Atualizar avatares
    const avatarUrl = `https://ui-avatars.com/api/?name=${encodeURIComponent(userName)}&background=F28A1A&color=fff&bold=true`;
    if($('#userAvatar')) $('#userAvatar').src = avatarUrl;
    if($('#userAvatarDropdown')) $('#userAvatarDropdown').src = avatarUrl;

    // Toggle dropdown
    if(userMenuBtn){
      on(userMenuBtn, 'click', (e)=>{
        e.stopPropagation();
        const isHidden = userDropdown.hasAttribute('hidden');
        if(isHidden){
          userDropdown.removeAttribute('hidden');
        }else{
          userDropdown.setAttribute('hidden', '');
        }
      });
    }

    // Fechar ao clicar fora
    on(document, 'click', (e)=>{
      if(!userDropdown.hasAttribute('hidden') && !userDropdown.contains(e.target)){
        userDropdown.setAttribute('hidden', '');
      }
    });

    // Logout
    if(logoutBtn){
      on(logoutBtn, 'click', ()=>{
        localStorage.removeItem('userSession');
        window.location.href = '../1. TELA DE LOGIN/login.html';
      });
    }
  }

  document.addEventListener('DOMContentLoaded', async ()=>{
    bindUI();
    setupUserMenu();
    await reload();
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
