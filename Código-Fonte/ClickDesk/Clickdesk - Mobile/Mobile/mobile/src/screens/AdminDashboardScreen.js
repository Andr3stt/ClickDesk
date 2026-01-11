// Importa React e o hook useState para gerenciar estados do componente
import React, { useState } from 'react';
// Importa componentes essenciais do React Native para construir a interface
import {
  View, // Container para agrupar outros componentes
  Text, // Componente para exibir texto
  StyleSheet, // Utilitário para criar estilos
  SafeAreaView, // Container seguro que respeita áreas do dispositivo (notch, etc)
  ScrollView, // Container rolável para conteúdo que excede a tela
  TouchableOpacity, // Botão personalizável com feedback de toque
  StatusBar, // Controla a barra de status do sistema
  RefreshControl, // Componente para implementar pull-to-refresh
  Modal, // Componente para exibir conteúdo sobreposto à tela
} from 'react-native';
// Importa biblioteca de ícones do Material Design
import { MaterialCommunityIcons } from '@expo/vector-icons';

// Componente funcional principal da tela de Dashboard do Administrador
// Recebe navigation como prop para navegação entre telas
export default function AdminDashboardScreen({ navigation }) {
  // Estado para controlar se está atualizando os dados (pull-to-refresh)
  const [refreshing, setRefreshing] = useState(false);
  // Estado para armazenar o período selecionado (hoje, 7 dias ou 30 dias)
  const [period, setPeriod] = useState('today');
  // Estado para controlar a visibilidade do menu de perfil
  const [showProfileMenu, setShowProfileMenu] = useState(false);

  // Função executada quando o usuário puxa a tela para baixo (pull-to-refresh)
  const onRefresh = () => {
    // Ativa o indicador de carregamento
    setRefreshing(true);
    // Simula carregamento de dados por 1,5 segundos e depois desativa o indicador
    setTimeout(() => setRefreshing(false), 1500);
  };

  // Retorna a estrutura JSX da tela
  return (
    // SafeAreaView garante que o conteúdo não fique sob áreas do sistema
    <SafeAreaView style={styles.container}>
      {/* Configura a barra de status com texto escuro e fundo bege */}
      <StatusBar barStyle="dark-content" backgroundColor="#E8D5C4" />
      
      {/* Container do cabeçalho da tela */}
      <View style={styles.header}>
        {/* Container do texto do cabeçalho */}
        <View>
          {/* Subtítulo do cabeçalho explicando a tela */}
          <Text style={styles.headerSubtitle}>Visão geral de todos os chamados</Text>
        </View>
        {/* Botão para abrir o menu de perfil */}
        <TouchableOpacity style={styles.menuButton} onPress={() => setShowProfileMenu(true)}>
          {/* Ícone de perfil do usuário */}
          <MaterialCommunityIcons name="account-circle" size={32} color="#E67E22" />
        </TouchableOpacity>
      </View>

      {/* Modal para exibir o menu de perfil */}
      <Modal
        visible={showProfileMenu} // Controla se o modal está visível
        transparent // Torna o fundo do modal transparente
        animationType="fade" // Animação de fade ao abrir/fechar
        onRequestClose={() => setShowProfileMenu(false)} // Fecha o modal ao pressionar voltar (Android)
      >
        {/* Overlay que fecha o modal ao ser tocado */}
        <TouchableOpacity 
          style={styles.modalOverlay} // Estilo do fundo do modal
          activeOpacity={1} // Mantém opacidade total ao tocar
          onPress={() => setShowProfileMenu(false)} // Fecha o modal ao tocar fora
        >
          {/* Container do conteúdo do menu de perfil */}
          <View style={styles.profileMenuContainer}>
            {/* Opção de menu: Editar Perfil */}
            <TouchableOpacity
              style={styles.profileMenuItem} // Estilo do item de menu
              onPress={() => {
                setShowProfileMenu(false); // Fecha o modal
                navigation.navigate('EditProfile'); // Navega para a tela de edição de perfil
              }}
            >
              {/* Ícone de edição de conta */}
              <MaterialCommunityIcons name="account-edit" size={22} color="#2C3E50" />
              {/* Texto da opção de menu */}
              <Text style={styles.profileMenuText}>Editar Perfil</Text>
            </TouchableOpacity>
            {/* Linha divisória entre as opções */}
            <View style={styles.profileMenuDivider} />
            {/* Opção de menu: Sair */}
            <TouchableOpacity
              style={styles.profileMenuItem} // Estilo do item de menu
              onPress={() => {
                setShowProfileMenu(false); // Fecha o modal
                navigation.navigate('Logout'); // Navega para a tela de logout
              }}
            >
              {/* Ícone de logout */}
              <MaterialCommunityIcons name="logout" size={22} color="#E74C3C" />
              {/* Texto da opção de menu em vermelho */}
              <Text style={[styles.profileMenuText, { color: '#E74C3C' }]}>Sair</Text>
            </TouchableOpacity>
          </View>
        </TouchableOpacity>
      </Modal>

      {/* ScrollView permite rolagem do conteúdo principal */}
      <ScrollView
        style={styles.scrollView} // Estilo do container de rolagem
        contentContainerStyle={styles.scrollContent} // Estilo do conteúdo interno
        showsVerticalScrollIndicator={false} // Esconde a barra de rolagem vertical
        refreshControl={
          // Componente que adiciona funcionalidade de pull-to-refresh
          <RefreshControl refreshing={refreshing} onRefresh={onRefresh} colors={['#E67E22']} />
        }
      >
        {/* Barra de ferramentas com filtros de período */}
        <View style={styles.toolbar}>
          {/* Label do filtro de período */}
          <Text style={styles.toolbarLabel}>Período:</Text>
          {/* Container dos botões de período */}
          <View style={styles.periodButtons}>
            {/* Botão para filtrar por "Hoje" */}
            <TouchableOpacity
              style={[styles.periodButton, period === 'today' && styles.periodButtonActive]} // Adiciona estilo ativo se selecionado
              onPress={() => setPeriod('today')} // Define período como "hoje"
            >
              {/* Texto do botão com estilo dinâmico baseado na seleção */}
              <Text style={[styles.periodButtonText, period === 'today' && styles.periodButtonTextActive]}>
                Hoje
              </Text>
            </TouchableOpacity>
            {/* Botão para filtrar por "7 dias" */}
            <TouchableOpacity
              style={[styles.periodButton, period === '7d' && styles.periodButtonActive]} // Adiciona estilo ativo se selecionado
              onPress={() => setPeriod('7d')} // Define período como "7 dias"
            >
              {/* Texto do botão com estilo dinâmico baseado na seleção */}
              <Text style={[styles.periodButtonText, period === '7d' && styles.periodButtonTextActive]}>
                7 dias
              </Text>
            </TouchableOpacity>
            {/* Botão para filtrar por "30 dias" */}
            <TouchableOpacity
              style={[styles.periodButton, period === '30d' && styles.periodButtonActive]} // Adiciona estilo ativo se selecionado
              onPress={() => setPeriod('30d')} // Define período como "30 dias"
            >
              {/* Texto do botão com estilo dinâmico baseado na seleção */}
              <Text style={[styles.periodButtonText, period === '30d' && styles.periodButtonTextActive]}>
                30 dias
              </Text>
            </TouchableOpacity>
          </View>
        </View>

        {/* Seção de KPIs (Key Performance Indicators) - Indicadores chave de desempenho */}
        {/* Container dos cards de KPI */}
        <View style={styles.kpisContainer}>
          {/* Card KPI: Total de chamados */}
          <TouchableOpacity 
            style={styles.kpiCard} // Estilo do card
            onPress={() => navigation.navigate('MyTicketsAdmin')} // Navega para todos os chamados ao tocar
          >
            {/* Label do KPI */}
            <Text style={styles.kpiLabel}>Total</Text>
            {/* Valor numérico do KPI */}
            <Text style={styles.kpiValue}>145</Text>
            {/* Container do indicador de variação percentual */}
            <View style={styles.deltaContainer}>
              {/* Ícone de seta para cima indicando crescimento */}
              <MaterialCommunityIcons name="arrow-up" size={14} color="#34A853" />
              {/* Texto da variação percentual em verde (positivo) */}
              <Text style={[styles.deltaText, { color: '#34A853' }]}>+12%</Text>
            </View>
          </TouchableOpacity>
          {/* Card KPI: Chamados pendentes */}
          <TouchableOpacity 
            style={styles.kpiCard} // Estilo do card
            onPress={() => navigation.navigate('TicketApproval')} // Navega para aprovação de chamados ao tocar
          >
            {/* Label do KPI */}
            <Text style={styles.kpiLabel}>Pendentes</Text>
            {/* Valor numérico do KPI */}
            <Text style={styles.kpiValue}>38</Text>
            {/* Container do indicador de variação percentual */}
            <View style={styles.deltaContainer}>
              {/* Ícone de seta para cima indicando crescimento */}
              <MaterialCommunityIcons name="arrow-up" size={14} color="#FBBC04" />
              {/* Texto da variação percentual em amarelo (atenção) */}
              <Text style={[styles.deltaText, { color: '#FBBC04' }]}>+5%</Text>
            </View>
          </TouchableOpacity>
          {/* Card KPI: Chamados em progresso */}
          <TouchableOpacity 
            style={styles.kpiCard} // Estilo do card
            onPress={() => navigation.navigate('MyTicketsAdmin', { filterStatus: 'em-andamento' })} // Navega com filtro de status
          >
            {/* Label do KPI */}
            <Text style={styles.kpiLabel}>Em Progresso</Text>
            {/* Valor numérico do KPI */}
            <Text style={styles.kpiValue}>42</Text>
            {/* Container do indicador de variação percentual */}
            <View style={styles.deltaContainer}>
              {/* Ícone de seta para cima indicando crescimento */}
              <MaterialCommunityIcons name="arrow-up" size={14} color="#4285F4" />
              {/* Texto da variação percentual em azul */}
              <Text style={[styles.deltaText, { color: '#4285F4' }]}>+8%</Text>
            </View>
          </TouchableOpacity>
          {/* Card KPI: Chamados resolvidos */}
          <TouchableOpacity 
            style={styles.kpiCard} // Estilo do card
            onPress={() => navigation.navigate('MyTicketsAdmin', { filterStatus: 'resolvido' })} // Navega com filtro de status
          >
            {/* Label do KPI */}
            <Text style={styles.kpiLabel}>Resolvidos</Text>
            {/* Valor numérico do KPI */}
            <Text style={styles.kpiValue}>65</Text>
            {/* Container do indicador de variação percentual */}
            <View style={styles.deltaContainer}>
              {/* Ícone de seta para cima indicando crescimento */}
              <MaterialCommunityIcons name="arrow-up" size={14} color="#34A853" />
              {/* Texto da variação percentual em verde (positivo) */}
              <Text style={[styles.deltaText, { color: '#34A853' }]}>+15%</Text>
            </View>
          </TouchableOpacity>
        </View>

        {/* Título da seção de Ações Rápidas */}
        <Text style={styles.sectionTitle}>Ações Rápidas</Text>
        {/* Grid de cards de ações rápidas */}
        <View style={styles.actionsGrid}>
          {/* Card de ação: Aprovar Chamados */}
          <TouchableOpacity style={styles.actionCard} onPress={() => navigation.navigate('TicketApproval')}>
            {/* Ícone de check/aprovação */}
            <MaterialCommunityIcons name="check-circle" size={40} color="#E67E22" />
            {/* Título da ação */}
            <Text style={styles.actionTitle}>Aprovar Chamados</Text>
            {/* Subtítulo com contador de pendências */}
            <Text style={styles.actionSubtitle}>12 pendentes</Text>
          </TouchableOpacity>
          {/* Card de ação: Meus Chamados */}
          <TouchableOpacity style={styles.actionCard} onPress={() => navigation.navigate('MyTicketsAdmin')}>
            {/* Ícone de lista de chamados */}
            <MaterialCommunityIcons name="clipboard-list" size={40} color="#E67E22" />
            {/* Título da ação */}
            <Text style={styles.actionTitle}>Meus Chamados</Text>
            {/* Subtítulo com contador de chamados atribuídos */}
            <Text style={styles.actionSubtitle}>42 atribuídos</Text>
          </TouchableOpacity>
          {/* Card de ação: FAQ */}
          <TouchableOpacity style={styles.actionCard} onPress={() => navigation.navigate('FAQAdmin')}>
            {/* Ícone de ajuda/FAQ */}
            <MaterialCommunityIcons name="help-circle" size={40} color="#E67E22" />
            {/* Título da ação */}
            <Text style={styles.actionTitle}>FAQ</Text>
            {/* Subtítulo descritivo */}
            <Text style={styles.actionSubtitle}>Central de ajuda</Text>
          </TouchableOpacity>
        </View>

        {/* Título da seção de Chamados Recentes */}
        <Text style={styles.sectionTitle}>Chamados Recentes</Text>
        {/* Lista de chamados recentes */}
        <View style={styles.ticketsList}>
          {/* Mapeia um array de 3 elementos para criar 3 cards de chamados */}
          {[1, 2, 3].map((_, index) => (
            // Card de chamado individual (clicável)
            <TouchableOpacity 
              key={index} // Key única para cada item da lista
              style={styles.ticketCard} // Estilo do card
              onPress={() => navigation.navigate('TicketDetails', { ticketId: `CD-${String(145 - index).padStart(3, '0')}` })} // Navega para detalhes do chamado
            >
              {/* Cabeçalho do card com ID e status */}
              <View style={styles.ticketHeader}>
                {/* ID do chamado formatado (ex: #CD-145) */}
                <Text style={styles.ticketId}>#CD-{String(145 - index).padStart(3, '0')}</Text>
                {/* Badge de status do chamado */}
                <View style={[styles.statusBadge, { backgroundColor: '#FBBC0420' }]}>
                  {/* Texto do status em amarelo */}
                  <Text style={[styles.statusText, { color: '#FBBC04' }]}>Pendente</Text>
                </View>
              </View>
              {/* Título/descrição do chamado */}
              <Text style={styles.ticketTitle}>Problema com impressora HP do 3º andar</Text>
              {/* Informações adicionais do chamado */}
              <View style={styles.ticketInfo}>
                {/* Categoria do chamado */}
                <Text style={styles.ticketCategory}>Hardware</Text>
                {/* Data e hora do chamado */}
                <Text style={styles.ticketDate}>Hoje • 14:30</Text>
              </View>
            </TouchableOpacity>
          ))}
        </View>
      </ScrollView>
    </SafeAreaView>
  );
}

// Objeto StyleSheet que define todos os estilos utilizados no componente
const styles = StyleSheet.create({
  // Estilo do container principal - ocupa toda a tela com fundo bege
  container: { flex: 1, backgroundColor: '#E8D5C4' },
  // Estilo do cabeçalho - layout horizontal com espaçamento entre elementos
  header: { flexDirection: 'row', justifyContent: 'space-between', alignItems: 'center', paddingHorizontal: 20, paddingVertical: 16, backgroundColor: '#E8D5C4' },
  // Estilo do título do cabeçalho (não usado atualmente)
  headerTitle: { fontSize: 22, fontWeight: 'bold', color: '#E67E22' },
  // Estilo do subtítulo do cabeçalho - texto pequeno em cinza
  headerSubtitle: { fontSize: 13, color: '#7F8C8D', marginTop: 2 },
  // Estilo do botão de menu (ícone de perfil)
  menuButton: { padding: 4 },
  // Estilo do overlay do modal - fundo escuro semitransparente
  modalOverlay: { flex: 1, backgroundColor: 'rgba(0,0,0,0.3)', justifyContent: 'flex-start', alignItems: 'flex-end', paddingTop: 60, paddingRight: 16 },
  // Estilo do container do menu de perfil - card branco com sombra
  profileMenuContainer: { backgroundColor: 'white', borderRadius: 12, minWidth: 200, shadowColor: '#000', shadowOffset: { width: 0, height: 4 }, shadowOpacity: 0.2, shadowRadius: 8, elevation: 8 },
  // Estilo de cada item do menu de perfil - layout horizontal com ícone e texto
  profileMenuItem: { flexDirection: 'row', alignItems: 'center', paddingHorizontal: 20, paddingVertical: 16, gap: 12 },
  // Estilo do texto dos itens do menu
  profileMenuText: { fontSize: 15, fontWeight: '600', color: '#2C3E50' },
  // Estilo da linha divisória entre itens do menu
  profileMenuDivider: { height: 1, backgroundColor: '#E1E8ED', marginHorizontal: 12 },
  // Estilo do ScrollView
  scrollView: { flex: 1 },
  // Estilo do conteúdo dentro do ScrollView - padding horizontal e inferior
  scrollContent: { paddingHorizontal: 16, paddingBottom: 24 },
  // Estilo da barra de ferramentas - layout horizontal com filtros
  toolbar: { flexDirection: 'row', alignItems: 'center', marginBottom: 16 },
  // Estilo do label "Período:" na toolbar
  toolbarLabel: { fontSize: 14, fontWeight: '600', color: '#2C3E50', marginRight: 12 },
  // Container dos botões de período - layout horizontal com espaçamento
  periodButtons: { flexDirection: 'row', gap: 8 },
  // Estilo padrão dos botões de período - fundo cinza claro com borda
  periodButton: { paddingHorizontal: 16, paddingVertical: 8, borderRadius: 8, backgroundColor: '#F8F9FA', borderWidth: 1.5, borderColor: '#E1E8ED' },
  // Estilo do botão de período quando ativo/selecionado - fundo laranja
  periodButtonActive: { backgroundColor: '#E67E22', borderColor: '#E67E22' },
  // Estilo do texto dos botões de período - cinza médio
  periodButtonText: { fontSize: 13, fontWeight: '600', color: '#7F8C8D' },
  // Estilo do texto do botão ativo - branco
  periodButtonTextActive: { color: 'white' },
  // Container dos cards de KPI - grid flexível com 2 colunas
  kpisContainer: { flexDirection: 'row', flexWrap: 'wrap', gap: 12, marginBottom: 24 },
  // Estilo individual de cada card de KPI - fundo branco com sombra
  kpiCard: { backgroundColor: 'white', borderRadius: 12, padding: 16, minWidth: '47%', flex: 1, shadowColor: '#000', shadowOffset: { width: 0, height: 2 }, shadowOpacity: 0.05, shadowRadius: 4, elevation: 3 },
  // Estilo do label do KPI (ex: "Total", "Pendentes")
  kpiLabel: { fontSize: 12, color: '#7F8C8D', marginBottom: 8 },
  // Estilo do valor numérico do KPI - número grande em negrito
  kpiValue: { fontSize: 28, fontWeight: 'bold', color: '#2C3E50', marginBottom: 4 },
  // Container do indicador de variação (delta) - layout horizontal
  deltaContainer: { flexDirection: 'row', alignItems: 'center' },
  // Estilo do texto de variação percentual
  deltaText: { fontSize: 12, fontWeight: '600', marginLeft: 2 },
  // Estilo dos títulos de seção - texto grande em negrito
  sectionTitle: { fontSize: 18, fontWeight: 'bold', color: '#2C3E50', marginBottom: 12 },
  // Grid de cards de ações rápidas - layout flexível em linha
  actionsGrid: { flexDirection: 'row', flexWrap: 'wrap', gap: 12, marginBottom: 24 },
  // Estilo de cada card de ação - centralizado com ícone e texto
  actionCard: { backgroundColor: 'white', borderRadius: 12, padding: 16, alignItems: 'center', flex: 1, minWidth: '30%', shadowColor: '#000', shadowOffset: { width: 0, height: 1 }, shadowOpacity: 0.05, shadowRadius: 2, elevation: 2 },
  // Estilo do título do card de ação
  actionTitle: { fontSize: 13, fontWeight: '600', color: '#2C3E50', marginTop: 8, textAlign: 'center' },
  // Estilo do subtítulo do card de ação - texto menor em cinza
  actionSubtitle: { fontSize: 11, color: '#7F8C8D', marginTop: 4, textAlign: 'center' },
  // Container da lista de chamados - espaçamento entre cards
  ticketsList: { gap: 12, marginBottom: 16 },
  // Estilo de cada card de chamado - fundo branco com sombra
  ticketCard: { backgroundColor: 'white', borderRadius: 12, padding: 16, shadowColor: '#000', shadowOffset: { width: 0, height: 1 }, shadowOpacity: 0.05, shadowRadius: 2, elevation: 2 },
  // Cabeçalho do card de chamado - ID e status lado a lado
  ticketHeader: { flexDirection: 'row', justifyContent: 'space-between', alignItems: 'center', marginBottom: 8 },
  // Estilo do ID do chamado - texto médio em cinza
  ticketId: { fontSize: 14, fontWeight: '600', color: '#7F8C8D' },
  // Estilo da badge de status - background colorido com padding
  statusBadge: { paddingHorizontal: 10, paddingVertical: 4, borderRadius: 8 },
  // Estilo do texto dentro da badge de status
  statusText: { fontSize: 11, fontWeight: '600' },
  // Estilo do título do chamado - texto médio em negrito
  ticketTitle: { fontSize: 15, fontWeight: '600', color: '#2C3E50', marginBottom: 8 },
  // Container das informações do chamado - categoria e data lado a lado
  ticketInfo: { flexDirection: 'row', justifyContent: 'space-between' },
  // Estilo da categoria do chamado - texto laranja em negrito
  ticketCategory: { fontSize: 12, color: '#E67E22', fontWeight: '600' },
  // Estilo da data do chamado - texto pequeno em cinza
  ticketDate: { fontSize: 12, color: '#7F8C8D' },
});