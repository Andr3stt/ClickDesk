// Lógica da tela de Termos de Uso
document.addEventListener('DOMContentLoaded', function() {
  const acceptCheckbox = document.getElementById('accept-terms');
  const acceptBtn = document.getElementById('accept-btn');

  // Verificar se há um usuário pendente de aceitar termos
  const pendingUser = localStorage.getItem('pendingTermsUser');
  
  // Permitir visualização da tela mesmo sem usuário pendente
  // Se não houver usuário pendente, apenas mostrar os termos sem funcionalidade de aceite
  if (!pendingUser) {
    console.log('Visualizando termos sem usuário pendente');
    // Não redirecionar - permitir leitura dos termos
  }

  // Habilita/desabilita botão baseado no checkbox
  acceptCheckbox.addEventListener('change', function() {
    acceptBtn.disabled = !this.checked;
  });

  // Ao clicar em aceitar, marca que o usuário aceitou os termos
  acceptBtn.addEventListener('click', function() {
    if (acceptCheckbox.checked) {
      if (pendingUser) {
        // Obter lista de usuários
        let usuarios = {};
        try {
          const usuariosStorage = localStorage.getItem('registeredUsers');
          if (usuariosStorage) {
            usuarios = JSON.parse(usuariosStorage);
          }
        } catch (e) {
          usuarios = {};
        }

        // Marcar que o usuário aceitou os termos
        if (usuarios[pendingUser]) {
          usuarios[pendingUser].termosAceitos = true;
          usuarios[pendingUser].termosAceitosEm = new Date().toISOString();
          localStorage.setItem('registeredUsers', JSON.stringify(usuarios));
        }

        // Remover flag de pendente
        localStorage.removeItem('pendingTermsUser');

        // Redirecionar para login
        window.location.href = '../1. TELA DE LOGIN/login.html';
      } else {
        // Se não há usuário pendente, apenas redirecionar para login
        alert('Termos aceitos! Redirecionando para login.');
        window.location.href = '../1. TELA DE LOGIN/login.html';
      }
    }
  });
});
