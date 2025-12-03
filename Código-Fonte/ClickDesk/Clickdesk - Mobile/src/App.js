// 📱 Componente Principal do Aplicativo ClickDesk Mobile
import React from 'react';
import { StatusBar } from 'expo-status-bar';
import { SafeAreaProvider } from 'react-native-safe-area-context';
import { AppNavigator } from './navegacao/NavegadorPrincipal';

/**
 * Componente raiz da aplicação
 * Configura o provedor de área segura e o navegador principal
 */
export default function App() {
  return (
    <SafeAreaProvider>
      {/* Configura a barra de status com estilo escuro e fundo bege */}
      <StatusBar style="dark" backgroundColor="#EDE6D9" />
      {/* Renderiza o navegador principal com todas as telas */}
      <AppNavigator />
    </SafeAreaProvider>
  );
}