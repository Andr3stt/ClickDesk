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
  TextInput,      // Input de texto
  Alert,          // Alertas
  Modal,          // Modal
  Animated,       // Animações
} from 'react-native';

// Importação de ícones do Material Design
import { MaterialCommunityIcons } from '@expo/vector-icons';
import { Cores } from '../../estilos/cores';

/**
 * Componente principal da tela de detalhes do chamado
 * @param {object} route - Contém os parâmetros passados pela navegação
 * @param {object} navigation - Objeto de navegação para transitar entre telas
 */
export default function TicketDetailsScreen({ route, navigation }) {
  // Extrai o ID do chamado dos parâmetros da rota, usa '001' como fallback
  const { id, userType } = route.params || { id: '001', userType: 'user' };
  
  // Estado para controlar loading dos botões da IA
  const [isLoadingIA, setIsLoadingIA] = useState(false);
  // Estado para controlar modal de confirmação
  const [showConfirmModal, setShowConfirmModal] = useState(false);
  // Animação para o modal
  const [scaleAnim] = useState(new Animated.Value(0));

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
   * FUNÇÃO: Abrir modal de confirmação
   */
  const handleResolveWithIA = () => {
    setShowConfirmModal(true);
    Animated.spring(scaleAnim, {
      toValue: 1,
      friction: 5,
      tension: 40,
      useNativeDriver: true,
    }).start();
  };

  /**
   * FUNÇÃO: Confirmar resolução com IA
   * Envia requisição para marcar o chamado como resolvido pela IA
   */
  const confirmResolveWithIA = async () => {
    setIsLoadingIA(true);
    try {
      // Simula chamada API
      await new Promise(resolve => setTimeout(resolve, 1000));
      
      // Fecha modal com animação
      Animated.timing(scaleAnim, {
        toValue: 0,
        duration: 200,
        useNativeDriver: true,
      }).start(() => {
        setShowConfirmModal(false);
        Alert.alert(
          'Sucesso! 🎉',
          'Chamado resolvido com sucesso pela IA!',
          [
            {
              text: 'OK',
              onPress: () => navigation.goBack()
            }
          ]
        );
      });
    } catch (error) {
      Alert.alert('Erro', 'Erro ao resolver chamado com IA.');
    } finally {
      setIsLoadingIA(false);
    }
  };

  /**
   * FUNÇÃO: Cancelar resolução
   */
  const cancelResolve = () => {
    Animated.timing(scaleAnim, {
      toValue: 0,
      duration: 200,
      useNativeDriver: true,
    }).start(() => setShowConfirmModal(false));
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

  /**
   * FUNÇÃO: Editar chamado
   * Navega para edição do chamado
   */
  const handleEditar = () => {
    Alert.alert('Editar Chamado', 'Funcionalidade em desenvolvimento');
  };

  /**
   * FUNÇÃO: Resolver chamado
   * Marca o chamado como resolvido
   */
  const handleResolver = () => {
    Alert.alert(
      'Resolver Chamado',
      'Tem certeza que deseja marcar este chamado como resolvido?',
      [
        { text: 'Cancelar', style: 'cancel' },
        {
          text: 'Resolver',
          onPress: () => {
            Alert.alert('Sucesso', 'Chamado resolvido!');
            navigation.goBack();
          },
        },
      ]
    );
  };

  return (
    <SafeAreaView style={styles.container}>
      <StatusBar barStyle="dark-content" backgroundColor={Cores.background} />
      
      <View style={styles.header}>
        <TouchableOpacity 
          style={styles.backButton}
          onPress={() => navigation.goBack()}
        >
          <MaterialCommunityIcons name="arrow-left" size={24} color={Cores.textoPrincipal} />
        </TouchableOpacity>
        
        <Text style={styles.headerTitle}>Detalhes do Chamado</Text>
        
        <TouchableOpacity 
          style={styles.headerActionButton}
          onPress={handleEditar}
        >
          <MaterialCommunityIcons name="pencil" size={18} color={Cores.brand} />
          <Text style={styles.headerActionText}>EDITAR</Text>
        </TouchableOpacity>
      </View>

      <ScrollView
        style={styles.scrollView}
        contentContainerStyle={styles.scrollContent}
        showsVerticalScrollIndicator={false}
      >
        <View style={styles.card}>
          <View style={styles.cardHeader}>
            <View style={styles.idContainer}>
              <Text style={styles.ticketId}>#{ticket.id}</Text>
              <View style={[styles.statusBadge, { backgroundColor: getStatusColor(ticket.status) + '20' }]}>
                <Text style={[styles.statusText, { color: getStatusColor(ticket.status) }]}>
                  {ticket.status}
                </Text>
              </View>
            </View>
            <View style={styles.dateChip}>
              <MaterialCommunityIcons name="calendar" size={16} color={Cores.textoSecundario} />
              <Text style={styles.dateText}>{ticket.date}</Text>
            </View>
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

        {/* Card de Sugestão da IA - Só aparece para usuários comuns */}
        {userType !== 'admin' && userType !== 'tech' && (
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
              <Text style={styles.iaButtonSecondaryText}>Aguardar atendimento</Text>
            </TouchableOpacity>
          </View>
        </View>
        )}

      </ScrollView>

      {/* Modal de Confirmação Animado */}
      <Modal
        visible={showConfirmModal}
        transparent
        animationType="fade"
        onRequestClose={cancelResolve}
      >
        <View style={styles.modalOverlay}>
          <Animated.View 
            style={[
              styles.modalContent,
              { transform: [{ scale: scaleAnim }] }
            ]}
          >
            <Animated.View style={[styles.modalIconContainer, { transform: [{ scale: scaleAnim }] }]}>
              <View style={styles.iconCircle}>
                <MaterialCommunityIcons name="robot-happy" size={70} color="#FFF" />
              </View>
              <View style={styles.sparkleContainer}>
                <MaterialCommunityIcons name="sparkles" size={24} color={Cores.brand} style={styles.sparkle1} />
                <MaterialCommunityIcons name="sparkles" size={20} color={Cores.brand} style={styles.sparkle2} />
                <MaterialCommunityIcons name="sparkles" size={18} color={Cores.brand} style={styles.sparkle3} />
              </View>
            </Animated.View>
            
            <Text style={styles.modalTitle}>🎉 Resolver com IA?</Text>
            <Text style={styles.modalMessage}>
              A IA analisou o problema e sugeriu uma solução. Deseja marcar este chamado como resolvido pela inteligência artificial?
            </Text>
            
            <View style={styles.modalButtons}>
              <TouchableOpacity 
                style={styles.modalButtonCancel}
                onPress={cancelResolve}
                disabled={isLoadingIA}
              >
                <Text style={styles.modalButtonCancelText}>Cancelar</Text>
              </TouchableOpacity>
              
              <TouchableOpacity 
                style={styles.modalButtonConfirm}
                onPress={confirmResolveWithIA}
                disabled={isLoadingIA}
              >
                {isLoadingIA ? (
                  <Text style={styles.modalButtonConfirmText}>Processando...</Text>
                ) : (
                  <>
                    <MaterialCommunityIcons name="check-circle" size={20} color="#FFF" />
                    <Text style={styles.modalButtonConfirmText}>Confirmar</Text>
                  </>
                )}
              </TouchableOpacity>
            </View>
          </Animated.View>
        </View>
      </Modal>
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  container: { 
    flex: 1, 
    backgroundColor: Cores.background,
  },
  header: { 
    flexDirection: 'row', 
    alignItems: 'center', 
    justifyContent: 'space-between', 
    paddingHorizontal: 20, 
    paddingVertical: 16,
    backgroundColor: Cores.background,
    borderBottomWidth: 0,
  },
  backButton: { 
    padding: 8,
  },
  headerTitle: { 
    fontSize: 18, 
    fontWeight: 'bold', 
    color: Cores.textoPrincipal,
    flex: 1,
    marginLeft: 8,
  },
  headerActions: {
    flexDirection: 'row',
    gap: 8,
  },
  headerActionButton: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 10,
    paddingVertical: 6,
    borderRadius: 6,
    borderWidth: 1.5,
    borderColor: Cores.brand,
    backgroundColor: '#FFFFFF',
    gap: 4,
  },
  headerActionText: {
    fontSize: 11,
    fontWeight: 'bold',
    color: Cores.brand,
  },
  resolverButton: {
    backgroundColor: Cores.brand,
    borderColor: Cores.brand,
  },
  resolverButtonText: {
    color: '#FFFFFF',
  },
  scrollView: { flex: 1 },
  scrollContent: { 
    paddingHorizontal: 16, 
    paddingTop: 16,
    paddingBottom: 24,
  },
  card: { 
    backgroundColor: '#FFFFFF', 
    borderRadius: 12, 
    padding: 20, 
    marginBottom: 16,
    shadowColor: '#000', 
    shadowOffset: { width: 0, height: 2 }, 
    shadowOpacity: 0.1, 
    shadowRadius: 8, 
    elevation: 3,
  },
  cardHeader: { 
    flexDirection: 'row', 
    justifyContent: 'space-between', 
    alignItems: 'center', 
    marginBottom: 20, 
    paddingBottom: 16, 
    borderBottomWidth: 1, 
    borderBottomColor: '#E8E8E8',
  },
  idContainer: { 
    flexDirection: 'row', 
    alignItems: 'center', 
    gap: 10,
  },
  ticketId: { 
    fontSize: 20, 
    fontWeight: 'bold', 
    color: Cores.brand,
  },
  statusBadge: { 
    paddingHorizontal: 12, 
    paddingVertical: 6, 
    borderRadius: 6,
  },
  statusText: { 
    fontSize: 12, 
    fontWeight: '700',
  },
  dateChip: { 
    flexDirection: 'row', 
    alignItems: 'center', 
    gap: 6, 
    paddingHorizontal: 12, 
    paddingVertical: 6, 
    backgroundColor: Cores.background, 
    borderRadius: 6,
  },
  dateText: { 
    fontSize: 12, 
    fontWeight: '600', 
    color: Cores.textoSecundario,
  },
  detailsGrid: { 
    gap: 18, 
    marginBottom: 20,
  },
  detailRow: { 
    flexDirection: 'row', 
    justifyContent: 'space-between', 
    alignItems: 'flex-start',
  },
  fullRow: { 
    flexDirection: 'column', 
    alignItems: 'flex-start',
  },
  detailLabel: { 
    fontSize: 13, 
    fontWeight: '600', 
    color: Cores.textoSecundario, 
    marginBottom: 4,
  },
  detailValue: { 
    fontSize: 14, 
    fontWeight: '600', 
    color: Cores.textoPrincipal, 
    flex: 1, 
    textAlign: 'right',
  },
  titleValue: { 
    fontSize: 15, 
    fontWeight: 'bold',
  },
  categoryBadge: { 
    paddingHorizontal: 10, 
    paddingVertical: 6, 
    borderRadius: 6,
  },
  categoryText: { 
    fontSize: 12, 
    fontWeight: '700',
  },
  descriptionValue: { 
    fontSize: 14, 
    color: Cores.textoPrincipal, 
    lineHeight: 22, 
    marginTop: 8,
  },
  actions: { flexDirection: 'row', gap: 12, paddingTop: 16, borderTopWidth: 1, borderTopColor: '#E1E8ED' },
  backButtonAction: { flex: 1, paddingVertical: 14, borderRadius: 10, borderWidth: 1.5, borderColor: '#E1E8ED', alignItems: 'center', backgroundColor: 'white' },
  backButtonText: { fontSize: 15, fontWeight: '600', color: '#7F8C8D' },
  refreshButton: { flex: 1, flexDirection: 'row', paddingVertical: 14, borderRadius: 10, alignItems: 'center', justifyContent: 'center', backgroundColor: '#E67E22', shadowColor: '#E67E22', shadowOffset: { width: 0, height: 2 }, shadowOpacity: 0.3, shadowRadius: 4, elevation: 4, gap: 6 },
  refreshButtonText: { fontSize: 15, fontWeight: 'bold', color: 'white' },
  
  // Estilos do Card de IA
  iaCard: { 
    backgroundColor: Cores.brand + '10', 
    borderRadius: 12, 
    padding: 20, 
    marginBottom: 16, 
    borderWidth: 2, 
    borderColor: Cores.brand, 
    shadowColor: '#000', 
    shadowOffset: { width: 0, height: 2 }, 
    shadowOpacity: 0.1, 
    shadowRadius: 6, 
    elevation: 3,
  },
  iaHeader: { 
    flexDirection: 'row', 
    alignItems: 'center', 
    gap: 10, 
    marginBottom: 16,
  },
  iaTitle: { 
    fontSize: 18, 
    fontWeight: 'bold', 
    color: Cores.brand,
  },
  iaContent: { 
    backgroundColor: '#FFFFFF', 
    borderRadius: 8, 
    padding: 16, 
    marginBottom: 16,
  },
  iaSuggestionTitle: { 
    fontSize: 14, 
    fontWeight: '600', 
    color: Cores.textoPrincipal, 
    marginBottom: 12, 
    lineHeight: 20,
  },
  iaStep: { 
    flexDirection: 'row', 
    marginBottom: 10, 
    paddingLeft: 4,
  },
  iaStepNumber: { 
    fontSize: 14, 
    fontWeight: 'bold', 
    color: Cores.brand, 
    marginRight: 8, 
    minWidth: 20,
  },
  iaStepText: { 
    fontSize: 14, 
    color: Cores.textoPrincipal, 
    lineHeight: 22, 
    flex: 1,
  },
  iaSuggestionFooter: { 
    fontSize: 13, 
    fontStyle: 'italic', 
    color: Cores.textoSecundario, 
    marginTop: 12, 
    lineHeight: 20,
  },
  // Estilos do Modal
  modalOverlay: {
    flex: 1,
    backgroundColor: 'rgba(0, 0, 0, 0.6)',
    justifyContent: 'center',
    alignItems: 'center',
    padding: 20,
  },
  modalContent: {
    backgroundColor: '#FFFFFF',
    borderRadius: 20,
    padding: 30,
    width: '90%',
    maxWidth: 400,
    alignItems: 'center',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 10 },
    shadowOpacity: 0.3,
    shadowRadius: 20,
    elevation: 10,
  },
  modalIconContainer: {
    marginBottom: 24,
    position: 'relative',
    alignItems: 'center',
    justifyContent: 'center',
  },
  iconCircle: {
    width: 120,
    height: 120,
    borderRadius: 60,
    backgroundColor: Cores.brand,
    justifyContent: 'center',
    alignItems: 'center',
    shadowColor: Cores.brand,
    shadowOffset: { width: 0, height: 8 },
    shadowOpacity: 0.4,
    shadowRadius: 12,
    elevation: 10,
  },
  sparkleContainer: {
    position: 'absolute',
    width: 160,
    height: 160,
  },
  sparkle1: {
    position: 'absolute',
    top: 10,
    right: 10,
  },
  sparkle2: {
    position: 'absolute',
    bottom: 15,
    left: 5,
  },
  sparkle3: {
    position: 'absolute',
    top: 5,
    left: 15,
  },
  modalTitle: {
    fontSize: 24,
    fontWeight: 'bold',
    color: Cores.textoPrincipal,
    marginBottom: 12,
    textAlign: 'center',
  },
  modalMessage: {
    fontSize: 16,
    color: Cores.textoSecundario,
    textAlign: 'center',
    lineHeight: 24,
    marginBottom: 30,
  },
  modalButtons: {
    flexDirection: 'row',
    gap: 12,
    width: '100%',
  },
  modalButtonCancel: {
    flex: 1,
    paddingVertical: 14,
    paddingHorizontal: 20,
    borderRadius: 10,
    backgroundColor: Cores.background,
    borderWidth: 2,
    borderColor: Cores.textoSecundario + '30',
    alignItems: 'center',
  },
  modalButtonCancelText: {
    fontSize: 16,
    fontWeight: '600',
    color: Cores.textoSecundario,
  },
  modalButtonConfirm: {
    flex: 1,
    paddingVertical: 14,
    paddingHorizontal: 20,
    borderRadius: 10,
    backgroundColor: Cores.brand,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    gap: 8,
    shadowColor: Cores.brand,
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.3,
    shadowRadius: 8,
    elevation: 6,
  },
  modalButtonConfirmText: {
    fontSize: 16,
    fontWeight: 'bold',
    color: '#FFFFFF',
  },
  iaActions: { 
    gap: 10,
  },
  iaButtonPrimary: { 
    flexDirection: 'row', 
    paddingVertical: 14, 
    borderRadius: 8, 
    alignItems: 'center', 
    justifyContent: 'center', 
    backgroundColor: Cores.brand, 
    gap: 8,
  },
  iaButtonPrimaryText: { 
    fontSize: 15, 
    fontWeight: 'bold', 
    color: '#FFFFFF',
  },
  iaButtonSecondary: { 
    flexDirection: 'row', 
    paddingVertical: 14, 
    borderRadius: 8, 
    alignItems: 'center', 
    justifyContent: 'center', 
    backgroundColor: '#FFFFFF', 
    borderWidth: 2, 
    borderColor: Cores.brand, 
    gap: 8,
  },
  iaButtonSecondaryText: { 
    fontSize: 15, 
    fontWeight: '600', 
    color: Cores.brand,
  },
});