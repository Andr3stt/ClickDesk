import React, { useState } from 'react';
import {
  View,
  Text,
  StyleSheet,
  SafeAreaView,
  ScrollView,
  TouchableOpacity,
  StatusBar,
} from 'react-native';
import { MaterialCommunityIcons } from '@expo/vector-icons';

export default function FAQScreen({ navigation }) {
  const [expandedItems, setExpandedItems] = useState([]);

  const faqData = [
    {
      id: 1,
      question: 'Como criar um novo chamado?',
      answer: 'Para criar um novo chamado, clique no botão "Novo Chamado" no dashboard. Preencha todos os campos obrigatórios incluindo título, categoria, departamento e descrição detalhada do problema. Você também pode anexar arquivos se necessário.',
    },
    {
      id: 2,
      question: 'Esqueci minha senha, o que fazer?',
      answer: 'Na tela de login, clique em "Esqueci minha senha". Digite seu e-mail cadastrado e você receberá um link para redefinir sua senha. O link é válido por 24 horas.',
    },
    {
      id: 3,
      question: 'Como anexar arquivos ao chamado?',
      answer: 'Durante a criação do chamado, você verá uma área de "Anexar arquivos". Clique nela para selecionar arquivos do seu dispositivo. São aceitos arquivos JPG, PNG, PDF, DOC, XLS e ZIP com tamanho máximo de 10MB.',
    },
    {
      id: 4,
      question: 'Quais categorias de chamados existem?',
      answer: 'Temos 5 categorias principais: Hardware (problemas com equipamentos), Software (erros em sistemas), Rede (problemas de conexão), Acesso (permissões e logins) e Outros (demais solicitações).',
    },
    {
      id: 5,
      question: 'Como acompanhar o status do meu chamado?',
      answer: 'Acesse "Meus Chamados" no dashboard para ver todos os seus chamados e seus status. Os status possíveis são: Aberto (aguardando atendimento), Em Andamento (sendo resolvido), Aguardando (pendente de informações) e Resolvido (concluído).',
    },
    {
      id: 6,
      question: 'Quanto tempo leva para resposta?',
      answer: 'O tempo de resposta varia conforme a urgência e categoria. Chamados críticos são atendidos em até 2 horas, médios em até 8 horas e baixos em até 24 horas. Você receberá atualizações por e-mail.',
    },
    {
      id: 7,
      question: 'Posso editar um chamado após criar?',
      answer: 'Sim, você pode adicionar comentários e anexos a um chamado existente. Para editar informações básicas, abra o chamado e clique em "Adicionar comentário" para fornecer mais detalhes.',
    },
    {
      id: 8,
      question: 'Como funciona o sistema de prioridades?',
      answer: 'As prioridades são definidas automaticamente com base na categoria e descrição. Problemas críticos que afetam múltiplos usuários recebem prioridade alta, enquanto solicitações gerais têm prioridade normal.',
    },
    {
      id: 9,
      question: 'Posso deletar um chamado?',
      answer: 'Não é possível deletar chamados, mas você pode solicitar o cancelamento abrindo o chamado e selecionando "Solicitar cancelamento". Um administrador revisará e processará a solicitação.',
    },
    {
      id: 10,
      question: 'Como entrar em contato com o suporte?',
      answer: 'Além de criar chamados pelo sistema, você pode entrar em contato pelo e-mail suporte@clickdesk.com ou pelo telefone (11) 3456-7890 nos horários comerciais (8h às 18h, seg-sex).',
    },
  ];

  const toggleItem = (id) => {
    if (expandedItems.includes(id)) {
      setExpandedItems(expandedItems.filter(item => item !== id));
    } else {
      setExpandedItems([...expandedItems, id]);
    }
  };

  const expandAll = () => {
    setExpandedItems(faqData.map(item => item.id));
  };

  const collapseAll = () => {
    setExpandedItems([]);
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
          <View style={styles.titleRow}>
            <Text style={styles.cardTitle}>Central de ajuda</Text>
            <View style={styles.actionButtons}>
              <TouchableOpacity style={styles.actionButton} onPress={expandAll}>
                <Text style={styles.actionButtonText}>Expandir tudo</Text>
              </TouchableOpacity>
              <TouchableOpacity style={styles.actionButton} onPress={collapseAll}>
                <Text style={styles.actionButtonText}>Recolher tudo</Text>
              </TouchableOpacity>
            </View>
          </View>

          <View style={styles.accordion}>
            {faqData.map((item) => {
              const isExpanded = expandedItems.includes(item.id);
              return (
                <View key={item.id} style={styles.accordionItem}>
                  <TouchableOpacity
                    style={styles.questionButton}
                    onPress={() => toggleItem(item.id)}
                  >
                    <Text style={styles.questionText}>{item.question}</Text>
                    <MaterialCommunityIcons
                      name={isExpanded ? 'chevron-up' : 'chevron-down'}
                      size={24}
                      color="#E67E22"
                    />
                  </TouchableOpacity>
                  {isExpanded && (
                    <View style={styles.answerContainer}>
                      <Text style={styles.answerText}>{item.answer}</Text>
                    </View>
                  )}
                </View>
              );
            })}
          </View>
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
  card: { backgroundColor: 'white', borderRadius: 16, padding: 20, shadowColor: '#000', shadowOffset: { width: 0, height: 2 }, shadowOpacity: 0.08, shadowRadius: 4, elevation: 3 },
  titleRow: { marginBottom: 20 },
  cardTitle: { fontSize: 22, fontWeight: 'bold', color: '#2C3E50', marginBottom: 12 },
  actionButtons: { flexDirection: 'row', gap: 8 },
  actionButton: { paddingHorizontal: 12, paddingVertical: 6, borderRadius: 8, borderWidth: 1.5, borderColor: '#E1E8ED', backgroundColor: '#F8F9FA' },
  actionButtonText: { fontSize: 12, fontWeight: '600', color: '#E67E22' },
  accordion: { gap: 12 },
  accordionItem: { borderBottomWidth: 1, borderBottomColor: '#E1E8ED', paddingBottom: 12 },
  questionButton: { flexDirection: 'row', justifyContent: 'space-between', alignItems: 'center', paddingVertical: 8 },
  questionText: { flex: 1, fontSize: 15, fontWeight: '600', color: '#2C3E50', marginRight: 12 },
  answerContainer: { backgroundColor: '#F8F9FA', borderRadius: 8, padding: 16, marginTop: 8 },
  answerText: { fontSize: 14, color: '#7F8C8D', lineHeight: 22 },
});