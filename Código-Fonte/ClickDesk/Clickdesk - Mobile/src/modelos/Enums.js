// 📋 Enums e constantes do sistema

/**
 * Status possíveis de um chamado
 */
export const StatusChamado = {
  ABERTO: 'ABERTO',
  EM_ATENDIMENTO: 'EM_ATENDIMENTO',
  AGUARDANDO: 'AGUARDANDO',
  RESOLVIDO_AUTOMATICO: 'RESOLVIDO_AUTOMATICO',
  RESOLVIDO: 'RESOLVIDO',
  FECHADO: 'FECHADO',
  ESCALADO: 'ESCALADO'
};

/**
 * Severidades possíveis de um chamado
 */
export const SeveridadeChamado = {
  BAIXA: 'BAIXA',
  MEDIA: 'MEDIA',
  ALTA: 'ALTA',
  CRITICA: 'CRITICA'
};

/**
 * Categorias possíveis de um chamado
 */
export const CategoriaChamado = {
  SOFTWARE: 'SOFTWARE',
  HARDWARE: 'HARDWARE',
  REDES: 'REDES',
  TREINAMENTO: 'TREINAMENTO',
  OUTROS: 'OUTROS'
};

/**
 * Roles de usuário no sistema
 */
export const RolesUsuario = {
  USER: 'USER',
  TECH: 'TECH',
  ADMIN: 'ADMIN'
};

/**
 * Labels de exibição para Status de Chamado
 */
export const LabelStatusChamado = {
  [StatusChamado.ABERTO]: 'Aberto',
  [StatusChamado.EM_ATENDIMENTO]: 'Em Atendimento',
  [StatusChamado.AGUARDANDO]: 'Aguardando',
  [StatusChamado.RESOLVIDO_AUTOMATICO]: 'Resolvido Automaticamente',
  [StatusChamado.RESOLVIDO]: 'Resolvido',
  [StatusChamado.FECHADO]: 'Fechado',
  [StatusChamado.ESCALADO]: 'Escalado'
};

/**
 * Labels de exibição para Severidade
 */
export const LabelSeveridadeChamado = {
  [SeveridadeChamado.BAIXA]: 'Baixa',
  [SeveridadeChamado.MEDIA]: 'Média',
  [SeveridadeChamado.ALTA]: 'Alta',
  [SeveridadeChamado.CRITICA]: 'Crítica'
};

/**
 * Labels de exibição para Categoria
 */
export const LabelCategoriaChamado = {
  [CategoriaChamado.SOFTWARE]: 'Software',
  [CategoriaChamado.HARDWARE]: 'Hardware',
  [CategoriaChamado.REDES]: 'Redes',
  [CategoriaChamado.TREINAMENTO]: 'Treinamento',
  [CategoriaChamado.OUTROS]: 'Outros'
};

/**
 * Labels de exibição para Roles
 */
export const LabelRolesUsuario = {
  [RolesUsuario.USER]: 'Usuário',
  [RolesUsuario.TECH]: 'Técnico',
  [RolesUsuario.ADMIN]: 'Administrador'
};

/**
 * Cores associadas aos status de chamado
 */
export const CoresStatusChamado = {
  [StatusChamado.ABERTO]: '#3498DB',
  [StatusChamado.EM_ATENDIMENTO]: '#F39C12',
  [StatusChamado.AGUARDANDO]: '#9B59B6',
  [StatusChamado.RESOLVIDO_AUTOMATICO]: '#1ABC9C',
  [StatusChamado.RESOLVIDO]: '#27AE60',
  [StatusChamado.FECHADO]: '#95A5A6',
  [StatusChamado.ESCALADO]: '#E74C3C'
};

/**
 * Cores associadas às severidades
 */
export const CoresSeveridadeChamado = {
  [SeveridadeChamado.BAIXA]: '#27AE60',
  [SeveridadeChamado.MEDIA]: '#F39C12',
  [SeveridadeChamado.ALTA]: '#E67E22',
  [SeveridadeChamado.CRITICA]: '#E74C3C'
};

/**
 * Ícones associados às categorias (Material Community Icons)
 */
export const IconesCategoriaChamado = {
  [CategoriaChamado.SOFTWARE]: 'application',
  [CategoriaChamado.HARDWARE]: 'desktop-tower',
  [CategoriaChamado.REDES]: 'network',
  [CategoriaChamado.TREINAMENTO]: 'school',
  [CategoriaChamado.OUTROS]: 'dots-horizontal'
};
