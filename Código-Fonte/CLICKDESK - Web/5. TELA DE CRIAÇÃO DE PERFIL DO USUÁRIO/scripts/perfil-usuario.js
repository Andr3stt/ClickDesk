(function(){
  'use strict';
  const $ = (s,r=document)=> r.querySelector(s);
  
  // Controle do modal e etapas
  const modal = $('#profileModal');
  const welcomeStep = $('#welcomeStep');
  const formStep = $('#formStep');
  const successStep = $('#successStep');
  const profileForm = $('#profileForm');
  
  // Botões
  const skipBtn = $('#skipBtn');
  const startProfileBtn = $('#startProfileBtn');
  const closeModalBtn = $('#closeModalBtn');
  const saveProfileBtn = $('#saveProfileBtn');
  const continueBtn = $('#continueBtn');
  const logoutBtn = $('#logoutBtn');

  // Função para trocar de etapa
  function showStep(step) {
    welcomeStep.hidden = true;
    formStep.hidden = true;
    successStep.hidden = true;
    
    if (step === 'welcome') welcomeStep.hidden = false;
    if (step === 'form') formStep.hidden = false;
    if (step === 'success') successStep.hidden = false;
  }

  // Fechar modal
  function closeModal() {
    modal.setAttribute('aria-hidden', 'true');
    document.body.style.overflow = '';
  }

  // Abrir modal
  function openModal() {
    modal.setAttribute('aria-hidden', 'false');
    document.body.style.overflow = 'hidden';
  }

  // Event listeners
  if (startProfileBtn) {
    startProfileBtn.addEventListener('click', () => {
      showStep('form');
    });
  }

  if (continueBtn) {
    continueBtn.addEventListener('click', () => {
      // Redirecionar OBRIGATORIAMENTE para o dashboard
      window.location.href = '../6. TELA DE DASHBOARD/dashboard.html';
    });
  }
  
  // Bloquear fechamento do modal clicando no backdrop
  const modalBackdrop = $('#modalBackdrop');
  if (modalBackdrop) {
    modalBackdrop.addEventListener('click', (e) => {
      e.preventDefault();
      e.stopPropagation();
      // Não faz nada - modal não pode ser fechado
    });
  }
  
  // Bloquear tecla ESC
  document.addEventListener('keydown', (e) => {
    if (e.key === 'Escape' && modal.getAttribute('aria-hidden') === 'false') {
      e.preventDefault();
      e.stopPropagation();
      // Não faz nada - modal não pode ser fechado
    }
  });

  if (logoutBtn) {
    logoutBtn.addEventListener('click', () => {
      if (confirm('Deseja realmente sair?')) {
        window.location.href = '../1. TELA DE LOGIN/login.html';
      }
    });
  }

  // Submissão do formulário
  if (profileForm) {
    profileForm.addEventListener('submit', async (e) => {
      e.preventDefault();
      
      const fullName = $('#fullName').value.trim();
      const email = $('#email').value.trim();
      const department = $('#department').value.trim();
      
      // Validação básica
      if (!fullName || !email || !department) {
        alert('Por favor, preencha todos os campos obrigatórios.');
        return;
      }

      // Validar email
      const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
      if (!emailRegex.test(email)) {
        alert('Por favor, informe um e-mail válido.');
        return;
      }

      // Desabilitar botão durante o salvamento
      saveProfileBtn.disabled = true;
      saveProfileBtn.textContent = 'Salvando...';

      // Simular salvamento (você pode adicionar chamada de API aqui)
      try {
        await new Promise(resolve => setTimeout(resolve, 800));
        
        // Salvar no localStorage (mock)
        localStorage.setItem('userProfile', JSON.stringify({
          fullName,
          email,
          department,
          createdAt: new Date().toISOString()
        }));

        // Marcar perfil como criado
        localStorage.setItem('profileCompleted', 'true');
        
        // Mostrar tela de sucesso
        showStep('success');
        
        // Resetar o formulário
        profileForm.reset();
        
        // Redirecionar automaticamente para o dashboard após 2 segundos
        setTimeout(() => {
          window.location.href = '../6. TELA DE DASHBOARD/dashboard.html';
        }, 2000);
        
      } catch (error) {
        alert('Erro ao salvar perfil. Tente novamente.');
      } finally {
        saveProfileBtn.disabled = false;
        saveProfileBtn.textContent = 'Salvar';
      }
    });
  }

  // Inicializar - verificar se perfil já foi criado
  document.addEventListener('DOMContentLoaded', () => {
    const profileCompleted = localStorage.getItem('profileCompleted');
    
    // Se perfil já foi criado, redirecionar para dashboard
    if (profileCompleted === 'true') {
      window.location.href = '../6. TELA DE DASHBOARD/dashboard.html';
      return;
    }
    
    // Se não foi criado, abrir modal obrigatório
    showStep('form');
    openModal();
  });

  // === CONTROLE DO MENU DO USUÁRIO ===
  const userMenuBtn = $('#userMenuBtn');
  const userDropdown = $('#userDropdown');

  if (userMenuBtn && userDropdown) {
    // Toggle do menu do usuário
    userMenuBtn.addEventListener('click', (e) => {
      e.preventDefault();
      e.stopPropagation();
      const isHidden = userDropdown.hasAttribute('hidden');
      
      if (isHidden) {
        userDropdown.removeAttribute('hidden');
        userMenuBtn.setAttribute('aria-expanded', 'true');
      } else {
        userDropdown.setAttribute('hidden', '');
        userMenuBtn.setAttribute('aria-expanded', 'false');
      }
    });

    // Fechar menu ao clicar fora
    document.addEventListener('click', (e) => {
      if (!userMenuBtn.contains(e.target) && !userDropdown.contains(e.target)) {
        userDropdown.setAttribute('hidden', '');
        userMenuBtn.setAttribute('aria-expanded', 'false');
      }
    });

    // Funcionalidade dos itens do menu
    const changePhotoBtn = $('#changePhotoBtn');
    if (changePhotoBtn) {
      changePhotoBtn.addEventListener('click', () => {
        alert('Funcionalidade de alterar foto será implementada');
      });
    }
  }

})();
