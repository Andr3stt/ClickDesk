import clienteHttp from './clienteHttp';
import { ENDPOINTS } from '../../configuracao/constantes';

const chamadosService = {
  listar: async () => {
    const response = await clienteHttp.get(ENDPOINTS.CHAMADOS);
    return response.data;
  },

  listarMeusChamados: async () => {
    const response = await clienteHttp.get(ENDPOINTS.CHAMADOS_MEUS);
    return response.data;
  },

  listarPendentes: async () => {
    const response = await clienteHttp.get(ENDPOINTS.CHAMADOS_PENDENTES);
    return response.data;
  },

  buscarPorId: async (id) => {
    const response = await clienteHttp.get(`${ENDPOINTS.CHAMADOS}/${id}`);
    return response.data;
  },

  criar: async (chamado) => {
    const response = await clienteHttp.post(ENDPOINTS.CHAMADOS, chamado);
    return response.data;
  },

  atualizarStatus: async (id, novoStatus) => {
    const response = await clienteHttp.put(
      `${ENDPOINTS.CHAMADOS_STATUS(id)}?novoStatus=${novoStatus}`
    );
    return response.data;
  },

  enviarFeedback: async (id, feedback) => {
    const response = await clienteHttp.post(
      ENDPOINTS.CHAMADOS_FEEDBACK(id),
      feedback
    );
    return response.data;
  },

  excluir: async (id) => {
    const response = await clienteHttp.delete(`${ENDPOINTS.CHAMADOS}/${id}`);
    return response.data;
  }
};

export default chamadosService;
