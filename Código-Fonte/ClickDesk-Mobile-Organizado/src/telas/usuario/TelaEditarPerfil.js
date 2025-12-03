import React, { useState } from 'react';
import {
  View,
  Text,
  StyleSheet,
  SafeAreaView,
  ScrollView,
  TouchableOpacity,
  StatusBar,
  TextInput,
  Alert,
} from 'react-native';
import { MaterialCommunityIcons } from '@expo/vector-icons';

export default function EditProfileScreen({ navigation }) {
  const [nome, setNome] = useState('João Silva');
  const [email, setEmail] = useState('joao.silva@empresa.com');
  const [departamento, setDepartamento] = useState('Tecnologia');

  const [senhaAtual, setSenhaAtual] = useState('');
  const [novaSenha, setNovaSenha] = useState('');
  const [confirmaSenha, setConfirmaSenha] = useState('');

  const departamentos = ['Tecnologia', 'Suporte', 'Vendas', 'Financeiro'];

  const handleSave = () => {
    if (!nome || !email || !departamento) {
      Alert.alert('Erro', 'Nome, e-mail e departamento são obrigatórios');
      return;
    }
    Alert.alert('Sucesso', 'Perfil atualizado com sucesso!');
  };

  const handleChangePassword = () => {
    if (!senhaAtual) {
      Alert.alert('Erro', 'Informe a senha atual para alterá-la');
      return;
    }
    if (!novaSenha || !confirmaSenha) {
      Alert.alert('Erro', 'Preencha a nova senha e confirme');
      return;
    }
    if (novaSenha !== confirmaSenha) {
      Alert.alert('Erro', 'As senhas não coincidem');
      return;
    }
    if (novaSenha.length < 6) {
      Alert.alert('Erro', 'A senha deve ter no mínimo 6 caracteres');
      return;
    }
    Alert.alert('Sucesso', 'Senha atualizada com sucesso!');
    setSenhaAtual('');
    setNovaSenha('');
    setConfirmaSenha('');
  };

  return (
    <SafeAreaView style={styles.container}>
      <StatusBar barStyle="dark-content" backgroundColor="#EDE6D9" />
      
      <View style={styles.header}>
      </View>

      <ScrollView
        style={styles.scrollView}
        contentContainerStyle={styles.scrollContent}
        showsVerticalScrollIndicator={false}
      >
        {/* INFORMAÇÕES DO PERFIL */}
        <View style={styles.card}>
          <Text style={styles.cardTitle}>Informações do Perfil</Text>
          <Text style={styles.cardSubtitle}>Atualize seus dados pessoais</Text>
          
          <View style={styles.inputContainer}>
            <Text style={styles.label}>Nome Completo *</Text>
            <TextInput
              style={styles.input}
              placeholder="Seu nome completo"
              value={nome}
              onChangeText={setNome}
            />
          </View>

          <View style={styles.inputContainer}>
            <Text style={styles.label}>Email *</Text>
            <TextInput
              style={styles.input}
              placeholder="voce@empresa.com"
              value={email}
              onChangeText={setEmail}
              keyboardType="email-address"
              autoCapitalize="none"
            />
          </View>

          <View style={styles.inputContainer}>
            <Text style={styles.label}>Departamento *</Text>
            <View style={styles.pickerContainer}>
              {departamentos.map((dept) => (
                <TouchableOpacity
                  key={dept}
                  style={[styles.pickerItem, departamento === dept && styles.pickerItemActive]}
                  onPress={() => setDepartamento(dept)}
                >
                  <Text style={[styles.pickerText, departamento === dept && styles.pickerTextActive]}>
                    {dept}
                  </Text>
                </TouchableOpacity>
              ))}
            </View>
          </View>

          <TouchableOpacity style={styles.saveButton} onPress={handleSave}>
            <MaterialCommunityIcons name="check" size={20} color="white" />
            <Text style={styles.saveButtonText}>Salvar Alterações</Text>
          </TouchableOpacity>
        </View>

        {/* ALTERAR SENHA */}
        <View style={styles.card}>
          <Text style={styles.cardTitle}>Alterar Senha</Text>
          <Text style={styles.cardSubtitle}>Informe se deseja alterar sua senha</Text>
          
          <View style={styles.inputContainer}>
            <Text style={styles.label}>Senha Atual</Text>
            <TextInput
              style={styles.input}
              placeholder="Informe se deseja alterar"
              value={senhaAtual}
              onChangeText={setSenhaAtual}
              secureTextEntry
            />
          </View>

          <View style={styles.inputContainer}>
            <Text style={styles.label}>Nova Senha</Text>
            <TextInput
              style={styles.input}
              placeholder="Mínimo 6 caracteres"
              value={novaSenha}
              onChangeText={setNovaSenha}
              secureTextEntry
            />
          </View>

          <View style={styles.inputContainer}>
            <Text style={styles.label}>Confirme a Senha</Text>
            <TextInput
              style={styles.input}
              placeholder="Repita a nova senha"
              value={confirmaSenha}
              onChangeText={setConfirmaSenha}
              secureTextEntry
            />
          </View>

          <TouchableOpacity style={styles.saveButton} onPress={handleChangePassword}>
            <MaterialCommunityIcons name="lock-reset" size={20} color="white" />
            <Text style={styles.saveButtonText}>Atualizar Senha</Text>
          </TouchableOpacity>
        </View>
      </ScrollView>
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  container: { flex: 1, backgroundColor: '#EDE6D9' },
  header: { flexDirection: 'row', justifyContent: 'space-between', alignItems: 'center', paddingHorizontal: 20, paddingVertical: 16, backgroundColor: Cores.background, borderBottomWidth: 0 },
  backButton: { padding: 4 },
  headerTitle: { fontSize: 20, fontWeight: 'bold', color: '#E67E22' },
  scrollView: { flex: 1 },
  scrollContent: { paddingHorizontal: 16, paddingBottom: 24 },
  card: { backgroundColor: 'white', borderRadius: 16, padding: 20, marginBottom: 16, shadowColor: '#000', shadowOffset: { width: 0, height: 2 }, shadowOpacity: 0.08, shadowRadius: 4, elevation: 3 },
  cardTitle: { fontSize: 20, fontWeight: 'bold', color: '#2C3E50', marginBottom: 4 },
  cardSubtitle: { fontSize: 13, color: '#7F8C8D', marginBottom: 20 },
  inputContainer: { marginBottom: 16 },
  label: { fontSize: 14, fontWeight: '600', color: '#2C3E50', marginBottom: 8 },
  input: { backgroundColor: '#F8F9FA', borderWidth: 1.5, borderColor: '#E1E8ED', borderRadius: 10, paddingHorizontal: 16, paddingVertical: 12, fontSize: 15, color: '#2C3E50' },
  pickerContainer: { flexDirection: 'row', flexWrap: 'wrap', gap: 8 },
  pickerItem: { paddingHorizontal: 16, paddingVertical: 8, borderRadius: 8, backgroundColor: '#F8F9FA', borderWidth: 1.5, borderColor: '#E1E8ED' },
  pickerItemActive: { backgroundColor: '#E67E22', borderColor: '#E67E22' },
  pickerText: { fontSize: 13, fontWeight: '600', color: '#7F8C8D' },
  pickerTextActive: { color: 'white' },
  saveButton: { flexDirection: 'row', paddingVertical: 14, borderRadius: 10, alignItems: 'center', justifyContent: 'center', backgroundColor: '#E67E22', shadowColor: '#E67E22', shadowOffset: { width: 0, height: 2 }, shadowOpacity: 0.3, shadowRadius: 4, elevation: 4, marginTop: 8, gap: 8 },
  saveButtonText: { fontSize: 15, fontWeight: 'bold', color: 'white' },
});