// 💾 Utilitário para gerenciar armazenamento local com AsyncStorage
import AsyncStorage from '@react-native-async-storage/async-storage';

/**
 * Salva um item no armazenamento local
 * @param {string} chave - Chave do item
 * @param {any} valor - Valor a ser salvo (será convertido para JSON)
 */
export const salvarItem = async (chave, valor) => {
  try {
    const valorString = JSON.stringify(valor);
    await AsyncStorage.setItem(chave, valorString);
    return true;
  } catch (erro) {
    console.error(`❌ Erro ao salvar item '${chave}':`, erro);
    return false;
  }
};

/**
 * Recupera um item do armazenamento local
 * @param {string} chave - Chave do item
 * @returns {any} Valor recuperado (parseado do JSON)
 */
export const obterItem = async (chave) => {
  try {
    const valorString = await AsyncStorage.getItem(chave);
    return valorString ? JSON.parse(valorString) : null;
  } catch (erro) {
    console.error(`❌ Erro ao obter item '${chave}':`, erro);
    return null;
  }
};

/**
 * Remove um item do armazenamento local
 * @param {string} chave - Chave do item a ser removido
 */
export const removerItem = async (chave) => {
  try {
    await AsyncStorage.removeItem(chave);
    return true;
  } catch (erro) {
    console.error(`❌ Erro ao remover item '${chave}':`, erro);
    return false;
  }
};

/**
 * Limpa todo o armazenamento local
 */
export const limparTudo = async () => {
  try {
    await AsyncStorage.clear();
    return true;
  } catch (erro) {
    console.error('❌ Erro ao limpar armazenamento:', erro);
    return false;
  }
};

/**
 * Verifica se um item existe no armazenamento
 * @param {string} chave - Chave do item
 * @returns {boolean} true se o item existe
 */
export const itemExiste = async (chave) => {
  try {
    const valor = await AsyncStorage.getItem(chave);
    return valor !== null;
  } catch (erro) {
    console.error(`❌ Erro ao verificar item '${chave}':`, erro);
    return false;
  }
};

// 🔑 Funções específicas para autenticação
export const salvarToken = (token) => salvarItem('token', token);
export const obterToken = () => obterItem('token');
export const removerToken = () => removerItem('token');

export const salvarUsuario = (usuario) => salvarItem('usuario', usuario);
export const obterUsuario = () => obterItem('usuario');
export const removerUsuario = () => removerItem('usuario');

/**
 * Realiza logout completo removendo todos os dados do usuário
 */
export const limparDadosUsuario = async () => {
  await removerToken();
  await removerUsuario();
};
