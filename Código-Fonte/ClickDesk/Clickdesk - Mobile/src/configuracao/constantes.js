// 🔑 URL base da API Backend
export const API_BASE_URL = process.env.REACT_APP_API_URL || 'http://localhost:8080';

// 📡 Endpoints da API
export const ENDPOINTS = {
  // Autenticação
  AUTH_REGISTER: '/auth/register',
  AUTH_VERIFY_EMAIL: '/auth/verify-email',
  AUTH_SET_PASSWORD: '/auth/set-password',
  AUTH_LOGIN: '/auth/login',
  
  // Chamados
  CHAMADOS: '/api/chamados',
  CHAMADOS_MEUS: '/api/chamados/meus',
  CHAMADOS_TECNICO: '/api/chamados/tecnico',
  CHAMADOS_PENDENTES: '/api/chamados/pendentes',
  CHAMADOS_STATUS: (id) => `/api/chamados/${id}/status`,
  CHAMADOS_FEEDBACK: (id) => `/api/chamados/${id}/feedback`,
  
  // FAQ
  FAQS: '/api/faqs',
  FAQS_SEARCH: '/api/faqs/search',
  
  // IA
  IA_ASSIST: '/api/ia/assist',
};

// Enums
export const STATUS_CHAMADO = {
  ABERTO: 'ABERTO',
  EM_ATENDIMENTO: 'EM_ATENDIMENTO',
  AGUARDANDO: 'AGUARDANDO',
  RESOLVIDO_AUTOMATICO: 'RESOLVIDO_AUTOMATICO',
  RESOLVIDO: 'RESOLVIDO',
  FECHADO: 'FECHADO',
  ESCALADO: 'ESCALADO'
};

export const SEVERIDADE_CHAMADO = {
  BAIXA: 'BAIXA',
  MEDIA: 'MEDIA',
  ALTA: 'ALTA',
  CRITICA: 'CRITICA'
};

export const CATEGORIA_CHAMADO = {
  SOFTWARE: 'SOFTWARE',
  HARDWARE: 'HARDWARE',
  REDES: 'REDES',
  TREINAMENTO: 'TREINAMENTO',
  OUTROS: 'OUTROS'
};

export const ROLES_USUARIO = {
  USER: 'USER',
  TECH: 'TECH',
  ADMIN: 'ADMIN'
};
