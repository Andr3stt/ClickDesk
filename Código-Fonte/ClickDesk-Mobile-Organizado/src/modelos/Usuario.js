/**
 * Modelo de Usuário
 * 
 * Representa um usuário do sistema ClickDesk
 */

export class Usuario {
  constructor(dados = {}) {
    this.id = dados.id || null;
    this.firstName = dados.firstName || '';
    this.lastName = dados.lastName || '';
    this.email = dados.email || '';
    this.role = dados.role || 'USER';
    this.ativo = dados.ativo !== undefined ? dados.ativo : true;
    this.dataCriacao = dados.dataCriacao || null;
    this.dataAtualizacao = dados.dataAtualizacao || null;
  }
  
  /**
   * Retorna o nome completo do usuário
   */
  get nomeCompleto() {
    return `${this.firstName} ${this.lastName}`.trim();
  }
  
  /**
   * Retorna as iniciais do usuário
   */
  get iniciais() {
    const primeiroNome = this.firstName.charAt(0).toUpperCase();
    const ultimoNome = this.lastName.charAt(0).toUpperCase();
    return `${primeiroNome}${ultimoNome}`;
  }
  
  /**
   * Verifica se o usuário é admin
   */
  get isAdmin() {
    return this.role === 'ADMIN';
  }
  
  /**
   * Verifica se o usuário é técnico
   */
  get isTech() {
    return this.role === 'TECH' || this.role === 'ADMIN';
  }
  
  /**
   * Verifica se o usuário é comum
   */
  get isUser() {
    return this.role === 'USER';
  }
  
  /**
   * Converte o modelo para JSON
   */
  toJSON() {
    return {
      id: this.id,
      firstName: this.firstName,
      lastName: this.lastName,
      email: this.email,
      role: this.role,
      ativo: this.ativo,
      dataCriacao: this.dataCriacao,
      dataAtualizacao: this.dataAtualizacao,
    };
  }
  
  /**
   * Cria um modelo a partir de um objeto JSON
   */
  static fromJSON(json) {
    return new Usuario(json);
  }
}

export default Usuario;
