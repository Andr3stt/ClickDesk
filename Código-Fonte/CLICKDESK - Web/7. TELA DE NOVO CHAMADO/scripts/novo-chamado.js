/* Novo chamado — HTML/CSS/JS puros
   - Mantém layout do mock (rótulos à esquerda, campos à direita)
   - Anexos com visual de arquivos e remover
   - Validação básica
   - Pontos de integração para API em C# (fetch comentado)
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

  const $ = (s, r=document)=> r.querySelector(s);
  const $$ = (s, r=document)=> Array.from(r.querySelectorAll(s));
  const on = (el, ev, fn)=> el && el.addEventListener(ev, fn);

  const form = $('#ticketForm');
  const date = $('#date');
  const attachBtn = $('#attachBtn');
  const fileInput = $('#attachments');
  const filesList = $('#filesList');
  const submitBtn = $('#submitBtn');

  // Data padrão: hoje
  document.addEventListener('DOMContentLoaded', ()=>{
    if (date && !date.value){
      const d = new Date();
      const v = `${d.getFullYear()}-${String(d.getMonth()+1).padStart(2,'0')}-${String(d.getDate()).padStart(2,'0')}`;
      date.value = v;
    }
  });

  // Anexos
  on(attachBtn, 'click', ()=> fileInput && fileInput.click());
  on(fileInput, 'change', renderFiles);

  function renderFiles(){
    filesList.innerHTML = '';
    const files = Array.from(fileInput.files || []);
    files.forEach((f, idx)=>{
      const li = document.createElement('li');
      li.innerHTML = `
        <span title="${escapeHtml(f.name)}">${truncate(f.name, 34)}</span>
        <small>(${formatSize(f.size)})</small>
        <button type="button" class="file-remove" aria-label="Remover ${escapeHtml(f.name)}" data-i="${idx}">×</button>
      `;
      filesList.appendChild(li);
    });
  }

  on(filesList, 'click', (e)=>{
    const btn = e.target.closest('.file-remove');
    if(!btn || !fileInput?.files) return;
    const i = Number(btn.getAttribute('data-i'));
    const dt = new DataTransfer();
    Array.from(fileInput.files).forEach((f, idx)=>{ if(idx!==i) dt.items.add(f); });
    fileInput.files = dt.files;
    renderFiles();
  });

  function truncate(t, n){ return t.length>n ? t.slice(0, n-3)+'...' : t; }
  function formatSize(b){
    if (b<1024) return b+' B';
    if (b<1024*1024) return (b/1024).toFixed(1)+' KB';
    return (b/1024/1024).toFixed(1)+' MB';
  }
  function escapeHtml(s){ return String(s).replace(/[&<>"']/g, m=> ({'&':'&amp;','<':'&lt;','>':'&gt;','"':'&quot;',"'":'&#39;'}[m])); }

  // Envio
  on(form, 'submit', async (e)=>{
    e.preventDefault();
    if (!form.checkValidity()){
      form.reportValidity();
      return;
    }

    const data = Object.fromEntries(new FormData(form).entries());

    // Validações simples
    if ((data.subject||'').trim().length < 3){
      alert('Informe um assunto válido (mín. 3 caracteres).');
      $('#subject').focus(); return;
    }
    if ((data.description||'').trim().length < 10){
      alert('Descreva melhor o chamado (mín. 10 caracteres).');
      $('#description').focus(); return;
    }

    // Desabilita enquanto “envia”
    harmSubmit(true);

    // PONTO DE INTEGRAÇÃO (C#):
    // Opção A) Envio como multipart (com anexos)
    // const fd = new FormData(form);
    // fetch('/api/tickets', { method:'POST', body: fd })
    //   .then(r=>r.json())
    //   .then(res=>{
    //     if(res.success){ alert('Chamado enviado com sucesso.'); form.reset(); filesList.innerHTML=''; }
    //     else{ alert('Erro ao enviar: ' + (res.error || 'tente novamente')); }
    //   })
    //   .catch(()=> alert('Falha de comunicação com o servidor.'))
    //   .finally(()=> harmSubmit(false));

    // Opção B) Envio em JSON (sem anexos)
    // fetch('/api/tickets', {
    //   method:'POST',
    //   headers:{ 'Content-Type':'application/json' },
    //   body: JSON.stringify({
    //     name: data.name,
    //     date: data.date,
    //     department: data.department,
    //     subject: data.subject,
    //     category: data.category,
    //     description: data.description
    //   })
    // }).then(...)

    // SIMULAÇÃO local
    await delay(600);
    console.log('Novo chamado (simulação):', {
      name: data.name,
      date: data.date,
      department: data.department,
      subject: data.subject,
      category: data.category,
      description: data.description,
      files: Array.from(fileInput.files || []).map(f=>({ name:f.name, size:f.size, type:f.type }))
    });
    alert('Chamado enviado (simulação). Integre com a API em C# para envio real.');
    form.reset();
    filesList.innerHTML = '';
    harmSubmit(false);
  });

  function harmSubmit(lock){
    if (!submitBtn) return;
    submitBtn.disabled = lock;
    submitBtn.textContent = lock ? 'Enviando...' : 'Enviar';
  }
  function delay(ms){ return new Promise(r=>setTimeout(r,ms)); }

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

  // Inicializar menu
  document.addEventListener('DOMContentLoaded', setupUserMenu);
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
