/**
 * Tela de Termos de Uso
 * 
 * Exibe os termos de uso e política de privacidade do ClickDesk.
 */

import React, { useState } from 'react';
import { View, Text, StyleSheet, SafeAreaView, TouchableOpacity, ScrollView, StatusBar, Alert } from 'react-native';

export default function TelaTermos({ navigation, route }) {
  const [acceptTerms, setAcceptTerms] = useState(false);
  const returnTo = route?.params?.returnTo;
  const userData = route?.params?.userData;

  const handleAccept = () => {
    if (!acceptTerms) {
      Alert.alert('Atenção', 'Por favor, aceite os termos para continuar');
      return;
    }
    
    if (returnTo === 'Register') {
      // Volta para o registro com os termos aceitos
      navigation.navigate('Register', { termsAccepted: true, userData });
    } else {
      Alert.alert('Sucesso', 'Termos aceitos com sucesso!');
      navigation.goBack();
    }
  };

  return (
    <SafeAreaView style={styles.container}>
      <StatusBar barStyle="light-content" backgroundColor="#E67E22" />
      <ScrollView contentContainerStyle={styles.scrollContent} showsVerticalScrollIndicator={false}>
        <View style={styles.header}>
          <Text style={styles.brandTitle}>Termos</Text>
          <Text style={styles.brandSubtitle}>de Uso</Text>
          <Text style={styles.subtitle}>Leia atentamente nossos termos e condições</Text>
        </View>

        <View style={styles.termsCard}>
          <ScrollView style={styles.termsScroll} nestedScrollEnabled={true}>
            <Text style={styles.sectionTitle}>1. Aceitação dos Termos</Text>
            <Text style={styles.paragraph}>
              Ao utilizar o Clickdesk, você concorda em cumprir estes Termos de Uso. Se não concordar com qualquer parte destes termos, não utilize a plataforma.
            </Text>

            <Text style={styles.sectionTitle}>2. Uso da Plataforma</Text>
            <Text style={styles.paragraph}>
              O Clickdesk é uma plataforma de gerenciamento de chamados. Você se compromete a usar o serviço apenas para fins legítimos e de acordo com as leis aplicáveis.
            </Text>

            <Text style={styles.sectionTitle}>3. Privacidade e Proteção de Dados (LGPD)</Text>
            <Text style={styles.paragraph}>
              <Text style={styles.bold}>Em conformidade com a Lei Geral de Proteção de Dados (LGPD - Lei nº 13.709/2018)</Text>, informamos:
            </Text>
            <Text style={styles.listItem}>• Coletamos apenas dados necessários para o funcionamento do sistema</Text>
            <Text style={styles.listItem}>• Seus dados pessoais são armazenados com segurança e criptografia</Text>
            <Text style={styles.listItem}>• Não compartilhamos seus dados com terceiros sem consentimento</Text>
            <Text style={styles.listItem}>• Você tem direito de acessar, corrigir ou excluir seus dados a qualquer momento</Text>
            <Text style={styles.listItem}>• Você pode revogar consentimentos através das configurações de perfil</Text>
            <Text style={styles.listItem}>• Mantemos seus dados pelo tempo necessário conforme legislação vigente</Text>

            <Text style={styles.sectionTitle}>4. Responsabilidades do Usuário</Text>
            <Text style={styles.paragraph}>
              Você é responsável por manter a confidencialidade de sua senha e por todas as atividades que ocorram sob sua conta.
            </Text>

            <Text style={styles.sectionTitle}>5. Limitação de Responsabilidade</Text>
            <Text style={styles.paragraph}>
              O Clickdesk não se responsabiliza por quaisquer danos diretos, indiretos, incidentais ou consequenciais decorrentes do uso ou incapacidade de usar a plataforma.
            </Text>

            <Text style={styles.sectionTitle}>6. Alterações nos Termos</Text>
            <Text style={[styles.paragraph, { marginBottom: 0 }]}>
              Reservamo-nos o direito de modificar estes termos a qualquer momento. Notificaremos os usuários sobre alterações significativas.
            </Text>
          </ScrollView>
        </View>

        <View style={styles.acceptContainer}>
          <TouchableOpacity
            style={styles.checkboxContainer}
            onPress={() => setAcceptTerms(!acceptTerms)}
            activeOpacity={0.7}
          >
            <View style={[styles.checkbox, acceptTerms && styles.checkboxChecked]}>
              {acceptTerms && <Text style={styles.checkboxIcon}>✓</Text>}
            </View>
            <Text style={styles.acceptText}>
              Li e aceito os Termos de Uso e Política de Privacidade, incluindo o tratamento de dados pessoais conforme a LGPD
            </Text>
          </TouchableOpacity>
        </View>

        <View style={styles.buttonContainer}>
          <TouchableOpacity
            style={styles.backButton}
            onPress={() => navigation.goBack()}
          >
            <Text style={styles.backButtonText}>Voltar</Text>
          </TouchableOpacity>
          <TouchableOpacity
            style={[styles.acceptButton, !acceptTerms && styles.acceptButtonDisabled]}
            onPress={handleAccept}
            disabled={!acceptTerms}
          >
            <Text style={styles.acceptButtonText}>Aceitar e Continuar</Text>
          </TouchableOpacity>
        </View>
      </ScrollView>
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#E8D5C4',
  },
  scrollContent: {
    paddingHorizontal: 16,
    paddingVertical: 24,
  },
  header: {
    alignItems: 'center',
    marginBottom: 24,
  },
  brandTitle: {
    fontSize: 48,
    fontWeight: 'bold',
    color: '#E67E22',
    letterSpacing: 2,
  },
  brandSubtitle: {
    fontSize: 36,
    fontWeight: 'bold',
    color: '#E67E22',
    letterSpacing: 2,
    marginBottom: 8,
  },
  subtitle: {
    fontSize: 14,
    color: '#7F8C8D',
    textAlign: 'center',
  },
  termsCard: {
    backgroundColor: '#F8F9FA',
    borderRadius: 12,
    padding: 16,
    marginBottom: 20,
    maxHeight: 400,
  },
  termsScroll: {
    maxHeight: 380,
  },
  sectionTitle: {
    fontSize: 16,
    fontWeight: 'bold',
    color: '#2C3E50',
    marginTop: 16,
    marginBottom: 8,
  },
  paragraph: {
    fontSize: 14,
    color: '#7F8C8D',
    lineHeight: 22,
    marginBottom: 16,
  },
  bold: {
    fontWeight: 'bold',
    color: '#2C3E50',
  },
  listItem: {
    fontSize: 14,
    color: '#7F8C8D',
    lineHeight: 24,
    marginLeft: 8,
  },
  acceptContainer: {
    backgroundColor: '#F8F9FA',
    padding: 16,
    borderRadius: 12,
    marginBottom: 20,
  },
  checkboxContainer: {
    flexDirection: 'row',
    alignItems: 'flex-start',
  },
  checkbox: {
    width: 24,
    height: 24,
    borderWidth: 2,
    borderColor: '#E1E8ED',
    borderRadius: 6,
    marginRight: 12,
    justifyContent: 'center',
    alignItems: 'center',
    marginTop: 2,
  },
  checkboxChecked: {
    backgroundColor: '#E67E22',
    borderColor: '#E67E22',
  },
  checkboxIcon: {
    color: 'white',
    fontSize: 16,
    fontWeight: 'bold',
  },
  acceptText: {
    flex: 1,
    fontSize: 13,
    color: '#2C3E50',
    lineHeight: 20,
  },
  buttonContainer: {
    flexDirection: 'row',
    gap: 12,
    justifyContent: 'center',
  },
  backButton: {
    flex: 1,
    borderWidth: 1.5,
    borderColor: '#E1E8ED',
    borderRadius: 10,
    paddingVertical: 12,
    alignItems: 'center',
  },
  backButtonText: {
    color: '#7F8C8D',
    fontSize: 15,
    fontWeight: '600',
  },
  acceptButton: {
    flex: 1,
    backgroundColor: '#E67E22',
    borderRadius: 10,
    paddingVertical: 12,
    alignItems: 'center',
    shadowColor: '#E67E22',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.3,
    shadowRadius: 4,
    elevation: 4,
  },
  acceptButtonDisabled: {
    opacity: 0.5,
  },
  acceptButtonText: {
    color: 'white',
    fontSize: 15,
    fontWeight: 'bold',
  },
});
