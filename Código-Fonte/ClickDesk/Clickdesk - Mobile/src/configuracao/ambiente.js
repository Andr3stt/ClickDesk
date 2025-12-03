// 🌍 Configurações de ambiente da aplicação

/**
 * Determina se está em modo de desenvolvimento
 */
export const ehDesenvolvimento = __DEV__;

/**
 * Configurações de ambiente
 */
export const AMBIENTE = {
  DESENVOLVIMENTO: 'desenvolvimento',
  HOMOLOGACAO: 'homologacao',
  PRODUCAO: 'producao'
};

/**
 * Ambiente atual (pode ser configurado via variável de ambiente)
 */
export const AMBIENTE_ATUAL = process.env.REACT_APP_ENV || AMBIENTE.DESENVOLVIMENTO;

/**
 * Configuração de log
 */
export const CONFIG_LOG = {
  habilitarLogs: ehDesenvolvimento,
  habilitarLogsAPI: ehDesenvolvimento,
  habilitarLogsNavegacao: ehDesenvolvimento
};

/**
 * Configuração de timeout para requisições
 */
export const TIMEOUT_PADRAO = 30000; // 30 segundos

/**
 * Configuração de cache
 */
export const CONFIG_CACHE = {
  tempoExpiracaoMinutos: 30,
  habilitarCache: true
};

/**
 * Configuração de tentativas de requisição
 */
export const CONFIG_RETRY = {
  numeroTentativas: 3,
  intervaloMilissegundos: 1000
};
