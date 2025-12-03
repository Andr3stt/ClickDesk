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
} from 'react-native';
import { MaterialCommunityIcons } from '@expo/vector-icons';

export default function MyTicketScreen({ navigation }) {
  const [statusFilter, setStatusFilter] = useState('Todos');
  const [categoryFilter, setCategoryFilter] = useState('Todas');

  const statusOptions = ['Todos', 'Aberto', 'Em Andamento', 'Aguardando', 'Resolvido'];
  const categoryOptions = ['Todas', 'Hardware', 'Software', 'Rede', 'Acesso', 'E-mail'];

  const mockTickets = [
    { id: '001', title: 'Impressora não funciona', category: 'Hardware', status: 'Aberto', date: 'Hoje', description: 'A impressora do 2º andar não está imprimindo' },
    { id: '002', title: 'Erro no sistema', category: 'Software', status: 'Em Andamento', date: 'Ontem', description: 'Sistema apresenta erro ao salvar dados' },
    { id: '003', title: 'Internet lenta', category: 'Rede', status: 'Aguardando', date: '15/11/2025', description: 'Conexão muito lenta na sala 301' },
    { id: '004', title: 'Acesso negado', category: 'Acesso', status: 'Resolvido', date: '14/11/2025', description: 'Não consigo acessar o sistema' },
    { id: '005', title: 'Monitor piscando', category: 'Hardware', status: 'Aberto', date: '13/11/2025', description: 'Monitor fica piscando constantemente' },
    { id: '006', title: 'E-mail não envia', category: 'E-mail', status: 'Em Andamento', date: '12/11/2025', description: 'Não consigo enviar e-mails' },
  ];

  const filteredTickets = mockTickets.filter(ticket => {
    const statusMatch = statusFilter === 'Todos' || ticket.status === statusFilter;
    const categoryMatch = categoryFilter === 'Todas' || ticket.category === categoryFilter;
    return statusMatch && categoryMatch;
  });

  const getStatusColor = (status) => {
    const colors = {
      'Aberto': '#FBBC04',
      'Em Andamento': '#4285F4',
      'Aguardando': '#9E9E9E',
      'Resolvido': '#34A853',
    };
    return colors[status] || '#9E9E9E';
  };

  const kpis = [
    { label: 'Total de chamados', value: '8', delta: '-27%', isPositive: false },
    { label: 'Chamados atendidos', value: '6', delta: '-45%', isPositive: false },
    { label: 'Chamados em espera', value: '10', delta: '+100%', isPositive: true },
    { label: 'Chamados em progresso', value: '15', delta: '+25%', isPositive: true },
  ];

  const renderTicket = ({ item }) => (
    <TouchableOpacity
      style={styles.ticketCard}
      onPress={() => navigation.navigate('TicketDetails', { id: item.id })}
    >
      <View style={styles.ticketHeader}>
        <Text style={styles.ticketId}>#{item.id}</Text>
        <View style={[styles.statusBadge, { backgroundColor: getStatusColor(item.status) + '20' }]}>
          <Text style={[styles.statusText, { color: getStatusColor(item.status) }]}>{item.status}</Text>
        </View>
      </View>
      <Text style={styles.ticketTitle}>{item.title}</Text>
      <View style={styles.ticketInfo}>
        <View style={styles.categoryBadge}>
          <Text style={styles.categoryText}>{item.category}</Text>
        </View>
        <Text style={styles.ticketDate}>{item.date}</Text>
      </View>
      <Text style={styles.ticketDescription} numberOfLines={2}>{item.description}</Text>
    </TouchableOpacity>
  );

  return (
    <SafeAreaView style={styles.container}>
      <StatusBar barStyle="dark-content" backgroundColor="#E8D5C4" />
      
      <View style={styles.header}>
        <TouchableOpacity style={styles.refreshButton}>
          <MaterialCommunityIcons name="refresh" size={24} color="#E67E22" />
        </TouchableOpacity>
      </View>

      <ScrollView
        style={styles.scrollView}
        contentContainerStyle={styles.scrollContent}
        showsVerticalScrollIndicator={false}
      >
        <View style={styles.filtersCard}>
          <Text style={styles.filtersTitle}>Filtros</Text>
          <View style={styles.filterRow}>
            <Text style={styles.filterLabel}>Status:</Text>
            <View style={styles.filterOptions}>
              {statusOptions.map(status => (
                <TouchableOpacity
                  key={status}
                  style={[styles.filterButton, statusFilter === status && styles.filterButtonActive]}
                  onPress={() => setStatusFilter(status)}
                >
                  <Text style={[styles.filterButtonText, statusFilter === status && styles.filterButtonTextActive]}>
                    {status}
                  </Text>
                </TouchableOpacity>
              ))}
            </View>
          </View>
          <View style={styles.filterRow}>
            <Text style={styles.filterLabel}>Categoria:</Text>
            <View style={styles.filterOptions}>
              {categoryOptions.map(category => (
                <TouchableOpacity
                  key={category}
                  style={[styles.filterButton, categoryFilter === category && styles.filterButtonActive]}
                  onPress={() => setCategoryFilter(category)}
                >
                  <Text style={[styles.filterButtonText, categoryFilter === category && styles.filterButtonTextActive]}>
                    {category}
                  </Text>
                </TouchableOpacity>
              ))}
            </View>
          </View>
        </View>

        <View style={styles.kpisContainer}>
          {kpis.map((kpi, index) => (
            <View key={index} style={styles.kpiCard}>
              <Text style={styles.kpiLabel}>{kpi.label}</Text>
              <View style={styles.kpiValueRow}>
                <Text style={styles.kpiValue}>{kpi.value}</Text>
                <View style={[styles.deltaBadge, { backgroundColor: kpi.isPositive ? '#34A85320' : '#EA433520' }]}>
                  <MaterialCommunityIcons 
                    name={kpi.isPositive ? 'arrow-up' : 'arrow-down'} 
                    size={12} 
                    color={kpi.isPositive ? '#34A853' : '#EA4335'} 
                  />
                  <Text style={[styles.deltaText, { color: kpi.isPositive ? '#34A853' : '#EA4335' }]}>
                    {kpi.delta}
                  </Text>
                </View>
              </View>
            </View>
          ))}
        </View>

        {filteredTickets.length === 0 ? (
          <View style={styles.emptyState}>
            <MaterialCommunityIcons name="file-document-outline" size={64} color="#BDC3C7" />
            <Text style={styles.emptyStateTitle}>Nenhum chamado encontrado</Text>
            <Text style={styles.emptyStateText}>Ajuste os filtros ou crie um novo chamado</Text>
          </View>
        ) : (
          <FlatList
            data={filteredTickets}
            renderItem={renderTicket}
            keyExtractor={item => item.id}
            scrollEnabled={false}
            contentContainerStyle={styles.ticketsList}
          />
        )}
      </ScrollView>
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  container: { flex: 1, backgroundColor: '#E8D5C4' },
  header: { flexDirection: 'row', justifyContent: 'space-between', alignItems: 'center', paddingHorizontal: 20, paddingVertical: 16, backgroundColor: '#E8D5C4' },
  backButton: { padding: 4 },
  headerTitle: { fontSize: 20, fontWeight: 'bold', color: '#E67E22' },
  refreshButton: { padding: 4 },
  scrollView: { flex: 1 },
  scrollContent: { paddingHorizontal: 16, paddingBottom: 24 },
  filtersCard: { backgroundColor: 'white', borderRadius: 16, padding: 20, marginBottom: 16, shadowColor: '#000', shadowOffset: { width: 0, height: 2 }, shadowOpacity: 0.08, shadowRadius: 4, elevation: 3 },
  filtersTitle: { fontSize: 18, fontWeight: 'bold', color: '#2C3E50', marginBottom: 16 },
  filterRow: { marginBottom: 16 },
  filterLabel: { fontSize: 14, fontWeight: '600', color: '#2C3E50', marginBottom: 8 },
  filterOptions: { flexDirection: 'row', flexWrap: 'wrap', gap: 8 },
  filterButton: { paddingHorizontal: 12, paddingVertical: 6, borderRadius: 8, backgroundColor: '#F8F9FA', borderWidth: 1.5, borderColor: '#E1E8ED' },
  filterButtonActive: { backgroundColor: '#E67E22', borderColor: '#E67E22' },
  filterButtonText: { fontSize: 12, fontWeight: '600', color: '#7F8C8D' },
  filterButtonTextActive: { color: 'white' },
  kpisContainer: { flexDirection: 'row', flexWrap: 'wrap', gap: 12, marginBottom: 16 },
  kpiCard: { backgroundColor: 'white', borderRadius: 12, padding: 16, minWidth: '47%', flex: 1, shadowColor: '#000', shadowOffset: { width: 0, height: 1 }, shadowOpacity: 0.05, shadowRadius: 2, elevation: 2 },
  kpiLabel: { fontSize: 12, color: '#7F8C8D', marginBottom: 8 },
  kpiValueRow: { flexDirection: 'row', alignItems: 'center', justifyContent: 'space-between' },
  kpiValue: { fontSize: 24, fontWeight: 'bold', color: '#2C3E50' },
  deltaBadge: { flexDirection: 'row', alignItems: 'center', paddingHorizontal: 6, paddingVertical: 2, borderRadius: 6 },
  deltaText: { fontSize: 11, fontWeight: '600', marginLeft: 2 },
  ticketsList: { gap: 12 },
  ticketCard: { backgroundColor: 'white', borderRadius: 12, padding: 16, shadowColor: '#000', shadowOffset: { width: 0, height: 1 }, shadowOpacity: 0.05, shadowRadius: 2, elevation: 2 },
  ticketHeader: { flexDirection: 'row', justifyContent: 'space-between', alignItems: 'center', marginBottom: 8 },
  ticketId: { fontSize: 14, fontWeight: '600', color: '#7F8C8D' },
  statusBadge: { paddingHorizontal: 10, paddingVertical: 4, borderRadius: 8 },
  statusText: { fontSize: 11, fontWeight: '600' },
  ticketTitle: { fontSize: 16, fontWeight: 'bold', color: '#2C3E50', marginBottom: 8 },
  ticketInfo: { flexDirection: 'row', alignItems: 'center', marginBottom: 8 },
  categoryBadge: { backgroundColor: '#E67E2220', paddingHorizontal: 8, paddingVertical: 4, borderRadius: 6, marginRight: 12 },
  categoryText: { fontSize: 11, fontWeight: '600', color: '#E67E22' },
  ticketDate: { fontSize: 12, color: '#7F8C8D' },
  ticketDescription: { fontSize: 13, color: '#7F8C8D', lineHeight: 18 },
  emptyState: { alignItems: 'center', paddingVertical: 60, backgroundColor: 'white', borderRadius: 16, marginTop: 16 },
  emptyStateTitle: { fontSize: 18, fontWeight: 'bold', color: '#2C3E50', marginTop: 16, marginBottom: 8 },
  emptyStateText: { fontSize: 14, color: '#7F8C8D', textAlign: 'center' },
});