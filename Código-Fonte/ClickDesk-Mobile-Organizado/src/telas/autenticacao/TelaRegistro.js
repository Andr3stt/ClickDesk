/**
 * Tela de Registro
 * 
 * Permite que novos usuários criem uma conta no ClickDesk
 */

import React, { useState } from 'react';
import {
  View,
  Text,
  TextInput,
  TouchableOpacity,
  StyleSheet,
  ScrollView,
  Alert,
  ActivityIndicator,
  KeyboardAvoidingView,
  Platform,
} from 'react-native';
import { Cores } from '../../estilos/cores';
import { registrar } from '../../servicos/api/autenticacaoService';
import { validarEmail } from '../../servicos/utilitarios/validadores';
import LogoClickDesk from '../../componentes/LogoClickDesk';

export default function TelaRegistro({ navigation }) {
  const [nome, setNome] = useState('');
  const [sobrenome, setSobrenome] = useState('');
  const [email, setEmail] = useState('');
  const [carregando, setCarregando] = useState(false);
  const [erros, setErros] = useState({});

  const validarFormulario = () => {
    const novosErros = {};

    if (!nome.trim()) {
      novosErros.nome = 'Nome é obrigatório';
    }

    if (!sobrenome.trim()) {
      novosErros.sobrenome = 'Sobrenome é obrigatório';
    }

    if (!email.trim()) {
      novosErros.email = 'Email é obrigatório';
    } else if (!validarEmail(email)) {
      novosErros.email = 'Email inválido';
    }

    setErros(novosErros);
    return Object.keys(novosErros).length === 0;
  };

  const handleRegistro = async () => {
    if (!validarFormulario()) {
      return;
    }

    setCarregando(true);

    try {
      const resultado = await registrar({
        firstName: nome,
        lastName: sobrenome,
        email: email,
        password: 'temp123', // Senha temporária - será definida via email
        userType: 'USER',
      });

      if (resultado.success) {
        Alert.alert(
          'Sucesso!',
          'Conta criada com sucesso! Verifique seu email para definir sua senha.',
          [
            {
              text: 'OK',
              onPress: () => navigation.navigate('Login'),
            },
          ]
        );
      }
    } catch (error) {
      Alert.alert(
        'Erro',
        error.mensagem || 'Não foi possível criar a conta. Tente novamente.'
      );
    } finally {
      setCarregando(false);
    }
  };

  return (
    <KeyboardAvoidingView
      style={styles.container}
      behavior={Platform.OS === 'ios' ? 'padding' : 'height'}
    >
      <ScrollView
        contentContainerStyle={styles.scrollContainer}
        keyboardShouldPersistTaps="handled"
      >
        {/* Logo e Título */}
        <View style={styles.headerContainer}>
          <LogoClickDesk size="large" showText={false} />
          <Text style={styles.titulo}>ClickDesk</Text>
          <Text style={styles.subtitulo}>Criar nova conta</Text>
        </View>

        {/* Formulário */}
        <View style={styles.formContainer}>
          {/* Campo Nome */}
          <View style={styles.inputContainer}>
            <Text style={styles.label}>Nome</Text>
            <TextInput
              style={[styles.input, erros.nome && styles.inputErro]}
              placeholder="Digite seu nome"
              placeholderTextColor={Cores.textoSecundario}
              value={nome}
              onChangeText={(text) => {
                setNome(text);
                if (erros.nome) {
                  setErros({ ...erros, nome: null });
                }
              }}
              autoCapitalize="words"
              autoCorrect={false}
            />
            {erros.nome && (
              <Text style={styles.textoErro}>{erros.nome}</Text>
            )}
          </View>

          {/* Campo Sobrenome */}
          <View style={styles.inputContainer}>
            <Text style={styles.label}>Sobrenome</Text>
            <TextInput
              style={[styles.input, erros.sobrenome && styles.inputErro]}
              placeholder="Digite seu sobrenome"
              placeholderTextColor={Cores.textoSecundario}
              value={sobrenome}
              onChangeText={(text) => {
                setSobrenome(text);
                if (erros.sobrenome) {
                  setErros({ ...erros, sobrenome: null });
                }
              }}
              autoCapitalize="words"
              autoCorrect={false}
            />
            {erros.sobrenome && (
              <Text style={styles.textoErro}>{erros.sobrenome}</Text>
            )}
          </View>

          {/* Campo Email */}
          <View style={styles.inputContainer}>
            <Text style={styles.label}>Email</Text>
            <TextInput
              style={[styles.input, erros.email && styles.inputErro]}
              placeholder="Digite seu email"
              placeholderTextColor={Cores.textoSecundario}
              value={email}
              onChangeText={(text) => {
                setEmail(text);
                if (erros.email) {
                  setErros({ ...erros, email: null });
                }
              }}
              keyboardType="email-address"
              autoCapitalize="none"
              autoCorrect={false}
            />
            {erros.email && (
              <Text style={styles.textoErro}>{erros.email}</Text>
            )}
          </View>

          {/* Botão Registrar */}
          <TouchableOpacity
            style={[styles.botao, carregando && styles.botaoDesabilitado]}
            onPress={handleRegistro}
            disabled={carregando}
          >
            {carregando ? (
              <ActivityIndicator color={Cores.branco} />
            ) : (
              <Text style={styles.botaoTexto}>Criar Conta</Text>
            )}
          </TouchableOpacity>

          {/* Link para Login */}
          <View style={styles.rodapeContainer}>
            <Text style={styles.textoRodape}>Já tem uma conta? </Text>
            <TouchableOpacity onPress={() => navigation.navigate('Login')}>
              <Text style={styles.linkRodape}>Fazer Login</Text>
            </TouchableOpacity>
          </View>
        </View>
      </ScrollView>
    </KeyboardAvoidingView>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: Cores.background,
  },
  scrollContainer: {
    flexGrow: 1,
    justifyContent: 'center',
    padding: 24,
  },
  headerContainer: {
    alignItems: 'center',
    marginBottom: 40,
  },
  titulo: {
    fontSize: 32,
    fontWeight: 'bold',
    color: Cores.brand,
    marginTop: 16,
    marginBottom: 8,
  },
  subtitulo: {
    fontSize: 16,
    color: Cores.textoSecundario,
  },
  formContainer: {
    width: '100%',
  },
  inputContainer: {
    marginBottom: 20,
  },
  label: {
    fontSize: 14,
    fontWeight: '600',
    color: Cores.textoPrincipal,
    marginBottom: 8,
  },
  input: {
    backgroundColor: Cores.branco,
    borderWidth: 1,
    borderColor: Cores.bordaPadrao,
    borderRadius: 8,
    padding: 14,
    fontSize: 16,
    color: Cores.textoPrincipal,
  },
  inputErro: {
    borderColor: Cores.danger,
  },
  textoErro: {
    fontSize: 12,
    color: Cores.danger,
    marginTop: 4,
  },
  botao: {
    backgroundColor: Cores.brand,
    borderRadius: 8,
    padding: 16,
    alignItems: 'center',
    justifyContent: 'center',
    marginTop: 12,
    shadowColor: Cores.preto,
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.2,
    shadowRadius: 4,
    elevation: 3,
  },
  botaoDesabilitado: {
    opacity: 0.6,
  },
  botaoTexto: {
    color: Cores.branco,
    fontSize: 16,
    fontWeight: 'bold',
  },
  rodapeContainer: {
    flexDirection: 'row',
    justifyContent: 'center',
    alignItems: 'center',
    marginTop: 24,
  },
  textoRodape: {
    fontSize: 14,
    color: Cores.textoSecundario,
  },
  linkRodape: {
    fontSize: 14,
    color: Cores.brand,
    fontWeight: '600',
  },
});
