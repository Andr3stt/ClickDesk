import clienteHttp from './clienteHttp';
import { ENDPOINTS } from '../../configuracao/constantes';

const faqService = {
  listar: async () => {
    const response = await clienteHttp.get(ENDPOINTS.FAQS);
    return response.data;
  },

  buscar: async (keyword) => {
    const response = await clienteHttp.get(
      `${ENDPOINTS.FAQS_SEARCH}?keyword=${keyword}`
    );
    return response.data;
  },

  criar: async (faq) => {
    const response = await clienteHttp.post(ENDPOINTS.FAQS, faq);
    return response.data;
  },

  atualizar: async (id, faq) => {
    const response = await clienteHttp.put(`${ENDPOINTS.FAQS}/${id}`, faq);
    return response.data;
  },

  excluir: async (id) => {
    const response = await clienteHttp.delete(`${ENDPOINTS.FAQS}/${id}`);
    return response.data;
  }
};

export default faqService;
