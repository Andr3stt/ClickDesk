import React, { useState } from 'react';
import {
  View,
  Text,
  StyleSheet,
  SafeAreaView,
  ScrollView,
  TouchableOpacity,
  StatusBar,
  FlatList,
  Modal,
  TextInput,
  Alert,
} from 'react-native';
import { MaterialCommunityIcons } from '@expo/vector-icons';

export default function TicketApprovalScreen({ navigation }) {
  const [statusFilter, setStatusFilter] = useState('pendente');
  const [categoryFilter, setCategoryFilter] = useState('todas');
  const [modalVisible, setModalVisible] = useState(false);
  const [selectedTicketId, setSelectedTicketId] = useState(null);
  const [rejectionReason, setRejectionReason] = useState('');
  const [tickets, setTickets] = useState([
    { id: 'CD-145', title: 'Problema com impressora HP', category: 'Hardware', priority: 'Alta', requester: 'João Silva', date: 'Hoje • 14:30' },
    { id: 'CD-144', title: 'Acesso negado ao sistema', category: 'Acesso', priority: 'Média', requester: 'Maria Santos', date: 'Hoje • 13:15' },
    { id: 'CD-143', title: 'Instalação de software', category: 'Software', priority: 'Baixa', requester: 'Pedro Costa', date: 'Hoje • 11:45' },
    { id: 'CD-142', title: 'Conexão de rede instável', category: 'Rede', priority: 'Alta', requester: 'Ana Lima', date: 'Ontem • 16:20' },
    { id: 'CD-141', title: 'Erro no e-mail corporativo', category: 'E-mail', priority: 'Média', requester: 'Carlos Souza', date: 'Ontem • 15:00' },
  ]);

  const handleApprove = (ticketId) => {
    Alert.alert(
      'Aprovar Chamado',
      `Deseja aprovar o chamado ${ticketId}?`,
      [
        { text: 'Cancelar', style: 'cancel' },
        {
          text: 'Aprovar',
          onPress: () => {
            setTickets(tickets.filter(t => t.id !== ticketId));
            Alert.alert('Sucesso', 'Chamado aprovado com sucesso!');
          }
        }
      ]
    );
  };

  const handleReject = (ticketId) => {
    setSelectedTicketId(ticketId);
    setModalVisible(true);
  };

  const confirmRejection = () => {
    if (!rejectionReason.trim()) {
      Alert.alert('Atenção', 'Por favor, informe o motivo da rejeição.');
      return;
    }
    setTickets(tickets.filter(t => t.id !== selectedTicketId));
    setModalVisible(false);
    setRejectionReason('');
    Alert.alert('Rejeitado', `Chamado ${selectedTicketId} foi rejeitado.`);
  };

  const getPriorityColor = (priority) => {
    switch (priority) {
      case 'Alta': return '#E74C3C';
      case 'Média': return '#E67E22';
      case 'Baixa': return '#3498DB';
      default: return '#95A5A6';
    }
  };

  const renderTicket = ({ item }) => (
    <View style={styles.ticketCard}>
      <View style={styles.ticketHeader}>
        <Text style={styles.ticketId}>{item.id}</Text>
        <View style={[styles.priorityBadge, { backgroundColor: getPriorityColor(item.priority) + '20' }]}>
          <Text style={[styles.priorityText, { color: getPriorityColor(item.priority) }]}>
            {item.priority}
          </Text>
        </View>
      </View>
      <Text style={styles.ticketTitle}>{item.title}</Text>
      <View style={styles.ticketInfo}>
        <View style={styles.categoryBadge}>
          <MaterialCommunityIcons name="tag" size={14} color="#E67E22" />
          <Text style={styles.categoryText}>{item.category}</Text>
        </View>
        <Text style={styles.requesterText}>
          <MaterialCommunityIcons name="account" size={14} color="#7F8C8D" /> {item.requester}
        </Text>
      </View>
      <Text style={styles.dateText}>
        <MaterialCommunityIcons name="clock-outline" size={14} color="#7F8C8D" /> {item.date}
      </Text>
      <View style={styles.actionButtons}>
        <TouchableOpacity
          style={[styles.actionButton, styles.rejectButton]}
          onPress={() => handleReject(item.id)}
        >
          <MaterialCommunityIcons name="close" size={18} color="white" />
          <Text style={styles.actionButtonText}>Rejeitar</Text>
        </TouchableOpacity>
        <TouchableOpacity
          style={[styles.actionButton, styles.approveButton]}
          onPress={() => handleApprove(item.id)}
        >
          <MaterialCommunityIcons name="check" size={18} color="white" />
          <Text style={styles.actionButtonText}>Aprovar</Text>
        </TouchableOpacity>
      </View>
    </View>
  );

  return (
    <SafeAreaView style={styles.container}>
      <StatusBar barStyle="dark-content" backgroundColor="#E8D5C4" />
      
      <View style={styles.header}>
        <TouchableOpacity onPress={() => navigation.goBack()} style={styles.backButton}>
          <MaterialCommunityIcons name="arrow-left" size={24} color="#2C3E50" />
        </TouchableOpacity>
        <Text style={styles.headerTitle}>Aprovação de Chamados</Text>
        <View style={{ width: 24 }} />
      </View>

      <View style={styles.filtersCard}>
        <View style={styles.filterRow}>
          <View style={styles.filterGroup}>
            <Text style={styles.filterLabel}>Status:</Text>
            <View style={styles.filterChips}>
              {['pendente', 'todos'].map(status => (
                <TouchableOpacity
                  key={status}
                  style={[styles.chip, statusFilter === status && styles.chipActive]}
                  onPress={() => setStatusFilter(status)}
                >
                  <Text style={[styles.chipText, statusFilter === status && styles.chipTextActive]}>
                    {status === 'pendente' ? 'Pendentes' : 'Todos'}
                  </Text>
                </TouchableOpacity>
              ))}
            </View>
          </View>
        </View>
        <View style={styles.filterRow}>
          <View style={styles.filterGroup}>
            <Text style={styles.filterLabel}>Categoria:</Text>
            <ScrollView horizontal showsHorizontalScrollIndicator={false}>
              <View style={styles.filterChips}>
                {['todas', 'Hardware', 'Software', 'Rede', 'Acesso'].map(cat => (
                  <TouchableOpacity
                    key={cat}
                    style={[styles.chip, categoryFilter === cat && styles.chipActive]}
                    onPress={() => setCategoryFilter(cat)}
                  >
                    <Text style={[styles.chipText, categoryFilter === cat && styles.chipTextActive]}>
                      {cat === 'todas' ? 'Todas' : cat}
                    </Text>
                  </TouchableOpacity>
                ))}
              </View>
            </ScrollView>
          </View>
        </View>
      </View>

      {tickets.length === 0 ? (
        <View style={styles.emptyState}>
          <MaterialCommunityIcons name="check-circle-outline" size={64} color="#95A5A6" />
          <Text style={styles.emptyTitle}>Nenhum chamado pendente</Text>
          <Text style={styles.emptySubtitle}>Todos os chamados foram processados</Text>
        </View>
      ) : (
        <FlatList
          data={tickets}
          renderItem={renderTicket}
          keyExtractor={item => item.id}
          contentContainerStyle={styles.listContent}
          showsVerticalScrollIndicator={false}
        />
      )}

      <Modal visible={modalVisible} transparent animationType="fade">
        <View style={styles.modalOverlay}>
          <View style={styles.modalContent}>
            <Text style={styles.modalTitle}>Motivo da Rejeição</Text>
            <TextInput
              style={styles.modalInput}
              placeholder="Informe o motivo..."
              placeholderTextColor="#95A5A6"
              multiline
              numberOfLines={4}
              value={rejectionReason}
              onChangeText={setRejectionReason}
            />
            <View style={styles.modalButtons}>
              <TouchableOpacity
                style={[styles.modalButton, styles.modalCancelButton]}
                onPress={() => {
                  setModalVisible(false);
                  setRejectionReason('');
                }}
              >
                <Text style={styles.modalCancelText}>Cancelar</Text>
              </TouchableOpacity>
              <TouchableOpacity
                style={[styles.modalButton, styles.modalConfirmButton]}
                onPress={confirmRejection}
              >
                <Text style={styles.modalConfirmText}>Confirmar</Text>
              </TouchableOpacity>
            </View>
          </View>
        </View>
      </Modal>
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  container: { flex: 1, backgroundColor: '#E8D5C4' },
  header: { flexDirection: 'row', justifyContent: 'space-between', alignItems: 'center', paddingHorizontal: 20, paddingVertical: 16, backgroundColor: '#E8D5C4' },
  backButton: { padding: 4 },
  headerTitle: { fontSize: 18, fontWeight: 'bold', color: '#2C3E50' },
  filtersCard: { backgroundColor: 'white', marginHorizontal: 16, marginBottom: 16, padding: 16, borderRadius: 12, shadowColor: '#000', shadowOffset: { width: 0, height: 2 }, shadowOpacity: 0.05, shadowRadius: 4, elevation: 3 },
  filterRow: { marginBottom: 12 },
  filterGroup: { flex: 1 },
  filterLabel: { fontSize: 13, fontWeight: '600', color: '#2C3E50', marginBottom: 8 },
  filterChips: { flexDirection: 'row', gap: 8 },
  chip: { paddingHorizontal: 14, paddingVertical: 8, borderRadius: 8, backgroundColor: '#F8F9FA', borderWidth: 1.5, borderColor: '#E1E8ED' },
  chipActive: { backgroundColor: '#E67E22', borderColor: '#E67E22' },
  chipText: { fontSize: 12, fontWeight: '600', color: '#7F8C8D' },
  chipTextActive: { color: 'white' },
  listContent: { paddingHorizontal: 16, paddingBottom: 16 },
  ticketCard: { backgroundColor: 'white', borderRadius: 12, padding: 16, marginBottom: 12, shadowColor: '#000', shadowOffset: { width: 0, height: 1 }, shadowOpacity: 0.05, shadowRadius: 2, elevation: 2 },
  ticketHeader: { flexDirection: 'row', justifyContent: 'space-between', alignItems: 'center', marginBottom: 8 },
  ticketId: { fontSize: 14, fontWeight: '600', color: '#7F8C8D' },
  priorityBadge: { paddingHorizontal: 10, paddingVertical: 4, borderRadius: 8 },
  priorityText: { fontSize: 11, fontWeight: '600' },
  ticketTitle: { fontSize: 15, fontWeight: '600', color: '#2C3E50', marginBottom: 10 },
  ticketInfo: { flexDirection: 'row', justifyContent: 'space-between', marginBottom: 6 },
  categoryBadge: { flexDirection: 'row', alignItems: 'center', gap: 4 },
  categoryText: { fontSize: 12, color: '#E67E22', fontWeight: '600' },
  requesterText: { fontSize: 12, color: '#7F8C8D' },
  dateText: { fontSize: 12, color: '#7F8C8D', marginBottom: 12 },
  actionButtons: { flexDirection: 'row', gap: 10 },
  actionButton: { flex: 1, flexDirection: 'row', alignItems: 'center', justifyContent: 'center', paddingVertical: 10, borderRadius: 8, gap: 6 },
  approveButton: { backgroundColor: '#34A853' },
  rejectButton: { backgroundColor: '#E74C3C' },
  actionButtonText: { color: 'white', fontSize: 13, fontWeight: '600' },
  emptyState: { flex: 1, justifyContent: 'center', alignItems: 'center', paddingHorizontal: 40 },
  emptyTitle: { fontSize: 18, fontWeight: 'bold', color: '#2C3E50', marginTop: 16 },
  emptySubtitle: { fontSize: 14, color: '#7F8C8D', marginTop: 8, textAlign: 'center' },
  modalOverlay: { flex: 1, backgroundColor: 'rgba(0,0,0,0.5)', justifyContent: 'center', alignItems: 'center', paddingHorizontal: 24 },
  modalContent: { width: '100%', backgroundColor: 'white', borderRadius: 16, padding: 24 },
  modalTitle: { fontSize: 18, fontWeight: 'bold', color: '#2C3E50', marginBottom: 16 },
  modalInput: { backgroundColor: '#F8F9FA', borderRadius: 10, padding: 12, minHeight: 100, textAlignVertical: 'top', fontSize: 14, color: '#2C3E50', borderWidth: 1.5, borderColor: '#E1E8ED', marginBottom: 20 },
  modalButtons: { flexDirection: 'row', gap: 12 },
  modalButton: { flex: 1, paddingVertical: 12, borderRadius: 10, alignItems: 'center' },
  modalCancelButton: { backgroundColor: '#F8F9FA', borderWidth: 1.5, borderColor: '#E1E8ED' },
  modalConfirmButton: { backgroundColor: '#E74C3C' },
  modalCancelText: { fontSize: 14, fontWeight: '600', color: '#7F8C8D' },
  modalConfirmText: { fontSize: 14, fontWeight: '600', color: 'white' },
});