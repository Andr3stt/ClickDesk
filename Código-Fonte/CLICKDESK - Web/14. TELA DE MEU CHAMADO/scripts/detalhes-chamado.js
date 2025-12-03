// ===========================
// Login Clickdesk - Lógica
// Foco: simples e didático para iniciantes
// O que este arquivo faz?
// 1) Habilita o botão "Entrar" quando os campos têm conteúdo
// 2) Mostra mensagens de erro amigáveis
// 3) Deixa pronto um "gancho" para integrar com a API (sem chamar nada por enquanto)
// ===========================

// 1) Pegamos elementos da tela (DOM)
const loginForm = document.getElementById('login-form');
const usernameInput = document.getElementById('username');
const passwordInput = document.getElementById('password');
const loginBtn = document.getElementById('login-btn');
const errorDiv = document.getElementById('login-error');

// 2) Função que checa se os campos estão preenchidos e habilita/desabilita o botão
function validarCampos() {
  const usuario = usernameInput.value.trim();
  const senha = passwordInput.value.trim();

  // Habilita o botão se os dois tiverem algum texto
  loginBtn.disabled = !(usuario && senha);

  // Remove marcação de erro visual enquanto digita
  if (usuario) usernameInput.removeAttribute('aria-invalid');
  if (senha) passwordInput.removeAttribute('aria-invalid');
}

// 3) Limpa a mensagem de erro (se houver)
function limparErro() {
  errorDiv.textContent = '';
}

// 4) Mostra uma mensagem de erro e marca os campos (acessibilidade)
function mostrarErro(mensagem, marcarCampos = []) {
  errorDiv.textContent = mensagem || 'Ocorreu um erro.';
  marcarCampos.forEach(el => el.setAttribute('aria-invalid', 'true'));

  // Pequena animação para chamar atenção (desabilitada se o usuário preferir menos movimento)
  loginForm.classList.add('shake');
  loginForm.addEventListener('animationend', () => loginForm.classList.remove('shake'), { once: true });
}

// 5) "Ganchos" de eventos: quando o usuário digita, validamos em tempo real
usernameInput.addEventListener('input', () => { validarCampos(); limparErro(); });
passwordInput.addEventListener('input', () => { validarCampos(); limparErro(); });

// 6) Envio do formulário (aqui você conectará a API no futuro)
loginForm.addEventListener('submit', (e) => {
  e.preventDefault(); // Evita recarregar a página

  const usuario = usernameInput.value.trim();
  const senha = passwordInput.value.trim();

  // Validação simples do lado do cliente (antes da API)
  if (!usuario || !senha) {
    mostrarErro('Preencha todos os campos.', [!usuario ? usernameInput : null, !senha ? passwordInput : null].filter(Boolean));
    return;
  }

  // === Integração com API (futuro) ===
  // Exemplo de estrutura (não executa nada agora):
  /*
  loginBtn.disabled = true;              // Evita clique duplo
  loginBtn.textContent = 'Entrando...';  // Feedback de carregamento

  fetch('/api/login', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ usuario, senha })
  })
  .then(res => res.ok ? res.json() : Promise.reject(res))
  .then(data => {
    if (data.sucesso) {
      window.location.href = '/dashboard/dashboard.html';
    } else {
      mostrarErro(data.mensagem || 'Usuário ou senha inválidos.', [usernameInput, passwordInput]);
    }
  })
  .catch(() => {
    mostrarErro('Erro ao conectar com o servidor. Tente novamente mais tarde.');
  })
  .finally(() => {
    loginBtn.disabled = false;
    loginBtn.textContent = 'Entrar';
  });
  */

  // Enquanto a API não existe, simulamos um erro amigável:
  mostrarErro('Usuário ou senha inválidos.', [usernameInput, passwordInput]);
});

// 7) Ações dos links
document.getElementById('forgot-password').addEventListener('click', (e) => {
  e.preventDefault();
  // Redireciona para a tela de "Esqueci a senha"
  window.location.href = '../3. TELA ESQUECI SENHA/esqueci-senha.html';
});

document.getElementById('register-link').addEventListener('click', (e) => {
  e.preventDefault();
  // Redireciona para a tela de registro
  window.location.href = '../2. TELA DE REGISTRO/registro.html';
});

// 8) Estado inicial: valida uma vez para manter o botão no estado correto
validarCampos();