/**
 * Cliente HTTP configurado com Axios
 * 
 * Este arquivo configura o cliente HTTP com interceptadores para:
 * - Adicionar token JWT automaticamente em todas as requisições
 * - Tratar erros de forma centralizada
 * - Gerenciar timeout
 * - Renovar token expirado automaticamente
 */

import axios from 'axios';
import AsyncStorage from '@react-native-async-storage/async-storage';
import { getApiUrl, getTimeout, log, logError } from '../../configuracao/ambiente';
import { STORAGE_KEYS, ERROR_MESSAGES } from '../../configuracao/constantes';

/**
 * Instância do Axios configurada
 */
const clienteHttp = axios.create({
  baseURL: getApiUrl(),
  timeout: getTimeout(),
  headers: {
    'Content-Type': 'application/json',
    'Accept': 'application/json',
  },
});

/**
 * Interceptador de Requisição
 * Adiciona o token JWT automaticamente em todas as requisições
 */
clienteHttp.interceptors.request.use(
  async (config) => {
    try {
      // Busca o token do AsyncStorage
      const token = await AsyncStorage.getItem(STORAGE_KEYS.AUTH_TOKEN);
      
      if (token) {
        // Adiciona o token no header Authorization
        config.headers.Authorization = `Bearer ${token}`;
        log('Token JWT adicionado à requisição');
      }
      
      // Log da requisição em desenvolvimento
      log(`Requisição: ${config.method?.toUpperCase()} ${config.url}`);
      
      return config;
    } catch (error) {
      logError(error, 'Erro ao adicionar token à requisição');
      return config;
    }
  },
  (error) => {
    logError(error, 'Erro no interceptador de requisição');
    return Promise.reject(error);
  }
);

/**
 * Interceptador de Resposta
 * Trata erros de forma centralizada
 */
clienteHttp.interceptors.response.use(
  (response) => {
    // Resposta bem-sucedida
    log(`Resposta: ${response.status} - ${response.config.url}`);
    return response;
  },
  async (error) => {
    // Extrai informações do erro
    const originalRequest = error.config;
    const status = error.response?.status;
    
    logError(error, `Erro na requisição: ${originalRequest?.url}`);
    
    // Tratamento específico por código de status
    if (status === 401) {
      // Token expirado ou inválido
      
      // Evita loop infinito
      if (originalRequest._retry) {
        // Se já tentou renovar o token, desloga o usuário
        await limparSessao();
        return Promise.reject(criarErroAmigavel(error, ERROR_MESSAGES.UNAUTHORIZED));
      }
      
      // Marca que esta requisição já foi reprocessada
      originalRequest._retry = true;
      
      try {
        // Tenta renovar o token
        const tokenRenovado = await tentarRenovarToken();
        
        if (tokenRenovado) {
          // Atualiza o header da requisição original
          originalRequest.headers.Authorization = `Bearer ${tokenRenovado}`;
          // Tenta novamente a requisição original
          return clienteHttp(originalRequest);
        }
      } catch (refreshError) {
        // Falha ao renovar token, desloga o usuário
        await limparSessao();
        return Promise.reject(criarErroAmigavel(error, ERROR_MESSAGES.UNAUTHORIZED));
      }
    }
    
    if (status === 403) {
      // Acesso negado
      return Promise.reject(criarErroAmigavel(error, ERROR_MESSAGES.FORBIDDEN));
    }
    
    if (status === 404) {
      // Recurso não encontrado
      return Promise.reject(criarErroAmigavel(error, ERROR_MESSAGES.NOT_FOUND));
    }
    
    if (status >= 500) {
      // Erro no servidor
      return Promise.reject(criarErroAmigavel(error, ERROR_MESSAGES.SERVER_ERROR));
    }
    
    if (error.code === 'ECONNABORTED') {
      // Timeout
      return Promise.reject(criarErroAmigavel(error, ERROR_MESSAGES.TIMEOUT_ERROR));
    }
    
    if (error.message === 'Network Error') {
      // Erro de rede
      return Promise.reject(criarErroAmigavel(error, ERROR_MESSAGES.NETWORK_ERROR));
    }
    
    // Erro genérico
    return Promise.reject(criarErroAmigavel(error, ERROR_MESSAGES.UNKNOWN_ERROR));
  }
);

/**
 * Cria um objeto de erro amigável para o usuário
 */
const criarErroAmigavel = (error, mensagemPadrao) => {
  return {
    mensagem: error.response?.data?.message || mensagemPadrao,
    status: error.response?.status,
    dados: error.response?.data,
    erroOriginal: error,
  };
};

/**
 * Tenta renovar o token JWT
 */
const tentarRenovarToken = async () => {
  try {
    // Busca o token atual
    const token = await AsyncStorage.getItem(STORAGE_KEYS.AUTH_TOKEN);
    
    if (!token) {
      return null;
    }
    
    // Faz requisição para renovar o token
    const response = await axios.post(
      `${getApiUrl()}/auth/refresh`,
      {},
      {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }
    );
    
    const novoToken = response.data?.token;
    
    if (novoToken) {
      // Salva o novo token
      await AsyncStorage.setItem(STORAGE_KEYS.AUTH_TOKEN, novoToken);
      log('Token renovado com sucesso');
      return novoToken;
    }
    
    return null;
  } catch (error) {
    logError(error, 'Erro ao renovar token');
    return null;
  }
};

/**
 * Limpa a sessão do usuário
 */
const limparSessao = async () => {
  try {
    await AsyncStorage.multiRemove([
      STORAGE_KEYS.AUTH_TOKEN,
      STORAGE_KEYS.USER_DATA,
    ]);
    log('Sessão limpa');
  } catch (error) {
    logError(error, 'Erro ao limpar sessão');
  }
};

/**
 * Define o token de autenticação manualmente
 */
export const setAuthToken = async (token) => {
  try {
    if (token) {
      await AsyncStorage.setItem(STORAGE_KEYS.AUTH_TOKEN, token);
      log('Token definido');
    } else {
      await AsyncStorage.removeItem(STORAGE_KEYS.AUTH_TOKEN);
      log('Token removido');
    }
  } catch (error) {
    logError(error, 'Erro ao definir token');
  }
};

/**
 * Obtém o token de autenticação
 */
export const getAuthToken = async () => {
  try {
    return await AsyncStorage.getItem(STORAGE_KEYS.AUTH_TOKEN);
  } catch (error) {
    logError(error, 'Erro ao obter token');
    return null;
  }
};

/**
 * Remove o token de autenticação
 */
export const clearAuthToken = async () => {
  try {
    await AsyncStorage.removeItem(STORAGE_KEYS.AUTH_TOKEN);
    log('Token removido');
  } catch (error) {
    logError(error, 'Erro ao remover token');
  }
};

export default clienteHttp;
