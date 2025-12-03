/**
 * Temas do ClickDesk
 * 
 * Define os temas claro e escuro da aplicação
 */

import { Cores } from './cores';

/**
 * Tema Claro (padrão)
 */
export const TemaClaro = {
  // Backgrounds
  background: Cores.background,
  backgroundCard: Cores.backgroundCard,
  backgroundSecundario: Cores.gray50,
  
  // Textos
  texto: Cores.textoPrincipal,
  textoSecundario: Cores.textoSecundario,
  textoInverso: Cores.branco,
  
  // Bordas
  borda: Cores.bordaPadrao,
  bordaEscura: Cores.bordaEscura,
  
  // Cores de ação
  primary: Cores.brand,
  secondary: Cores.primary,
  
  // Cores semânticas
  success: Cores.success,
  danger: Cores.danger,
  warning: Cores.warning,
  info: Cores.info,
  
  // Sombras
  sombra: {
    shadowColor: Cores.preto,
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.1,
    shadowRadius: 4,
    elevation: 3,
  },
  
  sombraCard: {
    shadowColor: Cores.preto,
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.15,
    shadowRadius: 8,
    elevation: 5,
  },
};

/**
 * Tema Escuro
 */
export const TemaEscuro = {
  // Backgrounds
  background: Cores.gray900,
  backgroundCard: Cores.gray800,
  backgroundSecundario: Cores.gray700,
  
  // Textos
  texto: Cores.gray100,
  textoSecundario: Cores.gray400,
  textoInverso: Cores.gray900,
  
  // Bordas
  borda: Cores.gray700,
  bordaEscura: Cores.gray600,
  
  // Cores de ação
  primary: Cores.brandLight,
  secondary: Cores.primaryLight,
  
  // Cores semânticas
  success: Cores.successLight,
  danger: Cores.dangerLight,
  warning: Cores.warningLight,
  info: Cores.infoLight,
  
  // Sombras
  sombra: {
    shadowColor: Cores.preto,
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.3,
    shadowRadius: 4,
    elevation: 3,
  },
  
  sombraCard: {
    shadowColor: Cores.preto,
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.4,
    shadowRadius: 8,
    elevation: 5,
  },
};

/**
 * Tema padrão da aplicação
 */
export const TemaAtual = TemaClaro;

export default {
  TemaClaro,
  TemaEscuro,
  TemaAtual,
};
