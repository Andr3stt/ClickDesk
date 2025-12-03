/**
 * ========================================
 * TELA DE DETALHES DO CHAMADO
 * ========================================
 * Exibe informações completas de um chamado específico
 * Inclui card com sugestões da IA para resolução automática
 */

// Importação do React e hook useState para gerenciar estado
import React, { useState } from 'react';

// Importação dos componentes nativos do React Native
import {
  View,           // Container básico
  Text,           // Texto
  StyleSheet,     // Estilos
  SafeAreaView,   // Área segura (evita notch/status bar)
  ScrollView,     // Área rolável
  TouchableOpacity, // Botão tocável
  StatusBar,      // Barra de status do sistema
} from 'react-native';

// Importação de ícones do Material Design
import { MaterialCommunityIcons } from '@expo/vector-icons';

/**
 * Componente principal da tela de detalhes do chamado
 * @param {object} route - Contém os parâmetros passados pela navegação
 * @param {object} navigation - Objeto de navegação para transitar entre telas
 */
export default function TicketDetailsScreen({ route, navigation }) {
  // Extrai o ID do chamado dos parâmetros da rota, usa '001' como fallback
  const { id } = route.params || { id: '001' };
  
  // Estado para controlar loading dos botões da IA
  // Estado para controlar loading dos botões da IA
  const [isLoadingIA, setIsLoadingIA] = useState(false);

  /**
   * DADOS MOCKADOS DO CHAMADO
   * Em produção, estes dados viriam de uma chamada API usando o ID
   * Estrutura contém todas as informações do chamado
   */
  const ticket = {
    id: id || '001',                    // ID único do chamado
    title: 'Impressora não funciona',   // Título/assunto
    status: 'Aberto',                   // Status atual (Aberto, Em Andamento, etc)
    date: '17/11/2025',                 // Data de abertura
    requester: 'João Silva',            // Nome do solicitante
    department: 'TI',                   // Departamento
    category: 'Hardware',               // Categoria do problema
    description: 'A impressora do 2º andar não está imprimindo documentos. Tentei reiniciar mas o problema persiste. Preciso imprimir relatórios urgentes.', // Descrição detalhada
  };

  /**
   * SUGESTÃO DA IA
   * Objeto contendo a análise e recomendações da IA para resolver o problema
   * Em produção, viria de uma API de Machine Learning
   */
  const iaSuggestion = {
    title: 'Com base na análise do problema relatado, recomendamos verificar os seguintes itens:', // Cabeçalho
    steps: [  // Array com passos recomendados
      'Realizar limpeza de arquivos temporários no disco rígido',
      'Verificar se há atualizações pendentes do sistema operacional',
      'Executar uma varredura de malware/vírus',
      'Verificar se o disco está com mais de 80% de uso',
    ],
    footer: 'Essas ações podem resolver o problema de lentidão sem necessidade de intervenção técnica.', // Texto de rodapé
  };

  /**
   * FUNÇÃO: Resolver chamado com IA
   * Envia requisição para marcar o chamado como resolvido pela IA
   * Simula um delay de 1 segundo para representar chamada API
   */
  const handleResolveWithIA = async () => {
    setIsLoadingIA(true); // Ativa loading
    try {
      // Simula chamada API com delay de 1 segundo
      await new Promise(resolve => setTimeout(resolve, 1000));
      
      // Exibe mensagem de sucesso
      alert('Chamado resolvido com sucesso pela IA!');
      
      // Volta para tela anterior
      navigation.goBack();
    } catch (error) {
      // Em caso de erro, exibe mensagem
      alert('Erro ao resolver chamado com IA.');
    } finally {
      // Sempre desativa loading ao final
      setIsLoadingIA(false);
    }
  };

  /**
   * FUNÇÃO: Encaminhar para técnico
   * Envia requisição para atribuir o chamado a um técnico
   * Simula um delay de 1 segundo para representar chamada API
   */
  const handleWaitTechnician = async () => {
    setIsLoadingIA(true); // Ativa loading
    try {
      // Simula chamada API com delay de 1 segundo
      await new Promise(resolve => setTimeout(resolve, 1000));
      
      // Exibe mensagem de sucesso
      alert('Chamado encaminhado ao técnico com sucesso!');
      
      // Volta para tela anterior
      navigation.goBack();
    } catch (error) {
      // Em caso de erro, exibe mensagem
      alert('Erro ao encaminhar chamado.');
    } finally {
      // Sempre desativa loading ao final
      setIsLoadingIA(false);
    }
  };

  /**
   * FUNÇÃO: Obter cor do status
   * Retorna a cor correspondente ao status do chamado
   * @param {string} status - Status do chamado
   * @returns {string} Código hexadecimal da cor
   */
  const getStatusColor = (status) => {
    const colors = {
      'Aberto': '#FBBC04',          // Amarelo
      'Em Andamento': '#4285F4',    // Azul
      'Aguardando': '#9E9E9E',      // Cinza
      'Resolvido': '#34A853',       // Verde
    };
    return colors[status] || '#9E9E9E'; // Cinza como fallback
  };

  /**
   * FUNÇÃO: Obter cor da categoria
   * Retorna a cor correspondente à categoria do chamado
   * @param {string} category - Categoria do problema
   * @returns {string} Código hexadecimal da cor
   */
  const getCategoryColor = (category) => {
    const colors = {
      'Hardware': '#E67E22',   // Laranja
      'Software': '#3498DB',   // Azul claro
      'Rede': '#9B59B6',       // Roxo
      'Acesso': '#E74C3C',     // Vermelho
      'Outros': '#95A5A6',     // Cinza
    };
    return colors[category] || '#95A5A6'; // Cinza como fallback
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
          <View style={styles.cardHeader}>
            <View style={styles.idContainer}>
              <Text style={styles.ticketId}>CD-{ticket.id}</Text>
              <View style={[styles.statusBadge, { backgroundColor: getStatusColor(ticket.status) + '20' }]}>
                <Text style={[styles.statusText, { color: getStatusColor(ticket.status) }]}>
                  {ticket.status}
                </Text>
              </View>
            </View>
            <TouchableOpacity style={styles.dateChip}>
              <MaterialCommunityIcons name="calendar" size={16} color="#7F8C8D" />
              <Text style={styles.dateText}>{ticket.date}</Text>
            </TouchableOpacity>
          </View>

          <View style={styles.detailsGrid}>
            <View style={styles.detailRow}>
              <Text style={styles.detailLabel}>Solicitante</Text>
              <Text style={styles.detailValue}>{ticket.requester}</Text>
            </View>

            <View style={styles.detailRow}>
              <Text style={styles.detailLabel}>Departamento</Text>
              <Text style={styles.detailValue}>{ticket.department}</Text>
            </View>

            <View style={styles.detailRow}>
              <Text style={styles.detailLabel}>Título</Text>
              <Text style={[styles.detailValue, styles.titleValue]}>{ticket.title}</Text>
            </View>

            <View style={styles.detailRow}>
              <Text style={styles.detailLabel}>Categoria</Text>
              <View style={[styles.categoryBadge, { backgroundColor: getCategoryColor(ticket.category) + '20' }]}>
                <Text style={[styles.categoryText, { color: getCategoryColor(ticket.category) }]}>
                  {ticket.category}
                </Text>
              </View>
            </View>

            <View style={[styles.detailRow, styles.fullRow]}>
              <Text style={styles.detailLabel}>Descrição</Text>
              <Text style={styles.descriptionValue}>{ticket.description}</Text>
            </View>
          </View>

          <View style={styles.actions}>
            <TouchableOpacity style={styles.backButtonAction} onPress={() => navigation.goBack()}>
              <Text style={styles.backButtonText}>Voltar</Text>
            </TouchableOpacity>
            <TouchableOpacity style={styles.refreshButton}>
              <MaterialCommunityIcons name="refresh" size={18} color="white" />
              <Text style={styles.refreshButtonText}>Atualizar</Text>
            </TouchableOpacity>
          </View>
        </View>

        {/* Card de Sugestão da IA */}
        <View style={styles.iaCard}>
          <View style={styles.iaHeader}>
            <MaterialCommunityIcons name="robot" size={24} color="#E67E22" />
            <Text style={styles.iaTitle}>Sugestão da IA</Text>
          </View>

          <View style={styles.iaContent}>
            <Text style={styles.iaSuggestionTitle}>{iaSuggestion.title}</Text>
            {iaSuggestion.steps.map((step, index) => (
              <View key={index} style={styles.iaStep}>
                <Text style={styles.iaStepNumber}>{index + 1}.</Text>
                <Text style={styles.iaStepText}>{step}</Text>
              </View>
            ))}
            <Text style={styles.iaSuggestionFooter}>{iaSuggestion.footer}</Text>
          </View>

          <View style={styles.iaActions}>
            <TouchableOpacity 
              style={styles.iaButtonPrimary} 
              onPress={handleResolveWithIA}
              disabled={isLoadingIA}
            >
              <MaterialCommunityIcons name="check-circle" size={18} color="white" />
              <Text style={styles.iaButtonPrimaryText}>Resolvido com IA</Text>
            </TouchableOpacity>
            <TouchableOpacity 
              style={styles.iaButtonSecondary} 
              onPress={handleWaitTechnician}
              disabled={isLoadingIA}
            >
              <MaterialCommunityIcons name="clock-outline" size={18} color="#E67E22" />
              <Text style={styles.iaButtonSecondaryText}>Esperar Técnico</Text>
            </TouchableOpacity>
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
  cardHeader: { flexDirection: 'row', justifyContent: 'space-between', alignItems: 'center', marginBottom: 24, paddingBottom: 16, borderBottomWidth: 1, borderBottomColor: '#E1E8ED' },
  idContainer: { flexDirection: 'row', alignItems: 'center', gap: 12 },
  ticketId: { fontSize: 18, fontWeight: 'bold', color: '#2C3E50' },
  statusBadge: { paddingHorizontal: 12, paddingVertical: 6, borderRadius: 8 },
  statusText: { fontSize: 12, fontWeight: '600' },
  dateChip: { flexDirection: 'row', alignItems: 'center', gap: 6, paddingHorizontal: 12, paddingVertical: 6, backgroundColor: '#F8F9FA', borderRadius: 8 },
  dateText: { fontSize: 12, fontWeight: '600', color: '#7F8C8D' },
  detailsGrid: { gap: 16, marginBottom: 24 },
  detailRow: { flexDirection: 'row', justifyContent: 'space-between', alignItems: 'flex-start' },
  fullRow: { flexDirection: 'column', alignItems: 'flex-start' },
  detailLabel: { fontSize: 13, fontWeight: '600', color: '#7F8C8D', marginBottom: 4 },
  detailValue: { fontSize: 14, fontWeight: '600', color: '#2C3E50', flex: 1, textAlign: 'right' },
  titleValue: { fontSize: 15, fontWeight: 'bold' },
  categoryBadge: { paddingHorizontal: 10, paddingVertical: 5, borderRadius: 8 },
  categoryText: { fontSize: 12, fontWeight: '600' },
  descriptionValue: { fontSize: 14, color: '#2C3E50', lineHeight: 22, marginTop: 8 },
  actions: { flexDirection: 'row', gap: 12, paddingTop: 16, borderTopWidth: 1, borderTopColor: '#E1E8ED' },
  backButtonAction: { flex: 1, paddingVertical: 14, borderRadius: 10, borderWidth: 1.5, borderColor: '#E1E8ED', alignItems: 'center', backgroundColor: 'white' },
  backButtonText: { fontSize: 15, fontWeight: '600', color: '#7F8C8D' },
  refreshButton: { flex: 1, flexDirection: 'row', paddingVertical: 14, borderRadius: 10, alignItems: 'center', justifyContent: 'center', backgroundColor: '#E67E22', shadowColor: '#E67E22', shadowOffset: { width: 0, height: 2 }, shadowOpacity: 0.3, shadowRadius: 4, elevation: 4, gap: 6 },
  refreshButtonText: { fontSize: 15, fontWeight: 'bold', color: 'white' },
  
  // Estilos do Card de IA
  iaCard: { backgroundColor: '#FFF9F0', borderRadius: 16, padding: 20, marginTop: 16, borderWidth: 2, borderColor: '#E67E22', shadowColor: '#E67E22', shadowOffset: { width: 0, height: 2 }, shadowOpacity: 0.15, shadowRadius: 6, elevation: 4 },
  iaHeader: { flexDirection: 'row', alignItems: 'center', gap: 10, marginBottom: 16 },
  iaTitle: { fontSize: 18, fontWeight: 'bold', color: '#E67E22' },
  iaContent: { backgroundColor: 'rgba(255, 255, 255, 0.7)', borderRadius: 12, padding: 16, marginBottom: 16 },
  iaSuggestionTitle: { fontSize: 14, fontWeight: '600', color: '#2C3E50', marginBottom: 12, lineHeight: 20 },
  iaStep: { flexDirection: 'row', marginBottom: 10, paddingLeft: 4 },
  iaStepNumber: { fontSize: 14, fontWeight: 'bold', color: '#E67E22', marginRight: 8, minWidth: 20 },
  iaStepText: { fontSize: 14, color: '#2C3E50', lineHeight: 22, flex: 1 },
  iaSuggestionFooter: { fontSize: 13, fontStyle: 'italic', color: '#7F8C8D', marginTop: 12, lineHeight: 20 },
  iaActions: { gap: 10 },
  iaButtonPrimary: { flexDirection: 'row', paddingVertical: 14, borderRadius: 10, alignItems: 'center', justifyContent: 'center', backgroundColor: '#E67E22', gap: 8, shadowColor: '#E67E22', shadowOffset: { width: 0, height: 2 }, shadowOpacity: 0.3, shadowRadius: 4, elevation: 4 },
  iaButtonPrimaryText: { fontSize: 15, fontWeight: 'bold', color: 'white' },
  iaButtonSecondary: { flexDirection: 'row', paddingVertical: 14, borderRadius: 10, alignItems: 'center', justifyContent: 'center', backgroundColor: 'white', borderWidth: 2, borderColor: '#E67E22', gap: 8 },
  iaButtonSecondaryText: { fontSize: 15, fontWeight: '600', color: '#E67E22' },
});