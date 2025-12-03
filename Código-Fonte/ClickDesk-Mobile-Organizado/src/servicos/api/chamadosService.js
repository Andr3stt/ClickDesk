/**
 * Serviço de Chamados
 * 
 * Gerencia todas as operações relacionadas a chamados (tickets):
 * - Listar chamados
 * - Criar novo chamado
 * - Atualizar chamado
 * - Atualizar status
 * - Enviar feedback
 * - Excluir chamado
 * - Comentários
 */

import clienteHttp from './clienteHttp';
import { CHAMADO_ENDPOINTS } from '../../configuracao/constantes';
import { log, logError } from '../../configuracao/ambiente';

/**
 * Lista todos os chamados (para Admin/Tech)
 * 
 * @returns {Promise<Array>} Lista de chamados
 */
export const listar = async () => {
  try {
    log('Buscando todos os chamados...');
    
    const response = await clienteHttp.get(CHAMADO_ENDPOINTS.BASE);
    
    log(`${response.data.length} chamados encontrados`);
    return response.data;
  } catch (error) {
    logError(error, 'Erro ao listar chamados');
    throw error;
  }
};

/**
 * Lista os chamados do usuário logado
 * 
 * @returns {Promise<Array>} Lista de chamados do usuário
 */
export const listarMeusChamados = async () => {
  try {
    log('Buscando meus chamados...');
    
    const response = await clienteHttp.get(CHAMADO_ENDPOINTS.MEUS);
    
    log(`${response.data.length} chamados encontrados`);
    return response.data;
  } catch (error) {
    logError(error, 'Erro ao listar meus chamados');
    throw error;
  }
};

/**
 * Lista os chamados pendentes
 * 
 * @returns {Promise<Array>} Lista de chamados pendentes
 */
export const listarPendentes = async () => {
  try {
    log('Buscando chamados pendentes...');
    
    const response = await clienteHttp.get(CHAMADO_ENDPOINTS.PENDENTES);
    
    log(`${response.data.length} chamados pendentes encontrados`);
    return response.data;
  } catch (error) {
    logError(error, 'Erro ao listar chamados pendentes');
    throw error;
  }
};

/**
 * Busca um chamado específico por ID
 * 
 * @param {number} id - ID do chamado
 * @returns {Promise<Object>} Dados do chamado
 */
export const buscarPorId = async (id) => {
  try {
    log(`Buscando chamado ${id}...`);
    
    const response = await clienteHttp.get(CHAMADO_ENDPOINTS.BY_ID(id));
    
    log('Chamado encontrado');
    return response.data;
  } catch (error) {
    logError(error, `Erro ao buscar chamado ${id}`);
    throw error;
  }
};

/**
 * Cria um novo chamado
 * 
 * @param {Object} chamado - Dados do chamado
 * @param {string} chamado.titulo - Título do chamado
 * @param {string} chamado.descricao - Descrição do problema
 * @param {string} chamado.categoria - Categoria (HARDWARE, SOFTWARE, REDE, etc.)
 * @param {string} chamado.prioridade - Prioridade (BAIXA, MEDIA, ALTA, CRITICA)
 * @returns {Promise<Object>} Chamado criado com resposta da IA
 */
export const criar = async (chamado) => {
  try {
    log('Criando novo chamado...');
    
    const response = await clienteHttp.post(CHAMADO_ENDPOINTS.BASE, {
      titulo: chamado.titulo,
      descricao: chamado.descricao,
      categoria: chamado.categoria,
      prioridade: chamado.prioridade || 'MEDIA',
    });
    
    log('Chamado criado com sucesso');
    return response.data;
  } catch (error) {
    logError(error, 'Erro ao criar chamado');
    throw error;
  }
};

/**
 * Atualiza um chamado existente
 * 
 * @param {number} id - ID do chamado
 * @param {Object} dados - Dados atualizados
 * @returns {Promise<Object>} Chamado atualizado
 */
export const atualizar = async (id, dados) => {
  try {
    log(`Atualizando chamado ${id}...`);
    
    const response = await clienteHttp.put(CHAMADO_ENDPOINTS.BY_ID(id), dados);
    
    log('Chamado atualizado com sucesso');
    return response.data;
  } catch (error) {
    logError(error, `Erro ao atualizar chamado ${id}`);
    throw error;
  }
};

/**
 * Atualiza o status de um chamado
 * 
 * @param {number} id - ID do chamado
 * @param {string} status - Novo status (PENDENTE, EM_ANALISE, EM_ATENDIMENTO, RESOLVIDO, etc.)
 * @param {string} observacao - Observação opcional sobre a mudança de status
 * @returns {Promise<Object>} Chamado atualizado
 */
export const atualizarStatus = async (id, status, observacao = null) => {
  try {
    log(`Atualizando status do chamado ${id} para ${status}...`);
    
    const response = await clienteHttp.put(CHAMADO_ENDPOINTS.STATUS(id), {
      status,
      observacao,
    });
    
    log('Status atualizado com sucesso');
    return response.data;
  } catch (error) {
    logError(error, `Erro ao atualizar status do chamado ${id}`);
    throw error;
  }
};

/**
 * Envia feedback sobre a resolução do chamado
 * 
 * @param {number} id - ID do chamado
 * @param {Object} feedback - Dados do feedback
 * @param {boolean} feedback.resolvido - Se o problema foi resolvido
 * @param {number} feedback.nota - Nota de 1 a 5
 * @param {string} feedback.comentario - Comentário opcional
 * @returns {Promise<Object>} Chamado atualizado
 */
export const enviarFeedback = async (id, feedback) => {
  try {
    log(`Enviando feedback para chamado ${id}...`);
    
    const response = await clienteHttp.post(CHAMADO_ENDPOINTS.FEEDBACK(id), {
      resolvido: feedback.resolvido,
      nota: feedback.nota,
      comentario: feedback.comentario,
    });
    
    log('Feedback enviado com sucesso');
    return response.data;
  } catch (error) {
    logError(error, `Erro ao enviar feedback do chamado ${id}`);
    throw error;
  }
};

/**
 * Adiciona um comentário ao chamado
 * 
 * @param {number} id - ID do chamado
 * @param {string} texto - Texto do comentário
 * @returns {Promise<Object>} Comentário criado
 */
export const adicionarComentario = async (id, texto) => {
  try {
    log(`Adicionando comentário ao chamado ${id}...`);
    
    const response = await clienteHttp.post(CHAMADO_ENDPOINTS.COMENTARIOS(id), {
      texto,
    });
    
    log('Comentário adicionado com sucesso');
    return response.data;
  } catch (error) {
    logError(error, `Erro ao adicionar comentário ao chamado ${id}`);
    throw error;
  }
};

/**
 * Lista os comentários de um chamado
 * 
 * @param {number} id - ID do chamado
 * @returns {Promise<Array>} Lista de comentários
 */
export const listarComentarios = async (id) => {
  try {
    log(`Buscando comentários do chamado ${id}...`);
    
    const response = await clienteHttp.get(CHAMADO_ENDPOINTS.COMENTARIOS(id));
    
    log(`${response.data.length} comentários encontrados`);
    return response.data;
  } catch (error) {
    logError(error, `Erro ao listar comentários do chamado ${id}`);
    throw error;
  }
};

/**
 * Obtém estatísticas dos chamados para o dashboard
 * 
 * @returns {Promise<Object>} Estatísticas dos chamados
 */
export const obterEstatisticas = async () => {
  try {
    log('Buscando estatísticas dos chamados...');
    
    const response = await clienteHttp.get(CHAMADO_ENDPOINTS.STATS);
    
    log('Estatísticas obtidas com sucesso');
    return response.data;
  } catch (error) {
    logError(error, 'Erro ao obter estatísticas');
    throw error;
  }
};

/**
 * Exclui um chamado (apenas Admin)
 * 
 * @param {number} id - ID do chamado
 * @returns {Promise<Object>} Resultado da exclusão
 */
export const excluir = async (id) => {
  try {
    log(`Excluindo chamado ${id}...`);
    
    const response = await clienteHttp.delete(CHAMADO_ENDPOINTS.BY_ID(id));
    
    log('Chamado excluído com sucesso');
    return response.data;
  } catch (error) {
    logError(error, `Erro ao excluir chamado ${id}`);
    throw error;
  }
};

export default {
  listar,
  listarMeusChamados,
  listarPendentes,
  buscarPorId,
  criar,
  atualizar,
  atualizarStatus,
  enviarFeedback,
  adicionarComentario,
  listarComentarios,
  obterEstatisticas,
  excluir,
};
