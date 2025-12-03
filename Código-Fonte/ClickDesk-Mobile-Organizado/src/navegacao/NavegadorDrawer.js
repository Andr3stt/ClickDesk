/**
 * Navegador Drawer (Barra Lateral)
 * 
 * Configura a navegação com drawer (menu lateral) para
 * as telas principais da aplicação após o login.
 */

import React from 'react';
import { createDrawerNavigator } from '@react-navigation/drawer';
import { MaterialCommunityIcons } from '@expo/vector-icons';
import { Cores } from '../estilos/cores';
import ConteudoDrawer from './ConteudoDrawer';

// Importar telas de usuário
import TelaDashboard from '../telas/usuario/TelaDashboard';
import TelaMeusChamados from '../telas/usuario/TelaMeusChamados';
import TelaNovoChamado from '../telas/usuario/TelaNovoChamado';
import TelaDetalhesChamado from '../telas/usuario/TelaDetalhesChamado';
import TelaEditarPerfil from '../telas/usuario/TelaEditarPerfil';

// Importar telas compartilhadas
import TelaFAQ from '../telas/compartilhadas/TelaFAQ';
import TelaListaChamados from '../telas/compartilhadas/TelaListaChamados';

// Importar telas de administrador
import TelaDashboardAdmin from '../telas/administrador/TelaDashboardAdmin';
import TelaChamadosAdmin from '../telas/administrador/TelaChamadosAdmin';
import TelaAprovarChamados from '../telas/administrador/TelaAprovarChamados';
import TelaFAQAdmin from '../telas/administrador/TelaFAQAdmin';

const Drawer = createDrawerNavigator();

/**
 * Navegador Drawer para Usuários
 */
export function NavegadorDrawerUsuario() {
  return (
    <Drawer.Navigator
      drawerContent={(props) => <ConteudoDrawer {...props} />}
      screenOptions={{
        headerStyle: {
          backgroundColor: Cores.brand,
        },
        headerTintColor: '#fff',
        headerTitleStyle: {
          fontWeight: 'bold',
        },
        drawerStyle: {
          backgroundColor: Cores.background,
          width: 300,
        },
        drawerActiveBackgroundColor: '#FFF',
        drawerActiveTintColor: Cores.brand,
        drawerInactiveTintColor: '#666',
      }}
    >
      {/* Telas principais do usuário */}
      <Drawer.Screen 
        name="Dashboard" 
        component={TelaDashboard}
        options={{
          title: 'Dashboard',
          drawerIcon: ({ color, size }) => (
            <MaterialCommunityIcons name="view-dashboard" size={size} color={color} />
          ),
        }}
      />
      <Drawer.Screen 
        name="MeusChamados" 
        component={TelaMeusChamados}
        options={{
          title: 'Meus Chamados',
          drawerIcon: ({ color, size }) => (
            <MaterialCommunityIcons name="ticket-account" size={size} color={color} />
          ),
        }}
      />
      <Drawer.Screen 
        name="NovoChamado" 
        component={TelaNovoChamado}
        options={{
          title: 'Novo Chamado',
          drawerIcon: ({ color, size }) => (
            <MaterialCommunityIcons name="ticket-plus" size={size} color={color} />
          ),
        }}
      />
      <Drawer.Screen 
        name="FAQ" 
        component={TelaFAQ}
        options={{
          title: 'FAQ',
          drawerIcon: ({ color, size }) => (
            <MaterialCommunityIcons name="help-circle" size={size} color={color} />
          ),
        }}
      />
      <Drawer.Screen 
        name="EditarPerfil" 
        component={TelaEditarPerfil}
        options={{
          title: 'Editar Perfil',
          drawerIcon: ({ color, size }) => (
            <MaterialCommunityIcons name="account-edit" size={size} color={color} />
          ),
        }}
      />

      {/* Telas de detalhes (não aparecem no menu) */}
      <Drawer.Screen 
        name="DetalhesChamado" 
        component={TelaDetalhesChamado}
        options={{
          title: 'Detalhes do Chamado',
          drawerItemStyle: { display: 'none' },
        }}
      />
      <Drawer.Screen 
        name="ListaChamados" 
        component={TelaListaChamados}
        options={{
          title: 'Lista de Chamados',
          drawerItemStyle: { display: 'none' },
        }}
      />
    </Drawer.Navigator>
  );
}

/**
 * Navegador Drawer para Administradores
 */
export function NavegadorDrawerAdmin() {
  return (
    <Drawer.Navigator
      drawerContent={(props) => <ConteudoDrawer {...props} />}
      screenOptions={{
        headerStyle: {
          backgroundColor: Cores.brand,
        },
        headerTintColor: '#fff',
        headerTitleStyle: {
          fontWeight: 'bold',
        },
        drawerStyle: {
          backgroundColor: Cores.background,
          width: 300,
        },
        drawerActiveBackgroundColor: '#FFF',
        drawerActiveTintColor: Cores.brand,
        drawerInactiveTintColor: '#666',
      }}
    >
      {/* Telas principais do administrador */}
      <Drawer.Screen 
        name="DashboardAdmin" 
        component={TelaDashboardAdmin}
        options={{
          title: 'Dashboard Admin',
          drawerIcon: ({ color, size }) => (
            <MaterialCommunityIcons name="view-dashboard" size={size} color={color} />
          ),
        }}
      />
      <Drawer.Screen 
        name="ChamadosAdmin" 
        component={TelaChamadosAdmin}
        options={{
          title: 'Todos os Chamados',
          drawerIcon: ({ color, size }) => (
            <MaterialCommunityIcons name="ticket-outline" size={size} color={color} />
          ),
        }}
      />
      <Drawer.Screen 
        name="AprovarChamados" 
        component={TelaAprovarChamados}
        options={{
          title: 'Aprovar Chamados',
          drawerIcon: ({ color, size }) => (
            <MaterialCommunityIcons name="ticket-confirmation" size={size} color={color} />
          ),
        }}
      />
      <Drawer.Screen 
        name="FAQAdmin" 
        component={TelaFAQAdmin}
        options={{
          title: 'Gerenciar FAQ',
          drawerIcon: ({ color, size }) => (
            <MaterialCommunityIcons name="comment-question" size={size} color={color} />
          ),
        }}
      />

      {/* Telas de detalhes (não aparecem no menu) */}
      <Drawer.Screen 
        name="DetalhesChamado" 
        component={TelaDetalhesChamado}
        options={{
          title: 'Detalhes do Chamado',
          drawerItemStyle: { display: 'none' },
        }}
      />
      <Drawer.Screen 
        name="EditarPerfil" 
        component={TelaEditarPerfil}
        options={{
          title: 'Editar Perfil',
          drawerItemStyle: { display: 'none' },
        }}
      />
    </Drawer.Navigator>
  );
}
