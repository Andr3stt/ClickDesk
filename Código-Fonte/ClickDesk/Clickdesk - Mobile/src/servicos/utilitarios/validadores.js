// ✅ Utilitários de validação para formulários

/**
 * Valida se um email é válido
 * @param {string} email - Email a ser validado
 * @returns {boolean} true se o email é válido
 */
export const validarEmail = (email) => {
  const regexEmail = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
  return regexEmail.test(email);
};

/**
 * Valida se uma senha atende aos requisitos mínimos
 * @param {string} senha - Senha a ser validada
 * @returns {object} { valida: boolean, mensagem: string }
 */
export const validarSenha = (senha) => {
  if (!senha || senha.length < 6) {
    return { valida: false, mensagem: 'A senha deve ter no mínimo 6 caracteres' };
  }
  if (senha.length > 50) {
    return { valida: false, mensagem: 'A senha deve ter no máximo 50 caracteres' };
  }
  return { valida: true, mensagem: '' };
};

/**
 * Valida se um nome de usuário é válido
 * @param {string} usuario - Nome de usuário a ser validado
 * @returns {object} { valido: boolean, mensagem: string }
 */
export const validarUsuario = (usuario) => {
  if (!usuario || usuario.length < 3) {
    return { valido: false, mensagem: 'O nome de usuário deve ter no mínimo 3 caracteres' };
  }
  if (usuario.length > 30) {
    return { valido: false, mensagem: 'O nome de usuário deve ter no máximo 30 caracteres' };
  }
  const regexUsuario = /^[a-zA-Z0-9_]+$/;
  if (!regexUsuario.test(usuario)) {
    return { valido: false, mensagem: 'O nome de usuário deve conter apenas letras, números e _' };
  }
  return { valido: true, mensagem: '' };
};

/**
 * Valida se um campo não está vazio
 * @param {string} valor - Valor a ser validado
 * @returns {boolean} true se o campo não está vazio
 */
export const validarCampoObrigatorio = (valor) => {
  return valor && valor.trim().length > 0;
};

/**
 * Valida se dois campos são iguais (útil para confirmar senha)
 * @param {string} valor1 - Primeiro valor
 * @param {string} valor2 - Segundo valor
 * @returns {boolean} true se os valores são iguais
 */
export const validarCamposIguais = (valor1, valor2) => {
  return valor1 === valor2;
};

/**
 * Valida um CPF brasileiro
 * @param {string} cpf - CPF a ser validado
 * @returns {boolean} true se o CPF é válido
 */
export const validarCPF = (cpf) => {
  cpf = cpf.replace(/[^\d]/g, '');
  
  if (cpf.length !== 11) return false;
  
  // Verifica se todos os dígitos são iguais
  if (/^(\d)\1{10}$/.test(cpf)) return false;
  
  // Validação do primeiro dígito verificador
  let soma = 0;
  for (let i = 0; i < 9; i++) {
    soma += parseInt(cpf.charAt(i)) * (10 - i);
  }
  let resto = (soma * 10) % 11;
  if (resto === 10 || resto === 11) resto = 0;
  if (resto !== parseInt(cpf.charAt(9))) return false;
  
  // Validação do segundo dígito verificador
  soma = 0;
  for (let i = 0; i < 10; i++) {
    soma += parseInt(cpf.charAt(i)) * (11 - i);
  }
  resto = (soma * 10) % 11;
  if (resto === 10 || resto === 11) resto = 0;
  if (resto !== parseInt(cpf.charAt(10))) return false;
  
  return true;
};

/**
 * Valida um telefone brasileiro
 * @param {string} telefone - Telefone a ser validado
 * @returns {boolean} true se o telefone é válido
 */
export const validarTelefone = (telefone) => {
  const telefoneLimpo = telefone.replace(/[^\d]/g, '');
  // Aceita (XX) XXXXX-XXXX ou (XX) XXXX-XXXX
  return telefoneLimpo.length === 10 || telefoneLimpo.length === 11;
};
