/**
 * Estilos Globais do ClickDesk
 * 
 * Define estilos reutilizáveis em toda a aplicação
 */

import { StyleSheet } from 'react-native';
import { Cores } from './cores';

export const EstilosGlobais = StyleSheet.create({
  // ============================================
  // CONTAINERS
  // ============================================
  
  container: {
    flex: 1,
    backgroundColor: Cores.background,
  },
  
  containerCentralizado: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: Cores.background,
  },
  
  containerPadding: {
    flex: 1,
    padding: 16,
    backgroundColor: Cores.background,
  },
  
  // ============================================
  // CARDS
  // ============================================
  
  card: {
    backgroundColor: Cores.backgroundCard,
    borderRadius: 12,
    padding: 16,
    marginVertical: 8,
    shadowColor: Cores.preto,
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.1,
    shadowRadius: 4,
    elevation: 3,
  },
  
  cardSemSombra: {
    backgroundColor: Cores.backgroundCard,
    borderRadius: 12,
    padding: 16,
    marginVertical: 8,
  },
  
  // ============================================
  // TEXTOS
  // ============================================
  
  titulo: {
    fontSize: 24,
    fontWeight: 'bold',
    color: Cores.textoPrincipal,
    marginBottom: 8,
  },
  
  subtitulo: {
    fontSize: 18,
    fontWeight: '600',
    color: Cores.textoPrincipal,
    marginBottom: 4,
  },
  
  texto: {
    fontSize: 16,
    color: Cores.textoPrincipal,
  },
  
  textoSecundario: {
    fontSize: 14,
    color: Cores.textoSecundario,
  },
  
  textoPequeno: {
    fontSize: 12,
    color: Cores.textoSecundario,
  },
  
  // ============================================
  // BOTÕES
  // ============================================
  
  botao: {
    backgroundColor: Cores.brand,
    borderRadius: 8,
    padding: 14,
    alignItems: 'center',
    justifyContent: 'center',
  },
  
  botaoSecundario: {
    backgroundColor: Cores.primary,
    borderRadius: 8,
    padding: 14,
    alignItems: 'center',
    justifyContent: 'center',
  },
  
  botaoTexto: {
    color: Cores.branco,
    fontSize: 16,
    fontWeight: 'bold',
  },
  
  botaoOutline: {
    backgroundColor: 'transparent',
    borderWidth: 2,
    borderColor: Cores.brand,
    borderRadius: 8,
    padding: 14,
    alignItems: 'center',
    justifyContent: 'center',
  },
  
  botaoOutlineTexto: {
    color: Cores.brand,
    fontSize: 16,
    fontWeight: 'bold',
  },
  
  // ============================================
  // INPUTS
  // ============================================
  
  input: {
    backgroundColor: Cores.branco,
    borderWidth: 1,
    borderColor: Cores.bordaPadrao,
    borderRadius: 8,
    padding: 12,
    fontSize: 16,
    color: Cores.textoPrincipal,
  },
  
  inputErro: {
    borderColor: Cores.danger,
  },
  
  labelInput: {
    fontSize: 14,
    fontWeight: '600',
    color: Cores.textoPrincipal,
    marginBottom: 4,
  },
  
  textoErro: {
    fontSize: 12,
    color: Cores.danger,
    marginTop: 4,
  },
  
  // ============================================
  // BADGES/TAGS
  // ============================================
  
  badge: {
    paddingHorizontal: 8,
    paddingVertical: 4,
    borderRadius: 12,
    alignSelf: 'flex-start',
  },
  
  badgeTexto: {
    fontSize: 12,
    fontWeight: '600',
    color: Cores.branco,
  },
  
  // ============================================
  // SEPARADORES
  // ============================================
  
  separador: {
    height: 1,
    backgroundColor: Cores.bordaPadrao,
    marginVertical: 8,
  },
  
  separadorGrosso: {
    height: 2,
    backgroundColor: Cores.bordaEscura,
    marginVertical: 16,
  },
  
  // ============================================
  // LISTAS
  // ============================================
  
  lista: {
    flex: 1,
  },
  
  listaConteudo: {
    padding: 16,
  },
  
  itemLista: {
    padding: 12,
    marginVertical: 4,
    backgroundColor: Cores.backgroundCard,
    borderRadius: 8,
  },
  
  // ============================================
  // CENTRALIZAÇÃO
  // ============================================
  
  centralizadoHorizontal: {
    alignItems: 'center',
  },
  
  centralizadoVertical: {
    justifyContent: 'center',
  },
  
  centralizado: {
    alignItems: 'center',
    justifyContent: 'center',
  },
  
  // ============================================
  // FLEX
  // ============================================
  
  flexRow: {
    flexDirection: 'row',
  },
  
  flexColumn: {
    flexDirection: 'column',
  },
  
  flexBetween: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
  },
  
  flexWrap: {
    flexWrap: 'wrap',
  },
  
  // ============================================
  // ESPAÇAMENTOS
  // ============================================
  
  mb4: { marginBottom: 4 },
  mb8: { marginBottom: 8 },
  mb12: { marginBottom: 12 },
  mb16: { marginBottom: 16 },
  mb24: { marginBottom: 24 },
  
  mt4: { marginTop: 4 },
  mt8: { marginTop: 8 },
  mt12: { marginTop: 12 },
  mt16: { marginTop: 16 },
  mt24: { marginTop: 24 },
  
  p4: { padding: 4 },
  p8: { padding: 8 },
  p12: { padding: 12 },
  p16: { padding: 16 },
  p24: { padding: 24 },
});

export default EstilosGlobais;
