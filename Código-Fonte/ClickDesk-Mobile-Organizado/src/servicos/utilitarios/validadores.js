/**
 * Utilitário de Validadores
 * 
 * Fornece funções para validar diferentes tipos de dados
 */

/**
 * Valida se o email é válido
 * 
 * @param {string} email - Email a ser validado
 * @returns {boolean} True se válido
 */
export const validarEmail = (email) => {
  if (!email || typeof email !== 'string') {
    return false;
  }
  
  const regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
  return regex.test(email.trim());
};

/**
 * Valida se a senha atende aos requisitos mínimos
 * 
 * @param {string} senha - Senha a ser validada
 * @param {number} tamanhoMinimo - Tamanho mínimo (padrão: 6)
 * @returns {Object} Objeto com resultado da validação
 */
export const validarSenha = (senha, tamanhoMinimo = 6) => {
  const erros = [];
  
  if (!senha || typeof senha !== 'string') {
    return {
      valido: false,
      erros: ['Senha é obrigatória'],
    };
  }
  
  if (senha.length < tamanhoMinimo) {
    erros.push(`Senha deve ter pelo menos ${tamanhoMinimo} caracteres`);
  }
  
  return {
    valido: erros.length === 0,
    erros,
  };
};

/**
 * Valida se as senhas são iguais
 * 
 * @param {string} senha - Senha
 * @param {string} confirmacao - Confirmação da senha
 * @returns {boolean} True se iguais
 */
export const validarConfirmacaoSenha = (senha, confirmacao) => {
  return senha === confirmacao;
};

/**
 * Valida se o campo não está vazio
 * 
 * @param {string} valor - Valor a ser validado
 * @returns {boolean} True se não vazio
 */
export const validarCampoObrigatorio = (valor) => {
  if (valor === null || valor === undefined) {
    return false;
  }
  
  if (typeof valor === 'string') {
    return valor.trim().length > 0;
  }
  
  return true;
};

/**
 * Valida se o texto tem um tamanho mínimo
 * 
 * @param {string} texto - Texto a ser validado
 * @param {number} tamanhoMinimo - Tamanho mínimo
 * @returns {boolean} True se válido
 */
export const validarTamanhoMinimo = (texto, tamanhoMinimo) => {
  if (!texto || typeof texto !== 'string') {
    return false;
  }
  
  return texto.trim().length >= tamanhoMinimo;
};

/**
 * Valida se o texto tem um tamanho máximo
 * 
 * @param {string} texto - Texto a ser validado
 * @param {number} tamanhoMaximo - Tamanho máximo
 * @returns {boolean} True se válido
 */
export const validarTamanhoMaximo = (texto, tamanhoMaximo) => {
  if (!texto || typeof texto !== 'string') {
    return true; // Vazio é válido para tamanho máximo
  }
  
  return texto.trim().length <= tamanhoMaximo;
};

/**
 * Valida se o valor é um número
 * 
 * @param {any} valor - Valor a ser validado
 * @returns {boolean} True se é número válido
 */
export const validarNumero = (valor) => {
  return !isNaN(parseFloat(valor)) && isFinite(valor);
};

/**
 * Valida se o valor está dentro de um range
 * 
 * @param {number} valor - Valor a ser validado
 * @param {number} min - Valor mínimo
 * @param {number} max - Valor máximo
 * @returns {boolean} True se válido
 */
export const validarRange = (valor, min, max) => {
  if (!validarNumero(valor)) {
    return false;
  }
  
  const num = parseFloat(valor);
  return num >= min && num <= max;
};

/**
 * Valida um formulário completo
 * 
 * @param {Object} dados - Dados do formulário
 * @param {Object} regras - Regras de validação
 * @returns {Object} Objeto com resultado da validação
 */
export const validarFormulario = (dados, regras) => {
  const erros = {};
  let valido = true;
  
  Object.keys(regras).forEach((campo) => {
    const regra = regras[campo];
    const valor = dados[campo];
    const errosCampo = [];
    
    // Verifica se é obrigatório
    if (regra.obrigatorio && !validarCampoObrigatorio(valor)) {
      errosCampo.push(`${regra.label || campo} é obrigatório`);
    }
    
    // Verifica email
    if (regra.email && valor && !validarEmail(valor)) {
      errosCampo.push('Email inválido');
    }
    
    // Verifica tamanho mínimo
    if (regra.tamanhoMinimo && valor && !validarTamanhoMinimo(valor, regra.tamanhoMinimo)) {
      errosCampo.push(`Mínimo de ${regra.tamanhoMinimo} caracteres`);
    }
    
    // Verifica tamanho máximo
    if (regra.tamanhoMaximo && valor && !validarTamanhoMaximo(valor, regra.tamanhoMaximo)) {
      errosCampo.push(`Máximo de ${regra.tamanhoMaximo} caracteres`);
    }
    
    // Verifica se é número
    if (regra.numero && valor && !validarNumero(valor)) {
      errosCampo.push('Deve ser um número');
    }
    
    // Verifica confirmação de senha
    if (regra.confirmarCom && valor && dados[regra.confirmarCom] !== valor) {
      errosCampo.push('As senhas não coincidem');
    }
    
    if (errosCampo.length > 0) {
      erros[campo] = errosCampo;
      valido = false;
    }
  });
  
  return {
    valido,
    erros,
  };
};

export default {
  validarEmail,
  validarSenha,
  validarConfirmacaoSenha,
  validarCampoObrigatorio,
  validarTamanhoMinimo,
  validarTamanhoMaximo,
  validarNumero,
  validarRange,
  validarFormulario,
};
