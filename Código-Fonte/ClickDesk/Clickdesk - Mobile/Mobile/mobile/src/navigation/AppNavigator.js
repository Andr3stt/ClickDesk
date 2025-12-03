import React from 'react';
import { NavigationContainer } from '@react-navigation/native';
import { createNativeStackNavigator } from '@react-navigation/native-stack';

// Importando as telas (vamos criar elas em seguida)
import LoginScreen from '../screens/LoginScreen';
import RegisterScreen from '../screens/RegisterScreen';
import ForgotPasswordScreen from '../screens/ForgotPasswordScreen';
import TermsScreen from '../screens/TermsScreen';
import CreateProfileScreen from '../screens/CreateProfileScreen';
import EditProfileScreen from '../screens/EditProfileScreen';
import DashboardScreen from '../screens/DashboardScreen';
import NewTicketScreen from '../screens/NewTicketScreen';
import TicketDetailsScreen from '../screens/TicketDetailsScreen';
import TicketListScreen from '../screens/TicketListScreen';
import MyTicketScreen from '../screens/MyTicketScreen';
import FAQScreen from '../screens/FAQScreen';
import AdminDashboardScreen from '../screens/AdminDashboardScreen';
import TicketApprovalScreen from '../screens/TicketApprovalScreen';
import MyTicketsAdminScreen from '../screens/MyTicketsAdminScreen';
import LogoutScreen from '../screens/LogoutScreen';

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
        <Stack.Screen 
          name="Login" 
          component={LoginScreen}
          options={{ headerShown: false }}
        />
        <Stack.Screen 
          name="Register" 
          component={RegisterScreen}
          options={{ title: 'Cadastro' }}
        />
        <Stack.Screen 
          name="ForgotPassword" 
          component={ForgotPasswordScreen}
          options={{ title: 'Esqueci a Senha' }}
        />
        <Stack.Screen 
          name="Terms" 
          component={TermsScreen}
          options={{ title: 'Termos de Uso' }}
        />
        <Stack.Screen 
          name="CreateProfile" 
          component={CreateProfileScreen}
          options={{ title: 'Criar Perfil' }}
        />
        <Stack.Screen 
          name="EditProfile" 
          component={EditProfileScreen}
          options={{ title: 'Editar Perfil' }}
        />
        <Stack.Screen 
          name="Dashboard" 
          component={DashboardScreen}
          options={{ title: 'Dashboard', headerShown: false }}
        />
        <Stack.Screen 
          name="NewTicket" 
          component={NewTicketScreen}
          options={{ title: 'Novo Chamado' }}
        />
        <Stack.Screen 
          name="TicketDetails" 
          component={TicketDetailsScreen}
          options={{ title: 'Detalhes do Chamado' }}
        />
        <Stack.Screen 
          name="TicketList" 
          component={TicketListScreen}
          options={{ title: 'Lista de Chamados' }}
        />
        <Stack.Screen 
          name="MyTicket" 
          component={MyTicketScreen}
          options={{ title: 'Meu Chamado' }}
        />
        <Stack.Screen 
          name="FAQ" 
          component={FAQScreen}
          options={{ title: 'Perguntas Frequentes' }}
        />
        <Stack.Screen 
          name="AdminDashboard" 
          component={AdminDashboardScreen}
          options={{ title: 'Dashboard Admin' }}
        />
        <Stack.Screen 
          name="TicketApproval" 
          component={TicketApprovalScreen}
          options={{ title: 'Aprovação de Chamados' }}
        />
        <Stack.Screen 
          name="MyTicketsAdmin" 
          component={MyTicketsAdminScreen}
          options={{ title: 'Meus Chamados Admin' }}
        />
        <Stack.Screen 
          name="Logout" 
          component={LogoutScreen}
          options={{ title: 'Sair' }}
        />
      </Stack.Navigator>
    </NavigationContainer>
  );
}