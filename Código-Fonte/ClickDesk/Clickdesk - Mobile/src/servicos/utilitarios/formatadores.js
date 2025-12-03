// 🎨 Utilitários de formatação de dados

/**
 * Formata uma data para o padrão brasileiro
 * @param {Date|string} data - Data a ser formatada
 * @returns {string} Data formatada (DD/MM/YYYY)
 */
export const formatarData = (data) => {
  const dataObj = typeof data === 'string' ? new Date(data) : data;
  const dia = String(dataObj.getDate()).padStart(2, '0');
  const mes = String(dataObj.getMonth() + 1).padStart(2, '0');
  const ano = dataObj.getFullYear();
  return `${dia}/${mes}/${ano}`;
};

/**
 * Formata uma data e hora para o padrão brasileiro
 * @param {Date|string} data - Data a ser formatada
 * @returns {string} Data e hora formatadas (DD/MM/YYYY HH:MM)
 */
export const formatarDataHora = (data) => {
  const dataObj = typeof data === 'string' ? new Date(data) : data;
  const dia = String(dataObj.getDate()).padStart(2, '0');
  const mes = String(dataObj.getMonth() + 1).padStart(2, '0');
  const ano = dataObj.getFullYear();
  const hora = String(dataObj.getHours()).padStart(2, '0');
  const minuto = String(dataObj.getMinutes()).padStart(2, '0');
  return `${dia}/${mes}/${ano} ${hora}:${minuto}`;
};

/**
 * Formata um CPF
 * @param {string} cpf - CPF a ser formatado
 * @returns {string} CPF formatado (XXX.XXX.XXX-XX)
 */
export const formatarCPF = (cpf) => {
  const cpfLimpo = cpf.replace(/[^\d]/g, '');
  return cpfLimpo.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, '$1.$2.$3-$4');
};

/**
 * Formata um telefone brasileiro
 * @param {string} telefone - Telefone a ser formatado
 * @returns {string} Telefone formatado
 */
export const formatarTelefone = (telefone) => {
  const telefoneLimpo = telefone.replace(/[^\d]/g, '');
  
  if (telefoneLimpo.length === 11) {
    // Celular: (XX) XXXXX-XXXX
    return telefoneLimpo.replace(/(\d{2})(\d{5})(\d{4})/, '($1) $2-$3');
  } else if (telefoneLimpo.length === 10) {
    // Fixo: (XX) XXXX-XXXX
    return telefoneLimpo.replace(/(\d{2})(\d{4})(\d{4})/, '($1) $2-$3');
  }
  
  return telefone;
};

/**
 * Formata um valor monetário em real brasileiro
 * @param {number} valor - Valor a ser formatado
 * @returns {string} Valor formatado (R$ X.XXX,XX)
 */
export const formatarMoeda = (valor) => {
  return new Intl.NumberFormat('pt-BR', {
    style: 'currency',
    currency: 'BRL'
  }).format(valor);
};

/**
 * Trunca um texto com reticências
 * @param {string} texto - Texto a ser truncado
 * @param {number} tamanhoMaximo - Tamanho máximo do texto
 * @returns {string} Texto truncado
 */
export const truncarTexto = (texto, tamanhoMaximo = 50) => {
  if (texto.length <= tamanhoMaximo) return texto;
  return texto.substring(0, tamanhoMaximo) + '...';
};

/**
 * Capitaliza a primeira letra de cada palavra
 * @param {string} texto - Texto a ser capitalizado
 * @returns {string} Texto capitalizado
 */
export const capitalizarTexto = (texto) => {
  return texto
    .toLowerCase()
    .split(' ')
    .map(palavra => palavra.charAt(0).toUpperCase() + palavra.slice(1))
    .join(' ');
};

/**
 * Formata o status do chamado para exibição
 * @param {string} status - Status do chamado
 * @returns {string} Status formatado
 */
export const formatarStatusChamado = (status) => {
  const statusMap = {
    'ABERTO': 'Aberto',
    'EM_ATENDIMENTO': 'Em Atendimento',
    'AGUARDANDO': 'Aguardando',
    'RESOLVIDO_AUTOMATICO': 'Resolvido Automaticamente',
    'RESOLVIDO': 'Resolvido',
    'FECHADO': 'Fechado',
    'ESCALADO': 'Escalado'
  };
  return statusMap[status] || status;
};

/**
 * Formata a severidade do chamado para exibição
 * @param {string} severidade - Severidade do chamado
 * @returns {string} Severidade formatada
 */
export const formatarSeveridade = (severidade) => {
  const severidadeMap = {
    'BAIXA': 'Baixa',
    'MEDIA': 'Média',
    'ALTA': 'Alta',
    'CRITICA': 'Crítica'
  };
  return severidadeMap[severidade] || severidade;
};

/**
 * Formata a categoria do chamado para exibição
 * @param {string} categoria - Categoria do chamado
 * @returns {string} Categoria formatada
 */
export const formatarCategoria = (categoria) => {
  const categoriaMap = {
    'SOFTWARE': 'Software',
    'HARDWARE': 'Hardware',
    'REDES': 'Redes',
    'TREINAMENTO': 'Treinamento',
    'OUTROS': 'Outros'
  };
  return categoriaMap[categoria] || categoria;
};
