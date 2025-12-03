// =====================================================
// Registro Clickdesk — Ícone minimalista no 1º campo
// =====================================================

console.log("[registro.js] carregado");

const form = document.getElementById('register-form');
const password = document.getElementById('password');
const confirmPassword = document.getElementById('confirm-password');
const email = document.getElementById('email');
const userType = document.getElementById('user-type');
const btn = document.getElementById('register-btn');
const msgBox = document.getElementById('register-error');

const loginLink = document.getElementById('login-link');
const togglePasswordBtn = document.getElementById('toggle-password');

function buildUrl(...parts){ return encodeURI(parts.join('/')); }
const LOGIN_URL = buildUrl('..', '1. TELA DE LOGIN', 'login.html');

// Ícones minimalistas
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
  togglePasswordBtn.innerHTML = shown ? ICON_EYE_OFF : ICON_EYE;
  togglePasswordBtn.setAttribute('aria-label', shown ? 'Ocultar senha' : 'Mostrar senha');
  togglePasswordBtn.setAttribute('aria-pressed', String(shown));
}

function emailValido(txt){
  const regra = /^[^\s@]+@[^\s@]+\.[^\s@]{2,}$/i;
  return regra.test(txt);
}

// Manipula aria-describedby sem remover helps fixos
function addDescribedBy(el, id){
  const current = (el.getAttribute('aria-describedby') || '').trim();
  const set = new Set(current.split(/\s+/).filter(Boolean));
  set.add(id);
  el.setAttribute('aria-describedby', Array.from(set).join(' '));
}
function removeDescribedBy(el, id){
  const current = (el.getAttribute('aria-describedby') || '').trim();
  if (!current) return;
  const list = current.split(/\s+/).filter(Boolean).filter(x => x !== id);
  if (list.length) el.setAttribute('aria-describedby', list.join(' '));
  else el.removeAttribute('aria-describedby');
}

function mostrarErro(texto, campos = []){
  msgBox.style.color = 'var(--danger)';
  msgBox.textContent = texto || 'Verifique os campos destacados.';
  campos.forEach(c => {
    if (!c) return;
    c.setAttribute('aria-invalid','true');
    addDescribedBy(c, 'register-error');
  });
  form.classList.add('shake');
  form.addEventListener('animationend', () => form.classList.remove('shake'), { once:true });
}

function limparMensagem(){
  msgBox.textContent = '';
  msgBox.removeAttribute('style');
  [password, confirmPassword, email].forEach(el => removeDescribedBy(el, 'register-error'));
}

function atualizarBotao(){
  const s = password.value.trim();
  const c = confirmPassword.value.trim();
  const em = email.value.trim();
  const tipo = userType.value.trim();

  if (s) password.removeAttribute('aria-invalid');
  if (c) confirmPassword.removeAttribute('aria-invalid');
  if (em) email.removeAttribute('aria-invalid');
  if (tipo) userType.removeAttribute('aria-invalid');

  const senhaOK = s.length >= 8;
  const confirmaOK = c.length >= 8 && c === s;
  const emailOK = emailValido(em);

  btn.disabled = !(senhaOK && confirmaOK && emailOK && tipo);

  if (!msgBox.textContent) {
    [password, confirmPassword, email, userType].forEach(el => removeDescribedBy(el, 'register-error'));
  }
}

// Olhinho minimalista no 1º campo
togglePasswordBtn.addEventListener('click', () => {
  const isPassword = password.type === 'password';
  password.type = isPassword ? 'text' : 'password';
  setIcon(isPassword);
});

// Digitação e mudanças
[password, confirmPassword, email, userType].forEach(el => {
  ['input','change','keyup','paste'].forEach(evt => {
    el.addEventListener(evt, () => { limparMensagem(); atualizarBotao(); });
  });
});

// Envio
form.addEventListener('submit', (e) => {
  e.preventDefault();

  const s = password.value.trim();
  const c = confirmPassword.value.trim();
  const em = email.value.trim();
  const tipo = userType.value.trim();

  if (!s || !c || !em || !tipo){
    mostrarErro('Preencha todos os campos.', [
      !s && password, !c && confirmPassword, !em && email, !tipo && userType
    ].filter(Boolean));
    atualizarBotao();
    return;
  }
  if (s.length < 8){
    mostrarErro('A senha deve ter pelo menos 8 caracteres.', [password]);
    atualizarBotao();
    return;
  }
  if (c !== s){
    mostrarErro('As senhas não conferem. Confirme sua senha.', [confirmPassword, password]);
    atualizarBotao();
    return;
  }
  if (!emailValido(em)){
    mostrarErro('Informe um e-mail válido.', [email]);
    atualizarBotao();
    return;
  }

  // Salvar dados do novo usuário no localStorage
  const novoUsuario = {
    email: em,
    senha: s,
    tipo: tipo,
    nome: em.split('@')[0], // usa a parte antes do @ como nome
    registradoEm: new Date().toISOString()
  };

  // Obter lista de usuários registrados
  let usuarios = {};
  try {
    const usuariosStorage = localStorage.getItem('registeredUsers');
    if (usuariosStorage) {
      usuarios = JSON.parse(usuariosStorage);
    }
  } catch (e) {
    usuarios = {};
  }

  // Verificar se usuário já existe
  if (usuarios[em.toLowerCase()]) {
    mostrarErro('Este e-mail já está em uso. Escolha outro.', [email]);
    atualizarBotao();
    return;
  }

  // Adicionar novo usuário
  usuarios[em.toLowerCase()] = novoUsuario;
  localStorage.setItem('registeredUsers', JSON.stringify(usuarios));

  // Marcar que o usuário precisa aceitar os termos
  localStorage.setItem('pendingTermsUser', em.toLowerCase());

  btn.classList.add('is-loading');
  btn.textContent = 'Registrando...';
  btn.disabled = true;

  setTimeout(() => {
    btn.classList.remove('is-loading');
    btn.classList.add('is-success');
    btn.textContent = 'Registrado!';
    btn.disabled = true;

    // Redirecionar para tela de termos
    const TERMOS_URL = buildUrl('..', '4. TELA LEIA OS TERMOS', 'leia-termos.html');
    setTimeout(() => {
      window.location.href = TERMOS_URL;
    }, 800);
  }, 500);
});

// Link "Logar" por JS
loginLink.addEventListener('click', (e) => {
  e.preventDefault();
  window.location.href = LOGIN_URL;
});

// Inicial
setIcon(false); // senha começa oculta
document.addEventListener('DOMContentLoaded', atualizarBotao);
window.addEventListener('pageshow', atualizarBotao);
setTimeout(atualizarBotao, 50);
setTimeout(atualizarBotao, 400);