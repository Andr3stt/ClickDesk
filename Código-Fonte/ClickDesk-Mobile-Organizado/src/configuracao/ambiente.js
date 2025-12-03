/**
 * Configuração de Ambiente
 * 
 * Este arquivo gerencia as configurações específicas de cada ambiente
 * (desenvolvimento, homologação, produção).
 */

import { API_BASE_URL, API_TIMEOUT } from './constantes';

/**
 * Ambiente atual da aplicação
 * Possíveis valores: 'development', 'staging', 'production'
 */
export const AMBIENTE_ATUAL = __DEV__ ? 'development' : 'production';

/**
 * Verifica se está em modo de desenvolvimento
 */
export const IS_DEV = AMBIENTE_ATUAL === 'development';

/**
 * Verifica se está em modo de produção
 */
export const IS_PROD = AMBIENTE_ATUAL === 'production';

/**
 * Configurações por ambiente
 */
const CONFIGURACOES_AMBIENTE = {
  development: {
    apiUrl: 'http://localhost:8080',
    timeout: 30000,
    enableLogs: true,
    enableDebug: true,
  },
  staging: {
    apiUrl: 'https://staging-api.clickdesk.com',
    timeout: 30000,
    enableLogs: true,
    enableDebug: false,
  },
  production: {
    apiUrl: 'https://api.clickdesk.com',
    timeout: 30000,
    enableLogs: false,
    enableDebug: false,
  },
};

/**
 * Obtém a configuração do ambiente atual
 */
export const getConfiguracao = () => {
  const config = CONFIGURACOES_AMBIENTE[AMBIENTE_ATUAL];
  
  // Permite sobrescrever via variável de ambiente
  return {
    ...config,
    apiUrl: process.env.REACT_APP_API_URL || config.apiUrl,
    timeout: process.env.REACT_APP_TIMEOUT || config.timeout,
  };
};

/**
 * URL da API para o ambiente atual
 */
export const getApiUrl = () => {
  return getConfiguracao().apiUrl;
};

/**
 * Timeout para o ambiente atual
 */
export const getTimeout = () => {
  return getConfiguracao().timeout;
};

/**
 * Verifica se logs estão habilitados
 */
export const logsHabilitados = () => {
  return getConfiguracao().enableLogs;
};

/**
 * Verifica se debug está habilitado
 */
export const debugHabilitado = () => {
  return getConfiguracao().enableDebug;
};

/**
 * Função de log condicional
 * Só loga se logs estiverem habilitados
 */
export const log = (...args) => {
  if (logsHabilitados()) {
    console.log('[ClickDesk]', ...args);
  }
};

/**
 * Função de erro condicional
 * Sempre loga erros, mas com mais detalhes em desenvolvimento
 */
export const logError = (error, contexto = '') => {
  if (logsHabilitados()) {
    console.error(`[ClickDesk Error] ${contexto}:`, error);
  } else {
    // Em produção, loga apenas mensagem básica
    console.error(`[ClickDesk Error] ${contexto}:`, error.message || error);
  }
};

/**
 * Função de debug condicional
 * Só loga se debug estiver habilitado
 */
export const debug = (...args) => {
  if (debugHabilitado()) {
    console.debug('[ClickDesk Debug]', ...args);
  }
};

export default {
  AMBIENTE_ATUAL,
  IS_DEV,
  IS_PROD,
  getConfiguracao,
  getApiUrl,
  getTimeout,
  logsHabilitados,
  debugHabilitado,
  log,
  logError,
  debug,
};
