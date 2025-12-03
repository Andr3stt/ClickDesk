/**
 * Utilitário de Armazenamento Local
 * 
 * Fornece funções para salvar e recuperar dados no AsyncStorage
 * de forma simplificada e com tratamento de erros.
 */

import AsyncStorage from '@react-native-async-storage/async-storage';
import { log, logError } from '../../configuracao/ambiente';

/**
 * Salva um valor no AsyncStorage
 * 
 * @param {string} chave - Chave para armazenar o valor
 * @param {any} valor - Valor a ser armazenado (será convertido para JSON)
 * @returns {Promise<boolean>} True se salvo com sucesso
 */
export const salvar = async (chave, valor) => {
  try {
    const valorString = typeof valor === 'string' ? valor : JSON.stringify(valor);
    await AsyncStorage.setItem(chave, valorString);
    log(`Valor salvo: ${chave}`);
    return true;
  } catch (error) {
    logError(error, `Erro ao salvar valor: ${chave}`);
    return false;
  }
};

/**
 * Recupera um valor do AsyncStorage
 * 
 * @param {string} chave - Chave do valor a ser recuperado
 * @param {any} valorPadrao - Valor padrão se não encontrado
 * @returns {Promise<any>} Valor recuperado ou valorPadrao
 */
export const obter = async (chave, valorPadrao = null) => {
  try {
    const valor = await AsyncStorage.getItem(chave);
    
    if (valor === null) {
      return valorPadrao;
    }
    
    // Tenta fazer parse se for JSON
    try {
      return JSON.parse(valor);
    } catch {
      // Se não for JSON, retorna como string
      return valor;
    }
  } catch (error) {
    logError(error, `Erro ao obter valor: ${chave}`);
    return valorPadrao;
  }
};

/**
 * Remove um valor do AsyncStorage
 * 
 * @param {string} chave - Chave do valor a ser removido
 * @returns {Promise<boolean>} True se removido com sucesso
 */
export const remover = async (chave) => {
  try {
    await AsyncStorage.removeItem(chave);
    log(`Valor removido: ${chave}`);
    return true;
  } catch (error) {
    logError(error, `Erro ao remover valor: ${chave}`);
    return false;
  }
};

/**
 * Remove múltiplos valores do AsyncStorage
 * 
 * @param {Array<string>} chaves - Array de chaves a serem removidas
 * @returns {Promise<boolean>} True se todas removidas com sucesso
 */
export const removerMultiplos = async (chaves) => {
  try {
    await AsyncStorage.multiRemove(chaves);
    log(`Valores removidos: ${chaves.join(', ')}`);
    return true;
  } catch (error) {
    logError(error, 'Erro ao remover múltiplos valores');
    return false;
  }
};

/**
 * Limpa todos os dados do AsyncStorage
 * 
 * @returns {Promise<boolean>} True se limpo com sucesso
 */
export const limparTudo = async () => {
  try {
    await AsyncStorage.clear();
    log('AsyncStorage limpo');
    return true;
  } catch (error) {
    logError(error, 'Erro ao limpar AsyncStorage');
    return false;
  }
};

/**
 * Obtém todas as chaves armazenadas
 * 
 * @returns {Promise<Array<string>>} Array de chaves
 */
export const obterTodasChaves = async () => {
  try {
    const chaves = await AsyncStorage.getAllKeys();
    return chaves;
  } catch (error) {
    logError(error, 'Erro ao obter chaves');
    return [];
  }
};

/**
 * Verifica se uma chave existe
 * 
 * @param {string} chave - Chave a ser verificada
 * @returns {Promise<boolean>} True se a chave existe
 */
export const existe = async (chave) => {
  try {
    const valor = await AsyncStorage.getItem(chave);
    return valor !== null;
  } catch (error) {
    logError(error, `Erro ao verificar existência: ${chave}`);
    return false;
  }
};

export default {
  salvar,
  obter,
  remover,
  removerMultiplos,
  limparTudo,
  obterTodasChaves,
  existe,
};
