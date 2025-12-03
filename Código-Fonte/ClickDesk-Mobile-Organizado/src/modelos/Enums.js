/**
 * Enumerações do Sistema
 * 
 * Centraliza todos os enums utilizados na aplicação
 */

import {
  STATUS_CHAMADO,
  PRIORIDADE_CHAMADO,
  CATEGORIA_CHAMADO,
  USER_ROLE,
  STATUS_LABELS,
  PRIORIDADE_LABELS,
  CATEGORIA_LABELS,
  ROLE_LABELS,
  STATUS_CORES,
  PRIORIDADE_CORES,
} from '../configuracao/constantes';

/**
 * Classe utilitária para trabalhar com enums
 */
export class Enums {
  // ============================================
  // STATUS DO CHAMADO
  // ============================================
  
  static StatusChamado = STATUS_CHAMADO;
  
  static obterLabelStatus(status) {
    return STATUS_LABELS[status] || status;
  }
  
  static obterCorStatus(status) {
    return STATUS_CORES[status] || '#6b7280';
  }
  
  static todosStatus() {
    return Object.values(STATUS_CHAMADO);
  }
  
  // ============================================
  // PRIORIDADE DO CHAMADO
  // ============================================
  
  static PrioridadeChamado = PRIORIDADE_CHAMADO;
  
  static obterLabelPrioridade(prioridade) {
    return PRIORIDADE_LABELS[prioridade] || prioridade;
  }
  
  static obterCorPrioridade(prioridade) {
    return PRIORIDADE_CORES[prioridade] || '#6b7280';
  }
  
  static todasPrioridades() {
    return Object.values(PRIORIDADE_CHAMADO);
  }
  
  // ============================================
  // CATEGORIA DO CHAMADO
  // ============================================
  
  static CategoriaChamado = CATEGORIA_CHAMADO;
  
  static obterLabelCategoria(categoria) {
    return CATEGORIA_LABELS[categoria] || categoria;
  }
  
  static todasCategorias() {
    return Object.values(CATEGORIA_CHAMADO);
  }
  
  // ============================================
  // TIPO DE USUÁRIO (ROLE)
  // ============================================
  
  static UserRole = USER_ROLE;
  
  static obterLabelRole(role) {
    return ROLE_LABELS[role] || role;
  }
  
  static todosRoles() {
    return Object.values(USER_ROLE);
  }
  
  // ============================================
  // CONVERSÕES PARA PICKER/SELECT
  // ============================================
  
  /**
   * Retorna array de objetos para uso em Picker de status
   */
  static obterOpcoesStatus() {
    return Object.entries(STATUS_LABELS).map(([value, label]) => ({
      value,
      label,
      cor: STATUS_CORES[value],
    }));
  }
  
  /**
   * Retorna array de objetos para uso em Picker de prioridade
   */
  static obterOpcoesPrioridade() {
    return Object.entries(PRIORIDADE_LABELS).map(([value, label]) => ({
      value,
      label,
      cor: PRIORIDADE_CORES[value],
    }));
  }
  
  /**
   * Retorna array de objetos para uso em Picker de categoria
   */
  static obterOpcoesCategoria() {
    return Object.entries(CATEGORIA_LABELS).map(([value, label]) => ({
      value,
      label,
    }));
  }
  
  /**
   * Retorna array de objetos para uso em Picker de role
   */
  static obterOpcoesRole() {
    return Object.entries(ROLE_LABELS).map(([value, label]) => ({
      value,
      label,
    }));
  }
}

export default Enums;
