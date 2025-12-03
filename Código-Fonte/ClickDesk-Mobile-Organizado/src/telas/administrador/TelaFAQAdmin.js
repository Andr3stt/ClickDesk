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

const FAQ_DATA = [
  {
    id: 1,
    question: 'Como aprovar um chamado?',
    answer: 'Acesse "Aprovação de Chamados" no menu principal, revise os detalhes do chamado e clique no botão "Aprovar". Você pode adicionar comentários antes de confirmar a aprovação.',
  },
  {
    id: 2,
    question: 'Como atribuir prioridades aos chamados?',
    answer: 'As prioridades são definidas com base em: Alta (problemas críticos que impedem o trabalho), Média (problemas que afetam produtividade) e Baixa (solicitações sem urgência). Clique no badge de prioridade para alterar.',
  },
  {
    id: 3,
    question: 'Como gerenciar múltiplos chamados simultaneamente?',
    answer: 'Use os filtros por status e categoria na tela "Meus Chamados" para organizar e priorizar seu trabalho. Você pode atualizar o status de cada chamado diretamente na lista.',
  },
  {
    id: 4,
    question: 'Qual o SLA para cada nível de prioridade?',
    answer: 'Alta: 2 horas para primeiro atendimento, 8 horas para resolução\nMédia: 4 horas para primeiro atendimento, 24 horas para resolução\nBaixa: 8 horas para primeiro atendimento, 72 horas para resolução',
  },
  {
    id: 5,
    question: 'Como reatribuir um chamado para outro técnico?',
    answer: 'Abra os detalhes do chamado, clique em "Reatribuir" no menu de ações e selecione o técnico desejado. O técnico receberá uma notificação imediata.',
  },
  {
    id: 6,
    question: 'Como adicionar notas internas ao chamado?',
    answer: 'Nos detalhes do chamado, use a seção "Notas Internas" para adicionar comentários visíveis apenas para a equipe técnica. Útil para documentar tentativas de solução.',
  },
  {
    id: 7,
    question: 'Como rejeitar um chamado?',
    answer: 'Na tela de aprovação, clique em "Rejeitar" e informe o motivo da rejeição. O solicitante receberá uma notificação com sua justificativa.',
  },
  {
    id: 8,
    question: 'Onde visualizar métricas de desempenho?',
    answer: 'Acesse o Dashboard Técnico para ver estatísticas como: total de chamados resolvidos, tempo médio de resolução, taxa de aprovação e chamados pendentes.',
  },
  {
    id: 9,
    question: 'Como configurar notificações?',
    answer: 'Vá em "Editar Perfil" > "Notificações" e escolha quais eventos você deseja receber alertas: novos chamados, atualizações, escalações, etc.',
  },
  {
    id: 10,
    question: 'Como escalar um chamado?',
    answer: 'Se um chamado exigir conhecimento especializado, abra os detalhes e clique em "Escalar". Selecione o nível (Nível 2 ou Gerência) e adicione contexto sobre o problema.',
  },
  {
    id: 11,
    question: 'Quem contatar para problemas técnicos?',
    answer: 'Para problemas com o sistema ClickDesk, entre em contato com:\nE-mail: suporte@clickdesk.com\nTelefone: (11) 3000-0000\nHorário: Segunda a sexta, 8h às 18h',
  },
  {
    id: 12,
    question: 'Como acessar histórico completo de um chamado?',
    answer: 'Nos detalhes do chamado, role até a seção "Histórico e Logs" para ver todas as ações realizadas, alterações de status, reatribuições e comentários em ordem cronológica.',
  },
];

export default function FAQAdminScreen({ navigation }) {
  const [expandedItems, setExpandedItems] = useState([]);

  const toggleItem = (id) => {
    setExpandedItems(prev =>
      prev.includes(id) ? prev.filter(item => item !== id) : [...prev, id]
    );
  };

  const expandAll = () => {
    setExpandedItems(FAQ_DATA.map(item => item.id));
  };

  const collapseAll = () => {
    setExpandedItems([]);
  };

  return (
    <SafeAreaView style={styles.container}>
      <StatusBar barStyle="dark-content" backgroundColor="#EDE6D9" />
      
      <View style={styles.header}>
        <TouchableOpacity onPress={() => navigation.goBack()} style={styles.backButton}>
          <MaterialCommunityIcons name="arrow-left" size={24} color="#2C3E50" />
        </TouchableOpacity>
        <Text style={styles.headerTitle}>FAQ - Técnico</Text>
        <View style={{ width: 24 }} />
      </View>

      <View style={styles.actionsBar}>
        <TouchableOpacity style={styles.actionButton} onPress={expandAll}>
          <MaterialCommunityIcons name="chevron-down" size={18} color="#E67E22" />
          <Text style={styles.actionButtonText}>Expandir tudo</Text>
        </TouchableOpacity>
        <TouchableOpacity style={styles.actionButton} onPress={collapseAll}>
          <MaterialCommunityIcons name="chevron-up" size={18} color="#E67E22" />
          <Text style={styles.actionButtonText}>Recolher tudo</Text>
        </TouchableOpacity>
      </View>

      <ScrollView
        style={styles.scrollView}
        contentContainerStyle={styles.scrollContent}
        showsVerticalScrollIndicator={false}
      >
        {FAQ_DATA.map((item, index) => {
          const isExpanded = expandedItems.includes(item.id);
          return (
            <View key={item.id} style={styles.faqItem}>
              <TouchableOpacity
                style={styles.questionContainer}
                onPress={() => toggleItem(item.id)}
              >
                <View style={styles.questionContent}>
                  <View style={styles.numberBadge}>
                    <Text style={styles.numberText}>{index + 1}</Text>
                  </View>
                  <Text style={styles.questionText}>{item.question}</Text>
                </View>
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

        <View style={styles.footer}>
          <MaterialCommunityIcons name="help-circle" size={32} color="#E67E22" />
          <Text style={styles.footerTitle}>Precisa de mais ajuda?</Text>
          <Text style={styles.footerText}>
            Entre em contato com o suporte técnico em{'\n'}
            suporte@clickdesk.com
          </Text>
        </View>
      </ScrollView>
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  container: { flex: 1, backgroundColor: '#EDE6D9' },
  header: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    paddingHorizontal: 20,
    paddingVertical: 16,
    backgroundColor: '#EDE6D9',
  },
  backButton: { padding: 4 },
  headerTitle: { fontSize: 18, fontWeight: 'bold', color: '#2C3E50' },
  actionsBar: {
    flexDirection: 'row',
    justifyContent: 'space-around',
    paddingHorizontal: 16,
    paddingVertical: 12,
    backgroundColor: 'white',
    marginHorizontal: 16,
    marginBottom: 16,
    borderRadius: 12,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.05,
    shadowRadius: 2,
    elevation: 2,
  },
  actionButton: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 6,
    paddingHorizontal: 16,
    paddingVertical: 8,
  },
  actionButtonText: { fontSize: 13, fontWeight: '600', color: '#E67E22' },
  scrollView: { flex: 1 },
  scrollContent: { paddingHorizontal: 16, paddingBottom: 24 },
  faqItem: {
    backgroundColor: 'white',
    borderRadius: 12,
    marginBottom: 12,
    overflow: 'hidden',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.05,
    shadowRadius: 2,
    elevation: 2,
  },
  questionContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    padding: 16,
  },
  questionContent: {
    flex: 1,
    flexDirection: 'row',
    alignItems: 'center',
    gap: 12,
  },
  numberBadge: {
    width: 28,
    height: 28,
    borderRadius: 14,
    backgroundColor: '#E67E22',
    alignItems: 'center',
    justifyContent: 'center',
  },
  numberText: {
    color: 'white',
    fontSize: 13,
    fontWeight: 'bold',
  },
  questionText: {
    flex: 1,
    fontSize: 15,
    fontWeight: '600',
    color: '#2C3E50',
  },
  answerContainer: {
    paddingHorizontal: 16,
    paddingBottom: 16,
    paddingTop: 0,
    borderTopWidth: 1,
    borderTopColor: '#E1E8ED',
  },
  answerText: {
    fontSize: 14,
    color: '#7F8C8D',
    lineHeight: 22,
    marginTop: 12,
    marginLeft: 40,
  },
  footer: {
    backgroundColor: 'white',
    borderRadius: 12,
    padding: 24,
    alignItems: 'center',
    marginTop: 12,
  },
  footerTitle: {
    fontSize: 16,
    fontWeight: 'bold',
    color: '#2C3E50',
    marginTop: 12,
    marginBottom: 8,
  },
  footerText: {
    fontSize: 13,
    color: '#7F8C8D',
    textAlign: 'center',
    lineHeight: 20,
  },
});
