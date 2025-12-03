/**
 * Serviço de FAQ (Perguntas Frequentes)
 * 
 * Gerencia todas as operações relacionadas a FAQs:
 * - Listar FAQs
 * - Buscar FAQs
 * - Criar FAQ (Admin)
 * - Atualizar FAQ (Admin)
 * - Excluir FAQ (Admin)
 */

import clienteHttp from './clienteHttp';
import { FAQ_ENDPOINTS } from '../../configuracao/constantes';
import { log, logError } from '../../configuracao/ambiente';

/**
 * Lista todas as FAQs ativas
 * 
 * @returns {Promise<Array>} Lista de FAQs
 */
export const listar = async () => {
  try {
    log('Buscando FAQs...');
    
    const response = await clienteHttp.get(FAQ_ENDPOINTS.BASE);
    
    log(`${response.data.length} FAQs encontradas`);
    return response.data;
  } catch (error) {
    logError(error, 'Erro ao listar FAQs');
    throw error;
  }
};

/**
 * Busca FAQs por palavra-chave
 * 
 * @param {string} keyword - Palavra-chave para busca
 * @returns {Promise<Array>} Lista de FAQs correspondentes
 */
export const buscar = async (keyword) => {
  try {
    log(`Buscando FAQs com palavra-chave: ${keyword}...`);
    
    const response = await clienteHttp.get(
      `${FAQ_ENDPOINTS.SEARCH}?q=${encodeURIComponent(keyword)}`
    );
    
    log(`${response.data.length} FAQs encontradas`);
    return response.data;
  } catch (error) {
    logError(error, 'Erro ao buscar FAQs');
    throw error;
  }
};

/**
 * Busca uma FAQ específica por ID
 * 
 * @param {number} id - ID da FAQ
 * @returns {Promise<Object>} Dados da FAQ
 */
export const buscarPorId = async (id) => {
  try {
    log(`Buscando FAQ ${id}...`);
    
    const response = await clienteHttp.get(FAQ_ENDPOINTS.BY_ID(id));
    
    log('FAQ encontrada');
    return response.data;
  } catch (error) {
    logError(error, `Erro ao buscar FAQ ${id}`);
    throw error;
  }
};

/**
 * Cria uma nova FAQ (apenas Admin)
 * 
 * @param {Object} faq - Dados da FAQ
 * @param {string} faq.pergunta - Pergunta
 * @param {string} faq.resposta - Resposta
 * @param {string} faq.categoria - Categoria (opcional)
 * @param {Array<string>} faq.tags - Tags para busca (opcional)
 * @returns {Promise<Object>} FAQ criada
 */
export const criar = async (faq) => {
  try {
    log('Criando nova FAQ...');
    
    const response = await clienteHttp.post(FAQ_ENDPOINTS.BASE, {
      pergunta: faq.pergunta,
      resposta: faq.resposta,
      categoria: faq.categoria,
      tags: faq.tags || [],
    });
    
    log('FAQ criada com sucesso');
    return response.data;
  } catch (error) {
    logError(error, 'Erro ao criar FAQ');
    throw error;
  }
};

/**
 * Atualiza uma FAQ existente (apenas Admin)
 * 
 * @param {number} id - ID da FAQ
 * @param {Object} faq - Dados atualizados
 * @returns {Promise<Object>} FAQ atualizada
 */
export const atualizar = async (id, faq) => {
  try {
    log(`Atualizando FAQ ${id}...`);
    
    const response = await clienteHttp.put(FAQ_ENDPOINTS.BY_ID(id), faq);
    
    log('FAQ atualizada com sucesso');
    return response.data;
  } catch (error) {
    logError(error, `Erro ao atualizar FAQ ${id}`);
    throw error;
  }
};

/**
 * Exclui uma FAQ (apenas Admin)
 * 
 * @param {number} id - ID da FAQ
 * @returns {Promise<Object>} Resultado da exclusão
 */
export const excluir = async (id) => {
  try {
    log(`Excluindo FAQ ${id}...`);
    
    const response = await clienteHttp.delete(FAQ_ENDPOINTS.BY_ID(id));
    
    log('FAQ excluída com sucesso');
    return response.data;
  } catch (error) {
    logError(error, `Erro ao excluir FAQ ${id}`);
    throw error;
  }
};

export default {
  listar,
  buscar,
  buscarPorId,
  criar,
  atualizar,
  excluir,
};
