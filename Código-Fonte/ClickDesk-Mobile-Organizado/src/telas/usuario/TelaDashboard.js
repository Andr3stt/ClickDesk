import React, { useState } from 'react';
import {
  View,
  Text,
  StyleSheet,
  SafeAreaView,
  StatusBar,
  TouchableOpacity,
  ScrollView,
  RefreshControl,
  Modal,
} from 'react-native';
import { MaterialCommunityIcons } from '@expo/vector-icons';

export default function DashboardScreen({ navigation }) {
  const [refreshing, setRefreshing] = useState(false);
  const [period, setPeriod] = useState('today');
  const [menuVisible, setMenuVisible] = useState(false);

  const onRefresh = () => {
    setRefreshing(true);
    setTimeout(() => setRefreshing(false), 1500);
  };

  return (
    <SafeAreaView style={styles.container}>
      <StatusBar barStyle="dark-content" backgroundColor="#E8D5C4" />
      
      <View style={styles.header}>
        <View>
          <Text style={styles.headerSubtitle}>Bem-vindo ao ClickDesk!</Text>
        </View>
        <TouchableOpacity style={styles.menuButton} onPress={() => setMenuVisible(true)}>
          <MaterialCommunityIcons name="account-circle" size={32} color="#E67E22" />
        </TouchableOpacity>
      </View>

      <Modal visible={menuVisible} transparent animationType="fade">
        <TouchableOpacity 
          style={styles.modalOverlay} 
          activeOpacity={1} 
          onPress={() => setMenuVisible(false)}
        >
          <View style={styles.dropdownMenu}>
            <TouchableOpacity
              style={styles.menuItem}
              onPress={() => {
                setMenuVisible(false);
                navigation.navigate('EditProfile');
              }}
            >
              <MaterialCommunityIcons name="account-edit" size={20} color="#2C3E50" />
              <Text style={styles.menuItemText}>Editar Perfil</Text>
            </TouchableOpacity>
            <View style={styles.menuDivider} />
            <TouchableOpacity
              style={styles.menuItem}
              onPress={() => {
                setMenuVisible(false);
                navigation.navigate('Logout');
              }}
            >
              <MaterialCommunityIcons name="logout" size={20} color="#E74C3C" />
              <Text style={[styles.menuItemText, { color: '#E74C3C' }]}>Sair</Text>
            </TouchableOpacity>
          </View>
        </TouchableOpacity>
      </Modal>

      <ScrollView
        style={styles.scrollView}
        contentContainerStyle={styles.scrollContent}
        showsVerticalScrollIndicator={false}
        refreshControl={
          <RefreshControl refreshing={refreshing} onRefresh={onRefresh} colors={['#E67E22']} />
        }
      >
        <View style={styles.toolbar}>
          <Text style={styles.toolbarLabel}>Período:</Text>
          <View style={styles.periodButtons}>
            <TouchableOpacity
              style={[styles.periodButton, period === 'today' && styles.periodButtonActive]}
              onPress={() => setPeriod('today')}
            >
              <Text style={[styles.periodButtonText, period === 'today' && styles.periodButtonTextActive]}>
                Hoje
              </Text>
            </TouchableOpacity>
            <TouchableOpacity
              style={[styles.periodButton, period === '7d' && styles.periodButtonActive]}
              onPress={() => setPeriod('7d')}
            >
              <Text style={[styles.periodButtonText, period === '7d' && styles.periodButtonTextActive]}>
                7 dias
              </Text>
            </TouchableOpacity>
            <TouchableOpacity
              style={[styles.periodButton, period === '30d' && styles.periodButtonActive]}
              onPress={() => setPeriod('30d')}
            >
              <Text style={[styles.periodButtonText, period === '30d' && styles.periodButtonTextActive]}>
                30 dias
              </Text>
            </TouchableOpacity>
          </View>
        </View>

        <View style={styles.kpisContainer}>
          <TouchableOpacity 
            style={styles.kpiCard}
            onPress={() => navigation.navigate('MyTicket')}
          >
            <Text style={styles.kpiLabel}>Total de Chamados</Text>
            <Text style={styles.kpiValue}>24</Text>
            <View style={styles.deltaContainer}>
              <MaterialCommunityIcons name="arrow-up" size={14} color="#34A853" />
              <Text style={[styles.deltaText, { color: '#34A853' }]}>+12%</Text>
            </View>
          </TouchableOpacity>
          <TouchableOpacity 
            style={styles.kpiCard}
            onPress={() => navigation.navigate('MyTicket', { filterStatus: 'aberto' })}
          >
            <Text style={styles.kpiLabel}>Em Aberto</Text>
            <Text style={styles.kpiValue}>8</Text>
            <View style={styles.deltaContainer}>
              <MaterialCommunityIcons name="arrow-down" size={14} color="#E74C3C" />
              <Text style={[styles.deltaText, { color: '#E74C3C' }]}>-3%</Text>
            </View>
          </TouchableOpacity>
          <TouchableOpacity 
            style={styles.kpiCard}
            onPress={() => navigation.navigate('MyTicket', { filterStatus: 'em-andamento' })}
          >
            <Text style={styles.kpiLabel}>Em Andamento</Text>
            <Text style={styles.kpiValue}>5</Text>
            <View style={styles.deltaContainer}>
              <MaterialCommunityIcons name="arrow-up" size={14} color="#4285F4" />
              <Text style={[styles.deltaText, { color: '#4285F4' }]}>+2</Text>
            </View>
          </TouchableOpacity>
          <TouchableOpacity 
            style={styles.kpiCard}
            onPress={() => navigation.navigate('MyTicket', { filterStatus: 'resolvido' })}
          >
            <Text style={styles.kpiLabel}>Resolvidos</Text>
            <Text style={styles.kpiValue}>11</Text>
            <View style={styles.deltaContainer}>
              <MaterialCommunityIcons name="arrow-up" size={14} color="#34A853" />
              <Text style={[styles.deltaText, { color: '#34A853' }]}>+8%</Text>
            </View>
          </TouchableOpacity>
        </View>

        <Text style={styles.sectionTitle}>Ações Rápidas</Text>
        <View style={styles.actionsGrid}>
          <TouchableOpacity style={styles.actionCard} onPress={() => navigation.navigate('NewTicket')}>
            <MaterialCommunityIcons name="plus-circle" size={40} color="#E67E22" />
            <Text style={styles.actionTitle}>Novo Chamado</Text>
            <Text style={styles.actionSubtitle}>Criar solicitação</Text>
          </TouchableOpacity>
          <TouchableOpacity style={styles.actionCard} onPress={() => navigation.navigate('MyTicket')}>
            <MaterialCommunityIcons name="format-list-checks" size={40} color="#E67E22" />
            <Text style={styles.actionTitle}>Meus Chamados</Text>
            <Text style={styles.actionSubtitle}>Acompanhar status</Text>
          </TouchableOpacity>
          <TouchableOpacity style={styles.actionCard} onPress={() => navigation.navigate('FAQ')}>
            <MaterialCommunityIcons name="help-circle" size={40} color="#E67E22" />
            <Text style={styles.actionTitle}>FAQ</Text>
            <Text style={styles.actionSubtitle}>Perguntas frequentes</Text>
          </TouchableOpacity>
        </View>

        <Text style={styles.sectionTitle}>Chamados Recentes</Text>
        <View style={styles.ticketsList}>
          {[1, 2, 3].map((_, index) => (
            <TouchableOpacity 
              key={index} 
              style={styles.ticketCard}
              onPress={() => navigation.navigate('TicketDetails', { ticketId: `CD-${String(100 + index).padStart(3, '0')}` })}
            >
              <View style={styles.ticketHeader}>
                <Text style={styles.ticketId}>#CD-{String(100 + index).padStart(3, '0')}</Text>
                <View style={[styles.statusBadge, { backgroundColor: '#FBBC0420' }]}>
                  <Text style={[styles.statusText, { color: '#FBBC04' }]}>Em Aberto</Text>
                </View>
              </View>
              <Text style={styles.ticketTitle}>Problema no computador da sala {index + 1}</Text>
              <View style={styles.ticketInfo}>
                <Text style={styles.ticketCategory}>Hardware</Text>
                <Text style={styles.ticketDate}>Hoje • {14 + index}:30</Text>
              </View>
            </TouchableOpacity>
          ))}
        </View>
      </ScrollView>
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  container: { flex: 1, backgroundColor: '#E8D5C4' },
  header: { flexDirection: 'row', justifyContent: 'space-between', alignItems: 'center', paddingHorizontal: 20, paddingVertical: 16, backgroundColor: '#E8D5C4' },
  headerTitle: { fontSize: 22, fontWeight: 'bold', color: '#E67E22' },
  headerSubtitle: { fontSize: 13, color: '#7F8C8D', marginTop: 2 },
  menuButton: { padding: 4 },
  modalOverlay: { flex: 1, backgroundColor: 'rgba(0,0,0,0.3)', justifyContent: 'flex-start', alignItems: 'flex-end', paddingTop: 60, paddingRight: 20 },
  dropdownMenu: { backgroundColor: 'white', borderRadius: 12, minWidth: 180, shadowColor: '#000', shadowOffset: { width: 0, height: 4 }, shadowOpacity: 0.15, shadowRadius: 8, elevation: 8 },
  menuItem: { flexDirection: 'row', alignItems: 'center', padding: 16, gap: 12 },
  menuItemText: { fontSize: 15, fontWeight: '600', color: '#2C3E50' },
  menuDivider: { height: 1, backgroundColor: '#E1E8ED', marginHorizontal: 12 },
  scrollView: { flex: 1 },
  scrollContent: { paddingHorizontal: 16, paddingBottom: 24 },
  toolbar: { flexDirection: 'row', alignItems: 'center', marginBottom: 16 },
  toolbarLabel: { fontSize: 14, fontWeight: '600', color: '#2C3E50', marginRight: 12 },
  periodButtons: { flexDirection: 'row', gap: 8 },
  periodButton: { paddingHorizontal: 16, paddingVertical: 8, borderRadius: 8, backgroundColor: '#F8F9FA', borderWidth: 1.5, borderColor: '#E1E8ED' },
  periodButtonActive: { backgroundColor: '#E67E22', borderColor: '#E67E22' },
  periodButtonText: { fontSize: 13, fontWeight: '600', color: '#7F8C8D' },
  periodButtonTextActive: { color: 'white' },
  kpisContainer: { flexDirection: 'row', flexWrap: 'wrap', gap: 12, marginBottom: 24 },
  kpiCard: { backgroundColor: 'white', borderRadius: 12, padding: 16, minWidth: '47%', flex: 1, shadowColor: '#000', shadowOffset: { width: 0, height: 2 }, shadowOpacity: 0.05, shadowRadius: 4, elevation: 3 },
  kpiLabel: { fontSize: 12, color: '#7F8C8D', marginBottom: 8 },
  kpiValue: { fontSize: 28, fontWeight: 'bold', color: '#2C3E50', marginBottom: 4 },
  deltaContainer: { flexDirection: 'row', alignItems: 'center' },
  deltaText: { fontSize: 12, fontWeight: '600', marginLeft: 2 },
  sectionTitle: { fontSize: 18, fontWeight: 'bold', color: '#2C3E50', marginBottom: 12 },
  actionsGrid: { flexDirection: 'row', flexWrap: 'wrap', gap: 12, marginBottom: 24 },
  actionCard: { backgroundColor: 'white', borderRadius: 12, padding: 16, alignItems: 'center', flex: 1, minWidth: '30%', shadowColor: '#000', shadowOffset: { width: 0, height: 1 }, shadowOpacity: 0.05, shadowRadius: 2, elevation: 2 },
  actionTitle: { fontSize: 13, fontWeight: '600', color: '#2C3E50', marginTop: 8, textAlign: 'center' },
  actionSubtitle: { fontSize: 11, color: '#7F8C8D', marginTop: 4, textAlign: 'center' },
  ticketsList: { gap: 12, marginBottom: 16 },
  ticketCard: { backgroundColor: 'white', borderRadius: 12, padding: 16, shadowColor: '#000', shadowOffset: { width: 0, height: 1 }, shadowOpacity: 0.05, shadowRadius: 2, elevation: 2 },
  ticketHeader: { flexDirection: 'row', justifyContent: 'space-between', alignItems: 'center', marginBottom: 8 },
  ticketId: { fontSize: 14, fontWeight: '600', color: '#7F8C8D' },
  statusBadge: { paddingHorizontal: 10, paddingVertical: 4, borderRadius: 8 },
  statusText: { fontSize: 11, fontWeight: '600' },
  ticketTitle: { fontSize: 15, fontWeight: '600', color: '#2C3E50', marginBottom: 8 },
  ticketInfo: { flexDirection: 'row', justifyContent: 'space-between' },
  ticketCategory: { fontSize: 12, color: '#E67E22', fontWeight: '600' },
  ticketDate: { fontSize: 12, color: '#7F8C8D' },
});