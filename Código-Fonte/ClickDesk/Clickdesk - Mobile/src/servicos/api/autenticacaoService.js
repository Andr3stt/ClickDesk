import clienteHttp from './clienteHttp';
import { ENDPOINTS } from '../../configuracao/constantes';

const autenticacaoService = {
  registrar: async (dados) => {
    const response = await clienteHttp.post(ENDPOINTS.AUTH_REGISTER, dados);
    return response.data;
  },

  verificarEmail: async (token, dados = {}) => {
    const response = await clienteHttp.post(
      `${ENDPOINTS.AUTH_VERIFY_EMAIL}?token=${token}`,
      dados
    );
    return response.data;
  },

  definirSenha: async (dados) => {
    const response = await clienteHttp.post(ENDPOINTS.AUTH_SET_PASSWORD, dados);
    return response.data;
  },

  login: async (credenciais) => {
    const response = await clienteHttp.post(ENDPOINTS.AUTH_LOGIN, credenciais);
    if (response.data.token) {
      localStorage.setItem('token', response.data.token);
      localStorage.setItem('usuario', JSON.stringify({
        id: response.data.id,
        email: response.data.email,
        username: response.data.username,
        roles: response.data.roles
      }));
    }
    return response.data;
  },

  logout: () => {
    localStorage.removeItem('token');
    localStorage.removeItem('usuario');
    window.location.href = '/login';
  },

  estaAutenticado: () => !!localStorage.getItem('token'),

  obterUsuarioLogado: () => {
    const usuario = localStorage.getItem('usuario');
    return usuario ? JSON.parse(usuario) : null;
  }
};

export default autenticacaoService;
