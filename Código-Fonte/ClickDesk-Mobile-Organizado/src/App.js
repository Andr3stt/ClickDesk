/**
 * Aplicação Principal do ClickDesk Mobile
 * 
 * Componente raiz da aplicação que configura:
 * - Navegação
 * - Contextos globais
 * - Status bar
 * - SafeAreaProvider
 */

import React from 'react';
import { StatusBar } from 'expo-status-bar';
import { SafeAreaProvider } from 'react-native-safe-area-context';
import { NavigationContainer } from '@react-navigation/native';
import { Cores } from './estilos/cores';

// Importar navegação quando estiver pronta
// import { NavegadorPrincipal } from './navegacao/NavegadorPrincipal';

export default function App() {
  return (
    <SafeAreaProvider>
      <StatusBar style="dark" backgroundColor={Cores.background} />
      <NavigationContainer>
        {/* TODO: Substituir por NavegadorPrincipal quando criado */}
        {/* <NavegadorPrincipal /> */}
      </NavigationContainer>
    </SafeAreaProvider>
  );
}
