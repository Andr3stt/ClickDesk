/**
 * Serviço de Inteligência Artificial
 * 
 * Gerencia as operações relacionadas à IA:
 * - Solicitar assistência da IA
 * - Analisar problemas
 * - Obter sugestões de solução
 */

import clienteHttp from './clienteHttp';
import { IA_ENDPOINTS } from '../../configuracao/constantes';
import { log, logError } from '../../configuracao/ambiente';

/**
 * Solicita assistência da IA para um problema
 * 
 * @param {string} descricao - Descrição do problema
 * @param {string} categoria - Categoria do problema (opcional)
 * @returns {Promise<Object>} Resposta da IA com sugestão de solução
 */
export const solicitarAssistencia = async (descricao, categoria = null) => {
  try {
    log('Solicitando assistência da IA...');
    
    const response = await clienteHttp.post(IA_ENDPOINTS.ASSIST, {
      descricao,
      categoria,
    });
    
    log('Resposta da IA recebida');
    return response.data;
  } catch (error) {
    logError(error, 'Erro ao solicitar assistência da IA');
    throw error;
  }
};

/**
 * Analisa um problema específico
 * 
 * @param {Object} problema - Dados do problema
 * @param {string} problema.titulo - Título do problema
 * @param {string} problema.descricao - Descrição detalhada
 * @param {string} problema.categoria - Categoria
 * @returns {Promise<Object>} Análise da IA
 */
export const analisarProblema = async (problema) => {
  try {
    log('Analisando problema com IA...');
    
    const response = await clienteHttp.post(IA_ENDPOINTS.ANALYZE, {
      titulo: problema.titulo,
      descricao: problema.descricao,
      categoria: problema.categoria,
    });
    
    log('Análise da IA concluída');
    return response.data;
  } catch (error) {
    logError(error, 'Erro ao analisar problema');
    throw error;
  }
};

export default {
  solicitarAssistencia,
  analisarProblema,
};
