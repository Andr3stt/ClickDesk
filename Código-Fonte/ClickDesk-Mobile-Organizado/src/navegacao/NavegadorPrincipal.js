/**
 * Navegador Principal do ClickDesk Mobile
 * 
 * Configura toda a estrutura de navegação da aplicação:
 * - Stack Navigator para fluxo de telas
 * - Menu lateral customizado
 * - Navegação condicional baseada em autenticação
 */

import React from 'react';
import { createNativeStackNavigator } from '@react-navigation/native-stack';
import { Cores } from '../estilos/cores';

// Importar telas de autenticação
import TelaLogin from '../telas/autenticacao/TelaLogin';
import TelaRegistro from '../telas/autenticacao/TelaRegistro';
import TelaTermos from '../telas/autenticacao/TelaTermos';
import TelaRecuperarSenha from '../telas/autenticacao/TelaRecuperarSenha';
import TelaLogout from '../telas/autenticacao/TelaLogout';

// Importar telas de usuário
import TelaDashboard from '../telas/usuario/TelaDashboard';
import TelaMeusChamados from '../telas/usuario/TelaMeusChamados';
import TelaNovoChamado from '../telas/usuario/TelaNovoChamado';
import TelaDetalhesChamado from '../telas/usuario/TelaDetalhesChamado';
import TelaEditarPerfil from '../telas/usuario/TelaEditarPerfil';

// Importar telas compartilhadas
import TelaFAQ from '../telas/compartilhadas/TelaFAQ';
import TelaListaChamados from '../telas/compartilhadas/TelaListaChamados';
import TelaCriarPerfil from '../telas/compartilhadas/TelaCriarPerfil';

// Importar telas de administrador
import TelaDashboardAdmin from '../telas/administrador/TelaDashboardAdmin';
import TelaChamadosAdmin from '../telas/administrador/TelaChamadosAdmin';
import TelaAprovarChamados from '../telas/administrador/TelaAprovarChamados';
import TelaFAQAdmin from '../telas/administrador/TelaFAQAdmin';

const Stack = createNativeStackNavigator();

/**
 * Configuração padrão de estilo para os headers
 */
const screenOptions = {
  headerStyle: {
    backgroundColor: Cores.brand,
  },
  headerTintColor: '#fff',
  headerTitleStyle: {
    fontWeight: 'bold',
  },
  headerBackTitle: 'Voltar',
};

/**
 * Navegador Principal
 * Define todas as rotas da aplicação
 */
export default function NavegadorPrincipal() {
  // TODO: Implementar lógica de autenticação para mostrar telas diferentes
  // Por enquanto, sempre começa no login
  const isAuthenticated = false;

  return (
    <Stack.Navigator
      screenOptions={screenOptions}
      initialRouteName="Login"
    >
      {/* Telas de Autenticação */}
      <Stack.Screen 
        name="Login" 
        component={TelaLogin}
        options={{ 
          title: 'ClickDesk - Login',
          headerShown: false 
        }}
      />
      <Stack.Screen 
        name="Registro" 
        component={TelaRegistro}
        options={{ title: 'Criar Conta' }}
      />
      <Stack.Screen 
        name="Termos" 
        component={TelaTermos}
        options={{ title: 'Termos de Uso' }}
      />
      <Stack.Screen 
        name="RecuperarSenha" 
        component={TelaRecuperarSenha}
        options={{ 
          title: 'Recuperar Senha',
          headerShown: false 
        }}
      />
      <Stack.Screen 
        name="TelaLogout" 
        component={TelaLogout}
        options={{ 
          title: 'Sair',
          headerShown: false 
        }}
      />

      {/* Telas de Usuário */}
      <Stack.Screen 
        name="Dashboard" 
        component={TelaDashboard}
        options={{ 
          title: 'Dashboard',
          headerShown: false 
        }}
      />
      <Stack.Screen 
        name="MeusChamados" 
        component={TelaMeusChamados}
        options={{ title: 'Meus Chamados' }}
      />
      <Stack.Screen 
        name="NovoChamado" 
        component={TelaNovoChamado}
        options={{ title: 'Novo Chamado' }}
      />
      <Stack.Screen 
        name="DetalhesChamado" 
        component={TelaDetalhesChamado}
        options={{ title: 'Detalhes do Chamado' }}
      />
      <Stack.Screen 
        name="EditarPerfil" 
        component={TelaEditarPerfil}
        options={{ title: 'Editar Perfil' }}
      />

      {/* Telas Compartilhadas */}
      <Stack.Screen 
        name="FAQ" 
        component={TelaFAQ}
        options={{ title: 'Perguntas Frequentes' }}
      />
      <Stack.Screen 
        name="ListaChamados" 
        component={TelaListaChamados}
        options={{ title: 'Chamados' }}
      />
      <Stack.Screen 
        name="CriarPerfil" 
        component={TelaCriarPerfil}
        options={{ title: 'Criar Perfil' }}
      />

      {/* Telas de Administrador */}
      <Stack.Screen 
        name="DashboardAdmin" 
        component={TelaDashboardAdmin}
        options={{ 
          title: 'Dashboard Admin',
          headerShown: false 
        }}
      />
      <Stack.Screen 
        name="ChamadosAdmin" 
        component={TelaChamadosAdmin}
        options={{ title: 'Gerenciar Chamados' }}
      />
      <Stack.Screen 
        name="AprovarChamados" 
        component={TelaAprovarChamados}
        options={{ title: 'Aprovar Chamados' }}
      />
      <Stack.Screen 
        name="FAQAdmin" 
        component={TelaFAQAdmin}
        options={{ title: 'Gerenciar FAQ' }}
      />
    </Stack.Navigator>
  );
}
