/**
 * Utilitário de Formatadores
 * 
 * Fornece funções para formatar diferentes tipos de dados
 */

/**
 * Formata uma data para string legível
 * 
 * @param {Date|string} data - Data a ser formatada
 * @param {string} formato - Formato desejado ('curto', 'longo', 'completo')
 * @returns {string} Data formatada
 */
export const formatarData = (data, formato = 'curto') => {
  if (!data) {
    return '';
  }
  
  const dataObj = typeof data === 'string' ? new Date(data) : data;
  
  if (isNaN(dataObj.getTime())) {
    return '';
  }
  
  const opcoes = {
    curto: {
      day: '2-digit',
      month: '2-digit',
      year: 'numeric',
    },
    longo: {
      day: '2-digit',
      month: 'long',
      year: 'numeric',
    },
    completo: {
      weekday: 'long',
      day: '2-digit',
      month: 'long',
      year: 'numeric',
    },
  };
  
  return dataObj.toLocaleDateString('pt-BR', opcoes[formato] || opcoes.curto);
};

/**
 * Formata uma data e hora para string legível
 * 
 * @param {Date|string} dataHora - Data e hora a ser formatada
 * @returns {string} Data e hora formatadas
 */
export const formatarDataHora = (dataHora) => {
  if (!dataHora) {
    return '';
  }
  
  const dataObj = typeof dataHora === 'string' ? new Date(dataHora) : dataHora;
  
  if (isNaN(dataObj.getTime())) {
    return '';
  }
  
  return dataObj.toLocaleString('pt-BR', {
    day: '2-digit',
    month: '2-digit',
    year: 'numeric',
    hour: '2-digit',
    minute: '2-digit',
  });
};

/**
 * Formata uma data para tempo relativo (ex: "há 5 minutos")
 * 
 * @param {Date|string} data - Data a ser formatada
 * @returns {string} Tempo relativo
 */
export const formatarTempoRelativo = (data) => {
  if (!data) {
    return '';
  }
  
  const dataObj = typeof data === 'string' ? new Date(data) : data;
  
  if (isNaN(dataObj.getTime())) {
    return '';
  }
  
  const agora = new Date();
  const diferencaMs = agora - dataObj;
  const diferencaMinutos = Math.floor(diferencaMs / 60000);
  const diferencaHoras = Math.floor(diferencaMinutos / 60);
  const diferencaDias = Math.floor(diferencaHoras / 24);
  
  if (diferencaMinutos < 1) {
    return 'agora';
  }
  
  if (diferencaMinutos < 60) {
    return `há ${diferencaMinutos} ${diferencaMinutos === 1 ? 'minuto' : 'minutos'}`;
  }
  
  if (diferencaHoras < 24) {
    return `há ${diferencaHoras} ${diferencaHoras === 1 ? 'hora' : 'horas'}`;
  }
  
  if (diferencaDias < 7) {
    return `há ${diferencaDias} ${diferencaDias === 1 ? 'dia' : 'dias'}`;
  }
  
  return formatarData(dataObj);
};

/**
 * Formata um número com separador de milhares
 * 
 * @param {number} numero - Número a ser formatado
 * @returns {string} Número formatado
 */
export const formatarNumero = (numero) => {
  if (numero === null || numero === undefined) {
    return '0';
  }
  
  return numero.toLocaleString('pt-BR');
};

/**
 * Formata um valor monetário
 * 
 * @param {number} valor - Valor a ser formatado
 * @param {string} moeda - Código da moeda (padrão: BRL)
 * @returns {string} Valor formatado
 */
export const formatarMoeda = (valor, moeda = 'BRL') => {
  if (valor === null || valor === undefined) {
    valor = 0;
  }
  
  return valor.toLocaleString('pt-BR', {
    style: 'currency',
    currency: moeda,
  });
};

/**
 * Trunca um texto para um tamanho máximo
 * 
 * @param {string} texto - Texto a ser truncado
 * @param {number} tamanhoMaximo - Tamanho máximo
 * @param {string} sufixo - Sufixo a ser adicionado (padrão: '...')
 * @returns {string} Texto truncado
 */
export const truncarTexto = (texto, tamanhoMaximo, sufixo = '...') => {
  if (!texto || texto.length <= tamanhoMaximo) {
    return texto || '';
  }
  
  return texto.substring(0, tamanhoMaximo - sufixo.length) + sufixo;
};

/**
 * Capitaliza a primeira letra de cada palavra
 * 
 * @param {string} texto - Texto a ser capitalizado
 * @returns {string} Texto capitalizado
 */
export const capitalizarPalavras = (texto) => {
  if (!texto) {
    return '';
  }
  
  return texto
    .toLowerCase()
    .split(' ')
    .map(palavra => palavra.charAt(0).toUpperCase() + palavra.slice(1))
    .join(' ');
};

/**
 * Formata um CPF
 * 
 * @param {string} cpf - CPF a ser formatado
 * @returns {string} CPF formatado
 */
export const formatarCPF = (cpf) => {
  if (!cpf) {
    return '';
  }
  
  const apenasNumeros = cpf.replace(/\D/g, '');
  
  if (apenasNumeros.length !== 11) {
    return cpf;
  }
  
  return apenasNumeros.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, '$1.$2.$3-$4');
};

/**
 * Formata um telefone
 * 
 * @param {string} telefone - Telefone a ser formatado
 * @returns {string} Telefone formatado
 */
export const formatarTelefone = (telefone) => {
  if (!telefone) {
    return '';
  }
  
  const apenasNumeros = telefone.replace(/\D/g, '');
  
  if (apenasNumeros.length === 10) {
    return apenasNumeros.replace(/(\d{2})(\d{4})(\d{4})/, '($1) $2-$3');
  }
  
  if (apenasNumeros.length === 11) {
    return apenasNumeros.replace(/(\d{2})(\d{5})(\d{4})/, '($1) $2-$3');
  }
  
  return telefone;
};

/**
 * Remove formatação de um texto, deixando apenas números
 * 
 * @param {string} texto - Texto a ser limpo
 * @returns {string} Apenas números
 */
export const apenasNumeros = (texto) => {
  if (!texto) {
    return '';
  }
  
  return texto.replace(/\D/g, '');
};

export default {
  formatarData,
  formatarDataHora,
  formatarTempoRelativo,
  formatarNumero,
  formatarMoeda,
  truncarTexto,
  capitalizarPalavras,
  formatarCPF,
  formatarTelefone,
  apenasNumeros,
};
