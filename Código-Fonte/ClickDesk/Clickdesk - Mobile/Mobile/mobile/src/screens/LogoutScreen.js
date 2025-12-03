import React, { useEffect, useState } from 'react';
import {
  View,
  Text,
  StyleSheet,
  SafeAreaView,
  TouchableOpacity,
  StatusBar,
  Animated,
} from 'react-native';
import { MaterialCommunityIcons } from '@expo/vector-icons';

export default function LogoutScreen({ navigation }) {
  const [progress] = useState(new Animated.Value(0));

  useEffect(() => {
    Animated.timing(progress, {
      toValue: 1,
      duration: 2000,
      useNativeDriver: false,
    }).start();
  }, []);

  const progressWidth = progress.interpolate({
    inputRange: [0, 1],
    outputRange: ['0%', '100%'],
  });

  return (
    <SafeAreaView style={styles.container}>
      <StatusBar barStyle="dark-content" backgroundColor="#E8D5C4" />
      
      <View style={styles.content}>
        {/* Branding Section */}
        <View style={styles.brandingSection}>
          <Text style={styles.logoText}>ClickDesk</Text>
          <Text style={styles.logoSubtitle}>Sistema de Gerenciamento de Chamados</Text>
          
          <View style={styles.logoutIconContainer}>
            <MaterialCommunityIcons name="logout" size={64} color="#E67E22" />
          </View>
        </View>

        {/* Card Section */}
        <View style={styles.card}>
          <View style={styles.header}>
            <Text style={styles.title}>Saindo do sistema...</Text>
            <Text style={styles.subtitle}>Você está sendo desconectado do ClickDesk.</Text>
            <Text style={styles.thanksMessage}>Obrigado por usar nosso sistema!</Text>
          </View>

          {/* Progress Bar */}
          <View style={styles.progressContainer}>
            <View style={styles.progressBar}>
              <Animated.View 
                style={[
                  styles.progressFill,
                  { width: progressWidth }
                ]} 
              />
            </View>
            <Text style={styles.progressText}>Finalizando sessão...</Text>
          </View>

          {/* Action Buttons */}
          <View style={styles.actionButtons}>
            <TouchableOpacity
              style={styles.primaryButton}
              onPress={() => navigation.navigate('Login')}
            >
              <MaterialCommunityIcons name="login" size={18} color="white" />
              <Text style={styles.primaryButtonText}>Fazer login novamente</Text>
            </TouchableOpacity>

            <TouchableOpacity
              style={styles.outlineButton}
              onPress={() => navigation.navigate('Login')}
            >
              <MaterialCommunityIcons name="home" size={18} color="#E67E22" />
              <Text style={styles.outlineButtonText}>Ir para início</Text>
            </TouchableOpacity>
          </View>

          {/* Session Info */}
          <View style={styles.sessionInfo}>
            <View style={styles.sessionDetail}>
              <Text style={styles.sessionLabel}>Usuário: </Text>
              <Text style={styles.sessionValue}>João Silva</Text>
            </View>
            <View style={styles.sessionDetail}>
              <Text style={styles.sessionLabel}>Tempo de sessão: </Text>
              <Text style={styles.sessionValue}>2h 35min</Text>
            </View>
          </View>

          {/* Security Note */}
          <Text style={styles.securityNote}>
            Por segurança, feche todas as abas após sair.
          </Text>
        </View>
      </View>
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#E8D5C4',
  },
  content: {
    flex: 1,
    padding: 20,
  },
  brandingSection: {
    alignItems: 'center',
    paddingVertical: 30,
  },
  logoText: {
    fontSize: 32,
    fontWeight: 'bold',
    color: '#E67E22',
    marginBottom: 8,
  },
  logoSubtitle: {
    fontSize: 14,
    color: '#7F8C8D',
    marginBottom: 30,
  },
  logoutIconContainer: {
    width: 100,
    height: 100,
    borderRadius: 50,
    backgroundColor: '#E67E2220',
    alignItems: 'center',
    justifyContent: 'center',
  },
  card: {
    backgroundColor: 'white',
    borderRadius: 16,
    padding: 24,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.1,
    shadowRadius: 8,
    elevation: 5,
  },
  header: {
    alignItems: 'center',
    marginBottom: 24,
  },
  title: {
    fontSize: 24,
    fontWeight: 'bold',
    color: '#2C3E50',
    marginBottom: 8,
  },
  subtitle: {
    fontSize: 14,
    color: '#7F8C8D',
    marginBottom: 8,
    textAlign: 'center',
  },
  thanksMessage: {
    fontSize: 15,
    color: '#E67E22',
    fontWeight: '600',
    textAlign: 'center',
  },
  progressContainer: {
    marginBottom: 24,
  },
  progressBar: {
    height: 8,
    backgroundColor: '#E1E8ED',
    borderRadius: 4,
    overflow: 'hidden',
    marginBottom: 8,
  },
  progressFill: {
    height: '100%',
    backgroundColor: '#E67E22',
    borderRadius: 4,
  },
  progressText: {
    fontSize: 13,
    color: '#7F8C8D',
    textAlign: 'center',
  },
  actionButtons: {
    gap: 12,
    marginBottom: 24,
  },
  primaryButton: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    backgroundColor: '#E67E22',
    paddingVertical: 14,
    borderRadius: 10,
    gap: 8,
  },
  primaryButtonText: {
    color: 'white',
    fontSize: 15,
    fontWeight: '600',
  },
  outlineButton: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    backgroundColor: 'transparent',
    paddingVertical: 14,
    borderRadius: 10,
    borderWidth: 2,
    borderColor: '#E67E22',
    gap: 8,
  },
  outlineButtonText: {
    color: '#E67E22',
    fontSize: 15,
    fontWeight: '600',
  },
  sessionInfo: {
    backgroundColor: '#F8F9FA',
    borderRadius: 10,
    padding: 16,
    marginBottom: 16,
    gap: 8,
  },
  sessionDetail: {
    flexDirection: 'row',
  },
  sessionLabel: {
    fontSize: 14,
    color: '#7F8C8D',
  },
  sessionValue: {
    fontSize: 14,
    color: '#2C3E50',
    fontWeight: '600',
  },
  securityNote: {
    fontSize: 12,
    color: '#7F8C8D',
    textAlign: 'center',
    fontStyle: 'italic',
  },
});