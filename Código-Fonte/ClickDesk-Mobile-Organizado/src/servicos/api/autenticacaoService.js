/**
 * Serviço de Autenticação
 * 
 * Gerencia todas as operações relacionadas à autenticação:
 * - Login
 * - Registro
 * - Logout
 * - Verificação de email
 * - Definição de senha
 * - Recuperação de senha
 */

import clienteHttp, { setAuthToken, clearAuthToken } from './clienteHttp';
import AsyncStorage from '@react-native-async-storage/async-storage';
import { AUTH_ENDPOINTS, STORAGE_KEYS } from '../../configuracao/constantes';
import { log, logError } from '../../configuracao/ambiente';

/**
 * Realiza o login do usuário
 * 
 * @param {Object} credenciais - Credenciais de login
 * @param {string} credenciais.username - Nome de usuário ou email
 * @param {string} credenciais.password - Senha
 * @returns {Promise<Object>} Dados do usuário e token
 */
export const login = async (credenciais) => {
  try {
    log('Tentando fazer login...');
    
    const response = await clienteHttp.post(AUTH_ENDPOINTS.LOGIN, {
      username: credenciais.username,
      password: credenciais.password,
    });
    
    const { token, user } = response.data;
    
    if (token && user) {
      // Salva o token e dados do usuário
      await setAuthToken(token);
      await AsyncStorage.setItem(STORAGE_KEYS.USER_DATA, JSON.stringify(user));
      
      log('Login realizado com sucesso');
      return { success: true, user, token };
    }
    
    throw new Error('Resposta inválida do servidor');
  } catch (error) {
    logError(error, 'Erro ao fazer login');
    throw error;
  }
};

/**
 * Realiza o registro de um novo usuário
 * 
 * @param {Object} dados - Dados do novo usuário
 * @param {string} dados.firstName - Nome
 * @param {string} dados.lastName - Sobrenome
 * @param {string} dados.email - Email
 * @param {string} dados.password - Senha
 * @param {string} dados.userType - Tipo de usuário (USER, TECH, ADMIN)
 * @returns {Promise<Object>} Resultado do registro
 */
export const registrar = async (dados) => {
  try {
    log('Tentando registrar novo usuário...');
    
    const response = await clienteHttp.post(AUTH_ENDPOINTS.REGISTER, {
      firstName: dados.firstName,
      lastName: dados.lastName,
      email: dados.email,
      password: dados.password,
      role: dados.userType || 'USER',
    });
    
    log('Registro realizado com sucesso');
    return { success: true, data: response.data };
  } catch (error) {
    logError(error, 'Erro ao registrar usuário');
    throw error;
  }
};

/**
 * Verifica o email do usuário
 * 
 * @param {string} token - Token de verificação enviado por email
 * @returns {Promise<Object>} Resultado da verificação
 */
export const verificarEmail = async (token) => {
  try {
    log('Verificando email...');
    
    const response = await clienteHttp.get(`${AUTH_ENDPOINTS.VERIFY_EMAIL}?token=${token}`);
    
    log('Email verificado com sucesso');
    return { success: true, data: response.data };
  } catch (error) {
    logError(error, 'Erro ao verificar email');
    throw error;
  }
};

/**
 * Define a senha do usuário (após registro ou recuperação)
 * 
 * @param {Object} dados - Dados para definição de senha
 * @param {string} dados.token - Token de verificação
 * @param {string} dados.password - Nova senha
 * @returns {Promise<Object>} Resultado da operação
 */
export const definirSenha = async (dados) => {
  try {
    log('Definindo senha...');
    
    const response = await clienteHttp.post(AUTH_ENDPOINTS.RESET_PASSWORD, {
      token: dados.token,
      password: dados.password,
    });
    
    log('Senha definida com sucesso');
    return { success: true, data: response.data };
  } catch (error) {
    logError(error, 'Erro ao definir senha');
    throw error;
  }
};

/**
 * Solicita recuperação de senha
 * 
 * @param {string} email - Email do usuário
 * @returns {Promise<Object>} Resultado da solicitação
 */
export const solicitarRecuperacaoSenha = async (email) => {
  try {
    log('Solicitando recuperação de senha...');
    
    const response = await clienteHttp.post(AUTH_ENDPOINTS.FORGOT_PASSWORD, {
      email,
    });
    
    log('Email de recuperação enviado');
    return { success: true, data: response.data };
  } catch (error) {
    logError(error, 'Erro ao solicitar recuperação de senha');
    throw error;
  }
};

/**
 * Realiza o logout do usuário
 * 
 * @returns {Promise<void>}
 */
export const logout = async () => {
  try {
    log('Fazendo logout...');
    
    // Tenta notificar o servidor (opcional, pode falhar)
    try {
      await clienteHttp.post(AUTH_ENDPOINTS.LOGOUT);
    } catch (error) {
      // Ignora erros do servidor no logout
      log('Erro ao notificar servidor do logout (ignorado)');
    }
    
    // Sempre limpa a sessão local
    await clearAuthToken();
    await AsyncStorage.removeItem(STORAGE_KEYS.USER_DATA);
    
    log('Logout realizado com sucesso');
    return { success: true };
  } catch (error) {
    logError(error, 'Erro ao fazer logout');
    // Mesmo com erro, limpa a sessão local
    await clearAuthToken();
    await AsyncStorage.removeItem(STORAGE_KEYS.USER_DATA);
    throw error;
  }
};

/**
 * Verifica se o usuário está autenticado
 * 
 * @returns {Promise<boolean>} True se autenticado
 */
export const estaAutenticado = async () => {
  try {
    const token = await AsyncStorage.getItem(STORAGE_KEYS.AUTH_TOKEN);
    const userData = await AsyncStorage.getItem(STORAGE_KEYS.USER_DATA);
    
    return !!(token && userData);
  } catch (error) {
    logError(error, 'Erro ao verificar autenticação');
    return false;
  }
};

/**
 * Obtém os dados do usuário logado
 * 
 * @returns {Promise<Object|null>} Dados do usuário ou null
 */
export const obterUsuarioLogado = async () => {
  try {
    const userData = await AsyncStorage.getItem(STORAGE_KEYS.USER_DATA);
    
    if (userData) {
      return JSON.parse(userData);
    }
    
    return null;
  } catch (error) {
    logError(error, 'Erro ao obter dados do usuário');
    return null;
  }
};

/**
 * Atualiza os dados do usuário logado no storage
 * 
 * @param {Object} userData - Novos dados do usuário
 * @returns {Promise<void>}
 */
export const atualizarUsuarioLogado = async (userData) => {
  try {
    await AsyncStorage.setItem(STORAGE_KEYS.USER_DATA, JSON.stringify(userData));
    log('Dados do usuário atualizados');
  } catch (error) {
    logError(error, 'Erro ao atualizar dados do usuário');
    throw error;
  }
};

/**
 * Renova o token de autenticação
 * 
 * @returns {Promise<Object>} Novo token
 */
export const renovarToken = async () => {
  try {
    log('Renovando token...');
    
    const response = await clienteHttp.post(AUTH_ENDPOINTS.REFRESH_TOKEN);
    
    const { token, user } = response.data;
    
    if (token) {
      await setAuthToken(token);
      
      if (user) {
        await AsyncStorage.setItem(STORAGE_KEYS.USER_DATA, JSON.stringify(user));
      }
      
      log('Token renovado com sucesso');
      return { success: true, token, user };
    }
    
    throw new Error('Token inválido na resposta');
  } catch (error) {
    logError(error, 'Erro ao renovar token');
    throw error;
  }
};

export default {
  login,
  registrar,
  verificarEmail,
  definirSenha,
  solicitarRecuperacaoSenha,
  logout,
  estaAutenticado,
  obterUsuarioLogado,
  atualizarUsuarioLogado,
  renovarToken,
};
