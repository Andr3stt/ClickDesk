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
import NavegadorPrincipal from './navegacao/NavegadorPrincipal';

export default function App() {
  return (
    <SafeAreaProvider>
      <StatusBar style="dark" backgroundColor={Cores.background} />
      <NavigationContainer>
        <NavegadorPrincipal />
      </NavigationContainer>
    </SafeAreaProvider>
  );
}
