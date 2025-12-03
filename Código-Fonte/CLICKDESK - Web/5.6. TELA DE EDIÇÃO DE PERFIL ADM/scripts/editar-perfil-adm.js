// =============================================
// EDITAR PERFIL ADM - JAVASCRIPT
// =============================================

document.addEventListener('DOMContentLoaded', function() {
  // Configuração do menu do usuário na sidebar
  setupUserMenu();
  
  // Configuração dos controles de janela
  setupWindowControls();
  
  // Configuração dos switches de notificação
  setupNotificationSwitches();
  
  // Validação dos formulários
  setupFormValidation();
});

// Menu do usuário na sidebar
function setupUserMenu() {
  const userMenuBtn = document.getElementById('userMenuBtn');
  const userDropdown = document.getElementById('userDropdown');
  const logoutBtn = document.getElementById('logoutBtn');
  const changePhotoBtn = document.getElementById('changePhotoBtn');

  if (userMenuBtn && userDropdown) {
    userMenuBtn.addEventListener('click', function(e) {
      e.stopPropagation();
      const isHidden = userDropdown.hasAttribute('hidden');
      
      if (isHidden) {
        userDropdown.removeAttribute('hidden');
      } else {
        userDropdown.setAttribute('hidden', '');
      }
    });

    document.addEventListener('click', function(e) {
      if (!userMenuBtn.contains(e.target) && !userDropdown.contains(e.target)) {
        userDropdown.setAttribute('hidden', '');
      }
    });
  }

  if (logoutBtn) {
    logoutBtn.addEventListener('click', function() {
      window.location.href = '../15. TELA DE SAIR/logout.html';
    });
  }

  if (changePhotoBtn) {
    changePhotoBtn.addEventListener('click', function() {
      document.getElementById('photoInput').click();
      userDropdown.setAttribute('hidden', '');
    });
  }
}

// Controles de janela
function setupWindowControls() {
  const minimizeBtn = document.querySelector('.window-control.minimize');
  const maximizeBtn = document.querySelector('.window-control.maximize');
  const closeBtn = document.querySelector('.window-control.close');

  if (minimizeBtn) {
    minimizeBtn.addEventListener('click', () => {
      console.log('Minimizar janela');
    });
  }

  if (maximizeBtn) {
    maximizeBtn.addEventListener('click', () => {
      console.log('Maximizar janela');
    });
  }

  if (closeBtn) {
    closeBtn.addEventListener('click', () => {
      window.location.href = '../11. TELA DASHBOARD ADM/dashboard-adm.html';
    });
  }
}

// Switches de notificação
function setupNotificationSwitches() {
  const switches = document.querySelectorAll('.switch input[type="checkbox"]');
  
  switches.forEach(switchEl => {
    // Inicializar estado visual
    updateSwitchState(switchEl);
    
    // Listener para mudanças
    switchEl.addEventListener('change', function() {
      updateSwitchState(this);
      console.log('Preferência alterada:', this.checked);
    });
  });
}

function updateSwitchState(switchInput) {
  const slider = switchInput.nextElementSibling;
  if (slider && slider.classList.contains('slider')) {
    // O CSS já cuida do estado visual através do :checked
    // Aqui você pode salvar as preferências
  }
}

// Validação dos formulários
function setupFormValidation() {
  const personalForm = document.querySelector('.auth-form');
  const passwordForm = document.querySelector('.password-form');

  if (personalForm) {
    personalForm.addEventListener('submit', function(e) {
      e.preventDefault();
      
      const nome = document.getElementById('nome').value.trim();
      const email = document.getElementById('email').value.trim();
      const telefone = document.getElementById('telefone').value.trim();
      const departamento = document.getElementById('departamento').value;

      // Validações
      if (!nome) {
        showAlert('Por favor, preencha o nome completo.', 'error');
        return;
      }

      if (!email || !isValidEmail(email)) {
        showAlert('Por favor, informe um e-mail válido.', 'error');
        return;
      }

      if (!departamento) {
        showAlert('Por favor, selecione um departamento.', 'error');
        return;
      }

      // Sucesso
      showAlert('Informações atualizadas com sucesso!', 'success');
      console.log('Dados salvos:', { nome, email, telefone, departamento });
    });
  }

  if (passwordForm) {
    const updatePasswordBtn = passwordForm.querySelector('.btn-primary');
    if (updatePasswordBtn) {
      updatePasswordBtn.addEventListener('click', function(e) {
        e.preventDefault();
        
        const senhaAtual = document.getElementById('senha-atual').value;
        const novaSenha = document.getElementById('nova-senha').value;
        const confirmaSenha = document.getElementById('confirma-senha').value;

        // Validações
        if (!senhaAtual) {
          showAlert('Por favor, informe a senha atual.', 'error');
          return;
        }

        if (!novaSenha) {
          showAlert('Por favor, informe a nova senha.', 'error');
          return;
        }

        if (novaSenha.length < 8) {
          showAlert('A senha deve ter no mínimo 8 caracteres.', 'error');
          return;
        }

        if (!/[A-Z]/.test(novaSenha)) {
          showAlert('A senha deve conter pelo menos uma letra maiúscula.', 'error');
          return;
        }

        if (!/[0-9]/.test(novaSenha)) {
          showAlert('A senha deve conter pelo menos um número.', 'error');
          return;
        }

        if (novaSenha !== confirmaSenha) {
          showAlert('As senhas não coincidem.', 'error');
          return;
        }

        // Sucesso
        showAlert('Senha atualizada com sucesso!', 'success');
        // Limpar campos
        document.getElementById('senha-atual').value = '';
        document.getElementById('nova-senha').value = '';
        document.getElementById('confirma-senha').value = '';
        
        console.log('Senha atualizada');
      });
    }
  }
}

// Funções auxiliares
function isValidEmail(email) {
  const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
  return emailRegex.test(email);
}

function showAlert(message, type = 'info') {
  // Criar alerta temporário
  const alert = document.createElement('div');
  alert.className = `alert alert-${type}`;
  alert.style.cssText = `
    position: fixed;
    top: 80px;
    right: 24px;
    padding: 16px 24px;
    background: ${type === 'success' ? '#10b981' : type === 'error' ? '#ef4444' : '#3b82f6'};
    color: white;
    border-radius: var(--radius-md);
    box-shadow: var(--shadow-lg);
    z-index: 9999;
    animation: slideIn 0.3s ease;
  `;
  alert.textContent = message;
  
  document.body.appendChild(alert);
  
  setTimeout(() => {
    alert.style.animation = 'slideOut 0.3s ease';
    setTimeout(() => alert.remove(), 300);
  }, 3000);
}

// Funções para foto de perfil (mantidas do código original)
function previewPhoto(event) {
  const file = event.target.files[0];
  if (file) {
    if (!file.type.startsWith('image/')) {
      showAlert('Por favor, selecione apenas arquivos de imagem.', 'error');
      return;
    }
    if (file.size > 5 * 1024 * 1024) {
      showAlert('A imagem deve ter no máximo 5MB.', 'error');
      return;
    }
    
    const reader = new FileReader();
    reader.onload = function(e) {
      document.getElementById('profilePhotoPreview').src = e.target.result;
      showAlert('Foto atualizada com sucesso!', 'success');
    };
    reader.readAsDataURL(file);
  }
}

function removePhoto() {
  document.getElementById('profilePhotoPreview').src = 'https://ui-avatars.com/api/?name=Usuario&background=F28A1A&color=fff&bold=true&size=120';
  document.getElementById('photoInput').value = '';
  showAlert('Foto removida com sucesso!', 'success');
}

// Animações CSS
const style = document.createElement('style');
style.textContent = `
  @keyframes slideIn {
    from {
      transform: translateX(400px);
      opacity: 0;
    }
    to {
      transform: translateX(0);
      opacity: 1;
    }
  }
  
  @keyframes slideOut {
    from {
      transform: translateX(0);
      opacity: 1;
    }
    to {
      transform: translateX(400px);
      opacity: 0;
    }
  }
`;
document.head.appendChild(style);