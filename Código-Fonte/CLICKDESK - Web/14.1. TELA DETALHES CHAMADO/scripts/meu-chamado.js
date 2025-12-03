document.addEventListener('DOMContentLoaded', function() {
    // Elementos DOM
    const ticketNumber = document.getElementById('ticketNumber');
    const ticketDate = document.getElementById('ticketDate');
    const ticketStatus = document.getElementById('ticketStatus');
    const requesterName = document.getElementById('requesterName');
    const department = document.getElementById('department');
    const category = document.getElementById('category');
    const priority = document.getElementById('priority');
    const subject = document.getElementById('subject');
    const description = document.getElementById('description');
    const attachments = document.getElementById('attachments');
    const backBtn = document.getElementById('backBtn');
    const refreshBtn = document.getElementById('refreshBtn');
    const closeTicketBtn = document.getElementById('closeTicketBtn');

    function getUrlParameter(name) {
        const urlParams = new URLSearchParams(window.location.search);
        return urlParams.get(name);
    }

    const statusLabels = {
        open: 'Aberto',
        progress: 'Em progresso', 
        waiting: 'Em espera',
        closed: 'Fechado'
    };

    const priorityLabels = {
        low: 'Baixa',
        medium: 'Média', 
        high: 'Alta'
    };

    const allTickets = {
        '1': {
            id: '1001',
            date: '2025-11-01',
            status: 'open',
            requester: 'João Silva',
            department: 'Tecnologia da Informação',
            category: 'Software',
            priority: 'high',
            subject: 'Sistema lento após atualização',
            description: 'O sistema está muito lento após a última atualização. Todos os processos estão demorando mais que o normal.',
            attachments: []
        },
        '2': {
            id: '1002',
            date: '2025-10-30',
            status: 'progress',
            requester: 'Maria Santos',
            department: 'Recursos Humanos',
            category: 'Hardware',
            priority: 'medium',
            subject: 'Impressora não funciona',
            description: 'A impressora do setor parou de funcionar. A luz vermelha está piscando.',
            attachments: []
        }
    };

    function formatDate(dateStr) {
        try {
            const d = new Date(dateStr);
            const day = String(d.getDate()).padStart(2, '0');
            const month = String(d.getMonth() + 1).padStart(2, '0');
            const year = String(d.getFullYear()).slice(-2);
            return day + '/' + month + '/' + year;
        } catch (error) {
            return dateStr;
        }
    }

    function loadTicketData() {
        const ticketId = getUrlParameter('id') || '1';
        const ticketData = allTickets[ticketId];

        if (!ticketData) {
            alert('Chamado não encontrado!');
            window.location.href = '../9. TELA DE MEU CHAMADO/meus-chamado.html';
            return;
        }

        if (ticketNumber) ticketNumber.textContent = 'CD-' + ticketData.id;
        if (ticketDate) ticketDate.textContent = formatDate(ticketData.date);
        
        if (ticketStatus) {
            ticketStatus.className = 'badge ' + ticketData.status;
            ticketStatus.textContent = statusLabels[ticketData.status] || 'Desconhecido';
        }
        
        if (requesterName) requesterName.textContent = ticketData.requester;
        if (department) department.textContent = ticketData.department;
        if (category) category.textContent = ticketData.category;
        
        if (priority) {
            const prioSpan = priority.querySelector('.prio');
            if (prioSpan) {
                prioSpan.className = 'prio ' + ticketData.priority;
                prioSpan.textContent = priorityLabels[ticketData.priority] || 'Média';
            }
        }
        
        if (subject) subject.textContent = ticketData.subject;
        if (description) {
            description.innerHTML = ticketData.description.replace(/\n/g, '<br>');
        }
        
        if (attachments) {
            attachments.innerHTML = '<span class="muted">Nenhum anexo</span>';
        }
    }

    if (backBtn) {
        backBtn.addEventListener('click', function() {
            window.location.href = '../9. TELA DE MEU CHAMADO/meus-chamado.html';
        });
    }

    if (refreshBtn) {
        refreshBtn.addEventListener('click', function() {
            loadTicketData();
        });
    }

    if (closeTicketBtn) {
        closeTicketBtn.addEventListener('click', function() {
            if (confirm('Deseja realmente fechar este chamado?')) {
                alert('Chamado fechado com sucesso!');
                window.location.href = '../9. TELA DE MEU CHAMADO/meus-chamado.html';
            }
        });
    }

    // Setup user menu
    setupUserMenu();

    loadTicketData();
});

// User menu functionality
function setupUserMenu() {
    const userAvatar = document.querySelector('.user-avatar');
    const userDropdown = document.querySelector('.user-dropdown');
    const userName = document.getElementById('userName');
    const userEmail = document.getElementById('userEmail');

    if (!userAvatar || !userDropdown) return;

    const userSession = JSON.parse(localStorage.getItem('userSession') || '{}');
    const name = userSession.nome || 'Usuário';
    const email = userSession.username || 'usuario@email.com';

    // Update dropdown info
    if (userName) userName.textContent = name;
    if (userEmail) userEmail.textContent = email;

    // Toggle dropdown
    userAvatar.addEventListener('click', (e) => {
        e.stopPropagation();
        const isHidden = userDropdown.hasAttribute('hidden');
        if (isHidden) {
            userDropdown.removeAttribute('hidden');
        } else {
            userDropdown.setAttribute('hidden', '');
        }
    });

    // Close dropdown when clicking outside
    document.addEventListener('click', (e) => {
        if (!userDropdown.hasAttribute('hidden') && !userDropdown.contains(e.target)) {
            userDropdown.setAttribute('hidden', '');
        }
    });

    // Logout functionality
    const logoutBtn = document.getElementById('logoutBtn');
    if (logoutBtn) {
        logoutBtn.addEventListener('click', (e) => {
            e.preventDefault();
            localStorage.removeItem('userSession');
            window.location.href = '../1. TELA DE LOGIN/login.html';
        });
    }

    // Change photo functionality
    const changePhotoBtn = document.getElementById('changePhotoBtn');
    if (changePhotoBtn) {
        changePhotoBtn.addEventListener('click', () => {
            alert('Funcionalidade de alterar foto em desenvolvimento!');
        });
    }
}
