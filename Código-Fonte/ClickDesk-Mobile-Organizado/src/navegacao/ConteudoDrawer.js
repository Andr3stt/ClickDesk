/**
 * Conteúdo Customizado do Drawer (Barra Lateral)
 * 
 * Componente que define o conteúdo da barra lateral com:
 * - Perfil do usuário
 * - Menu de navegação
 * - Botão de sair
 */

import React from 'react';
import {
  View,
  Text,
  StyleSheet,
  TouchableOpacity,
  ScrollView,
  Image,
} from 'react-native';
import { DrawerContentScrollView } from '@react-navigation/drawer';
import { MaterialCommunityIcons } from '@expo/vector-icons';
import { Cores } from '../estilos/cores';

/**
 * Componente de Conteúdo do Drawer
 */
export default function ConteudoDrawer(props) {
  const { navigation, state } = props;
  
  // TODO: Buscar dados reais do usuário do contexto/async storage
  const usuario = {
    nome: 'Usuário ClickDesk',
    email: 'usuario@clickdesk.com',
    avatar: null,
    tipo: 'usuario', // 'usuario', 'tecnico', 'administrador'
  };

  /**
   * Itens do menu baseado no tipo de usuário
   */
  const menuUsuario = [
    { 
      nome: 'Dashboard', 
      rota: 'Dashboard', 
      icone: 'view-dashboard',
      descricao: 'Visão geral'
    },
    { 
      nome: 'Meus Chamados', 
      rota: 'MeusChamados', 
      icone: 'ticket-account',
      descricao: 'Acompanhe seus tickets'
    },
    { 
      nome: 'Novo Chamado', 
      rota: 'NovoChamado', 
      icone: 'ticket-plus',
      descricao: 'Abrir novo ticket'
    },
    { 
      nome: 'FAQ', 
      rota: 'FAQ', 
      icone: 'help-circle',
      descricao: 'Perguntas frequentes'
    },
    { 
      nome: 'Editar Perfil', 
      rota: 'EditarPerfil', 
      icone: 'account-edit',
      descricao: 'Atualizar informações'
    },
  ];

  const menuAdmin = [
    { 
      nome: 'Dashboard Admin', 
      rota: 'DashboardAdmin', 
      icone: 'view-dashboard',
      descricao: 'Painel administrativo'
    },
    { 
      nome: 'Todos os Chamados', 
      rota: 'ChamadosAdmin', 
      icone: 'ticket-outline',
      descricao: 'Gerenciar tickets'
    },
    { 
      nome: 'Aprovar Chamados', 
      rota: 'AprovarChamados', 
      icone: 'ticket-confirmation',
      descricao: 'Aprovar solicitações'
    },
    { 
      nome: 'Gerenciar FAQ', 
      rota: 'FAQAdmin', 
      icone: 'comment-question',
      descricao: 'Editar perguntas'
    },
  ];

  // Selecionar menu baseado no tipo de usuário
  const menuItens = usuario.tipo === 'administrador' ? menuAdmin : menuUsuario;

  /**
   * Navegar para uma tela
   */
  const navegarPara = (rota) => {
    navigation.navigate(rota);
  };

  /**
   * Verificar se a rota está ativa
   */
  const rotaAtiva = (rota) => {
    return state.routeNames[state.index] === rota;
  };

  /**
   * Fazer logout
   */
  const handleLogout = () => {
    navigation.navigate('TelaLogout');
  };

  return (
    <View style={styles.container}>
      <DrawerContentScrollView {...props} contentContainerStyle={styles.scrollContent}>
        {/* Cabeçalho com perfil do usuário */}
        <View style={styles.header}>
          <View style={styles.logoContainer}>
            <MaterialCommunityIcons name="ticket" size={32} color={Cores.brand} />
            <Text style={styles.logoText}>ClickDesk</Text>
          </View>
          
          <View style={styles.perfilContainer}>
            <View style={styles.avatarContainer}>
              {usuario.avatar ? (
                <Image source={{ uri: usuario.avatar }} style={styles.avatar} />
              ) : (
                <MaterialCommunityIcons name="account-circle" size={60} color={Cores.brand} />
              )}
            </View>
            <Text style={styles.nomeUsuario}>{usuario.nome}</Text>
            <Text style={styles.emailUsuario}>{usuario.email}</Text>
            {usuario.tipo === 'administrador' && (
              <View style={styles.badge}>
                <MaterialCommunityIcons name="shield-crown" size={14} color="#FFF" />
                <Text style={styles.badgeText}>Admin</Text>
              </View>
            )}
          </View>
        </View>

        {/* Menu de navegação */}
        <View style={styles.menuContainer}>
          {menuItens.map((item, index) => {
            const isActive = rotaAtiva(item.rota);
            return (
              <TouchableOpacity
                key={index}
                style={[styles.menuItem, isActive && styles.menuItemAtivo]}
                onPress={() => navegarPara(item.rota)}
              >
                <View style={styles.menuItemConteudo}>
                  <MaterialCommunityIcons 
                    name={item.icone} 
                    size={24} 
                    color={isActive ? Cores.brand : '#666'} 
                  />
                  <View style={styles.menuItemTextos}>
                    <Text style={[styles.menuItemNome, isActive && styles.menuItemNomeAtivo]}>
                      {item.nome}
                    </Text>
                    <Text style={styles.menuItemDescricao}>{item.descricao}</Text>
                  </View>
                </View>
                {isActive && <View style={styles.indicadorAtivo} />}
              </TouchableOpacity>
            );
          })}
        </View>
      </DrawerContentScrollView>

      {/* Footer com botão de sair */}
      <View style={styles.footer}>
        <TouchableOpacity style={styles.botaoSair} onPress={handleLogout}>
          <MaterialCommunityIcons name="logout" size={24} color="#E74C3C" />
          <Text style={styles.botaoSairTexto}>Sair</Text>
        </TouchableOpacity>
        
        <Text style={styles.versao}>Versão 1.0.0</Text>
      </View>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: Cores.background,
  },
  scrollContent: {
    flexGrow: 1,
  },
  header: {
    backgroundColor: '#FFF',
    borderBottomWidth: 1,
    borderBottomColor: '#E0E0E0',
    paddingBottom: 20,
  },
  logoContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 20,
    paddingTop: 20,
    paddingBottom: 15,
  },
  logoText: {
    fontSize: 24,
    fontWeight: 'bold',
    color: Cores.brand,
    marginLeft: 10,
  },
  perfilContainer: {
    alignItems: 'center',
    paddingHorizontal: 20,
  },
  avatarContainer: {
    marginBottom: 10,
  },
  avatar: {
    width: 60,
    height: 60,
    borderRadius: 30,
  },
  nomeUsuario: {
    fontSize: 18,
    fontWeight: 'bold',
    color: '#333',
    marginBottom: 4,
  },
  emailUsuario: {
    fontSize: 14,
    color: '#666',
    marginBottom: 8,
  },
  badge: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: Cores.brand,
    paddingHorizontal: 12,
    paddingVertical: 4,
    borderRadius: 12,
  },
  badgeText: {
    color: '#FFF',
    fontSize: 12,
    fontWeight: 'bold',
    marginLeft: 4,
  },
  menuContainer: {
    flex: 1,
    paddingTop: 10,
  },
  menuItem: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingVertical: 16,
    paddingHorizontal: 20,
    backgroundColor: 'transparent',
  },
  menuItemAtivo: {
    backgroundColor: '#FFF',
    borderLeftWidth: 4,
    borderLeftColor: Cores.brand,
  },
  menuItemConteudo: {
    flexDirection: 'row',
    alignItems: 'center',
    flex: 1,
  },
  menuItemTextos: {
    marginLeft: 15,
    flex: 1,
  },
  menuItemNome: {
    fontSize: 16,
    color: '#333',
    fontWeight: '500',
  },
  menuItemNomeAtivo: {
    color: Cores.brand,
    fontWeight: 'bold',
  },
  menuItemDescricao: {
    fontSize: 12,
    color: '#999',
    marginTop: 2,
  },
  indicadorAtivo: {
    width: 8,
    height: 8,
    borderRadius: 4,
    backgroundColor: Cores.brand,
  },
  footer: {
    borderTopWidth: 1,
    borderTopColor: '#E0E0E0',
    paddingVertical: 15,
    paddingHorizontal: 20,
    backgroundColor: '#FFF',
  },
  botaoSair: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingVertical: 12,
  },
  botaoSairTexto: {
    fontSize: 16,
    color: '#E74C3C',
    fontWeight: '600',
    marginLeft: 15,
  },
  versao: {
    fontSize: 12,
    color: '#999',
    textAlign: 'center',
    marginTop: 10,
  },
});
