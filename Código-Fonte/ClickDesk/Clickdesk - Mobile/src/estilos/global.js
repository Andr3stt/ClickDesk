// 🌐 Estilos globais da aplicação
import { StyleSheet } from 'react-native';
import { CoresPrincipais, Sombras } from './cores';

/**
 * Estilos globais reutilizáveis
 */
export const EstilosGlobais = StyleSheet.create({
  // Containers
  container: {
    flex: 1,
    backgroundColor: CoresPrincipais.begeClaro,
  },
  containerCentralizado: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: CoresPrincipais.begeClaro,
  },
  containerPadding: {
    flex: 1,
    padding: 16,
    backgroundColor: CoresPrincipais.begeClaro,
  },

  // Cartões
  cartao: {
    backgroundColor: CoresPrincipais.branco,
    borderRadius: 12,
    padding: 16,
    marginBottom: 16,
    ...Sombras.media,
  },
  cartaoClicavel: {
    backgroundColor: CoresPrincipais.branco,
    borderRadius: 12,
    padding: 16,
    marginBottom: 16,
    ...Sombras.media,
    activeOpacity: 0.7,
  },

  // Textos
  titulo: {
    fontSize: 24,
    fontWeight: 'bold',
    color: CoresPrincipais.textoEscuro,
    marginBottom: 8,
  },
  subtitulo: {
    fontSize: 18,
    fontWeight: '600',
    color: CoresPrincipais.textoMedio,
    marginBottom: 8,
  },
  texto: {
    fontSize: 16,
    color: CoresPrincipais.textoMedio,
    lineHeight: 24,
  },
  textoSecundario: {
    fontSize: 14,
    color: CoresPrincipais.textoClaro,
  },
  textoPequeno: {
    fontSize: 12,
    color: CoresPrincipais.textoClaro,
  },

  // Botões
  botao: {
    backgroundColor: CoresPrincipais.laranja,
    paddingVertical: 14,
    paddingHorizontal: 24,
    borderRadius: 8,
    alignItems: 'center',
    justifyContent: 'center',
    ...Sombras.leve,
  },
  botaoTexto: {
    color: CoresPrincipais.branco,
    fontSize: 16,
    fontWeight: 'bold',
  },
  botaoSecundario: {
    backgroundColor: CoresPrincipais.cinzaClaro,
    paddingVertical: 14,
    paddingHorizontal: 24,
    borderRadius: 8,
    alignItems: 'center',
    justifyContent: 'center',
  },
  botaoSecundarioTexto: {
    color: CoresPrincipais.textoEscuro,
    fontSize: 16,
    fontWeight: 'bold',
  },
  botaoDesabilitado: {
    backgroundColor: CoresPrincipais.cinzaMedio,
    opacity: 0.6,
  },

  // Inputs
  input: {
    backgroundColor: CoresPrincipais.branco,
    borderWidth: 1,
    borderColor: CoresPrincipais.cinzaMedio,
    borderRadius: 8,
    paddingVertical: 12,
    paddingHorizontal: 16,
    fontSize: 16,
    color: CoresPrincipais.textoEscuro,
    marginBottom: 12,
  },
  inputFocado: {
    borderColor: CoresPrincipais.laranja,
    borderWidth: 2,
  },
  inputErro: {
    borderColor: CoresPrincipais.erro,
  },
  labelInput: {
    fontSize: 14,
    fontWeight: '600',
    color: CoresPrincipais.textoMedio,
    marginBottom: 6,
  },
  textoErro: {
    fontSize: 12,
    color: CoresPrincipais.erro,
    marginTop: -8,
    marginBottom: 8,
  },

  // Cabeçalho
  cabecalho: {
    backgroundColor: CoresPrincipais.begeEscuro,
    paddingVertical: 16,
    paddingHorizontal: 16,
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    ...Sombras.leve,
  },

  // Divisores
  divisor: {
    height: 1,
    backgroundColor: CoresPrincipais.cinzaClaro,
    marginVertical: 16,
  },

  // Badges
  badge: {
    paddingVertical: 4,
    paddingHorizontal: 12,
    borderRadius: 12,
    alignSelf: 'flex-start',
  },
  badgeTexto: {
    fontSize: 12,
    fontWeight: 'bold',
    color: CoresPrincipais.branco,
  },

  // Espaçamentos
  margemSuperior: {
    marginTop: 16,
  },
  margemInferior: {
    marginBottom: 16,
  },
  margemHorizontal: {
    marginHorizontal: 16,
  },
  margemVertical: {
    marginVertical: 16,
  },
  paddingPequeno: {
    padding: 8,
  },
  paddingMedio: {
    padding: 16,
  },
  paddingGrande: {
    padding: 24,
  },

  // Flexbox
  linha: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  linhaEntre: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
  },
  coluna: {
    flexDirection: 'column',
  },
  centralizado: {
    justifyContent: 'center',
    alignItems: 'center',
  },
});
