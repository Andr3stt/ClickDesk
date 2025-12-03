/**
 * Tela de Login
 * 
 * Permite que o usuário faça login no sistema ClickDesk.
 * Utiliza o serviço de autenticação para realizar login com JWT.
 */

import React, { useState } from 'react';
import {
  View,
  Text,
  TextInput,
  TouchableOpacity,
  StyleSheet,
  SafeAreaView,
  StatusBar,
  Alert,
  KeyboardAvoidingView,
  Platform,
  ScrollView,
  ActivityIndicator,
} from 'react-native';
import { MaterialCommunityIcons } from '@expo/vector-icons';
import { login } from '../../servicos/api/autenticacaoService';
import { Cores } from '../../estilos/cores';
import { EstilosGlobais } from '../../estilos/global';
import { validarEmail, validarCampoObrigatorio } from '../../servicos/utilitarios/validadores';

/**
 * Componente principal da tela de login
 */
export default function TelaLogin({ navigation }) {
  // Estados do formulário
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [showPassword, setShowPassword] = useState(false);
  const [rememberMe, setRememberMe] = useState(false);
  const [carregando, setCarregando] = useState(false);

  /**
   * Manipulador de login
   * Valida os campos e chama o serviço de autenticação
   */
  const handleLogin = async () => {
    // Validação dos campos
    if (!validarCampoObrigatorio(username)) {
      Alert.alert('Erro', 'Por favor, informe seu usuário ou email');
      return;
    }

    if (!validarCampoObrigatorio(password)) {
      Alert.alert('Erro', 'Por favor, informe sua senha');
      return;
    }

    try {
      setCarregando(true);

      // Chama o serviço de login
      const resultado = await login({
        username: username.trim(),
        password: password,
      });

      if (resultado.success && resultado.user) {
        // Login bem-sucedido
        const usuario = resultado.user;

        // Navega para o dashboard apropriado com base no tipo de usuário
        if (usuario.role === 'ADMIN' || usuario.role === 'TECH') {
          navigation.replace('DashboardAdmin');
        } else {
          navigation.replace('Dashboard');
        }
      } else {
        Alert.alert('Erro', 'Falha ao realizar login');
      }
    } catch (error) {
      // Trata erros de autenticação
      const mensagemErro = error.mensagem || 'Usuário ou senha incorretos';
      Alert.alert('Erro de Login', mensagemErro);
    } finally {
      setCarregando(false);
    }
  };

  /**
   * Navega para a tela de registro
   */
  const navegarParaRegistro = () => {
    navigation.navigate('Registro');
  };

  /**
   * Navega para a tela de recuperação de senha
   */
  const navegarParaRecuperarSenha = () => {
    navigation.navigate('RecuperarSenha');
  };

  return (
    <SafeAreaView style={styles.container}>
      <StatusBar barStyle="light-content" backgroundColor={Cores.brandDark} />
      
      <KeyboardAvoidingView 
        behavior={Platform.OS === 'ios' ? 'padding' : 'height'}
        style={styles.keyboardAvoid}
      >
        <ScrollView 
          contentContainerStyle={styles.scrollContent} 
          showsVerticalScrollIndicator={false}
        >
          {/* Seção de Branding */}
          <View style={styles.brandingSection}>
            <Text style={styles.brandTitle}>ClickDesk</Text>
            <Text style={styles.brandSubtitle}>Sistema de Gerenciamento de Chamados</Text>
          </View>

          {/* Card do Formulário */}
          <View style={styles.formCard}>
            {/* Cabeçalho */}
            <View style={styles.header}>
              <Text style={styles.title}>
                <Text style={styles.titleHighlight}>Login</Text> ClickDesk
              </Text>
              <Text style={styles.subtitle}>Acesse sua conta para continuar</Text>
            </View>

            {/* Formulário */}
            <View style={styles.form}>
              {/* Campo de Usuário */}
              <View style={styles.inputContainer}>
                <Text style={styles.label}>Usuário ou Email</Text>
                <TextInput
                  style={styles.input}
                  placeholder="Digite seu usuário ou email"
                  placeholderTextColor={Cores.textoSecundario}
                  value={username}
                  onChangeText={setUsername}
                  autoCapitalize="none"
                  editable={!carregando}
                />
              </View>

              {/* Campo de Senha */}
              <View style={styles.inputContainer}>
                <Text style={styles.label}>Senha</Text>
                <View style={styles.passwordInputWrapper}>
                  <TextInput
                    style={styles.passwordInput}
                    placeholder="Digite sua senha"
                    placeholderTextColor={Cores.textoSecundario}
                    value={password}
                    onChangeText={setPassword}
                    secureTextEntry={!showPassword}
                    editable={!carregando}
                  />
                  <TouchableOpacity 
                    style={styles.toggleButton}
                    onPress={() => setShowPassword(!showPassword)}
                    disabled={carregando}
                  >
                    <MaterialCommunityIcons 
                      name={showPassword ? 'eye-off' : 'eye'} 
                      size={20} 
                      color={Cores.textoSecundario} 
                    />
                  </TouchableOpacity>
                </View>
              </View>

              {/* Lembrar-me e Esqueci a senha */}
              <View style={styles.rememberSection}>
                <TouchableOpacity 
                  style={styles.checkboxRow}
                  onPress={() => setRememberMe(!rememberMe)}
                  disabled={carregando}
                >
                  <MaterialCommunityIcons 
                    name={rememberMe ? 'checkbox-marked' : 'checkbox-blank-outline'} 
                    size={20} 
                    color={Cores.brand} 
                  />
                  <Text style={styles.rememberText}>Lembrar-me</Text>
                </TouchableOpacity>
                
                <TouchableOpacity 
                  onPress={navegarParaRecuperarSenha}
                  disabled={carregando}
                >
                  <Text style={styles.forgotPassword}>Esqueci minha senha</Text>
                </TouchableOpacity>
              </View>

              {/* Botão de Login */}
              <TouchableOpacity 
                style={[styles.loginButton, carregando && styles.loginButtonDisabled]}
                onPress={handleLogin}
                disabled={carregando}
              >
                {carregando ? (
                  <ActivityIndicator color={Cores.branco} />
                ) : (
                  <Text style={styles.loginButtonText}>ENTRAR</Text>
                )}
              </TouchableOpacity>

              {/* Link para Registro */}
              <View style={styles.registerSection}>
                <Text style={styles.registerText}>Não tem uma conta? </Text>
                <TouchableOpacity 
                  onPress={() => navigation.navigate('Registro')}
                  disabled={carregando}
                >
                  <Text style={styles.registerLink}>Cadastre-se</Text>
                </TouchableOpacity>
              </View>

              {/* Botões de Teste (REMOVER EM PRODUÇÃO) */}
              <View style={styles.testSection}>
                <Text style={styles.testTitle}>🧪 Modo Teste - Navegação Rápida:</Text>
                <TouchableOpacity 
                  style={styles.testButton}
                  onPress={() => navigation.navigate('Dashboard')}
                >
                  <Text style={styles.testButtonText}>📱 Dashboard Usuário</Text>
                </TouchableOpacity>
                <TouchableOpacity 
                  style={styles.testButton}
                  onPress={() => navigation.navigate('DashboardAdmin')}
                >
                  <Text style={styles.testButtonText}>👨‍💼 Dashboard Admin</Text>
                </TouchableOpacity>
                <TouchableOpacity 
                  style={styles.testButton}
                  onPress={() => navigation.navigate('FAQ')}
                >
                  <Text style={styles.testButtonText}>❓ FAQ</Text>
                </TouchableOpacity>
              </View>

              {/* Footer com Link de Termos */}
              <View style={styles.footerSection}>
                <Text style={styles.footerText}>
                  Ao fazer login, você concorda com nossos{' '}
                </Text>
                <TouchableOpacity 
                  onPress={() => navigation.navigate('TelaTermos')}
                  disabled={carregando}
                >
                  <Text style={styles.termosLink}>Termos de Uso</Text>
                </TouchableOpacity>
              </View>
            </View>
          </View>
        </ScrollView>
      </KeyboardAvoidingView>
    </SafeAreaView>
  );
}

/**
 * Estilos da tela
 */
const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: Cores.background, // #EDE6D9 - bege como no web
  },
  keyboardAvoid: {
    flex: 1,
  },
  scrollContent: {
    flexGrow: 1,
    padding: 20,
  },
  
  // Branding (lado esquerdo no web, topo no mobile)
  brandingSection: {
    alignItems: 'center',
    marginBottom: 30,
    marginTop: 20,
  },
  brandTitle: {
    fontSize: 42,
    fontWeight: 'bold',
    color: Cores.brand, // Laranja como no web
    marginBottom: 8,
  },
  brandSubtitle: {
    fontSize: 15,
    color: Cores.textoSecundario,
    opacity: 0.9,
  },
  
  // Card do formulário
  formCard: {
    backgroundColor: Cores.branco,
    borderRadius: 16,
    padding: 24,
    ...EstilosGlobais.sombraCard,
  },
  
  // Cabeçalho
  header: {
    marginBottom: 24,
  },
  title: {
    fontSize: 24,
    fontWeight: 'bold',
    color: Cores.textoPrincipal,
    marginBottom: 4,
  },
  titleHighlight: {
    color: Cores.brand,
  },
  subtitle: {
    fontSize: 14,
    color: Cores.textoSecundario,
  },
  
  // Formulário
  form: {
    gap: 16,
  },
  inputContainer: {
    marginBottom: 8,
  },
  label: {
    fontSize: 14,
    fontWeight: '600',
    color: Cores.textoPrincipal,
    marginBottom: 8,
  },
  input: {
    backgroundColor: '#FFFFFF',
    borderWidth: 2,
    borderColor: Cores.brand,
    borderRadius: 8,
    padding: 14,
    fontSize: 16,
    color: Cores.textoPrincipal,
  },
  passwordInputWrapper: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FFFFFF',
    borderWidth: 2,
    borderColor: Cores.brand,
    borderRadius: 8,
  },
  passwordInput: {
    flex: 1,
    padding: 14,
    fontSize: 16,
    color: Cores.textoPrincipal,
  },
  toggleButton: {
    padding: 12,
  },
  
  // Lembrar-me
  rememberSection: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    marginTop: 8,
    marginBottom: 16,
  },
  checkboxRow: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 8,
  },
  rememberText: {
    fontSize: 14,
    color: Cores.textoPrincipal,
  },
  forgotPassword: {
    fontSize: 14,
    color: Cores.brand,
    fontWeight: '500',
  },
  
  // Botão de login
  loginButton: {
    backgroundColor: Cores.brand,
    borderRadius: 8,
    padding: 16,
    alignItems: 'center',
    marginTop: 8,
  },
  loginButtonDisabled: {
    opacity: 0.6,
  },
  loginButtonText: {
    color: Cores.branco,
    fontSize: 16,
    fontWeight: 'bold',
  },
  
  // Registro
  registerSection: {
    flexDirection: 'row',
    justifyContent: 'center',
    alignItems: 'center',
    marginTop: 16,
  },
  registerText: {
    fontSize: 14,
    color: Cores.textoSecundario,
  },
  registerLink: {
    fontSize: 14,
    color: Cores.brand,
    fontWeight: 'bold',
  },
  
  // Botões de Teste
  testSection: {
    marginTop: 30,
    padding: 15,
    backgroundColor: '#FFF3CD',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#FFE69C',
  },
  testTitle: {
    fontSize: 14,
    fontWeight: 'bold',
    color: '#856404',
    marginBottom: 10,
    textAlign: 'center',
  },
  testButton: {
    backgroundColor: '#FFF',
    borderRadius: 6,
    padding: 12,
    marginTop: 8,
    borderWidth: 1,
    borderColor: '#FFE69C',
  },
  testButtonText: {
    fontSize: 14,
    color: '#856404',
    textAlign: 'center',
    fontWeight: '600',
  },

  // Footer
  footerSection: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    justifyContent: 'center',
    alignItems: 'center',
    marginTop: 24,
    paddingTop: 20,
    borderTopWidth: 1,
    borderTopColor: Cores.textoSecundario + '20',
  },
  footerText: {
    fontSize: 12,
    color: Cores.textoSecundario,
    textAlign: 'center',
  },
  termosLink: {
    fontSize: 12,
    color: Cores.brand,
    fontWeight: 'bold',
    textDecorationLine: 'underline',
  },
});
