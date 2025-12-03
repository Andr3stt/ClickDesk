/**
 * Funções Auxiliares Gerais
 * 
 * Funções utilitárias diversas para uso geral na aplicação
 */

/**
 * Aguarda um tempo determinado
 * 
 * @param {number} ms - Milissegundos para aguardar
 * @returns {Promise} Promise que resolve após o tempo
 */
export const aguardar = (ms) => {
  return new Promise(resolve => setTimeout(resolve, ms));
};

/**
 * Gera um ID único
 * 
 * @returns {string} ID único
 */
export const gerarId = () => {
  return `${Date.now()}_${Math.random().toString(36).substr(2, 9)}`;
};

/**
 * Debounce - Atrasa a execução de uma função
 * 
 * @param {Function} func - Função a ser executada
 * @param {number} delay - Delay em milissegundos
 * @returns {Function} Função com debounce
 */
export const debounce = (func, delay = 300) => {
  let timeoutId;
  
  return function(...args) {
    clearTimeout(timeoutId);
    timeoutId = setTimeout(() => func.apply(this, args), delay);
  };
};

/**
 * Throttle - Limita a frequência de execução de uma função
 * 
 * @param {Function} func - Função a ser executada
 * @param {number} limit - Limite em milissegundos
 * @returns {Function} Função com throttle
 */
export const throttle = (func, limit = 300) => {
  let inThrottle;
  
  return function(...args) {
    if (!inThrottle) {
      func.apply(this, args);
      inThrottle = true;
      setTimeout(() => inThrottle = false, limit);
    }
  };
};

/**
 * Embaralha um array (Fisher-Yates shuffle)
 * 
 * @param {Array} array - Array a ser embaralhado
 * @returns {Array} Array embaralhado
 */
export const embaralharArray = (array) => {
  const novoArray = [...array];
  for (let i = novoArray.length - 1; i > 0; i--) {
    const j = Math.floor(Math.random() * (i + 1));
    [novoArray[i], novoArray[j]] = [novoArray[j], novoArray[i]];
  }
  return novoArray;
};

/**
 * Agrupa elementos de um array por uma propriedade
 * 
 * @param {Array} array - Array a ser agrupado
 * @param {string} propriedade - Propriedade para agrupar
 * @returns {Object} Objeto com arrays agrupados
 */
export const agruparPor = (array, propriedade) => {
  return array.reduce((grupos, item) => {
    const chave = item[propriedade];
    if (!grupos[chave]) {
      grupos[chave] = [];
    }
    grupos[chave].push(item);
    return grupos;
  }, {});
};

/**
 * Remove duplicatas de um array
 * 
 * @param {Array} array - Array com possíveis duplicatas
 * @returns {Array} Array sem duplicatas
 */
export const removerDuplicatas = (array) => {
  return [...new Set(array)];
};

/**
 * Ordena um array de objetos por uma propriedade
 * 
 * @param {Array} array - Array a ser ordenado
 * @param {string} propriedade - Propriedade para ordenar
 * @param {string} ordem - 'asc' ou 'desc'
 * @returns {Array} Array ordenado
 */
export const ordenarPor = (array, propriedade, ordem = 'asc') => {
  return [...array].sort((a, b) => {
    const valorA = a[propriedade];
    const valorB = b[propriedade];
    
    if (valorA < valorB) {
      return ordem === 'asc' ? -1 : 1;
    }
    if (valorA > valorB) {
      return ordem === 'asc' ? 1 : -1;
    }
    return 0;
  });
};

/**
 * Filtra um array de objetos por uma busca em múltiplas propriedades
 * 
 * @param {Array} array - Array a ser filtrado
 * @param {string} busca - Termo de busca
 * @param {Array<string>} propriedades - Propriedades para buscar
 * @returns {Array} Array filtrado
 */
export const filtrarPorBusca = (array, busca, propriedades) => {
  if (!busca) return array;
  
  const buscaLower = busca.toLowerCase();
  
  return array.filter(item => {
    return propriedades.some(prop => {
      const valor = item[prop];
      if (!valor) return false;
      return valor.toString().toLowerCase().includes(buscaLower);
    });
  });
};

/**
 * Cria uma cópia profunda de um objeto
 * 
 * @param {Object} obj - Objeto a ser copiado
 * @returns {Object} Cópia do objeto
 */
export const copiarObjeto = (obj) => {
  return JSON.parse(JSON.stringify(obj));
};

/**
 * Verifica se um objeto está vazio
 * 
 * @param {Object} obj - Objeto a ser verificado
 * @returns {boolean} True se vazio
 */
export const objetoVazio = (obj) => {
  return Object.keys(obj).length === 0;
};

/**
 * Mescla objetos profundamente
 * 
 * @param {Object} target - Objeto alvo
 * @param {Object} source - Objeto fonte
 * @returns {Object} Objeto mesclado
 */
export const mesclarObjetos = (target, source) => {
  const output = { ...target };
  
  if (isObject(target) && isObject(source)) {
    Object.keys(source).forEach(key => {
      if (isObject(source[key])) {
        if (!(key in target)) {
          output[key] = source[key];
        } else {
          output[key] = mesclarObjetos(target[key], source[key]);
        }
      } else {
        output[key] = source[key];
      }
    });
  }
  
  return output;
};

/**
 * Verifica se um valor é um objeto
 * 
 * @param {any} item - Item a ser verificado
 * @returns {boolean} True se é objeto
 */
const isObject = (item) => {
  return item && typeof item === 'object' && !Array.isArray(item);
};

/**
 * Converte query string em objeto
 * 
 * @param {string} queryString - Query string (ex: ?page=1&limit=10)
 * @returns {Object} Objeto com parâmetros
 */
export const queryStringParaObjeto = (queryString) => {
  const params = {};
  const searchParams = new URLSearchParams(queryString);
  
  for (const [key, value] of searchParams) {
    params[key] = value;
  }
  
  return params;
};

/**
 * Converte objeto em query string
 * 
 * @param {Object} obj - Objeto com parâmetros
 * @returns {string} Query string
 */
export const objetoParaQueryString = (obj) => {
  const params = new URLSearchParams();
  
  Object.entries(obj).forEach(([key, value]) => {
    if (value !== null && value !== undefined) {
      params.append(key, value);
    }
  });
  
  return params.toString();
};

export default {
  aguardar,
  gerarId,
  debounce,
  throttle,
  embaralharArray,
  agruparPor,
  removerDuplicatas,
  ordenarPor,
  filtrarPorBusca,
  copiarObjeto,
  objetoVazio,
  mesclarObjetos,
  queryStringParaObjeto,
  objetoParaQueryString,
};
