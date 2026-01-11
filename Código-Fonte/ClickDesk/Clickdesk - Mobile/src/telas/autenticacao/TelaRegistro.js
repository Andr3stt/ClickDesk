import React, { useState, useEffect } from 'react';
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

export default function RegisterScreen({ navigation, route }) {
  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [showPassword, setShowPassword] = useState(false);
  const [showConfirmPassword, setShowConfirmPassword] = useState(false);
  const [termsAccepted, setTermsAccepted] = useState(false);
  const [userType, setUserType] = useState('usuario'); // 'usuario' ou 'tecnico'

  // Verifica se voltou da tela de termos
  useEffect(() => {
    if (route?.params?.termsAccepted) {
      setTermsAccepted(true);
      const userData = route.params.userData;
      if (userData) {
        setFirstName(userData.firstName);
        setLastName(userData.lastName);
        setEmail(userData.email);
        setPassword(userData.password);
        setConfirmPassword(userData.confirmPassword);
        setUserType(userData.userType);
      }
      // Limpa os parâmetros
      navigation.setParams({ termsAccepted: undefined, userData: undefined });
    }
  }, [route?.params]);

  const handleRegister = () => {
    if (!firstName || !lastName || !email || !password || !confirmPassword) {
      Alert.alert('Erro', 'Por favor, preencha todos os campos');
      return;
    }
    
    if (password !== confirmPassword) {
      Alert.alert('Erro', 'As senhas não correspondem');
      return;
    }

    if (!termsAccepted) {
      Alert.alert(
        'Termos de Uso',
        'Você precisa ler e aceitar os Termos de Uso para continuar.',
        [
          { text: 'Cancelar', style: 'cancel' },
          { 
            text: 'Ler Termos', 
            onPress: () => navigation.navigate('Terms', { 
              returnTo: 'Register',
              userData: { firstName, lastName, email, password, confirmPassword, userType }
            })
          }
        ]
      );
      return;
    }

    Alert.alert(
      'Sucesso', 
      `Conta criada com sucesso!\nTipo: ${userType === 'tecnico' ? 'Técnico' : 'Usuário'}`,
      [{ text: 'OK', onPress: () => navigation.navigate('Login') }]
    );
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
            <Text style={styles.brandSubtitle}>Crie sua conta e comece a gerenciar seus chamados</Text>
            
            <View style={styles.featuresList}>
              <View style={styles.featureItem}>
                <Text style={styles.featureIcon}>✓</Text>
                <Text style={styles.featureText}>Gestão completa de chamados</Text>
              </View>
              <View style={styles.featureItem}>
                <Text style={styles.featureIcon}>✓</Text>
                <Text style={styles.featureText}>Controle de prazos e prioridades</Text>
              </View>
              <View style={styles.featureItem}>
                <Text style={styles.featureIcon}>✓</Text>
                <Text style={styles.featureText}>Colaboração em equipe</Text>
              </View>
            </View>
          </View>

          <View style={styles.formCard}>
            <View style={styles.header}>
              <Text style={styles.title}>Criar nova conta</Text>
              <Text style={styles.subtitle}>Preencha as informações abaixo para começar</Text>
            </View>

            <View style={styles.form}>
              <View style={styles.formRow}>
                <View style={[styles.inputContainer, styles.halfWidth]}>
                  <Text style={styles.label}>Nome</Text>
                  <TextInput
                    style={styles.input}
                    placeholder="Seu nome"
                    placeholderTextColor="#BDC3C7"
                    value={firstName}
                    onChangeText={setFirstName}
                  />
                </View>

                <View style={[styles.inputContainer, styles.halfWidth]}>
                  <Text style={styles.label}>Sobrenome</Text>
                  <TextInput
                    style={styles.input}
                    placeholder="Seu sobrenome"
                    placeholderTextColor="#BDC3C7"
                    value={lastName}
                    onChangeText={setLastName}
                  />
                </View>
              </View>

              <View style={styles.inputContainer}>
                <Text style={styles.label}>E-mail</Text>
                <TextInput
                  style={styles.input}
                  placeholder="seu@email.com"
                  placeholderTextColor="#BDC3C7"
                  value={email}
                  onChangeText={setEmail}
                  keyboardType="email-address"
                  autoCapitalize="none"
                />
              </View>

              <View style={styles.inputContainer}>
                <Text style={styles.label}>Senha</Text>
                <View style={styles.passwordInputWrapper}>
                  <TextInput
                    style={styles.passwordInput}
                    placeholder="Crie uma senha segura"
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
                <Text style={styles.helpText}>Mínimo 8 caracteres, use letras, números e símbolos</Text>
              </View>

              <View style={styles.inputContainer}>
                <Text style={styles.label}>Confirmar senha</Text>
                <View style={styles.passwordInputWrapper}>
                  <TextInput
                    style={styles.passwordInput}
                    placeholder="Digite a senha novamente"
                    placeholderTextColor="#BDC3C7"
                    value={confirmPassword}
                    onChangeText={setConfirmPassword}
                    secureTextEntry={!showConfirmPassword}
                  />
                  <TouchableOpacity 
                    style={styles.toggleButton}
                    onPress={() => setShowConfirmPassword(!showConfirmPassword)}
                  >
                    <MaterialCommunityIcons 
                      name={showConfirmPassword ? 'eye-off' : 'eye'} 
                      size={20} 
                      color="#95A5A6" 
                    />
                  </TouchableOpacity>
                </View>
              </View>

              <View style={styles.inputContainer}>
                <Text style={styles.label}>Tipo de Conta</Text>
                <View style={styles.userTypeContainer}>
                  <TouchableOpacity
                    style={[styles.userTypeButton, userType === 'usuario' && styles.userTypeButtonActive]}
                    onPress={() => setUserType('usuario')}
                  >
                    <MaterialCommunityIcons 
                      name="account" 
                      size={24} 
                      color={userType === 'usuario' ? '#E67E22' : '#7F8C8D'} 
                    />
                    <Text style={[styles.userTypeText, userType === 'usuario' && styles.userTypeTextActive]}>
                      Usuário
                    </Text>
                  </TouchableOpacity>
                  <TouchableOpacity
                    style={[styles.userTypeButton, userType === 'tecnico' && styles.userTypeButtonActive]}
                    onPress={() => setUserType('tecnico')}
                  >
                    <MaterialCommunityIcons 
                      name="account-wrench" 
                      size={24} 
                      color={userType === 'tecnico' ? '#E67E22' : '#7F8C8D'} 
                    />
                    <Text style={[styles.userTypeText, userType === 'tecnico' && styles.userTypeTextActive]}>
                      Técnico
                    </Text>
                  </TouchableOpacity>
                </View>
              </View>

              <TouchableOpacity 
                style={styles.termsSection}
                onPress={() => setTermsAccepted(!termsAccepted)}
              >
                <View style={[styles.checkbox, termsAccepted && styles.checkboxChecked]}>
                  {termsAccepted && (
                    <MaterialCommunityIcons name="check" size={14} color="white" />
                  )}
                </View>
                <Text style={styles.termsLabel}>
                  Eu aceito os <Text style={styles.termsLink} onPress={() => navigation.navigate('Terms')}>Termos de Uso</Text> e <Text style={styles.termsLink} onPress={() => navigation.navigate('Terms')}>Política de Privacidade</Text>
                </Text>
              </TouchableOpacity>

              <TouchableOpacity
                style={styles.registerButton}
                onPress={handleRegister}
                activeOpacity={0.8}
              >
                <Text style={styles.registerButtonText}>Criar conta</Text>
              </TouchableOpacity>

              <View style={styles.divider}>
                <View style={styles.dividerLine} />
                <Text style={styles.dividerText}>ou</Text>
                <View style={styles.dividerLine} />
              </View>

              <View style={styles.loginSection}>
                <Text style={styles.loginText}>Já possui uma conta?</Text>
                <TouchableOpacity onPress={() => navigation.navigate('Login')}>
                  <Text style={styles.loginLink}>Fazer login</Text>
                </TouchableOpacity>
              </View>
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
    marginBottom: 16,
  },
  featuresList: {
    width: '100%',
  },
  featureItem: {
    flexDirection: 'row',
    alignItems: 'center',
    marginBottom: 12,
  },
  featureIcon: {
    fontSize: 18,
    color: '#E67E22',
    marginRight: 12,
    fontWeight: 'bold',
  },
  featureText: {
    fontSize: 13,
    color: '#7F8C8D',
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
  subtitle: {
    fontSize: 14,
    color: '#7F8C8D',
  },
  form: {
    width: '100%',
  },
  formRow: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    marginBottom: 18,
  },
  halfWidth: {
    width: '48%',
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
  helpText: {
    fontSize: 12,
    color: '#95A5A6',
    marginTop: 6,
  },
  termsSection: {
    flexDirection: 'row',
    alignItems: 'center',
    marginBottom: 20,
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
  termsLabel: {
    fontSize: 13,
    color: '#2C3E50',
    flex: 1,
  },
  termsLink: {
    color: '#E67E22',
    fontWeight: '600',
  },
  userTypeContainer: {
    flexDirection: 'row',
    gap: 12,
  },
  userTypeButton: {
    flex: 1,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    gap: 8,
    paddingVertical: 16,
    paddingHorizontal: 12,
    backgroundColor: '#F8F9FA',
    borderWidth: 2,
    borderColor: '#E1E8ED',
    borderRadius: 10,
  },
  userTypeButtonActive: {
    backgroundColor: '#FFF5ED',
    borderColor: '#E67E22',
  },
  userTypeText: {
    fontSize: 14,
    fontWeight: '600',
    color: '#7F8C8D',
  },
  userTypeTextActive: {
    color: '#E67E22',
  },
  registerButton: {
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
  registerButtonText: {
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
  loginSection: {
    alignItems: 'center',
  },
  loginText: {
    fontSize: 14,
    color: '#7F8C8D',
    marginBottom: 8,
  },
  loginLink: {
    fontSize: 14,
    color: '#E67E22',
    fontWeight: '600',
  },
});
