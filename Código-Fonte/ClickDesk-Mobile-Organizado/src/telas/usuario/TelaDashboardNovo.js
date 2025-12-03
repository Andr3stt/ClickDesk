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
} from 'react-native';
import { MaterialCommunityIcons } from '@expo/vector-icons';
import { Cores } from '../../estilos/cores';

export default function TelaDashboard({ navigation }) {
  const [refreshing, setRefreshing] = useState(false);
  const [filtroSelecionado, setFiltroSelecionado] = useState('todos');

  const onRefresh = () => {
    setRefreshing(true);
    setTimeout(() => setRefreshing(false), 1500);
  };

  const dadosKPI = {
    total: 24,
    abertos: 8,
    emAndamento: 5,
    resolvidos: 11,
  };

  const filtros = [
    { id: 'todos', label: 'Todos', count: dadosKPI.total },
    { id: 'abertos', label: 'Abertos', count: dadosKPI.abertos },
    { id: 'andamento', label: 'Em Andamento', count: dadosKPI.emAndamento },
    { id: 'resolvidos', label: 'Resolvidos', count: dadosKPI.resolvidos },
  ];

  return (
    <SafeAreaView style={styles.container}>
      <StatusBar barStyle="dark-content" backgroundColor={Cores.background} />
      
      {/* Header com Logo */}
      <View style={styles.header}>
        <View style={styles.logoContainer}>
          <MaterialCommunityIcons name="ticket" size={28} color={Cores.brand} />
          <Text style={styles.logoText}>ClickDesk</Text>
        </View>
        <TouchableOpacity 
          style={styles.menuButton}
          onPress={() => navigation.navigate('EditarPerfil')}
        >
          <MaterialCommunityIcons name="account-circle" size={32} color={Cores.brand} />
        </TouchableOpacity>
      </View>

      <ScrollView
        style={styles.scrollView}
        contentContainerStyle={styles.scrollContent}
        showsVerticalScrollIndicator={false}
        refreshControl={
          <RefreshControl 
            refreshing={refreshing} 
            onRefresh={onRefresh} 
            colors={[Cores.brand]}
            tintColor={Cores.brand}
          />
        }
      >
        {/* Bem-vindo */}
        <View style={styles.welcomeSection}>
          <Text style={styles.welcomeTitle}>Olá, Usuário! 👋</Text>
          <Text style={styles.welcomeSubtitle}>Gerencie seus chamados de forma eficiente</Text>
        </View>

        {/* Cards de Estatísticas */}
        <View style={styles.statsContainer}>
          <TouchableOpacity 
            style={[styles.statCard, styles.statCardPrimary]}
            onPress={() => navigation.navigate('MeusChamados')}
          >
            <View style={styles.statIcon}>
              <MaterialCommunityIcons name="ticket-confirmation" size={32} color="#FFFFFF" />
            </View>
            <View style={styles.statInfo}>
              <Text style={styles.statValue}>{dadosKPI.total}</Text>
              <Text style={styles.statLabel}>Total de Chamados</Text>
            </View>
          </TouchableOpacity>

          <View style={styles.statsRow}>
            <TouchableOpacity 
              style={styles.statCardSmall}
              onPress={() => navigation.navigate('MeusChamados', { filterStatus: 'aberto' })}
            >
              <MaterialCommunityIcons name="clock-alert" size={24} color={Cores.brand} />
              <Text style={styles.statSmallValue}>{dadosKPI.abertos}</Text>
              <Text style={styles.statSmallLabel}>Abertos</Text>
            </TouchableOpacity>

            <TouchableOpacity 
              style={styles.statCardSmall}
              onPress={() => navigation.navigate('MeusChamados', { filterStatus: 'em-andamento' })}
            >
              <MaterialCommunityIcons name="progress-clock" size={24} color="#4285F4" />
              <Text style={styles.statSmallValue}>{dadosKPI.emAndamento}</Text>
              <Text style={styles.statSmallLabel}>Em Andamento</Text>
            </TouchableOpacity>

            <TouchableOpacity 
              style={styles.statCardSmall}
              onPress={() => navigation.navigate('MeusChamados', { filterStatus: 'resolvido' })}
            >
              <MaterialCommunityIcons name="check-circle" size={24} color="#34A853" />
              <Text style={styles.statSmallValue}>{dadosKPI.resolvidos}</Text>
              <Text style={styles.statSmallLabel}>Resolvidos</Text>
            </TouchableOpacity>
          </View>
        </View>

        {/* Ações Rápidas */}
        <View style={styles.section}>
          <Text style={styles.sectionTitle}>Ações Rápidas</Text>
          <View style={styles.actionsGrid}>
            <TouchableOpacity 
              style={styles.actionCard}
              onPress={() => navigation.navigate('NovoChamado')}
            >
              <View style={styles.actionIconContainer}>
                <MaterialCommunityIcons name="plus-circle" size={32} color={Cores.brand} />
              </View>
              <Text style={styles.actionTitle}>Novo Chamado</Text>
            </TouchableOpacity>

            <TouchableOpacity 
              style={styles.actionCard}
              onPress={() => navigation.navigate('MeusChamados')}
            >
              <View style={styles.actionIconContainer}>
                <MaterialCommunityIcons name="format-list-checks" size={32} color={Cores.brand} />
              </View>
              <Text style={styles.actionTitle}>Meus Chamados</Text>
            </TouchableOpacity>

            <TouchableOpacity 
              style={styles.actionCard}
              onPress={() => navigation.navigate('FAQ')}
            >
              <View style={styles.actionIconContainer}>
                <MaterialCommunityIcons name="help-circle" size={32} color={Cores.brand} />
              </View>
              <Text style={styles.actionTitle}>Central de Ajuda</Text>
            </TouchableOpacity>
          </View>
        </View>

        {/* Chamados Recentes com Filtros */}
        <View style={styles.section}>
          <Text style={styles.sectionTitle}>Chamados Recentes</Text>
          
          {/* Filtros */}
          <ScrollView 
            horizontal 
            showsHorizontalScrollIndicator={false}
            style={styles.filtersContainer}
          >
            {filtros.map((filtro) => (
              <TouchableOpacity
                key={filtro.id}
                style={[
                  styles.filterChip,
                  filtroSelecionado === filtro.id && styles.filterChipActive
                ]}
                onPress={() => setFiltroSelecionado(filtro.id)}
              >
                <Text style={[
                  styles.filterChipText,
                  filtroSelecionado === filtro.id && styles.filterChipTextActive
                ]}>
                  {filtro.label}
                </Text>
                <View style={[
                  styles.filterBadge,
                  filtroSelecionado === filtro.id && styles.filterBadgeActive
                ]}>
                  <Text style={[
                    styles.filterBadgeText,
                    filtroSelecionado === filtro.id && styles.filterBadgeTextActive
                  ]}>
                    {filtro.count}
                  </Text>
                </View>
              </TouchableOpacity>
            ))}
          </ScrollView>

          {/* Lista de Chamados */}
          <View style={styles.ticketsList}>
            {[1, 2, 3].map((_, index) => (
              <TouchableOpacity 
                key={index} 
                style={styles.ticketCard}
                onPress={() => navigation.navigate('DetalhesChamado', { id: `${100 + index}` })}
              >
                <View style={styles.ticketHeader}>
                  <View style={styles.ticketIdContainer}>
                    <MaterialCommunityIcons name="ticket" size={16} color={Cores.brand} />
                    <Text style={styles.ticketId}>#{100 + index}</Text>
                  </View>
                  <View style={styles.statusBadge}>
                    <Text style={styles.statusText}>Aberto</Text>
                  </View>
                </View>
                
                <Text style={styles.ticketTitle}>
                  Problema no computador da sala {index + 1}
                </Text>
                
                <View style={styles.ticketFooter}>
                  <View style={styles.ticketCategory}>
                    <MaterialCommunityIcons name="tag" size={14} color={Cores.textoSecundario} />
                    <Text style={styles.ticketCategoryText}>Hardware</Text>
                  </View>
                  <View style={styles.ticketDate}>
                    <MaterialCommunityIcons name="clock-outline" size={14} color={Cores.textoSecundario} />
                    <Text style={styles.ticketDateText}>Hoje • {14 + index}:30</Text>
                  </View>
                </View>
              </TouchableOpacity>
            ))}
          </View>

          <TouchableOpacity 
            style={styles.viewAllButton}
            onPress={() => navigation.navigate('MeusChamados')}
          >
            <Text style={styles.viewAllButtonText}>Ver todos os chamados</Text>
            <MaterialCommunityIcons name="arrow-right" size={18} color={Cores.brand} />
          </TouchableOpacity>
        </View>
      </ScrollView>
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: Cores.background,
  },
  
  // Header
  header: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    paddingHorizontal: 20,
    paddingVertical: 16,
    backgroundColor: Cores.background,
    borderBottomWidth: 1,
    borderBottomColor: '#E8E8E8',
  },
  logoContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 10,
  },
  logoText: {
    fontSize: 24,
    fontWeight: 'bold',
    color: Cores.brand,
  },
  menuButton: {
    padding: 4,
  },

  // ScrollView
  scrollView: {
    flex: 1,
  },
  scrollContent: {
    paddingHorizontal: 20,
    paddingBottom: 30,
  },

  // Welcome
  welcomeSection: {
    marginTop: 24,
    marginBottom: 24,
  },
  welcomeTitle: {
    fontSize: 26,
    fontWeight: 'bold',
    color: Cores.textoPrincipal,
    marginBottom: 6,
  },
  welcomeSubtitle: {
    fontSize: 14,
    color: Cores.textoSecundario,
  },

  // Estatísticas
  statsContainer: {
    marginBottom: 28,
  },
  statCard: {
    backgroundColor: '#FFFFFF',
    borderRadius: 12,
    padding: 20,
    marginBottom: 12,
    flexDirection: 'row',
    alignItems: 'center',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.08,
    shadowRadius: 8,
    elevation: 3,
  },
  statCardPrimary: {
    backgroundColor: Cores.brand,
  },
  statIcon: {
    marginRight: 16,
  },
  statInfo: {
    flex: 1,
  },
  statValue: {
    fontSize: 36,
    fontWeight: 'bold',
    color: '#FFFFFF',
    marginBottom: 4,
  },
  statLabel: {
    fontSize: 14,
    color: '#FFFFFF',
    opacity: 0.9,
  },
  statsRow: {
    flexDirection: 'row',
    gap: 12,
  },
  statCardSmall: {
    flex: 1,
    backgroundColor: '#FFFFFF',
    borderRadius: 12,
    padding: 16,
    alignItems: 'center',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.06,
    shadowRadius: 6,
    elevation: 2,
  },
  statSmallValue: {
    fontSize: 28,
    fontWeight: 'bold',
    color: Cores.textoPrincipal,
    marginTop: 8,
    marginBottom: 4,
  },
  statSmallLabel: {
    fontSize: 12,
    color: Cores.textoSecundario,
    textAlign: 'center',
  },

  // Seção
  section: {
    marginBottom: 28,
  },
  sectionTitle: {
    fontSize: 20,
    fontWeight: 'bold',
    color: Cores.textoPrincipal,
    marginBottom: 16,
  },

  // Ações
  actionsGrid: {
    flexDirection: 'row',
    gap: 12,
  },
  actionCard: {
    flex: 1,
    backgroundColor: '#FFFFFF',
    borderRadius: 12,
    padding: 16,
    alignItems: 'center',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.06,
    shadowRadius: 6,
    elevation: 2,
  },
  actionIconContainer: {
    marginBottom: 12,
  },
  actionTitle: {
    fontSize: 13,
    fontWeight: '600',
    color: Cores.textoPrincipal,
    textAlign: 'center',
  },

  // Filtros
  filtersContainer: {
    marginBottom: 16,
  },
  filterChip: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 16,
    paddingVertical: 10,
    borderRadius: 20,
    backgroundColor: '#FFFFFF',
    marginRight: 10,
    borderWidth: 1.5,
    borderColor: '#E8E8E8',
    gap: 8,
  },
  filterChipActive: {
    backgroundColor: Cores.brand,
    borderColor: Cores.brand,
  },
  filterChipText: {
    fontSize: 14,
    fontWeight: '600',
    color: Cores.textoSecundario,
  },
  filterChipTextActive: {
    color: '#FFFFFF',
  },
  filterBadge: {
    backgroundColor: Cores.background,
    paddingHorizontal: 8,
    paddingVertical: 2,
    borderRadius: 10,
    minWidth: 24,
    alignItems: 'center',
  },
  filterBadgeActive: {
    backgroundColor: '#FFFFFF',
  },
  filterBadgeText: {
    fontSize: 12,
    fontWeight: 'bold',
    color: Cores.textoPrincipal,
  },
  filterBadgeTextActive: {
    color: Cores.brand,
  },

  // Tickets
  ticketsList: {
    gap: 12,
  },
  ticketCard: {
    backgroundColor: '#FFFFFF',
    borderRadius: 12,
    padding: 16,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.06,
    shadowRadius: 4,
    elevation: 2,
  },
  ticketHeader: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    marginBottom: 10,
  },
  ticketIdContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 6,
  },
  ticketId: {
    fontSize: 14,
    fontWeight: '700',
    color: Cores.brand,
  },
  statusBadge: {
    backgroundColor: '#FBBC04' + '20',
    paddingHorizontal: 10,
    paddingVertical: 4,
    borderRadius: 6,
  },
  statusText: {
    fontSize: 11,
    fontWeight: '700',
    color: '#FBBC04',
  },
  ticketTitle: {
    fontSize: 15,
    fontWeight: '600',
    color: Cores.textoPrincipal,
    marginBottom: 12,
    lineHeight: 20,
  },
  ticketFooter: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
  },
  ticketCategory: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 4,
  },
  ticketCategoryText: {
    fontSize: 12,
    fontWeight: '600',
    color: Cores.textoSecundario,
  },
  ticketDate: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 4,
  },
  ticketDateText: {
    fontSize: 12,
    color: Cores.textoSecundario,
  },

  // View All
  viewAllButton: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    marginTop: 16,
    paddingVertical: 14,
    backgroundColor: '#FFFFFF',
    borderRadius: 8,
    borderWidth: 1.5,
    borderColor: Cores.brand,
    gap: 8,
  },
  viewAllButtonText: {
    fontSize: 15,
    fontWeight: '600',
    color: Cores.brand,
  },
});
