// Esqueci a senha — Navegação por JS (encodeURI na construção)
console.log("[esqueci-senha.js] carregado");

const form = document.getElementById('forgot-form');
const email = document.getElementById('email');
const sendBtn = document.getElementById('send-btn');
const closeBtn = document.getElementById('close-btn');
const alertBox = document.getElementById('alert');

const modal = document.getElementById('success-modal');
const modalBackdrop = document.querySelector('.modal__backdrop');
const modalOk = document.getElementById('success-ok');

// Utilitário
function buildUrl(...parts){ return encodeURI(parts.join('/')); }

// Caminho relativo para o login (com espaços)
const LOGIN_URL = buildUrl('..', '1. TELA DE LOGIN', 'login.html');

function emailValido(texto){
  const regra = /^[^\s@]+@[^\s@]+\.[^\s@]{2,}$/i;
  return regra.test(texto);
}

function mostrarErro(msg){
  alertBox.style.color = 'var(--danger)';
  alertBox.textContent = msg || 'Informe um e-mail válido.';
  form.classList.add('shake');
  form.addEventListener('animationend', () => form.classList.remove('shake'), { once:true });
  email.setAttribute('aria-invalid','true');
  email.setAttribute('aria-describedby','alert');
}

function limparMensagem(){
  alertBox.textContent = '';
  alertBox.removeAttribute('style');
  email.removeAttribute('aria-invalid');
  email.removeAttribute('aria-describedby');
}

function atualizarBotao(){
  sendBtn.disabled = !emailValido(email.value.trim());
  if (!alertBox.textContent) email.removeAttribute('aria-describedby');
}

function abrirSucesso(){
  sendBtn.classList.remove('is-loading');
  sendBtn.textContent = 'Enviado!';

  modal.classList.add('open');
  modal.setAttribute('aria-hidden','false');
  document.body.classList.add('modal-open');

  setTimeout(() => modalOk.focus(), 50);

  const ir = () => {
    document.body.classList.remove('modal-open');
    window.location.href = LOGIN_URL; // já encodeURI
  };
  modalOk.addEventListener('click', ir, { once:true });
  modalBackdrop?.addEventListener('click', ir, { once:true });
  window.addEventListener('keydown', (e) => { if (e.key === 'Escape') ir(); }, { once:true });

  setTimeout(ir, 1600);
}

['input','change','keyup','paste'].forEach(evt => {
  email.addEventListener(evt, () => { limparMensagem(); atualizarBotao(); });
});

closeBtn.addEventListener('click', () => {
  window.location.href = LOGIN_URL;
});

form.addEventListener('submit', (e) => {
  e.preventDefault();

  const valor = email.value.trim();
  if (!emailValido(valor)){
    mostrarErro('Informe um e-mail válido.');
    atualizarBotao();
    return;
  }

  sendBtn.classList.add('is-loading');
  sendBtn.textContent = 'Enviando...';
  sendBtn.disabled = true;

  setTimeout(() => { abrirSucesso(); }, 600);
});

document.addEventListener('DOMContentLoaded', atualizarBotao);
window.addEventListener('pageshow', atualizarBotao);
setTimeout(atualizarBotao, 50);
setTimeout(atualizarBotao, 400);