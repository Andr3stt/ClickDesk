/**
 * Ponto de Entrada da Aplicação ClickDesk Mobile
 * 
 * Este arquivo é o ponto de entrada da aplicação React Native com Expo.
 * Ele registra o componente principal da aplicação.
 */

import 'react-native-gesture-handler';
import { registerRootComponent } from 'expo';
import App from './src/App';

// registerRootComponent chama AppRegistry.registerComponent('main', () => App);
// Também garante que se você carregar a aplicação no Expo Go, ela será registrada corretamente.
registerRootComponent(App);
