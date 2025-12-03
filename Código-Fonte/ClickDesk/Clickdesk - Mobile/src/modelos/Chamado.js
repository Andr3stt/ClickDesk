// 🎫 Modelo de dados do Chamado

/**
 * Classe que representa um chamado do sistema
 */
export class Chamado {
  constructor(dados = {}) {
    this.id = dados.id || null;
    this.titulo = dados.titulo || '';
    this.descricao = dados.descricao || '';
    this.categoria = dados.categoria || 'OUTROS';
    this.severidade = dados.severidade || 'MEDIA';
    this.status = dados.status || 'ABERTO';
    this.usuarioId = dados.usuarioId || null;
    this.tecnicoId = dados.tecnicoId || null;
    this.resolucao = dados.resolucao || '';
    this.feedback = dados.feedback || null;
    this.avaliacaoFeedback = dados.avaliacaoFeedback || null;
    this.dataCriacao = dados.dataCriacao ? new Date(dados.dataCriacao) : null;
    this.dataAtualizacao = dados.dataAtualizacao ? new Date(dados.dataAtualizacao) : null;
    this.dataResolucao = dados.dataResolucao ? new Date(dados.dataResolucao) : null;
  }

  /**
   * Verifica se o chamado está aberto
   */
  estaAberto() {
    return this.status === 'ABERTO';
  }

  /**
   * Verifica se o chamado está em atendimento
   */
  estaEmAtendimento() {
    return this.status === 'EM_ATENDIMENTO';
  }

  /**
   * Verifica se o chamado está resolvido
   */
  estaResolvido() {
    return this.status === 'RESOLVIDO' || this.status === 'RESOLVIDO_AUTOMATICO';
  }

  /**
   * Verifica se o chamado está fechado
   */
  estaFechado() {
    return this.status === 'FECHADO';
  }

  /**
   * Verifica se o chamado foi escalado
   */
  foiEscalado() {
    return this.status === 'ESCALADO';
  }

  /**
   * Verifica se o chamado possui técnico atribuído
   */
  possuiTecnico() {
    return this.tecnicoId !== null;
  }

  /**
   * Verifica se o chamado é crítico
   */
  ehCritico() {
    return this.severidade === 'CRITICA';
  }

  /**
   * Converte o objeto para JSON
   */
  toJSON() {
    return {
      id: this.id,
      titulo: this.titulo,
      descricao: this.descricao,
      categoria: this.categoria,
      severidade: this.severidade,
      status: this.status,
      usuarioId: this.usuarioId,
      tecnicoId: this.tecnicoId,
      resolucao: this.resolucao,
      feedback: this.feedback,
      avaliacaoFeedback: this.avaliacaoFeedback,
      dataCriacao: this.dataCriacao,
      dataAtualizacao: this.dataAtualizacao,
      dataResolucao: this.dataResolucao
    };
  }

  /**
   * Cria uma instância a partir de dados JSON
   */
  static fromJSON(dados) {
    return new Chamado(dados);
  }
}
