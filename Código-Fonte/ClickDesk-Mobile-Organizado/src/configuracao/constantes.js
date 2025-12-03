/**
 * Constantes da Aplicação ClickDesk Mobile
 * 
 * Este arquivo centraliza todos os endpoints da API e enums utilizados
 * na aplicação. Mapeado a partir da API Backend em C#.
 */

// ============================================
// CONFIGURAÇÃO DA API
// ============================================

/**
 * URL base da API
 * Pode ser sobrescrita via variável de ambiente REACT_APP_API_URL
 */
export const API_BASE_URL = process.env.REACT_APP_API_URL || 'http://localhost:8080';

/**
 * Timeout padrão para requisições (em milissegundos)
 */
export const API_TIMEOUT = process.env.REACT_APP_TIMEOUT || 30000;

/**
 * Timeout para upload de arquivos (em milissegundos)
 */
export const API_UPLOAD_TIMEOUT = 120000;

// ============================================
// ENDPOINTS DE AUTENTICAÇÃO
// ============================================

export const AUTH_ENDPOINTS = {
  /** POST - Realizar login */
  LOGIN: '/auth/login',
  
  /** POST - Registrar novo usuário */
  REGISTER: '/auth/register',
  
  /** POST - Realizar logout */
  LOGOUT: '/auth/logout',
  
  /** POST - Renovar token de autenticação */
  REFRESH_TOKEN: '/auth/refresh',
  
  /** POST - Solicitar recuperação de senha */
  FORGOT_PASSWORD: '/auth/forgot-password',
  
  /** POST - Redefinir senha */
  RESET_PASSWORD: '/auth/reset-password',
  
  /** GET - Verificar email */
  VERIFY_EMAIL: '/auth/verify-email',
};

// ============================================
// ENDPOINTS DE CHAMADOS
// ============================================

export const CHAMADO_ENDPOINTS = {
  /** GET - Listar todos os chamados (Admin/Tech) */
  /** POST - Criar novo chamado */
  BASE: '/api/chamados',
  
  /** GET - Listar chamados do usuário logado */
  MEUS: '/api/chamados/meus',
  
  /** GET - Obter estatísticas para dashboard */
  STATS: '/api/chamados/stats',
  
  /** GET - Listar chamados pendentes */
  PENDENTES: '/api/chamados/pendentes',
  
  /**
   * Retorna o endpoint de um chamado específico
   * @param {number} id - ID do chamado
   */
  BY_ID: (id) => `/api/chamados/${id}`,
  
  /**
   * Retorna o endpoint de feedback de um chamado
   * @param {number} id - ID do chamado
   */
  FEEDBACK: (id) => `/api/chamados/${id}/feedback`,
  
  /**
   * Retorna o endpoint de comentários de um chamado
   * @param {number} id - ID do chamado
   */
  COMENTARIOS: (id) => `/api/chamados/${id}/comentarios`,
  
  /**
   * Retorna o endpoint de atualização de status
   * @param {number} id - ID do chamado
   */
  STATUS: (id) => `/api/chamados/${id}/status`,
};

// ============================================
// ENDPOINTS DE FAQ
// ============================================

export const FAQ_ENDPOINTS = {
  /** GET - Listar todas as FAQs */
  /** POST - Criar nova FAQ (Admin) */
  BASE: '/api/faqs',
  
  /** GET - Buscar FAQs por termo */
  SEARCH: '/api/faqs/search',
  
  /**
   * Retorna o endpoint de uma FAQ específica
   * @param {number} id - ID da FAQ
   */
  BY_ID: (id) => `/api/faqs/${id}`,
};

// ============================================
// ENDPOINTS DE INTELIGÊNCIA ARTIFICIAL
// ============================================

export const IA_ENDPOINTS = {
  /** POST - Solicitar assistência da IA */
  ASSIST: '/api/ia/assist',
  
  /** POST - Analisar problema */
  ANALYZE: '/api/ia/analyze',
};

// ============================================
// ENDPOINTS DE USUÁRIOS
// ============================================

export const USER_ENDPOINTS = {
  /** GET - Listar todos os usuários (Admin) */
  /** POST - Criar novo usuário (Admin) */
  BASE: '/api/users',
  
  /** GET - Obter perfil do usuário logado */
  /** PUT - Atualizar perfil do usuário logado */
  PROFILE: '/api/users/profile',
  
  /** PUT - Alterar senha */
  CHANGE_PASSWORD: '/api/users/change-password',
  
  /**
   * Retorna o endpoint de um usuário específico
   * @param {number} id - ID do usuário
   */
  BY_ID: (id) => `/api/users/${id}`,
};

// ============================================
// ENUMS - STATUS DO CHAMADO
// ============================================

export const STATUS_CHAMADO = {
  /** Chamado aguardando análise */
  PENDENTE: 'PENDENTE',
  
  /** Chamado em análise pela IA */
  EM_ANALISE: 'EM_ANALISE',
  
  /** Chamado sendo atendido por técnico */
  EM_ATENDIMENTO: 'EM_ATENDIMENTO',
  
  /** Chamado resolvido com sucesso */
  RESOLVIDO: 'RESOLVIDO',
  
  /** Chamado cancelado */
  CANCELADO: 'CANCELADO',
  
  /** Chamado reaberto após feedback negativo */
  REABERTO: 'REABERTO',
};

// ============================================
// ENUMS - PRIORIDADE DO CHAMADO
// ============================================

export const PRIORIDADE_CHAMADO = {
  /** Baixa prioridade */
  BAIXA: 'BAIXA',
  
  /** Prioridade média */
  MEDIA: 'MEDIA',
  
  /** Alta prioridade */
  ALTA: 'ALTA',
  
  /** Prioridade crítica */
  CRITICA: 'CRITICA',
};

// ============================================
// ENUMS - CATEGORIA DO CHAMADO
// ============================================

export const CATEGORIA_CHAMADO = {
  /** Problemas de hardware */
  HARDWARE: 'HARDWARE',
  
  /** Problemas de software */
  SOFTWARE: 'SOFTWARE',
  
  /** Problemas de rede */
  REDE: 'REDE',
  
  /** Problemas de email */
  EMAIL: 'EMAIL',
  
  /** Solicitações de acesso */
  ACESSO: 'ACESSO',
  
  /** Outros problemas */
  OUTROS: 'OUTROS',
};

// ============================================
// ENUMS - TIPO DE USUÁRIO (ROLE)
// ============================================

export const USER_ROLE = {
  /** Usuário comum */
  USER: 'USER',
  
  /** Técnico de suporte */
  TECH: 'TECH',
  
  /** Administrador do sistema */
  ADMIN: 'ADMIN',
};

// ============================================
// LABELS LEGÍVEIS DOS ENUMS
// ============================================

export const STATUS_LABELS = {
  [STATUS_CHAMADO.PENDENTE]: 'Pendente',
  [STATUS_CHAMADO.EM_ANALISE]: 'Em Análise',
  [STATUS_CHAMADO.EM_ATENDIMENTO]: 'Em Atendimento',
  [STATUS_CHAMADO.RESOLVIDO]: 'Resolvido',
  [STATUS_CHAMADO.CANCELADO]: 'Cancelado',
  [STATUS_CHAMADO.REABERTO]: 'Reaberto',
};

export const PRIORIDADE_LABELS = {
  [PRIORIDADE_CHAMADO.BAIXA]: 'Baixa',
  [PRIORIDADE_CHAMADO.MEDIA]: 'Média',
  [PRIORIDADE_CHAMADO.ALTA]: 'Alta',
  [PRIORIDADE_CHAMADO.CRITICA]: 'Crítica',
};

export const CATEGORIA_LABELS = {
  [CATEGORIA_CHAMADO.HARDWARE]: 'Hardware',
  [CATEGORIA_CHAMADO.SOFTWARE]: 'Software',
  [CATEGORIA_CHAMADO.REDE]: 'Rede',
  [CATEGORIA_CHAMADO.EMAIL]: 'Email',
  [CATEGORIA_CHAMADO.ACESSO]: 'Acesso',
  [CATEGORIA_CHAMADO.OUTROS]: 'Outros',
};

export const ROLE_LABELS = {
  [USER_ROLE.USER]: 'Usuário',
  [USER_ROLE.TECH]: 'Técnico',
  [USER_ROLE.ADMIN]: 'Administrador',
};

// ============================================
// CORES DOS STATUS (para UI)
// ============================================

export const STATUS_CORES = {
  [STATUS_CHAMADO.PENDENTE]: '#f59e0b', // Amarelo/Warning
  [STATUS_CHAMADO.EM_ANALISE]: '#3b82f6', // Azul
  [STATUS_CHAMADO.EM_ATENDIMENTO]: '#8b5cf6', // Roxo
  [STATUS_CHAMADO.RESOLVIDO]: '#10b981', // Verde/Success
  [STATUS_CHAMADO.CANCELADO]: '#6b7280', // Cinza
  [STATUS_CHAMADO.REABERTO]: '#ef4444', // Vermelho/Danger
};

export const PRIORIDADE_CORES = {
  [PRIORIDADE_CHAMADO.BAIXA]: '#10b981', // Verde
  [PRIORIDADE_CHAMADO.MEDIA]: '#f59e0b', // Amarelo
  [PRIORIDADE_CHAMADO.ALTA]: '#f97316', // Laranja
  [PRIORIDADE_CHAMADO.CRITICA]: '#ef4444', // Vermelho
};

// ============================================
// CONSTANTES DE ARMAZENAMENTO LOCAL
// ============================================

export const STORAGE_KEYS = {
  /** Chave para armazenar o token JWT */
  AUTH_TOKEN: '@ClickDesk:authToken',
  
  /** Chave para armazenar dados do usuário */
  USER_DATA: '@ClickDesk:userData',
  
  /** Chave para armazenar preferências do tema */
  THEME: '@ClickDesk:theme',
  
  /** Chave para armazenar configurações */
  SETTINGS: '@ClickDesk:settings',
};

// ============================================
// MENSAGENS DE ERRO PADRÃO
// ============================================

export const ERROR_MESSAGES = {
  NETWORK_ERROR: 'Erro de conexão. Verifique sua internet.',
  TIMEOUT_ERROR: 'A requisição demorou muito. Tente novamente.',
  UNAUTHORIZED: 'Sessão expirada. Faça login novamente.',
  FORBIDDEN: 'Você não tem permissão para esta ação.',
  NOT_FOUND: 'Recurso não encontrado.',
  SERVER_ERROR: 'Erro no servidor. Tente novamente mais tarde.',
  VALIDATION_ERROR: 'Dados inválidos. Verifique os campos.',
  UNKNOWN_ERROR: 'Erro desconhecido. Tente novamente.',
};

// ============================================
// CONFIGURAÇÕES DA APLICAÇÃO
// ============================================

export const APP_CONFIG = {
  /** Nome da aplicação */
  APP_NAME: 'ClickDesk',
  
  /** Versão da aplicação */
  VERSION: '1.0.0',
  
  /** Número de itens por página (paginação) */
  ITEMS_PER_PAGE: 10,
  
  /** Tempo de expiração do token em minutos */
  TOKEN_EXPIRATION_MINUTES: 60,
  
  /** Intervalo para auto-refresh de dados em segundos */
  AUTO_REFRESH_INTERVAL: 30,
};
