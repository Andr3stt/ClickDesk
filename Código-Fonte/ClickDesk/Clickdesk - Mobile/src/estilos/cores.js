// 🎨 Paleta de cores do aplicativo ClickDesk

/**
 * Cores principais do tema
 */
export const CoresPrincipais = {
  // Cores primárias
  laranja: '#E67E22',
  laranjaEscuro: '#D35400',
  laranjaClaro: '#F39C12',
  
  // Cores de fundo
  bege: '#EDE6D9',
  begeClaro: '#F5F0E8',
  begeEscuro: '#E8D5C4',
  
  // Cores de texto
  textoEscuro: '#2C3E50',
  textoMedio: '#34495E',
  textoClaro: '#7F8C8D',
  branco: '#FFFFFF',
  
  // Cores de status
  sucesso: '#27AE60',
  erro: '#E74C3C',
  aviso: '#F39C12',
  info: '#3498DB',
  
  // Cores neutras
  cinzaClaro: '#ECF0F1',
  cinzaMedio: '#BDC3C7',
  cinzaEscuro: '#95A5A6',
};

/**
 * Cores para status de chamados
 */
export const CoresStatus = {
  aberto: '#3498DB',
  emAtendimento: '#F39C12',
  aguardando: '#9B59B6',
  resolvidoAutomatico: '#1ABC9C',
  resolvido: '#27AE60',
  fechado: '#95A5A6',
  escalado: '#E74C3C',
};

/**
 * Cores para severidades
 */
export const CoresSeveridade = {
  baixa: '#27AE60',
  media: '#F39C12',
  alta: '#E67E22',
  critica: '#E74C3C',
};

/**
 * Cores de sombra
 */
export const Sombras = {
  leve: {
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.1,
    shadowRadius: 2,
    elevation: 2,
  },
  media: {
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.15,
    shadowRadius: 4,
    elevation: 4,
  },
  forte: {
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.2,
    shadowRadius: 6,
    elevation: 6,
  },
};

/**
 * Gradientes
 */
export const Gradientes = {
  laranja: ['#E67E22', '#F39C12'],
  azul: ['#3498DB', '#2980B9'],
  verde: ['#27AE60', '#2ECC71'],
  vermelho: ['#E74C3C', '#C0392B'],
};
