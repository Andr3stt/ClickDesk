/**
 * Modelo de Chamado
 * 
 * Representa um chamado/ticket do sistema ClickDesk
 */

export class Chamado {
  constructor(dados = {}) {
    this.id = dados.id || null;
    this.titulo = dados.titulo || '';
    this.descricao = dados.descricao || '';
    this.categoria = dados.categoria || 'OUTROS';
    this.prioridade = dados.prioridade || 'MEDIA';
    this.status = dados.status || 'PENDENTE';
    this.usuarioId = dados.usuarioId || null;
    this.tecnicoId = dados.tecnicoId || null;
    this.resolucaoIA = dados.resolucaoIA || null;
    this.resolvido = dados.resolvido || false;
    this.feedback = dados.feedback || null;
    this.dataCriacao = dados.dataCriacao || null;
    this.dataAtualizacao = dados.dataAtualizacao || null;
    this.dataResolucao = dados.dataResolucao || null;
    
    // Dados relacionados (podem vir da API)
    this.usuario = dados.usuario || null;
    this.tecnico = dados.tecnico || null;
    this.comentarios = dados.comentarios || [];
  }
  
  /**
   * Verifica se o chamado está pendente
   */
  get isPendente() {
    return this.status === 'PENDENTE';
  }
  
  /**
   * Verifica se o chamado está em análise
   */
  get isEmAnalise() {
    return this.status === 'EM_ANALISE';
  }
  
  /**
   * Verifica se o chamado está em atendimento
   */
  get isEmAtendimento() {
    return this.status === 'EM_ATENDIMENTO';
  }
  
  /**
   * Verifica se o chamado foi resolvido
   */
  get isResolvido() {
    return this.status === 'RESOLVIDO';
  }
  
  /**
   * Verifica se o chamado foi cancelado
   */
  get isCancelado() {
    return this.status === 'CANCELADO';
  }
  
  /**
   * Verifica se o chamado foi reaberto
   */
  get isReaberto() {
    return this.status === 'REABERTO';
  }
  
  /**
   * Verifica se a prioridade é crítica
   */
  get isCritico() {
    return this.prioridade === 'CRITICA';
  }
  
  /**
   * Verifica se foi resolvido pela IA
   */
  get foiResolvidoPelaIA() {
    return this.resolucaoIA !== null && this.resolvido;
  }
  
  /**
   * Retorna o nome do usuário que criou o chamado
   */
  get nomeUsuario() {
    if (this.usuario) {
      return `${this.usuario.firstName} ${this.usuario.lastName}`;
    }
    return 'Usuário desconhecido';
  }
  
  /**
   * Retorna o nome do técnico responsável
   */
  get nomeTecnico() {
    if (this.tecnico) {
      return `${this.tecnico.firstName} ${this.tecnico.lastName}`;
    }
    return 'Não atribuído';
  }
  
  /**
   * Converte o modelo para JSON
   */
  toJSON() {
    return {
      id: this.id,
      titulo: this.titulo,
      descricao: this.descricao,
      categoria: this.categoria,
      prioridade: this.prioridade,
      status: this.status,
      usuarioId: this.usuarioId,
      tecnicoId: this.tecnicoId,
      resolucaoIA: this.resolucaoIA,
      resolvido: this.resolvido,
      feedback: this.feedback,
      dataCriacao: this.dataCriacao,
      dataAtualizacao: this.dataAtualizacao,
      dataResolucao: this.dataResolucao,
    };
  }
  
  /**
   * Cria um modelo a partir de um objeto JSON
   */
  static fromJSON(json) {
    return new Chamado(json);
  }
}

export default Chamado;
