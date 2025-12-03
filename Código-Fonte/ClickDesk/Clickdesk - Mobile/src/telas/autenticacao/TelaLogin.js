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
} from 'react-native';
import { MaterialCommunityIcons } from '@expo/vector-icons';

export default function LoginScreen({ navigation }) {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [showPassword, setShowPassword] = useState(false);
  const [rememberMe, setRememberMe] = useState(false);

  const handleLogin = () => {
    if (username && password) {
      // Simulação: verificar se é técnico (na prática, virá do backend)
      // Por enquanto: se username contém 'tecnico', redireciona para AdminDashboard
      const isTecnico = username.toLowerCase().includes('tecnico') || username.toLowerCase().includes('admin');
      
      if (isTecnico) {
        navigation.navigate('AdminDashboard');
      } else {
        navigation.navigate('Dashboard');
      }
    } else {
      Alert.alert('Erro', 'Por favor, preencha todos os campos');
    }
  };

  return (
    <SafeAreaView style={styles.container}>
      <StatusBar barStyle="light-content" backgroundColor="#E67E22" />
      <KeyboardAvoidingView 
        behavior={Platform.OS === 'ios' ? 'padding' : 'height'}
        style={styles.keyboardAvoid}
      >
        <ScrollView 
          contentContainerStyle={styles.scrollContent} 
          showsVerticalScrollIndicator={false}
        >
          <View style={styles.brandingSection}>
            <Text style={styles.brandTitle}>ClickDesk</Text>
            <Text style={styles.brandSubtitle}>Sistema de Gerenciamento de Chamados</Text>
          </View>

          <View style={styles.formCard}>
            <View style={styles.header}>
              <Text style={styles.title}>
                <Text style={styles.titleHighlight}>Login</Text> Clickdesk
              </Text>
              <Text style={styles.subtitle}>Acesse sua conta para continuar</Text>
            </View>

            <View style={styles.form}>
              <View style={styles.inputContainer}>
                <Text style={styles.label}>Usuário</Text>
                <TextInput
                  style={styles.input}
                  placeholder="vini"
                  placeholderTextColor="#BDC3C7"
                  value={username}
                  onChangeText={setUsername}
                  autoCapitalize="none"
                />
              </View>

              <View style={styles.inputContainer}>
                <Text style={styles.label}>Senha</Text>
                <View style={styles.passwordInputWrapper}>
                  <TextInput
                    style={styles.passwordInput}
                    placeholder="••••••••"
                    placeholderTextColor="#BDC3C7"
                    value={password}
                    onChangeText={setPassword}
                    secureTextEntry={!showPassword}
                  />
                  <TouchableOpacity 
                    style={styles.toggleButton}
                    onPress={() => setShowPassword(!showPassword)}
                  >
                    <MaterialCommunityIcons 
                      name={showPassword ? 'eye-off' : 'eye'} 
                      size={20} 
                      color="#95A5A6" 
                    />
                  </TouchableOpacity>
                </View>
              </View>

              <View style={styles.rememberSection}>
                <TouchableOpacity 
                  style={styles.checkboxContainer}
                  onPress={() => setRememberMe(!rememberMe)}
                >
                  <View style={[styles.checkbox, rememberMe && styles.checkboxChecked]}>
                    {rememberMe && (
                      <MaterialCommunityIcons name="check" size={14} color="white" />
                    )}
                  </View>
                  <Text style={styles.checkboxLabel}>Lembrar-me</Text>
                </TouchableOpacity>
                <TouchableOpacity onPress={() => navigation.navigate('ForgotPassword')}>
                  <Text style={styles.forgotPasswordText}>Esqueci minha senha</Text>
                </TouchableOpacity>
              </View>

              <TouchableOpacity
                style={styles.loginButton}
                onPress={handleLogin}
                activeOpacity={0.8}
              >
                <Text style={styles.loginButtonText}>ENTRAR</Text>
              </TouchableOpacity>

              <View style={styles.divider}>
                <View style={styles.dividerLine} />
                <Text style={styles.dividerText}>ou</Text>
                <View style={styles.dividerLine} />
              </View>

              <TouchableOpacity
                style={styles.signupButton}
                onPress={() => navigation.navigate('Register')}
              >
                <Text style={styles.signupButtonText}>Criar conta</Text>
              </TouchableOpacity>

              <Text style={styles.termsText}>
                Ao fazer login, você aceita nossos{'\n'}
                <Text style={styles.termsLink} onPress={() => navigation.navigate('Terms')}>Termos de Uso</Text> e{' '}
                <Text style={styles.termsLink} onPress={() => navigation.navigate('Terms')}>Política de Privacidade</Text>
              </Text>
            </View>
          </View>
        </ScrollView>
      </KeyboardAvoidingView>
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#E8D5C4',
  },
  keyboardAvoid: {
    flex: 1,
  },
  scrollContent: {
    flexGrow: 1,
    paddingHorizontal: 16,
    paddingVertical: 24,
  },
  brandingSection: {
    alignItems: 'center',
    marginBottom: 32,
    marginTop: 16,
  },
  brandTitle: {
    fontSize: 48,
    fontWeight: 'bold',
    color: '#E67E22',
    marginBottom: 8,
    letterSpacing: 2,
  },
  brandSubtitle: {
    fontSize: 14,
    color: '#7F8C8D',
    textAlign: 'center',
  },
  formCard: {
    backgroundColor: 'white',
    borderRadius: 16,
    paddingHorizontal: 20,
    paddingVertical: 28,
    marginBottom: 16,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.15,
    shadowRadius: 8,
    elevation: 8,
  },
  header: {
    alignItems: 'center',
    marginBottom: 28,
  },
  title: {
    fontSize: 24,
    fontWeight: '700',
    color: '#2C3E50',
    marginBottom: 8,
  },
  titleHighlight: {
    color: '#E67E22',
  },
  subtitle: {
    fontSize: 14,
    color: '#7F8C8D',
  },
  form: {
    width: '100%',
  },
  inputContainer: {
    marginBottom: 18,
  },
  label: {
    fontSize: 14,
    fontWeight: '600',
    color: '#2C3E50',
    marginBottom: 8,
  },
  input: {
    backgroundColor: '#F8F9FA',
    borderWidth: 1.5,
    borderColor: '#E1E8ED',
    borderRadius: 10,
    paddingHorizontal: 16,
    paddingVertical: 12,
    fontSize: 15,
    color: '#2C3E50',
  },
  passwordInputWrapper: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#F8F9FA',
    borderWidth: 1.5,
    borderColor: '#E1E8ED',
    borderRadius: 10,
  },
  passwordInput: {
    flex: 1,
    paddingHorizontal: 16,
    paddingVertical: 12,
    fontSize: 15,
    color: '#2C3E50',
  },
  toggleButton: {
    paddingHorizontal: 12,
    paddingVertical: 12,
  },
  rememberSection: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    marginBottom: 20,
  },
  checkboxContainer: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  checkbox: {
    width: 18,
    height: 18,
    borderWidth: 2,
    borderColor: '#E67E22',
    borderRadius: 4,
    marginRight: 8,
    justifyContent: 'center',
    alignItems: 'center',
  },
  checkboxChecked: {
    backgroundColor: '#E67E22',
  },
  checkboxLabel: {
    fontSize: 14,
    color: '#2C3E50',
    fontWeight: '500',
  },
  forgotPasswordText: {
    color: '#E67E22',
    fontSize: 13,
    fontWeight: '500',
  },
  loginButton: {
    backgroundColor: '#E67E22',
    borderRadius: 10,
    paddingVertical: 14,
    alignItems: 'center',
    marginBottom: 16,
    shadowColor: '#E67E22',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.3,
    shadowRadius: 4,
    elevation: 4,
  },
  loginButtonText: {
    color: 'white',
    fontSize: 16,
    fontWeight: 'bold',
    letterSpacing: 0.5,
  },
  divider: {
    flexDirection: 'row',
    alignItems: 'center',
    marginVertical: 20,
  },
  dividerLine: {
    flex: 1,
    height: 1,
    backgroundColor: '#E1E8ED',
  },
  dividerText: {
    color: '#95A5A6',
    marginHorizontal: 12,
    fontSize: 13,
  },
  signupButton: {
    borderWidth: 1.5,
    borderColor: '#E67E22',
    borderRadius: 10,
    paddingVertical: 12,
    alignItems: 'center',
    marginBottom: 16,
  },
  signupButtonText: {
    color: '#E67E22',
    fontSize: 15,
    fontWeight: '600',
  },
  termsText: {
    fontSize: 12,
    color: '#7F8C8D',
    textAlign: 'center',
    lineHeight: 18,
  },
  termsLink: {
    color: '#E67E22',
    fontWeight: '600',
  },
});
