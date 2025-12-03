/**
 * Modelo de FAQ
 * 
 * Representa uma pergunta frequente do sistema ClickDesk
 */

export class FAQ {
  constructor(dados = {}) {
    this.id = dados.id || null;
    this.pergunta = dados.pergunta || '';
    this.resposta = dados.resposta || '';
    this.categoria = dados.categoria || 'Geral';
    this.tags = dados.tags || [];
    this.visualizacoes = dados.visualizacoes || 0;
    this.util = dados.util || 0;
    this.naoUtil = dados.naoUtil || 0;
    this.ativo = dados.ativo !== undefined ? dados.ativo : true;
    this.dataCriacao = dados.dataCriacao || null;
    this.dataAtualizacao = dados.dataAtualizacao || null;
  }
  
  /**
   * Retorna uma prévia da resposta
   */
  get previewResposta() {
    if (!this.resposta) return '';
    const maxLength = 100;
    return this.resposta.length > maxLength
      ? this.resposta.substring(0, maxLength) + '...'
      : this.resposta;
  }
  
  /**
   * Retorna a porcentagem de utilidade
   */
  get porcentagemUtil() {
    const total = this.util + this.naoUtil;
    if (total === 0) return 0;
    return Math.round((this.util / total) * 100);
  }
  
  /**
   * Verifica se a FAQ tem alta utilidade (>= 80%)
   */
  get isAltaUtilidade() {
    return this.porcentagemUtil >= 80;
  }
  
  /**
   * Verifica se a FAQ é popular (muitas visualizações)
   */
  get isPopular() {
    return this.visualizacoes >= 100;
  }
  
  /**
   * Converte o modelo para JSON
   */
  toJSON() {
    return {
      id: this.id,
      pergunta: this.pergunta,
      resposta: this.resposta,
      categoria: this.categoria,
      tags: this.tags,
      visualizacoes: this.visualizacoes,
      util: this.util,
      naoUtil: this.naoUtil,
      ativo: this.ativo,
      dataCriacao: this.dataCriacao,
      dataAtualizacao: this.dataAtualizacao,
    };
  }
  
  /**
   * Cria um modelo a partir de um objeto JSON
   */
  static fromJSON(json) {
    return new FAQ(json);
  }
}

export default FAQ;
