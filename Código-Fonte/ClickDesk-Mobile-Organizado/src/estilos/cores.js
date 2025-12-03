/**
 * Cores do ClickDesk
 * 
 * Paleta de cores oficial do projeto ClickDesk
 */

export const Cores = {
  // ============================================
  // CORES PRINCIPAIS DA MARCA
  // ============================================
  
  /** Background principal da aplicação */
  background: '#EDE6D9',
  
  /** Cor de fundo dos cards */
  backgroundCard: '#F5EFE6',
  
  /** Cor laranja da marca (ações principais) */
  brand: '#F28A1A',
  brandDark: '#E67E22',
  brandLight: '#FFA94D',
  
  /** Cor de texto principal */
  textoPrincipal: '#1E2A22',
  
  /** Cor de texto secundário */
  textoSecundario: '#6B7280',
  
  // ============================================
  // CORES SEMÂNTICAS
  // ============================================
  
  /** Cor primária (botões principais, links) */
  primary: '#2563eb',
  primaryDark: '#1e40af',
  primaryLight: '#3b82f6',
  
  /** Cor de sucesso */
  success: '#10b981',
  successDark: '#059669',
  successLight: '#34d399',
  
  /** Cor de perigo/erro */
  danger: '#ef4444',
  dangerDark: '#dc2626',
  dangerLight: '#f87171',
  
  /** Cor de aviso/alerta */
  warning: '#f59e0b',
  warningDark: '#d97706',
  warningLight: '#fbbf24',
  
  /** Cor de informação */
  info: '#3b82f6',
  infoDark: '#2563eb',
  infoLight: '#60a5fa',
  
  // ============================================
  // CORES NEUTRAS (ESCALA DE CINZA)
  // ============================================
  
  gray900: '#111827',
  gray800: '#1f2937',
  gray700: '#374151',
  gray600: '#4b5563',
  gray500: '#6b7280',
  gray400: '#9ca3af',
  gray300: '#d1d5db',
  gray200: '#e5e7eb',
  gray100: '#f3f4f6',
  gray50: '#f9fafb',
  
  // ============================================
  // CORES UTILITÁRIAS
  // ============================================
  
  /** Branco puro */
  branco: '#FFFFFF',
  
  /** Preto puro */
  preto: '#000000',
  
  /** Transparente */
  transparente: 'transparent',
  
  // ============================================
  // CORES DE OVERLAY/MODAL
  // ============================================
  
  /** Fundo escuro semi-transparente para modais */
  overlayDark: 'rgba(0, 0, 0, 0.5)',
  overlayLight: 'rgba(0, 0, 0, 0.3)',
  
  // ============================================
  // CORES DE BORDAS
  // ============================================
  
  bordaPadrao: '#e5e7eb',
  bordaEscura: '#d1d5db',
  
  // ============================================
  // CORES DE STATUS DE CHAMADOS
  // ============================================
  
  statusPendente: '#f59e0b',
  statusEmAnalise: '#3b82f6',
  statusEmAtendimento: '#8b5cf6',
  statusResolvido: '#10b981',
  statusCancelado: '#6b7280',
  statusReaberto: '#ef4444',
  
  // ============================================
  // CORES DE PRIORIDADE
  // ============================================
  
  prioridadeBaixa: '#10b981',
  prioridadeMedia: '#f59e0b',
  prioridadeAlta: '#f97316',
  prioridadeCritica: '#ef4444',
};

/**
 * Obtém a cor de um status
 */
export const obterCorStatus = (status) => {
  const cores = {
    PENDENTE: Cores.statusPendente,
    EM_ANALISE: Cores.statusEmAnalise,
    EM_ATENDIMENTO: Cores.statusEmAtendimento,
    RESOLVIDO: Cores.statusResolvido,
    CANCELADO: Cores.statusCancelado,
    REABERTO: Cores.statusReaberto,
  };
  
  return cores[status] || Cores.gray500;
};

/**
 * Obtém a cor de uma prioridade
 */
export const obterCorPrioridade = (prioridade) => {
  const cores = {
    BAIXA: Cores.prioridadeBaixa,
    MEDIA: Cores.prioridadeMedia,
    ALTA: Cores.prioridadeAlta,
    CRITICA: Cores.prioridadeCritica,
  };
  
  return cores[prioridade] || Cores.gray500;
};

export default Cores;
