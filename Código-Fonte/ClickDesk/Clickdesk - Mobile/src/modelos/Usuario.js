// 👤 Modelo de dados do Usuario

/**
 * Classe que representa um usuário do sistema
 */
export class Usuario {
  constructor(dados = {}) {
    this.id = dados.id || null;
    this.username = dados.username || '';
    this.email = dados.email || '';
    this.nome = dados.nome || '';
    this.role = dados.role || 'USER';
    this.ativo = dados.ativo !== undefined ? dados.ativo : true;
    this.dataCriacao = dados.dataCriacao ? new Date(dados.dataCriacao) : null;
    this.dataAtualizacao = dados.dataAtualizacao ? new Date(dados.dataAtualizacao) : null;
  }

  /**
   * Verifica se o usuário é administrador
   */
  ehAdmin() {
    return this.role === 'ADMIN';
  }

  /**
   * Verifica se o usuário é técnico
   */
  ehTecnico() {
    return this.role === 'TECH' || this.role === 'ADMIN';
  }

  /**
   * Verifica se o usuário é comum
   */
  ehUsuarioComum() {
    return this.role === 'USER';
  }

  /**
   * Retorna o nome completo ou username se nome não estiver disponível
   */
  getNomeExibicao() {
    return this.nome || this.username;
  }

  /**
   * Converte o objeto para JSON
   */
  toJSON() {
    return {
      id: this.id,
      username: this.username,
      email: this.email,
      nome: this.nome,
      role: this.role,
      ativo: this.ativo,
      dataCriacao: this.dataCriacao,
      dataAtualizacao: this.dataAtualizacao
    };
  }

  /**
   * Cria uma instância a partir de dados JSON
   */
  static fromJSON(dados) {
    return new Usuario(dados);
  }
}
