import React, { useState } from 'react';
import {
  View,
  Text,
  StyleSheet,
  SafeAreaView,
  FlatList,
  TouchableOpacity,
  StatusBar,
  TextInput,
  Alert,
} from 'react-native';
import { MaterialCommunityIcons } from '@expo/vector-icons';

export default function MyTicketsAdminScreen({ navigation }) {
  const [statusFilter, setStatusFilter] = useState('todos');
  const [categoryFilter, setCategoryFilter] = useState('todas');
  const [searchQuery, setSearchQuery] = useState('');
  const [tickets, setTickets] = useState([
    { id: 'CD-145', title: 'Problema com impressora HP', category: 'Hardware', status: 'aberto', date: 'Hoje • 14:30', requester: 'João Silva' },
    { id: 'CD-144', title: 'Acesso negado ao sistema', category: 'Acesso', status: 'em-andamento', date: 'Hoje • 13:15', requester: 'Maria Santos' },
    { id: 'CD-143', title: 'Instalação de software', category: 'Software', status: 'aguardando', date: 'Hoje • 11:45', requester: 'Pedro Costa' },
    { id: 'CD-142', title: 'Conexão de rede instável', category: 'Rede', status: 'em-andamento', date: 'Ontem • 16:20', requester: 'Ana Lima' },
    { id: 'CD-141', title: 'Erro no e-mail corporativo', category: 'E-mail', status: 'aberto', date: 'Ontem • 15:00', requester: 'Carlos Souza' },
    { id: 'CD-140', title: 'Backup não realizado', category: 'Software', status: 'resolvido', date: '2 dias • 10:30', requester: 'Juliana Rocha' },
  ]);

  const statusOptions = [
    { value: 'todos', label: 'Todos' },
    { value: 'aberto', label: 'Aberto' },
    { value: 'em-andamento', label: 'Em Andamento' },
    { value: 'aguardando', label: 'Aguardando' },
    { value: 'resolvido', label: 'Resolvido' },
  ];

  const getStatusColor = (status) => {
    switch (status) {
      case 'aberto': return '#FBBC04';
      case 'em-andamento': return '#4285F4';
      case 'aguardando': return '#9E9E9E';
      case 'resolvido': return '#34A853';
      default: return '#95A5A6';
    }
  };

  const getStatusLabel = (status) => {
    const option = statusOptions.find(opt => opt.value === status);
    return option ? option.label : status;
  };

  const handleStatusChange = (ticketId, newStatus) => {
    Alert.alert(
      'Atualizar Status',
      `Alterar status do chamado ${ticketId} para "${getStatusLabel(newStatus)}"?`,
      [
        { text: 'Cancelar', style: 'cancel' },
        {
          text: 'Confirmar',
          onPress: () => {
            setTickets(tickets.map(t => t.id === ticketId ? { ...t, status: newStatus } : t));
            Alert.alert('Sucesso', 'Status atualizado!');
          }
        }
      ]
    );
  };

  const filteredTickets = tickets.filter(ticket => {
    const matchesStatus = statusFilter === 'todos' || ticket.status === statusFilter;
    const matchesCategory = categoryFilter === 'todas' || ticket.category === categoryFilter;
    const matchesSearch = ticket.title.toLowerCase().includes(searchQuery.toLowerCase()) || 
                         ticket.id.toLowerCase().includes(searchQuery.toLowerCase());
    return matchesStatus && matchesCategory && matchesSearch;
  });

  const renderTicket = ({ item }) => (
    <TouchableOpacity
      style={styles.ticketCard}
      onPress={() => navigation.navigate('TicketDetails', { ticketId: item.id })}
    >
      <View style={styles.ticketHeader}>
        <Text style={styles.ticketId}>{item.id}</Text>
        <TouchableOpacity
          style={[styles.statusBadge, { backgroundColor: getStatusColor(item.status) + '20' }]}
          onPress={() => {
            Alert.alert(
              'Alterar Status',
              'Escolha o novo status:',
              statusOptions
                .filter(opt => opt.value !== 'todos' && opt.value !== item.status)
                .map(opt => ({
                  text: opt.label,
                  onPress: () => handleStatusChange(item.id, opt.value)
                }))
                .concat([{ text: 'Cancelar', style: 'cancel' }])
            );
          }}
        >
          <Text style={[styles.statusText, { color: getStatusColor(item.status) }]}>
            {getStatusLabel(item.status)}
          </Text>
          <MaterialCommunityIcons name="chevron-down" size={14} color={getStatusColor(item.status)} />
        </TouchableOpacity>
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
    </TouchableOpacity>
  );

  return (
    <SafeAreaView style={styles.container}>
      <StatusBar barStyle="dark-content" backgroundColor="#E8D5C4" />
      
      <View style={styles.header}>
        <TouchableOpacity onPress={() => navigation.goBack()} style={styles.backButton}>
          <MaterialCommunityIcons name="arrow-left" size={24} color="#2C3E50" />
        </TouchableOpacity>
        <View style={styles.headerTextContainer}>
          <Text style={styles.headerTitle}>Meus Chamados</Text>
          <Text style={styles.headerSubtitle}>(Técnico)</Text>
        </View>
        <TouchableOpacity style={styles.refreshButton}>
          <MaterialCommunityIcons name="refresh" size={24} color="#E67E22" />
        </TouchableOpacity>
      </View>

      <View style={styles.filtersCard}>
        <View style={styles.searchContainer}>
          <MaterialCommunityIcons name="magnify" size={20} color="#7F8C8D" style={styles.searchIcon} />
          <TextInput
            style={styles.searchInput}
            placeholder="Buscar por título ou ID..."
            placeholderTextColor="#95A5A6"
            value={searchQuery}
            onChangeText={setSearchQuery}
          />
        </View>
        
        <View style={styles.filterRow}>
          <Text style={styles.filterLabel}>Status:</Text>
          <View style={styles.filterChips}>
            {statusOptions.map(option => (
              <TouchableOpacity
                key={option.value}
                style={[styles.chip, statusFilter === option.value && styles.chipActive]}
                onPress={() => setStatusFilter(option.value)}
              >
                <Text style={[styles.chipText, statusFilter === option.value && styles.chipTextActive]}>
                  {option.label}
                </Text>
              </TouchableOpacity>
            ))}
          </View>
        </View>
        
        <View style={styles.filterRow}>
          <Text style={styles.filterLabel}>Categoria:</Text>
          <View style={styles.filterChips}>
            {['todas', 'Hardware', 'Software', 'Rede', 'Acesso', 'E-mail'].map(cat => (
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
        </View>
      </View>

      <View style={styles.kpisContainer}>
        <View style={styles.kpiCard}>
          <Text style={styles.kpiValue}>15</Text>
          <Text style={styles.kpiLabel}>Atribuídos</Text>
        </View>
        <View style={styles.kpiCard}>
          <Text style={styles.kpiValue}>45</Text>
          <Text style={styles.kpiLabel}>Concluídos</Text>
        </View>
        <View style={styles.kpiCard}>
          <Text style={styles.kpiValue}>8</Text>
          <Text style={styles.kpiLabel}>Pendentes</Text>
        </View>
        <View style={styles.kpiCard}>
          <Text style={[styles.kpiValue, { color: '#E74C3C' }]}>2</Text>
          <Text style={styles.kpiLabel}>Vencidos</Text>
        </View>
      </View>

      {filteredTickets.length === 0 ? (
        <View style={styles.emptyState}>
          <MaterialCommunityIcons name="file-document-outline" size={64} color="#95A5A6" />
          <Text style={styles.emptyTitle}>Nenhum chamado encontrado</Text>
          <Text style={styles.emptySubtitle}>Tente ajustar os filtros</Text>
        </View>
      ) : (
        <FlatList
          data={filteredTickets}
          renderItem={renderTicket}
          keyExtractor={item => item.id}
          contentContainerStyle={styles.listContent}
          showsVerticalScrollIndicator={false}
        />
      )}
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  container: { flex: 1, backgroundColor: '#E8D5C4' },
  header: { flexDirection: 'row', justifyContent: 'space-between', alignItems: 'center', paddingHorizontal: 20, paddingVertical: 16, backgroundColor: '#E8D5C4' },
  backButton: { padding: 4 },
  headerTextContainer: { flex: 1, alignItems: 'center' },
  headerTitle: { fontSize: 18, fontWeight: 'bold', color: '#2C3E50' },
  headerSubtitle: { fontSize: 12, color: '#E67E22', fontWeight: '600' },
  refreshButton: { padding: 4 },
  filtersCard: { backgroundColor: 'white', marginHorizontal: 16, marginBottom: 16, padding: 16, borderRadius: 12, shadowColor: '#000', shadowOffset: { width: 0, height: 2 }, shadowOpacity: 0.05, shadowRadius: 4, elevation: 3 },
  searchContainer: { position: 'relative', marginBottom: 16 },
  searchIcon: { position: 'absolute', left: 16, top: 14, zIndex: 1 },
  searchInput: { backgroundColor: '#F8F9FA', borderRadius: 10, paddingLeft: 44, paddingRight: 16, paddingVertical: 12, fontSize: 14, color: '#2C3E50', borderWidth: 1.5, borderColor: '#E1E8ED' },
  filterRow: { marginBottom: 12 },
  filterLabel: { fontSize: 13, fontWeight: '600', color: '#2C3E50', marginBottom: 8 },
  filterChips: { flexDirection: 'row', flexWrap: 'wrap', gap: 8 },
  chip: { paddingHorizontal: 12, paddingVertical: 6, borderRadius: 8, backgroundColor: '#F8F9FA', borderWidth: 1.5, borderColor: '#E1E8ED' },
  chipActive: { backgroundColor: '#E67E22', borderColor: '#E67E22' },
  chipText: { fontSize: 11, fontWeight: '600', color: '#7F8C8D' },
  chipTextActive: { color: 'white' },
  kpisContainer: { flexDirection: 'row', gap: 10, paddingHorizontal: 16, marginBottom: 16 },
  kpiCard: { flex: 1, backgroundColor: 'white', borderRadius: 10, padding: 12, alignItems: 'center', shadowColor: '#000', shadowOffset: { width: 0, height: 1 }, shadowOpacity: 0.05, shadowRadius: 2, elevation: 2 },
  kpiValue: { fontSize: 20, fontWeight: 'bold', color: '#2C3E50', marginBottom: 4 },
  kpiLabel: { fontSize: 10, color: '#7F8C8D' },
  listContent: { paddingHorizontal: 16, paddingBottom: 16 },
  ticketCard: { backgroundColor: 'white', borderRadius: 12, padding: 16, marginBottom: 12, shadowColor: '#000', shadowOffset: { width: 0, height: 1 }, shadowOpacity: 0.05, shadowRadius: 2, elevation: 2 },
  ticketHeader: { flexDirection: 'row', justifyContent: 'space-between', alignItems: 'center', marginBottom: 8 },
  ticketId: { fontSize: 14, fontWeight: '600', color: '#7F8C8D' },
  statusBadge: { flexDirection: 'row', alignItems: 'center', paddingHorizontal: 10, paddingVertical: 4, borderRadius: 8, gap: 4 },
  statusText: { fontSize: 11, fontWeight: '600' },
  ticketTitle: { fontSize: 15, fontWeight: '600', color: '#2C3E50', marginBottom: 10 },
  ticketInfo: { flexDirection: 'row', justifyContent: 'space-between', marginBottom: 6 },
  categoryBadge: { flexDirection: 'row', alignItems: 'center', gap: 4 },
  categoryText: { fontSize: 12, color: '#E67E22', fontWeight: '600' },
  requesterText: { fontSize: 12, color: '#7F8C8D' },
  dateText: { fontSize: 12, color: '#7F8C8D' },
  emptyState: { flex: 1, justifyContent: 'center', alignItems: 'center', paddingHorizontal: 40 },
  emptyTitle: { fontSize: 18, fontWeight: 'bold', color: '#2C3E50', marginTop: 16 },
  emptySubtitle: { fontSize: 14, color: '#7F8C8D', marginTop: 8, textAlign: 'center' },
});