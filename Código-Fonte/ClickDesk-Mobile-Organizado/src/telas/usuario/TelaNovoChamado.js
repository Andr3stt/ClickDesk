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

export default function NewTicketScreen({ navigation }) {
  const [titulo, setTitulo] = useState('');
  const [categoria, setCategoria] = useState('');
  const [departamento, setDepartamento] = useState('');
  const [localizacao, setLocalizacao] = useState('');
  const [descricao, setDescricao] = useState('');

  const categorias = [
    { label: 'Hardware', value: 'hardware' },
    { label: 'Software', value: 'software' },
    { label: 'Rede', value: 'rede' },
    { label: 'Acesso', value: 'acesso' },
    { label: 'Outros', value: 'outros' },
  ];

  const departamentos = [
    { label: 'TI', value: 'ti' },
    { label: 'RH', value: 'rh' },
    { label: 'Financeiro', value: 'financeiro' },
    { label: 'Comercial', value: 'comercial' },
    { label: 'Suporte', value: 'suporte' },
  ];

  const handleSubmit = () => {
    if (!titulo || !categoria || !departamento || !descricao) {
      Alert.alert('Erro', 'Por favor, preencha todos os campos obrigatórios');
      return;
    }

    Alert.alert(
      'Sucesso',
      'Chamado criado com sucesso!',
      [{ text: 'OK', onPress: () => navigation.goBack() }]
    );
  };

  return (
    <SafeAreaView style={styles.container}>
      <StatusBar barStyle="dark-content" backgroundColor="#E8D5C4" />
      
      <View style={styles.header}>
      </View>

      <ScrollView
        style={styles.scrollView}
        contentContainerStyle={styles.scrollContent}
        showsVerticalScrollIndicator={false}
      >
        <View style={styles.card}>
          <Text style={styles.sectionTitle}>Informações do Chamado</Text>

          <View style={styles.inputContainer}>
            <Text style={styles.label}>Título do Chamado *</Text>
            <TextInput
              style={styles.input}
              placeholder="Ex: Problema com impressora"
              placeholderTextColor="#BDC3C7"
              value={titulo}
              onChangeText={setTitulo}
            />
          </View>

          <View style={styles.inputContainer}>
            <Text style={styles.label}>Categoria *</Text>
            <View style={styles.pickerContainer}>
              {categorias.map((cat) => (
                <TouchableOpacity
                  key={cat.value}
                  style={[styles.pickerItem, categoria === cat.value && styles.pickerItemActive]}
                  onPress={() => setCategoria(cat.value)}
                >
                  <Text style={[styles.pickerText, categoria === cat.value && styles.pickerTextActive]}>
                    {cat.label}
                  </Text>
                </TouchableOpacity>
              ))}
            </View>
          </View>

          <View style={styles.inputContainer}>
            <Text style={styles.label}>Departamento *</Text>
            <View style={styles.pickerContainer}>
              {departamentos.map((dept) => (
                <TouchableOpacity
                  key={dept.value}
                  style={[styles.pickerItem, departamento === dept.value && styles.pickerItemActive]}
                  onPress={() => setDepartamento(dept.value)}
                >
                  <Text style={[styles.pickerText, departamento === dept.value && styles.pickerTextActive]}>
                    {dept.label}
                  </Text>
                </TouchableOpacity>
              ))}
            </View>
          </View>

          <View style={styles.inputContainer}>
            <Text style={styles.label}>Localização</Text>
            <TextInput
              style={styles.input}
              placeholder="Ex: Andar 2, Sala 205"
              placeholderTextColor="#BDC3C7"
              value={localizacao}
              onChangeText={setLocalizacao}
            />
          </View>

          <View style={styles.inputContainer}>
            <Text style={styles.label}>Descrição Detalhada *</Text>
            <TextInput
              style={[styles.input, styles.textArea]}
              placeholder="Descreva o problema com o máximo de detalhes possível..."
              placeholderTextColor="#BDC3C7"
              value={descricao}
              onChangeText={setDescricao}
              multiline
              numberOfLines={6}
              textAlignVertical="top"
            />
          </View>

          <TouchableOpacity style={styles.attachButton}>
            <MaterialCommunityIcons name="paperclip" size={20} color="#E67E22" />
            <Text style={styles.attachButtonText}>Anexar arquivos</Text>
          </TouchableOpacity>
        </View>

        <View style={styles.buttonContainer}>
          <TouchableOpacity
            style={styles.cancelButton}
            onPress={() => navigation.goBack()}
          >
            <Text style={styles.cancelButtonText}>Cancelar</Text>
          </TouchableOpacity>
          <TouchableOpacity
            style={styles.submitButton}
            onPress={handleSubmit}
          >
            <MaterialCommunityIcons name="check" size={20} color="white" />
            <Text style={styles.submitButtonText}>Criar Chamado</Text>
          </TouchableOpacity>
        </View>
      </ScrollView>
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  container: { flex: 1, backgroundColor: '#E8D5C4' },
  header: { flexDirection: 'row', justifyContent: 'space-between', alignItems: 'center', paddingHorizontal: 20, paddingVertical: 16, backgroundColor: '#E8D5C4' },
  backButton: { padding: 4 },
  headerTitle: { fontSize: 20, fontWeight: 'bold', color: '#E67E22' },
  scrollView: { flex: 1 },
  scrollContent: { paddingHorizontal: 16, paddingBottom: 24 },
  card: { backgroundColor: 'white', borderRadius: 16, padding: 20, marginBottom: 16, shadowColor: '#000', shadowOffset: { width: 0, height: 2 }, shadowOpacity: 0.08, shadowRadius: 4, elevation: 3 },
  sectionTitle: { fontSize: 20, fontWeight: 'bold', color: '#2C3E50', marginBottom: 20 },
  inputContainer: { marginBottom: 20 },
  label: { fontSize: 14, fontWeight: '600', color: '#2C3E50', marginBottom: 8 },
  input: { backgroundColor: '#F8F9FA', borderWidth: 1.5, borderColor: '#E1E8ED', borderRadius: 10, paddingHorizontal: 16, paddingVertical: 12, fontSize: 15, color: '#2C3E50' },
  textArea: { height: 120, paddingTop: 12 },
  pickerContainer: { flexDirection: 'row', flexWrap: 'wrap', gap: 8 },
  pickerItem: { paddingHorizontal: 16, paddingVertical: 8, borderRadius: 8, backgroundColor: '#F8F9FA', borderWidth: 1.5, borderColor: '#E1E8ED' },
  pickerItemActive: { backgroundColor: '#E67E22', borderColor: '#E67E22' },
  pickerText: { fontSize: 13, fontWeight: '600', color: '#7F8C8D' },
  pickerTextActive: { color: 'white' },
  attachButton: { flexDirection: 'row', alignItems: 'center', justifyContent: 'center', paddingVertical: 12, borderRadius: 10, borderWidth: 1.5, borderColor: '#E1E8ED', backgroundColor: '#F8F9FA' },
  attachButtonText: { fontSize: 14, fontWeight: '600', color: '#E67E22', marginLeft: 8 },
  buttonContainer: { flexDirection: 'row', gap: 12, marginBottom: 16 },
  cancelButton: { flex: 1, paddingVertical: 14, borderRadius: 10, borderWidth: 1.5, borderColor: '#E1E8ED', alignItems: 'center', backgroundColor: 'white' },
  cancelButtonText: { fontSize: 15, fontWeight: '600', color: '#7F8C8D' },
  submitButton: { flex: 1, flexDirection: 'row', paddingVertical: 14, borderRadius: 10, alignItems: 'center', justifyContent: 'center', backgroundColor: '#E67E22', shadowColor: '#E67E22', shadowOffset: { width: 0, height: 2 }, shadowOpacity: 0.3, shadowRadius: 4, elevation: 4 },
  submitButtonText: { fontSize: 15, fontWeight: 'bold', color: 'white', marginLeft: 8 },
});