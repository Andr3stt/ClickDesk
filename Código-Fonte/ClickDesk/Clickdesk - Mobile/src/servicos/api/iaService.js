import clienteHttp from './clienteHttp';
import { ENDPOINTS } from '../../configuracao/constantes';

const iaService = {
  solicitarAssistencia: async (descricao) => {
    const response = await clienteHttp.post(ENDPOINTS.IA_ASSIST, { descricao });
    return response.data;
  }
};

export default iaService;
