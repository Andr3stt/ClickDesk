/**
 * Menu Lateral Customizado (Drawer)
 * 
 * Componente de menu lateral que funciona com Expo Go
 * sem dependências de bibliotecas nativas complexas.
 * Usa Modal e Animated para criar o efeito de drawer.
 */

import React, { useEffect, useRef } from 'react';
import {
  View,
  Text,
  StyleSheet,
  TouchableOpacity,
  ScrollView,
  Modal,
  Animated,
  Dimensions,
  TouchableWithoutFeedback,
} from 'react-native';
import { MaterialCommunityIcons } from '@expo/vector-icons';
import { Cores } from '../estilos/cores';

const { width } = Dimensions.get('window');
const DRAWER_WIDTH = 300;

/**
 * Componente de Menu Lateral
 */
export default function MenuLateral({ visible, onClose, navigation, tipoUsuario = 'usuario' }) {
  const slideAnim = useRef(new Animated.Value(-DRAWER_WIDTH)).current;
  const opacityAnim = useRef(new Animated.Value(0)).current;

  // TODO: Buscar dados reais do usuário
  const usuario = {
    nome: 'Usuário ClickDesk',
    email: 'usuario@clickdesk.com',
    avatar: null,
    tipo: tipoUsuario,
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
      icone: 'plus-circle',
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

  const menuItens = usuario.tipo === 'administrador' ? menuAdmin : menuUsuario;

  /**
   * Animação de abertura/fechamento
   */
  useEffect(() => {
    if (visible) {
      Animated.parallel([
        Animated.timing(slideAnim, {
          toValue: 0,
          duration: 300,
          useNativeDriver: true,
        }),
        Animated.timing(opacityAnim, {
          toValue: 1,
          duration: 300,
          useNativeDriver: true,
        }),
      ]).start();
    } else {
      Animated.parallel([
        Animated.timing(slideAnim, {
          toValue: -DRAWER_WIDTH,
          duration: 250,
          useNativeDriver: true,
        }),
        Animated.timing(opacityAnim, {
          toValue: 0,
          duration: 250,
          useNativeDriver: true,
        }),
      ]).start();
    }
  }, [visible]);

  /**
   * Navegar para uma tela
   */
  const navegarPara = (rota) => {
    onClose();
    setTimeout(() => {
      navigation.navigate(rota);
    }, 300);
  };

  /**
   * Fazer logout
   */
  const handleLogout = () => {
    onClose();
    setTimeout(() => {
      navigation.navigate('TelaLogout');
    }, 300);
  };

  return (
    <Modal
      visible={visible}
      transparent
      animationType="none"
      onRequestClose={onClose}
    >
      <View style={styles.modalContainer}>
        {/* Overlay */}
        <TouchableWithoutFeedback onPress={onClose}>
          <Animated.View 
            style={[
              styles.overlay,
              { opacity: opacityAnim }
            ]} 
          />
        </TouchableWithoutFeedback>

        {/* Drawer Content */}
        <Animated.View
          style={[
            styles.drawer,
            {
              transform: [{ translateX: slideAnim }]
            }
          ]}
        >
          <ScrollView 
            style={styles.scrollView}
            showsVerticalScrollIndicator={false}
          >
            {/* Cabeçalho com perfil do usuário */}
            <View style={styles.header}>
              <View style={styles.perfilContainer}>
                <View style={styles.avatarContainer}>
                  <MaterialCommunityIcons name="account-circle" size={60} color={Cores.brand} />
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
              {menuItens.map((item, index) => (
                <TouchableOpacity
                  key={index}
                  style={styles.menuItem}
                  onPress={() => navegarPara(item.rota)}
                  activeOpacity={0.7}
                >
                  <View style={styles.menuItemConteudo}>
                    <MaterialCommunityIcons 
                      name={item.icone} 
                      size={24} 
                      color={Cores.brand}
                    />
                    <View style={styles.menuItemTextos}>
                      <Text style={styles.menuItemNome}>
                        {item.nome}
                      </Text>
                      <Text style={styles.menuItemDescricao}>{item.descricao}</Text>
                    </View>
                  </View>
                  <MaterialCommunityIcons 
                    name="chevron-right" 
                    size={20} 
                    color="#CCC"
                  />
                </TouchableOpacity>
              ))}
            </View>
          </ScrollView>

          {/* Footer com botão de sair */}
          <View style={styles.footer}>
            <TouchableOpacity style={styles.botaoSair} onPress={handleLogout}>
              <MaterialCommunityIcons name="logout" size={24} color="#E74C3C" />
              <Text style={styles.botaoSairTexto}>Sair</Text>
            </TouchableOpacity>
            
            <Text style={styles.versao}>Versão 1.0.0</Text>
          </View>
        </Animated.View>
      </View>
    </Modal>
  );
}

const styles = StyleSheet.create({
  modalContainer: {
    flex: 1,
    flexDirection: 'row',
  },
  overlay: {
    flex: 1,
    backgroundColor: 'rgba(0, 0, 0, 0.5)',
  },
  drawer: {
    width: DRAWER_WIDTH,
    backgroundColor: Cores.background,
    position: 'absolute',
    left: 0,
    top: 0,
    bottom: 0,
    elevation: 16,
    shadowColor: '#000',
    shadowOffset: { width: 2, height: 0 },
    shadowOpacity: 0.3,
    shadowRadius: 8,
  },
  scrollView: {
    flex: 1,
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
    paddingTop: 50,
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
    paddingBottom: 20,
  },
  menuItem: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingVertical: 16,
    paddingHorizontal: 20,
    backgroundColor: '#FFF',
    marginHorizontal: 10,
    marginVertical: 4,
    borderRadius: 8,
    elevation: 1,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.1,
    shadowRadius: 2,
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
    fontWeight: '600',
  },
  menuItemDescricao: {
    fontSize: 12,
    color: '#999',
    marginTop: 2,
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
