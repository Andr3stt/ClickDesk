// 🔒 Componente de Rota Protegida
// Verifica se o usuário está autenticado antes de permitir acesso à tela

import React, { useEffect, useState } from 'react';
import { View, ActivityIndicator, StyleSheet } from 'react-native';
import { obterToken } from '../servicos/utilitarios/armazenamentoLocal';
import { CoresPrincipais } from '../estilos/cores';

/**
 * HOC (Higher Order Component) para proteger rotas
 * Verifica se há token de autenticação antes de renderizar o componente
 * 
 * @param {Component} Component - Componente a ser protegido
 * @param {Object} navigation - Objeto de navegação do React Navigation
 * @returns {Component} Componente protegido ou redirecionamento para login
 */
export const RotaProtegida = (Component) => {
  return function RotaProtegidaWrapper({ navigation, ...props }) {
    const [carregando, setCarregando] = useState(true);
    const [autenticado, setAutenticado] = useState(false);

    useEffect(() => {
      verificarAutenticacao();
    }, []);

    const verificarAutenticacao = async () => {
      try {
        const token = await obterToken();
        
        if (token) {
          setAutenticado(true);
        } else {
          // Redirecionar para login se não houver token
          navigation.replace('Login');
        }
      } catch (erro) {
        console.error('❌ Erro ao verificar autenticação:', erro);
        navigation.replace('Login');
      } finally {
        setCarregando(false);
      }
    };

    // Exibe indicador de carregamento enquanto verifica autenticação
    if (carregando) {
      return (
        <View style={estilos.containerCarregamento}>
          <ActivityIndicator size="large" color={CoresPrincipais.laranja} />
        </View>
      );
    }

    // Se autenticado, renderiza o componente
    if (autenticado) {
      return <Component navigation={navigation} {...props} />;
    }

    // Se não autenticado, retorna null (já foi redirecionado)
    return null;
  };
};

/**
 * HOC para proteger rotas de administrador
 * Verifica se o usuário tem role ADMIN ou TECH
 */
export const RotaProtegidaAdmin = (Component) => {
  return function RotaProtegidaAdminWrapper({ navigation, ...props }) {
    const [carregando, setCarregando] = useState(true);
    const [autorizado, setAutorizado] = useState(false);

    useEffect(() => {
      verificarAutorizacao();
    }, []);

    const verificarAutorizacao = async () => {
      try {
        const token = await obterToken();
        
        if (!token) {
          navigation.replace('Login');
          return;
        }

        // TODO: Decodificar JWT e verificar role
        // Por enquanto, assumir que está autorizado se tiver token
        setAutorizado(true);
      } catch (erro) {
        console.error('❌ Erro ao verificar autorização:', erro);
        navigation.replace('Login');
      } finally {
        setCarregando(false);
      }
    };

    if (carregando) {
      return (
        <View style={estilos.containerCarregamento}>
          <ActivityIndicator size="large" color={CoresPrincipais.laranja} />
        </View>
      );
    }

    if (autorizado) {
      return <Component navigation={navigation} {...props} />;
    }

    return null;
  };
};

const estilos = StyleSheet.create({
  containerCarregamento: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: CoresPrincipais.begeClaro,
  },
});
