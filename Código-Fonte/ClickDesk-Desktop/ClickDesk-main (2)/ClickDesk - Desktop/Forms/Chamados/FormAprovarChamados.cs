
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClickDesk.Models;
using ClickDesk.Services.API;
using ClickDesk.Utils;
using ClickDesk.Components;
using ClickDesk.Forms;

namespace ClickDesk.Forms.Chamados
{
    /// <summary>
    /// Formulário para aprovar/gerenciar chamados (admin/tech).
    /// Interface moderna seguindo o padrão Web Clean.
    /// </summary>
    public partial class FormAprovarChamados : Form
    {
        // ===== PROPRIEDADES PARA PAGINAÇÃO =====
        private List<Ticket> todosTickets = new List<Ticket>();
        private List<Ticket> ticketsFiltrados = new List<Ticket>();
        private int currentPage = 1;
        private int pageSize = 10;
        private string searchText = "";

        /// <summary>
        /// Construtor do formulário.
        /// </summary>
        public FormAprovarChamados()
        {
            InitializeComponent();
            WireEvents();
            CarregarDadosFake();
            AtualizarVisualizacao();
        }

        /// <summary>
        /// Conecta os eventos dos controles.
        /// </summary>
        private void WireEvents()
        {
            // Window controls
            this.btnClose.Click += (s, e) => this.Close();
            this.btnMaximize.Click += (s, e) => this.WindowState = this.WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;
            this.btnMinimize.Click += (s, e) => this.WindowState = FormWindowState.Minimized;
            
            // Pagination
            this.btnPrev.Click += (s, e) => IrParaPagina(currentPage - 1);
            this.btnNext.Click += (s, e) => IrParaPagina(currentPage + 1);
            this.comboPageSize.SelectedIndexChanged += (s, e) => AlterarTamanhoPagina();
            
            // Search
            this.txtSearch.TextChanged += (s, e) => FiltrarTickets();
        }

        /// <summary>
        /// Carrega dados fake baseados no print fornecido.
        /// </summary>
        private void CarregarDadosFake()
        {
            todosTickets = new List<Ticket>
            {
                new Ticket 
                { 
                    Id = "CD-1124", 
                    Titulo = "Problema com impressora HP LaserJet", 
                    Descricao = "A impressora está apresentando erro de papel atolado constantemente, mesmo com a bandeja vazia.",
                    Categoria = "Hardware", 
                    Data = DateTime.Now.AddDays(-2), 
                    Prioridade = "Normal", 
                    Responsavel = "Não atribuído", 
                    Status = "Pendente" 
                },
                new Ticket 
                { 
                    Id = "CD-1125", 
                    Titulo = "Sistema lento após atualização", 
                    Descricao = "Após a última atualização do Windows, o sistema está extremamente lento para inicializar aplicações.",
                    Categoria = "Software", 
                    Data = DateTime.Now.AddDays(-1), 
                    Prioridade = "Urgente", 
                    Responsavel = "João Silva", 
                    Status = "Em Análise" 
                },
                new Ticket 
                { 
                    Id = "CD-1126", 
                    Titulo = "Acesso negado ao servidor", 
                    Descricao = "Não conseguindo acessar a pasta compartilhada no servidor. Mensagem de acesso negado.",
                    Categoria = "Rede", 
                    Data = DateTime.Now.AddHours(-3), 
                    Prioridade = "Normal", 
                    Responsavel = "Maria Santos", 
                    Status = "Aguardando" 
                },
                new Ticket 
                { 
                    Id = "CD-1127", 
                    Titulo = "Email não está enviando", 
                    Descricao = "Os emails ficam presos na caixa de saída e não são enviados. Testei com diferentes destinatários.",
                    Categoria = "Software", 
                    Data = DateTime.Now.AddHours(-1), 
                    Prioridade = "Urgente", 
                    Responsavel = "Não atribuído", 
                    Status = "Pendente" 
                },
                new Ticket 
                { 
                    Id = "CD-1128", 
                    Titulo = "Monitor piscando", 
                    Descricao = "O monitor do usuário está piscando intermitentemente, pode ser problema no cabo ou na placa de vídeo.",
                    Categoria = "Hardware", 
                    Data = DateTime.Now.AddMinutes(-30), 
                    Prioridade = "Normal", 
                    Responsavel = "Carlos Lima", 
                    Status = "Em Análise" 
                },
                new Ticket 
                { 
                    Id = "CD-1129", 
                    Titulo = "Backup falhou novamente", 
                    Descricao = "O backup automático falhou pela terceira vez esta semana. Precisa verificar o espaço em disco.",
                    Categoria = "Sistema", 
                    Data = DateTime.Now.AddMinutes(-15), 
                    Prioridade = "Urgente", 
                    Responsavel = "Ana Costa", 
                    Status = "Aguardando" 
                },
                new Ticket 
                { 
                    Id = "CD-1130", 
                    Titulo = "Licença do software expirou", 
                    Descricao = "A licença do Adobe Creative Suite expirou e precisa ser renovada urgentemente para o departamento de design.",
                    Categoria = "Software", 
                    Data = DateTime.Now.AddDays(-3), 
                    Prioridade = "Normal", 
                    Responsavel = "Pedro Oliveira", 
                    Status = "Em Análise" 
                },
                new Ticket 
                { 
                    Id = "CD-1131", 
                    Titulo = "Conexão VPN instável", 
                    Descricao = "A VPN está caindo constantemente, impedindo o trabalho remoto dos funcionários.",
                    Categoria = "Rede", 
                    Data = DateTime.Now.AddHours(-2), 
                    Prioridade = "Urgente", 
                    Responsavel = "Não atribuído", 
                    Status = "Pendente" 
                },
                new Ticket 
                { 
                    Id = "CD-1132", 
                    Titulo = "Teclado com teclas defeituosas", 
                    Descricao = "Várias teclas do teclado não estão funcionando corretamente, dificultando a digitação.",
                    Categoria = "Hardware", 
                    Data = DateTime.Now.AddHours(-4), 
                    Prioridade = "Normal", 
                    Responsavel = "Laura Ferreira", 
                    Status = "Aguardando" 
                },
                new Ticket 
                { 
                    Id = "CD-1133", 
                    Titulo = "Erro de autenticação no sistema", 
                    Descricao = "Usuários relatam erro de autenticação ao tentar fazer login no sistema principal da empresa.",
                    Categoria = "Sistema", 
                    Data = DateTime.Now.AddMinutes(-45), 
                    Prioridade = "Urgente", 
                    Responsavel = "Roberto Mendes", 
                    Status = "Em Análise" 
                },
                new Ticket 
                { 
                    Id = "CD-1134", 
                    Titulo = "Antivírus detectou ameaça", 
                    Descricao = "O antivírus detectou múltiplas ameaças na estação de trabalho e precisa de limpeza completa.",
                    Categoria = "Segurança", 
                    Data = DateTime.Now.AddDays(-1), 
                    Prioridade = "Urgente", 
                    Responsavel = "Não atribuído", 
                    Status = "Pendente" 
                },
                new Ticket 
                { 
                    Id = "CD-1135", 
                    Titulo = "Atualização de sistema pendente", 
                    Descricao = "Sistema operacional com atualizações pendentes há mais de 2 semanas, precisa ser atualizado.",
                    Categoria = "Sistema", 
                    Data = DateTime.Now.AddHours(-6), 
                    Prioridade = "Normal", 
                    Responsavel = "Fernanda Silva", 
                    Status = "Aguardando" 
                },
                new Ticket 
                { 
                    Id = "CD-1136", 
                    Titulo = "Configuração de novo usuário", 
                    Descricao = "Novo funcionário precisa de configuração completa de usuário no domínio e acesso aos sistemas.",
                    Categoria = "Usuário", 
                    Data = DateTime.Now.AddMinutes(-20), 
                    Prioridade = "Normal", 
                    Responsavel = "Gustavo Alves", 
                    Status = "Em Análise" 
                },
                new Ticket 
                { 
                    Id = "CD-1137", 
                    Titulo = "Disco rígido com erro", 
                    Descricao = "SMART do disco rígido está reportando erros. Backup urgente necessário antes da falha completa.",
                    Categoria = "Hardware", 
                    Data = DateTime.Now.AddHours(-8), 
                    Prioridade = "Urgente", 
                    Responsavel = "Não atribuído", 
                    Status = "Pendente" 
                },
                new Ticket 
                { 
                    Id = "CD-1138", 
                    Titulo = "Configuração de impressora rede", 
                    Descricao = "Nova impressora de rede precisa ser configurada e compartilhada para todos os departamentos.",
                    Categoria = "Hardware", 
                    Data = DateTime.Now.AddDays(-2), 
                    Prioridade = "Normal", 
                    Responsavel = "Isabela Rocha", 
                    Status = "Aguardando" 
                }
            };
        }

        /// <summary>
        /// Filtra os tickets baseado no texto de busca.
        /// </summary>
        private void FiltrarTickets()
        {
            searchText = txtSearch.Text.Trim().ToLower();
            
            if (string.IsNullOrEmpty(searchText))
            {
                ticketsFiltrados = todosTickets.ToList();
            }
            else
            {
                ticketsFiltrados = todosTickets.Where(t =>
                    t.Id.ToLower().Contains(searchText) ||
                    t.Titulo.ToLower().Contains(searchText) ||
                    t.Categoria.ToLower().Contains(searchText) ||
                    t.Status.ToLower().Contains(searchText) ||
                    (t.Responsavel ?? "").ToLower().Contains(searchText)
                ).ToList();
            }
            
            currentPage = 1; // Reset to first page
            AtualizarVisualizacao();
        }

        /// <summary>
        /// Altera o tamanho da página.
        /// </summary>
        private void AlterarTamanhoPagina()
        {
            if (int.TryParse(comboPageSize.SelectedItem?.ToString(), out int newSize))
            {
                pageSize = newSize;
                currentPage = 1; // Reset to first page
                AtualizarVisualizacao();
            }
        }

        /// <summary>
        /// Navega para uma página específica.
        /// </summary>
        private void IrParaPagina(int page)
        {
            var totalPages = (int)Math.Ceiling((double)ticketsFiltrados.Count / pageSize);
            
            if (page >= 1 && page <= totalPages)
            {
                currentPage = page;
                AtualizarVisualizacao();
            }
        }

        /// <summary>
        /// Atualiza a visualização dos cards e controles de paginação.
        /// </summary>
        private void AtualizarVisualizacao()
        {
            // Garantir que ticketsFiltrados está inicializado
            if (ticketsFiltrados == null || ticketsFiltrados.Count == 0)
            {
                ticketsFiltrados = todosTickets.ToList();
            }

            // Calcular paginação
            var totalItems = ticketsFiltrados.Count;
            var totalPages = Math.Max(1, (int)Math.Ceiling((double)totalItems / pageSize));
            var startIndex = (currentPage - 1) * pageSize;
            var endIndex = Math.Min(startIndex + pageSize - 1, totalItems - 1);
            
            // Garantir que currentPage está dentro dos limites
            if (currentPage > totalPages)
            {
                currentPage = Math.Max(1, totalPages);
                startIndex = (currentPage - 1) * pageSize;
                endIndex = Math.Min(startIndex + pageSize - 1, totalItems - 1);
            }

            // Obter tickets da página atual
            var ticketsNaPagina = ticketsFiltrados
                .Skip(startIndex)
                .Take(pageSize)
                .ToList();

            // Limpar e adicionar novos cards
            flowCards.Controls.Clear();
            
            foreach (var ticket in ticketsNaPagina)
            {
                var card = new ChamadoCard(ticket);
                card.Margin = new Padding(10);
                card.OnAprovar += (s, t) => AprovarTicket(t);
                card.OnRecusar += (s, t) => RecusarTicket(t);
                flowCards.Controls.Add(card);
            }

            // Atualizar labels de informação
            if (totalItems > 0)
            {
                lblRangeInfo.Text = $"Mostrando {startIndex + 1}–{endIndex + 1} de {totalItems} entradas";
            }
            else
            {
                lblRangeInfo.Text = "Nenhuma entrada encontrada";
            }
            
            lblPageIndex.Text = currentPage.ToString();

            // Atualizar estado dos botões
            btnPrev.Enabled = currentPage > 1;
            btnNext.Enabled = currentPage < totalPages;
            
            // Visual feedback for disabled buttons
            btnPrev.BackColor = btnPrev.Enabled ? Color.White : ColorTranslator.FromHtml("#F5F5F5");
            btnNext.BackColor = btnNext.Enabled ? Color.White : ColorTranslator.FromHtml("#F5F5F5");
            btnPrev.ForeColor = btnPrev.Enabled ? ColorTranslator.FromHtml("#1E2A22") : ColorTranslator.FromHtml("#CCCCCC");
            btnNext.ForeColor = btnNext.Enabled ? ColorTranslator.FromHtml("#1E2A22") : ColorTranslator.FromHtml("#CCCCCC");
        }

        /// <summary>
        /// Aprova um ticket.
        /// </summary>
        private async void AprovarTicket(Ticket ticket)
        {
            var result = MessageBox.Show(
                $"Deseja aprovar o chamado {ticket.Id}?\n\n{ticket.Titulo}", 
                "Confirmar Aprovação", 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    // Simular aprovação (aqui você chamaria o ChamadoService real)
                    ticket.Status = "Aprovado";
                    ticket.Responsavel = "Sistema"; // ou o usuário logado
                    
                    MessageBox.Show($"Chamado {ticket.Id} aprovado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // Atualizar visualização
                    AtualizarVisualizacao();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao aprovar chamado: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Recusa um ticket.
        /// </summary>
        private void RecusarTicket(Ticket ticket)
        {
            var result = MessageBox.Show(
                $"Deseja recusar o chamado {ticket.Id}?\n\n{ticket.Titulo}", 
                "Confirmar Recusa", 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    // Simular recusa
                    ticket.Status = "Recusado";
                    ticket.Responsavel = "Sistema"; // ou o usuário logado
                    
                    MessageBox.Show($"Chamado {ticket.Id} recusado.", "Chamado Recusado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // Atualizar visualização
                    AtualizarVisualizacao();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao recusar chamado: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }

    /// <summary>
    /// Classe que representa um ticket/chamado para aprovação.
    /// </summary>
    public class Ticket
    {
        public string Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Categoria { get; set; }
        public DateTime Data { get; set; }
        public string Prioridade { get; set; }
        public string Responsavel { get; set; }
        public string Status { get; set; }
        public string Solicitante { get; set; }
        public string Departamento { get; set; }
        public List<dynamic> Anexos { get; set; }
    }
}
