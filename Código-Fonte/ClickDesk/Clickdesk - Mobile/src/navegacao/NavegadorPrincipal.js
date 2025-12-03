import React from 'react';
import { NavigationContainer } from '@react-navigation/native';
import { createNativeStackNavigator } from '@react-navigation/native-stack';

// 🔐 Importando telas de autenticação
import TelaLogin from '../telas/autenticacao/TelaLogin';
import TelaRegistro from '../telas/autenticacao/TelaRegistro';
import TelaEsqueciSenha from '../telas/autenticacao/TelaEsqueciSenha';
import TelaTermos from '../telas/autenticacao/TelaTermos';
import TelaLogout from '../telas/autenticacao/TelaLogout';

// 👤 Importando telas de usuário
import TelaDashboard from '../telas/usuario/TelaDashboard';
import TelaNovoChamado from '../telas/usuario/TelaNovoChamado';
import TelaDetalhesChamado from '../telas/usuario/TelaDetalhesChamado';
import TelaMeusChamados from '../telas/usuario/TelaMeusChamados';
import TelaEditarPerfil from '../telas/usuario/TelaEditarPerfil';
import TelaCriarPerfil from '../telas/usuario/TelaCriarPerfil';

// 👨‍💼 Importando telas de administrador
import TelaDashboardAdmin from '../telas/administrador/TelaDashboardAdmin';
import TelaAprovacaoChamados from '../telas/administrador/TelaAprovacaoChamados';
import TelaChamadosAdmin from '../telas/administrador/TelaChamadosAdmin';

// 🔗 Importando telas compartilhadas
import TelaFAQ from '../telas/compartilhadas/TelaFAQ';
import TelaListaChamados from '../telas/compartilhadas/TelaListaChamados';

const Stack = createNativeStackNavigator();

export function AppNavigator() {
  return (
    <NavigationContainer>
      <Stack.Navigator
        initialRouteName="Login"
        screenOptions={{
          headerStyle: {
            backgroundColor: '#EDE6D9',
          },
          headerTintColor: '#2C3E50',
          headerTitleStyle: {
            fontWeight: 'bold',
          },
        }}
      >
        {/* 🔐 Telas de Autenticação */}
        <Stack.Screen 
          name="Login" 
          component={TelaLogin}
          options={{ headerShown: false }}
        />
        <Stack.Screen 
          name="Register" 
          component={TelaRegistro}
          options={{ title: 'Cadastro' }}
        />
        <Stack.Screen 
          name="ForgotPassword" 
          component={TelaEsqueciSenha}
          options={{ title: 'Esqueci a Senha' }}
        />
        <Stack.Screen 
          name="Terms" 
          component={TelaTermos}
          options={{ title: 'Termos de Uso' }}
        />
        <Stack.Screen 
          name="Logout" 
          component={TelaLogout}
          options={{ title: 'Sair' }}
        />
        
        {/* 👤 Telas de Usuário */}
        <Stack.Screen 
          name="CreateProfile" 
          component={TelaCriarPerfil}
          options={{ title: 'Criar Perfil' }}
        />
        <Stack.Screen 
          name="EditProfile" 
          component={TelaEditarPerfil}
          options={{ title: 'Editar Perfil' }}
        />
        <Stack.Screen 
          name="Dashboard" 
          component={TelaDashboard}
          options={{ title: 'Dashboard', headerShown: false }}
        />
        <Stack.Screen 
          name="NewTicket" 
          component={TelaNovoChamado}
          options={{ title: 'Novo Chamado' }}
        />
        <Stack.Screen 
          name="TicketDetails" 
          component={TelaDetalhesChamado}
          options={{ title: 'Detalhes do Chamado' }}
        />
        <Stack.Screen 
          name="MyTicket" 
          component={TelaMeusChamados}
          options={{ title: 'Meus Chamados' }}
        />
        
        {/* 👨‍💼 Telas de Administrador */}
        <Stack.Screen 
          name="AdminDashboard" 
          component={TelaDashboardAdmin}
          options={{ title: 'Dashboard Admin', headerShown: false }}
        />
        <Stack.Screen 
          name="TicketApproval" 
          component={TelaAprovacaoChamados}
          options={{ title: 'Aprovação de Chamados' }}
        />
        <Stack.Screen 
          name="MyTicketsAdmin" 
          component={TelaChamadosAdmin}
          options={{ title: 'Gerenciar Chamados' }}
        />
        
        {/* 🔗 Telas Compartilhadas */}
        <Stack.Screen 
          name="TicketList" 
          component={TelaListaChamados}
          options={{ title: 'Lista de Chamados' }}
        />
        <Stack.Screen 
          name="FAQ" 
          component={TelaFAQ}
          options={{ title: 'Perguntas Frequentes' }}
        />
      </Stack.Navigator>
    </NavigationContainer>
  );
}