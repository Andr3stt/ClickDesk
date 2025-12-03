// Funcionalidades da tela de logout

document.addEventListener('DOMContentLoaded', function() {
    console.log('Tela de logout carregada');
    
    // Elementos da página
    const progressFill = document.querySelector('.progress-fill');
    const progressText = document.querySelector('.progress-text');
    const loginBtn = document.getElementById('loginBtn');
    const homeBtn = document.getElementById('homeBtn');
    
    // Dados da sessão (normalmente viriam do localStorage ou API)
    const sessionData = getSessionData();
    
    // Inicializar componentes
    initializeSessionInfo();
    initializeProgressBar();
    initializeEventListeners();
    
    // Mostrar informações da sessão
    function initializeSessionInfo() {
        const sessionUser = document.getElementById('sessionUser');
        const sessionTime = document.getElementById('sessionTime');
        
        if (sessionUser && sessionData.user) {
            sessionUser.textContent = sessionData.user;
        }
        
        if (sessionTime && sessionData.loginTime) {
            sessionTime.textContent = formatSessionTime(sessionData.loginTime);
        }
    }
    
    // Inicializar barra de progresso
    function initializeProgressBar() {
        let progress = 0;
        const duration = 3000; // 3 segundos
        const interval = 50; // Atualizar a cada 50ms
        const increment = (100 / duration) * interval;
        
        const progressInterval = setInterval(() => {
            progress += increment;
            
            if (progress >= 100) {
                progress = 100;
                clearInterval(progressInterval);
                progressText.textContent = 'Logout concluído com sucesso!';
                
                // Ativar botões após completar
                enableActionButtons();
            } else {
                progressText.textContent = `Finalizando sessão... ${Math.round(progress)}%`;
            }
            
            progressFill.style.width = progress + '%';
        }, interval);
    }
    
    // Ativar botões de ação
    function enableActionButtons() {
        if (loginBtn) {
            loginBtn.classList.remove('disabled');
            loginBtn.style.opacity = '1';
            loginBtn.style.pointerEvents = 'auto';
        }
        
        if (homeBtn) {
            homeBtn.classList.remove('disabled');
            homeBtn.style.opacity = '1';
            homeBtn.style.pointerEvents = 'auto';
        }
        
        // Adicionar animação de destaque
        setTimeout(() => {
            if (loginBtn) loginBtn.style.animation = 'pulse 1s ease-in-out';
            if (homeBtn) homeBtn.style.animation = 'pulse 1s ease-in-out';
        }, 500);
    }
    
    // Event listeners
    function initializeEventListeners() {
        // Botão de voltar ao login
        if (loginBtn) {
            loginBtn.addEventListener('click', function(e) {
                e.preventDefault();
                handleLoginRedirect();
            });
        }
        
        // Botão de ir para home
        if (homeBtn) {
            homeBtn.addEventListener('click', function(e) {
                e.preventDefault();
                handleHomeRedirect();
            });
        }
        
        // Teclas de atalho
        document.addEventListener('keydown', function(e) {
            if (e.key === 'Enter') {
                handleLoginRedirect();
            } else if (e.key === 'Escape') {
                handleHomeRedirect();
            }
        });
        
        // Desabilitar botões inicialmente
        disableActionButtons();
    }
    
    // Desabilitar botões durante o progresso
    function disableActionButtons() {
        if (loginBtn) {
            loginBtn.style.opacity = '0.5';
            loginBtn.style.pointerEvents = 'none';
        }
        
        if (homeBtn) {
            homeBtn.style.opacity = '0.5';
            homeBtn.style.pointerEvents = 'none';
        }
    }
    
    // Redirecionar para login
    function handleLoginRedirect() {
        console.log('Redirecionando para login...');
        
        // Limpar dados da sessão
        clearSessionData();
        
        // Mostrar feedback visual
        showRedirectFeedback('Redirecionando para login...');
        
        // Redirecionar após delay
        setTimeout(() => {
            window.location.href = '../1. TELA DE LOGIN/login.html';
        }, 1000);
    }
    
    // Redirecionar para home
    function handleHomeRedirect() {
        console.log('Redirecionando para página inicial...');
        
        // Limpar dados da sessão
        clearSessionData();
        
        // Mostrar feedback visual
        showRedirectFeedback('Redirecionando para página inicial...');
        
        // Redirecionar após delay
        setTimeout(() => {
            window.location.href = '../index.html';
        }, 1000);
    }
    
    // Mostrar feedback de redirecionamento
    function showRedirectFeedback(message) {
        if (progressText) {
            progressText.textContent = message;
            progressText.style.color = 'var(--brand)';
            progressText.style.fontWeight = '600';
        }
        
        // Animar barra de progresso para 100%
        if (progressFill) {
            progressFill.style.background = 'linear-gradient(90deg, #28a745, #20c997)';
            progressFill.style.width = '100%';
        }
    }
    
    // Obter dados da sessão
    function getSessionData() {
        try {
            // Tentar obter do localStorage
            const userData = localStorage.getItem('clickdesk_user');
            const loginTime = localStorage.getItem('clickdesk_login_time');
            
            if (userData && loginTime) {
                return {
                    user: JSON.parse(userData).nome || 'Usuário',
                    loginTime: new Date(loginTime)
                };
            }
        } catch (error) {
            console.warn('Erro ao obter dados da sessão:', error);
        }
        
        // Dados padrão se não encontrar no localStorage
        return {
            user: 'Usuário',
            loginTime: new Date() // Hora atual como fallback
        };
    }
    
    // Limpar dados da sessão
    function clearSessionData() {
        try {
            // Limpar localStorage
            localStorage.removeItem('clickdesk_user');
            localStorage.removeItem('clickdesk_login_time');
            localStorage.removeItem('clickdesk_session_token');
            localStorage.removeItem('clickdesk_user_preferences');
            
            // Limpar sessionStorage
            sessionStorage.clear();
            
            console.log('Dados da sessão limpos com sucesso');
        } catch (error) {
            console.error('Erro ao limpar dados da sessão:', error);
        }
    }
    
    // Formatar tempo da sessão
    function formatSessionTime(loginTime) {
        try {
            const now = new Date();
            const diffMs = now - loginTime;
            const diffMins = Math.floor(diffMs / (1000 * 60));
            const diffHours = Math.floor(diffMins / 60);
            
            if (diffHours > 0) {
                const remainingMins = diffMins % 60;
                return `${diffHours}h ${remainingMins}min`;
            } else if (diffMins > 0) {
                return `${diffMins} minutos`;
            } else {
                return 'Menos de 1 minuto';
            }
        } catch (error) {
            console.warn('Erro ao calcular tempo da sessão:', error);
            return 'Sessão ativa';
        }
    }
    
    // Função para simular logout em API (para desenvolvimento futuro)
    function performLogout() {
        return new Promise((resolve) => {
            // Simular chamada de API
            setTimeout(() => {
                console.log('Logout realizado com sucesso na API');
                resolve(true);
            }, 1000);
        });
    }
    
    // Verificar se há dados de sessão ao carregar
    function checkSessionOnLoad() {
        const hasSession = localStorage.getItem('clickdesk_user') || sessionStorage.getItem('clickdesk_user');
        
        if (!hasSession) {
            console.warn('Nenhuma sessão ativa encontrada');
            // Ainda assim permitir o uso da tela de logout
        }
    }
    
    // Executar verificação inicial
    checkSessionOnLoad();
    
    // Log para depuração
    console.log('Sistema de logout inicializado', {
        sessionData: sessionData,
        timestamp: new Date().toISOString()
    });
});

// Função para logout rápido (pode ser chamada de outras páginas)
function quickLogout() {
    // Limpar dados
    localStorage.clear();
    sessionStorage.clear();
    
    // Redirecionar para login
    window.location.href = '../1. TELA DE LOGIN/login.html';
}

// Expor função globalmente se necessário
window.quickLogout = quickLogout;