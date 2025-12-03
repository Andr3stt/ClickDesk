/**
 * Tela de Recuperar Senha
 * 
 * Permite que o usuário recupere o acesso à conta através do email.
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
import { Cores } from '../../estilos/cores';
import { validarEmail } from '../../servicos/utilitarios/validadores';

export default function TelaRecuperarSenha({ navigation }) {
  const [email, setEmail] = useState('');
  const [carregando, setCarregando] = useState(false);

  const handleEnviarLink = async () => {
    // Validação do email
    if (!email.trim()) {
      Alert.alert('Erro', 'Por favor, informe seu e-mail');
      return;
    }

    if (!validarEmail(email)) {
      Alert.alert('Erro', 'Por favor, informe um e-mail válido');
      return;
    }

    try {
      setCarregando(true);

      // Simula chamada API
      await new Promise(resolve => setTimeout(resolve, 1500));

      Alert.alert(
        'Link Enviado!',
        `Um link para redefinir sua senha foi enviado para ${email}. Por favor, verifique sua caixa de entrada.`,
        [
          {
            text: 'OK',
            onPress: () => navigation.navigate('Login')
          }
        ]
      );
    } catch (error) {
      Alert.alert('Erro', 'Não foi possível enviar o link. Tente novamente.');
    } finally {
      setCarregando(false);
    }
  };

  return (
    <SafeAreaView style={styles.container}>
      <StatusBar barStyle="dark-content" backgroundColor={Cores.background} />
      
      <KeyboardAvoidingView 
        behavior={Platform.OS === 'ios' ? 'padding' : 'height'}
        style={styles.keyboardAvoid}
      >
        <ScrollView 
          contentContainerStyle={styles.scrollContent}
          showsVerticalScrollIndicator={false}
        >
          {/* Branding */}
          <View style={styles.brandingSection}>
            <Text style={styles.brandTitle}>ClickDesk</Text>
            <Text style={styles.brandSubtitle}>Recupere o acesso à sua conta</Text>
          </View>

          {/* Card Principal */}
          <View style={styles.card}>
            {/* Ícone */}
            <View style={styles.iconContainer}>
              <View style={styles.iconCircle}>
                <MaterialCommunityIcons 
                  name="clock-outline" 
                  size={48} 
                  color={Cores.brand} 
                />
              </View>
            </View>

            {/* Título */}
            <View style={styles.header}>
              <Text style={styles.title}>Esqueceu a senha?</Text>
              <Text style={styles.subtitle}>
                Não se preocupe. Insira seu e-mail abaixo e nós lhe enviaremos um link para alterar sua senha.
              </Text>
            </View>

            {/* Benefícios */}
            <View style={styles.benefitsContainer}>
              <View style={styles.benefitItem}>
                <MaterialCommunityIcons 
                  name="check-circle" 
                  size={20} 
                  color={Cores.success} 
                />
                <Text style={styles.benefitText}>Link enviado por e-mail</Text>
              </View>
              <View style={styles.benefitItem}>
                <MaterialCommunityIcons 
                  name="check-circle" 
                  size={20} 
                  color={Cores.success} 
                />
                <Text style={styles.benefitText}>Recuperação segura</Text>
              </View>
              <View style={styles.benefitItem}>
                <MaterialCommunityIcons 
                  name="check-circle" 
                  size={20} 
                  color={Cores.success} 
                />
                <Text style={styles.benefitText}>Processo rápido e fácil</Text>
              </View>
            </View>

            {/* Formulário */}
            <View style={styles.form}>
              <Text style={styles.label}>E-mail</Text>
              <View style={styles.inputWrapper}>
                <MaterialCommunityIcons 
                  name="email-outline" 
                  size={20} 
                  color={Cores.textoSecundario}
                  style={styles.inputIcon}
                />
                <TextInput
                  style={styles.input}
                  placeholder="seu@email.com"
                  placeholderTextColor={Cores.textoSecundario}
                  value={email}
                  onChangeText={setEmail}
                  keyboardType="email-address"
                  autoCapitalize="none"
                  autoCorrect={false}
                  editable={!carregando}
                />
              </View>
            </View>

            {/* Botões */}
            <View style={styles.buttonsContainer}>
              <TouchableOpacity 
                style={[styles.primaryButton, carregando && styles.buttonDisabled]}
                onPress={handleEnviarLink}
                disabled={carregando}
              >
                {carregando ? (
                  <ActivityIndicator color="#FFFFFF" />
                ) : (
                  <>
                    <MaterialCommunityIcons name="email-fast" size={20} color="#FFFFFF" />
                    <Text style={styles.primaryButtonText}>Enviar link de recuperação</Text>
                  </>
                )}
              </TouchableOpacity>

              <TouchableOpacity 
                style={styles.secondaryButton}
                onPress={() => navigation.goBack()}
                disabled={carregando}
              >
                <Text style={styles.secondaryButtonText}>Cancelar</Text>
              </TouchableOpacity>
            </View>

            {/* Divider */}
            <View style={styles.divider}>
              <View style={styles.dividerLine} />
              <Text style={styles.dividerText}>ou</Text>
              <View style={styles.dividerLine} />
            </View>

            {/* Link para Login */}
            <TouchableOpacity 
              style={styles.loginLink}
              onPress={() => navigation.navigate('Login')}
              disabled={carregando}
            >
              <MaterialCommunityIcons name="arrow-left" size={18} color={Cores.brand} />
              <Text style={styles.loginLinkText}>Voltar para o login</Text>
            </TouchableOpacity>
          </View>
        </ScrollView>
      </KeyboardAvoidingView>
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: Cores.background,
  },
  keyboardAvoid: {
    flex: 1,
  },
  scrollContent: {
    flexGrow: 1,
    padding: 20,
  },

  // Branding
  brandingSection: {
    alignItems: 'center',
    marginBottom: 30,
    marginTop: 20,
  },
  brandTitle: {
    fontSize: 42,
    fontWeight: 'bold',
    color: Cores.brand,
    marginBottom: 8,
  },
  brandSubtitle: {
    fontSize: 15,
    color: Cores.textoSecundario,
    textAlign: 'center',
  },

  // Card
  card: {
    backgroundColor: '#FFFFFF',
    borderRadius: 12,
    padding: 24,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.1,
    shadowRadius: 8,
    elevation: 3,
  },

  // Ícone
  iconContainer: {
    alignItems: 'center',
    marginBottom: 20,
  },
  iconCircle: {
    width: 80,
    height: 80,
    borderRadius: 40,
    backgroundColor: Cores.brand + '15',
    alignItems: 'center',
    justifyContent: 'center',
  },

  // Header
  header: {
    marginBottom: 24,
  },
  title: {
    fontSize: 24,
    fontWeight: 'bold',
    color: Cores.textoPrincipal,
    marginBottom: 8,
    textAlign: 'center',
  },
  subtitle: {
    fontSize: 14,
    color: Cores.textoSecundario,
    lineHeight: 20,
    textAlign: 'center',
  },

  // Benefícios
  benefitsContainer: {
    backgroundColor: Cores.background,
    borderRadius: 8,
    padding: 16,
    marginBottom: 24,
  },
  benefitItem: {
    flexDirection: 'row',
    alignItems: 'center',
    marginBottom: 8,
    gap: 10,
  },
  benefitText: {
    fontSize: 14,
    color: Cores.textoPrincipal,
  },

  // Formulário
  form: {
    marginBottom: 20,
  },
  label: {
    fontSize: 14,
    fontWeight: '600',
    color: Cores.textoPrincipal,
    marginBottom: 8,
  },
  inputWrapper: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#FFFFFF',
    borderWidth: 2,
    borderColor: Cores.brand,
    borderRadius: 8,
    paddingHorizontal: 14,
  },
  inputIcon: {
    marginRight: 10,
  },
  input: {
    flex: 1,
    padding: 14,
    fontSize: 16,
    color: Cores.textoPrincipal,
  },

  // Botões
  buttonsContainer: {
    gap: 12,
    marginBottom: 20,
  },
  primaryButton: {
    flexDirection: 'row',
    backgroundColor: Cores.brand,
    borderRadius: 8,
    padding: 16,
    alignItems: 'center',
    justifyContent: 'center',
    gap: 8,
  },
  buttonDisabled: {
    opacity: 0.6,
  },
  primaryButtonText: {
    color: '#FFFFFF',
    fontSize: 16,
    fontWeight: 'bold',
  },
  secondaryButton: {
    backgroundColor: 'transparent',
    borderWidth: 2,
    borderColor: Cores.textoSecundario + '30',
    borderRadius: 8,
    padding: 16,
    alignItems: 'center',
  },
  secondaryButtonText: {
    color: Cores.textoSecundario,
    fontSize: 16,
    fontWeight: '600',
  },

  // Divider
  divider: {
    flexDirection: 'row',
    alignItems: 'center',
    marginBottom: 20,
  },
  dividerLine: {
    flex: 1,
    height: 1,
    backgroundColor: Cores.textoSecundario + '20',
  },
  dividerText: {
    marginHorizontal: 12,
    fontSize: 14,
    color: Cores.textoSecundario,
  },

  // Login Link
  loginLink: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    gap: 6,
    padding: 8,
  },
  loginLinkText: {
    fontSize: 15,
    color: Cores.brand,
    fontWeight: '600',
  },
});
