// ===========================
// Login Clickdesk - Lógica
// Ícone minimalista para mostrar/ocultar senha
// ===========================

const loginForm = document.getElementById('login-form');
const usernameInput = document.getElementById('username');
const passwordInput = document.getElementById('password');
const togglePasswordBtn = document.getElementById('toggle-password');
const loginBtn = document.getElementById('login-btn');
const errorDiv = document.getElementById('login-error');

// SVGs minimalistas (outline), herdam cor de currentColor
const ICON_EYE =
  '<svg aria-hidden="true" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">' +
    '<path d="M2 12c2.5-4 6.5-7 10-7s7.5 3 10 7c-2.5 4-6.5 7-10 7S4.5 16 2 12z"/>' +
    '<circle cx="12" cy="12" r="3"/>' +
  '</svg>';

const ICON_EYE_OFF =
  '<svg aria-hidden="true" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">' +
    '<path d="M2 12c2.5-4 6.5-7 10-7 2.1 0 4 .6 5.7 1.6"/>' +
    '<path d="M22 12c-2.5 4-6.5 7-10 7-2.1 0-4-.6-5.7-1.6"/>' +
    '<path d="M9.5 9.5a3 3 0 1 0 4.95 4.95"/>' +
    '<line x1="3" y1="3" x2="21" y2="21"/>' +
  '</svg>';

function setIcon(shown) {
  // shown = senha visível? então mostra "eye-off" para indicar ocultar
  togglePasswordBtn.innerHTML = shown ? ICON_EYE_OFF : ICON_EYE;
  togglePasswordBtn.setAttribute('aria-label', shown ? 'Ocultar senha' : 'Mostrar senha');
  togglePasswordBtn.setAttribute('aria-pressed', String(shown));
}

function validarCampos() {
  const usuario = usernameInput.value.trim();
  const senha = passwordInput.value.trim();
  loginBtn.disabled = !(usuario && senha);

  if (usuario) usernameInput.removeAttribute('aria-invalid');
  if (senha) passwordInput.removeAttribute('aria-invalid');

  if (!errorDiv.textContent) {
    usernameInput.removeAttribute('aria-describedby');
    passwordInput.removeAttribute('aria-describedby');
  }
}

function limparErro() {
  errorDiv.textContent = '';
  usernameInput.removeAttribute('aria-describedby');
  passwordInput.removeAttribute('aria-describedby');
}

function mostrarErro(mensagem, campos = []) {
  errorDiv.textContent = mensagem || 'Ocorreu um erro.';
  campos.forEach(el => {
    el.setAttribute('aria-invalid', 'true');
    el.setAttribute('aria-describedby', 'login-error');
  });

  loginForm.classList.add('shake');
  loginForm.addEventListener('animationend', () => loginForm.classList.remove('shake'), { once: true });
}

usernameInput.addEventListener('input', () => { validarCampos(); limparErro(); });
passwordInput.addEventListener('input', () => { validarCampos(); limparErro(); });

togglePasswordBtn.addEventListener('click', () => {
  const isPassword = passwordInput.type === 'password';
  passwordInput.type = isPassword ? 'text' : 'password';
  setIcon(isPassword); // se era password, agora está visível (shown = true)
});

loginForm.addEventListener('submit', async (e) => {
  e.preventDefault();

  const usuario = usernameInput.value.trim();
  const senha = passwordInput.value.trim();

  if (!usuario || !senha) {
    mostrarErro('Preencha todos os campos.', [!usuario ? usernameInput : null, !senha ? passwordInput : null].filter(Boolean));
    return;
  }

  // Desabilitar botão durante login
  loginBtn.disabled = true;
  loginBtn.textContent = 'Entrando...';

  try {
    // Simular chamada de API
    await new Promise(resolve => setTimeout(resolve, 800));

    // Base de usuários padrão para demonstração
    const usuariosPadrao = {
      // Técnicos/Administradores
      'admin': { senha: 'admin', tipo: 'tecnico', nome: 'Administrador' },
      'tecnico': { senha: 'tecnico', tipo: 'tecnico', nome: 'Técnico Suporte' },
      
      // Usuários comuns
      'vini': { senha: '123', tipo: 'usuario', nome: 'Vinicius' },
      'usuario': { senha: '123', tipo: 'usuario', nome: 'Usuário Comum' }
    };

    // Buscar usuários registrados no localStorage
    let usuariosRegistrados = {};
    try {
      const storage = localStorage.getItem('registeredUsers');
      if (storage) {
        const dados = JSON.parse(storage);
        // Converter formato do registro para formato de login
        Object.keys(dados).forEach(key => {
          const user = dados[key];
          usuariosRegistrados[key] = {
            senha: user.senha,
            tipo: user.tipo,
            nome: user.nome || user.username,
            termosAceitos: user.termosAceitos || false
          };
        });
      }
    } catch (e) {
      console.error('Erro ao carregar usuários registrados:', e);
    }

    // Combinar usuários padrão com registrados (registrados têm prioridade)
    const todosUsuarios = { ...usuariosPadrao, ...usuariosRegistrados };

    const usuarioEncontrado = todosUsuarios[usuario.toLowerCase()];

    if (!usuarioEncontrado || usuarioEncontrado.senha !== senha) {
      throw new Error('Credenciais inválidas');
    }

    // Verificar se o usuário aceitou os termos (apenas para usuários registrados, não para os padrão)
    const isUsuarioRegistrado = usuariosRegistrados[usuario.toLowerCase()];
    if (isUsuarioRegistrado && !usuarioEncontrado.termosAceitos) {
      // Redirecionar para tela de termos
      localStorage.setItem('pendingTermsUser', usuario.toLowerCase());
      window.location.href = '../4. TELA LEIA OS TERMOS/leia-termos.html';
      return;
    }

    // Salvar dados do usuário no localStorage
    localStorage.setItem('userSession', JSON.stringify({
      username: usuario,
      nome: usuarioEncontrado.nome,
      tipo: usuarioEncontrado.tipo,
      loginTime: new Date().toISOString()
    }));

    // Redirecionar baseado no tipo de usuário
    if (usuarioEncontrado.tipo === 'tecnico') {
      // Técnico/Admin vai para o Dashboard Administrativo
      window.location.href = encodeURI('../11. TELA DASHBOARD ADM/dashboard-adm.html');
    } else {
      // Usuário comum vai para o Dashboard normal
      window.location.href = encodeURI('../6. TELA DE DASHBOARD/dashboard.html');
    }

  } catch (error) {
    mostrarErro('Usuário ou senha inválidos.', [usernameInput, passwordInput]);
    loginBtn.disabled = false;
    loginBtn.textContent = 'Entrar';
  }
});

// Estado inicial
setIcon(false); // senha começa oculta
validarCampos();