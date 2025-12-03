(function(){
  'use strict';
  const $ = (s,r=document)=> r.querySelector(s);
  const $$ = (s,r=document)=> Array.from(r.querySelectorAll(s));
  const on = (el,ev,fn,opt)=> el && el.addEventListener(ev,fn,opt);

  // Dados mockados (pode vir da API)
  const faqData = [
    { title:'Problemas técnicos comuns', items:[
      { q:'O sistema está lento, o que posso fazer?', a:'Feche programas em segundo plano, limpe temporários e verifique a conexão.' },
      { q:'O que fazer se minha conexão Wi‑Fi estiver instável?', a:'Reinicie o roteador, aproxime-se do AP e teste via cabo.' },
      { q:'O sistema não está abrindo, o que faço?', a:'Verifique atualizações pendentes e erros; anexe prints ao chamado.' },
      { q:'O mouse não está funcionando, como resolver?', a:'Troque a porta USB, pilha e teste outro mouse.' },
    ]},
    { title:'Hardware', items:[
      { q:'A tela ficou preta de repente, o que pode ser?', a:'Cheque cabos de energia/vídeo; teste outro monitor.' },
      { q:'Meu computador não liga, e agora?', a:'Verifique fonte e tomadas; anote bipes se houver.' },
    ]},
    { title:'Software', items:[
      { q:'Não consigo abrir um PDF, qual pode ser o problema?', a:'Atualize o leitor, teste outro navegador e valide o arquivo.' },
      { q:'Como converter um arquivo de um formato para outro?', a:'Use o conversor homologado pela TI.' },
      { q:'Meu arquivo desapareceu, como recuperá‑lo?', a:'Verifique Lixeira e backups; solicite restauração se necessário.' },
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
    const userName = userSession.name || 'Usuário';
    const userEmail = userSession.email || 'usuario@email.com';
    
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
