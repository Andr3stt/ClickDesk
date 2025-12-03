(function(){
  'use strict';
  const $ = (s,r=document)=> r.querySelector(s);
  const $$ = (s,r=document)=> Array.from(r.querySelectorAll(s));
  const on = (el,ev,fn,opt)=> el && el.addEventListener(ev,fn,opt);

  // Dados mockados - FAQ específico para TÉCNICOS
  const faqData = [
    { title:'Gestão de Chamados', items:[
      { q:'Como aprovar um chamado pendente?', a:'Acesse "Aprovar chamados", revise os detalhes, prioridade e descrição. Clique em "Aprovar" para liberar ou "Recusar" com justificativa se necessário.' },
      { q:'Como alterar a prioridade de um chamado?', a:'Na tela de detalhes do chamado, clique no badge de prioridade e selecione: Baixa, Média, Alta ou Crítica conforme o SLA definido.' },
      { q:'Posso reatribuir um chamado para outro técnico?', a:'Sim. Na tela de detalhes, clique em "Reatribuir", selecione o técnico disponível e confirme a transferência.' },
      { q:'Como fechar um chamado resolvido?', a:'Após finalizar o atendimento, atualize o status para "Resolvido", adicione as notas de resolução e clique em "Fechar chamado".' },
    ]},
    { title:'Sistema e Procedimentos', items:[
      { q:'Como usar filtros no dashboard de chamados?', a:'Use os chips de prioridade (Baixa, Média, Alta, Crítica) ou o campo de busca para filtrar por ID, título ou status específico.' },
      { q:'Qual o SLA para cada prioridade?', a:'Crítica: 2h | Alta: 4h | Média: 8h | Baixa: 24h. Monitore os prazos pelo indicador visual de tempo restante.' },
      { q:'Como adicionar notas internas em um chamado?', a:'Na tela de detalhes, use a aba "Notas Internas" (visível apenas para técnicos). Adicione observações, diagnósticos ou procedimentos realizados.' },
      { q:'O sistema envia notificações automáticas?', a:'Sim. O usuário recebe email quando: chamado é aprovado, status muda, técnico adiciona resposta ou chamado é resolvido/fechado.' },
    ]},
    { title:'Solução de Problemas', items:[
      { q:'Como escalar um chamado para nível superior?', a:'Clique em "Escalar", selecione o motivo (complexidade técnica, SLA crítico, etc) e escolha o nível/equipe de destino.' },
      { q:'Usuário não recebeu resposta do chamado, o que fazer?', a:'Verifique se o email está correto no perfil. Reenvie a notificação manualmente ou oriente o usuário a checar spam/lixeira.' },
      { q:'Como consultar o histórico completo de um chamado?', a:'Na tela de detalhes, role até a seção "Timeline" onde aparecem todas as interações, mudanças de status e atualizações cronológicas.' },
      { q:'Dashboard não está atualizando os chamados, o que fazer?', a:'Clique no botão de atualização (ícone de refresh) no topo da tela. Se persistir, verifique a conexão ou contate o administrador do sistema.' },
    ]},
    { title:'Boas Práticas', items:[
      { q:'Como documentar a resolução de um problema recorrente?', a:'Após resolver, adicione uma nota detalhada com: diagnóstico, solução aplicada e passos para prevenção. Sugira criação de item no FAQ de usuários.' },
      { q:'Qual a melhor forma de comunicar com o usuário?', a:'Seja claro e objetivo. Evite jargões técnicos excessivos. Use a timeline do chamado para manter histórico organizado das interações.' },
      { q:'Como priorizar múltiplos chamados simultâneos?', a:'Siga o SLA e prioridade definida. Em caso de empate, priorize: 1) Impacto em produção 2) Número de usuários afetados 3) Ordem de chegada.' },
    ]},
  ];

  const chevSvg = (w=18,h=18)=> `
    <svg class="chev" viewBox="0 0 24 24" width="${w}" height="${h}" aria-hidden>
      <path d="M6 9l6 6 6-6" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
    </svg>`;
  const qChevSvg = (w=16,h=16)=> `
    <svg class="q-chev" viewBox="0 0 24 24" width="${w}" height="${h}" aria-hidden>
      <path d="M6 9l6 6 6-6" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
    </svg>`;

  function renderAccordion(){
    const root = $('#faqAccordion');
    if(!root) return;
    root.innerHTML = '';

    faqData.forEach((cat, cIdx)=>{
      const catEl = document.createElement('section');
      catEl.className = 'faq-cat';
      catEl.setAttribute('aria-expanded','true');

      const headBtn = document.createElement('button');
      headBtn.className = 'cat-head';
      headBtn.type = 'button';
      headBtn.setAttribute('aria-controls', `cat-${cIdx}`);
      headBtn.setAttribute('aria-expanded','true');
      headBtn.innerHTML = `<span>${cat.title}</span>${chevSvg()}`;

      const body = document.createElement('div');
      body.className = 'cat-body';
      body.id = `cat-${cIdx}`;
      body.role = 'group';

      cat.items.forEach((it, qIdx)=>{
        const item = document.createElement('div');
        item.className = 'q-item';
        item.setAttribute('aria-expanded','false');

        const btn = document.createElement('button');
        btn.className = 'q-toggle';
        btn.type = 'button';
        btn.setAttribute('aria-controls', `ans-${cIdx}-${qIdx}`);
        btn.setAttribute('aria-expanded','false');
        btn.innerHTML = `
          <span class="q-left">
            <span class="q-dot">•</span>
            <span class="q-text">${it.q}</span>
          </span>
          ${qChevSvg()}
        `;

        const ans = document.createElement('div');
        ans.className = 'answer';
        ans.id = `ans-${cIdx}-${qIdx}`;
        ans.textContent = it.a;

        item.append(btn, ans);
        body.appendChild(item);
      });

      catEl.append(headBtn, body);
      root.appendChild(catEl);
    });
  }

  function toggleCategory(btn){
    const expanded = btn.getAttribute('aria-expanded') === 'true';
    btn.setAttribute('aria-expanded', String(!expanded));
    const cat = btn.closest('.faq-cat');
    cat.setAttribute('aria-expanded', String(!expanded));
    const body = document.getElementById(btn.getAttribute('aria-controls'));
    if(body) body.style.display = expanded ? 'none' : '';
  }
  function toggleQuestion(btn){
    const expanded = btn.getAttribute('aria-expanded') === 'true';
    btn.setAttribute('aria-expanded', String(!expanded));
    const item = btn.closest('.q-item');
    item.setAttribute('aria-expanded', String(!expanded));
  }

  function expandAll(val){
    $$('.faq-cat .cat-head').forEach(b=>{
      const cur = b.getAttribute('aria-expanded') === 'true';
      if (cur !== val) toggleCategory(b);
    });
  }

  function bindEvents(){
    on($('#faqAccordion'),'click',(e)=>{
      const head = e.target.closest('.cat-head');
      if(head){ toggleCategory(head); return; }
      const q = e.target.closest('.q-toggle');
      if(q){ toggleQuestion(q); return; }
    });
    on($('#faqAccordion'),'keydown',(e)=>{
      if((e.key==='Enter'||e.key===' ') && (e.target.classList.contains('cat-head')||e.target.classList.contains('q-toggle'))){
        e.preventDefault(); e.target.click();
      }
    });

    on($('#expandAll'),'click',()=> expandAll(true));
    on($('#collapseAll'),'click',()=> expandAll(false));
  }

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
    renderAccordion();
    setupUserMenu();
    
    // Garante que o dropdown do usuário inicie fechado
    const userDropdown = $('#userDropdown');
    if(userDropdown && !userDropdown.hasAttribute('hidden')){
      userDropdown.setAttribute('hidden', '');
    }
    
    // abre todas as categorias ao iniciar
    $$('.faq-cat .cat-head').forEach(b=>{
      const body = document.getElementById(b.getAttribute('aria-controls'));
      if(body) body.style.display = '';
    });
    bindEvents();
  });
})();

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
