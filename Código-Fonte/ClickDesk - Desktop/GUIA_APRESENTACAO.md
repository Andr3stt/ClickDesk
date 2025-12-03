# Guia de Apresentação - ClickDesk Desktop

## 📋 Roteiro de Demonstração

Este guia apresenta um roteiro estruturado para demonstrar as funcionalidades do sistema ClickDesk Desktop.

---

## 🎯 Preparação (5 minutos antes)

### Checklist
- [ ] API backend rodando (`http://localhost:8080`)
- [ ] Aplicação compilada e pronta
- [ ] Usuários de teste criados:
  - `admin` / `admin123` (ADMIN)
  - `tech` / `tech123` (TECH)
  - `user` / `user123` (USER)
- [ ] Alguns chamados de exemplo no sistema
- [ ] FAQs cadastradas para demonstração

---

## 📝 Roteiro de Apresentação (20-30 minutos)

### 1️⃣ Introdução (2 minutos)

**Falar:**
> "O ClickDesk é um sistema de helpdesk desenvolvido para gerenciar chamados de suporte técnico. Esta é a versão desktop, desenvolvida em C# com Windows Forms, que integra com nossa API REST."

**Mostrar:**
- Tela de login
- Mencionar tecnologias: .NET Framework 4.8, Newtonsoft.Json

---

### 2️⃣ Autenticação (3 minutos)

**Demonstrar:**

1. **Tela de Login**
   - Mostrar design (fundo escuro, painel central branco)
   - Campos: usuário e senha
   - Validações (tentar logar sem preencher)

2. **Login bem-sucedido**
   - Fazer login como `user` / `user123`
   - Mostrar redirecionamento para dashboard

**Falar:**
> "O sistema utiliza autenticação JWT. O token é armazenado em memória e enviado em todas as requisições."

---

### 3️⃣ Dashboard do Usuário (3 minutos)

**Demonstrar:**

1. **Layout**
   - Sidebar com menu de navegação
   - Área de conteúdo principal
   - Cards de estatísticas

2. **Estatísticas**
   - Total de chamados
   - Chamados abertos
   - Chamados resolvidos
   - Chamados escalados

3. **Lista de Chamados**
   - Últimos chamados do usuário
   - Duplo clique para ver detalhes

**Falar:**
> "O dashboard mostra uma visão geral dos seus chamados. As estatísticas são atualizadas em tempo real da API."

---

### 4️⃣ Criação de Chamado com IA (5 minutos) ⭐

**Demonstrar:**

1. **Abrir FormNovoChamado**
   - Clicar em "Novo Chamado"

2. **Preencher dados**
   - Título: "Computador não liga"
   - Categoria: Hardware
   - Descrição: "Meu computador não está ligando. Ao apertar o botão de power, nenhuma luz acende e não há nenhum som."

3. **Mostrar integração IA**
   - Clicar em "Enviar"
   - Mostrar solução da IA (se resolver)
   - Demonstrar opções de feedback:
     - "Sim, resolveu" → Chamado fechado
     - "Não resolveu" → Pergunta se quer escalar

**Falar:**
> "A grande diferença do ClickDesk é a integração com IA. Quando você cria um chamado, a IA analisa a descrição e tenta resolver automaticamente. Se for severidade baixa ou média, ela propõe uma solução. O usuário pode dar feedback e, se não resolver, escala para um técnico humano."

---

### 5️⃣ Meus Chamados (3 minutos)

**Demonstrar:**

1. **Abrir FormMeusChamados**
   - Clicar em "Meus Chamados" na sidebar

2. **Filtros**
   - Filtrar por status
   - Filtrar por categoria
   - Buscar por texto

3. **Detalhes do Chamado**
   - Duplo clique em um chamado
   - Mostrar informações completas
   - Solução da IA (se houver)
   - Seção de comentários
   - Adicionar um comentário

**Falar:**
> "Aqui o usuário pode acompanhar todos os seus chamados, filtrar por status e ver o histórico completo de interações."

---

### 6️⃣ FAQ - Base de Conhecimento (2 minutos)

**Demonstrar:**

1. **Abrir FormFAQ**
   - Clicar em "FAQ" na sidebar

2. **Busca**
   - Digitar um termo de busca
   - Mostrar resultados filtrados

3. **Visualização**
   - Clicar em uma FAQ
   - Mostrar resposta completa

**Falar:**
> "A base de conhecimento permite que usuários encontrem respostas sem precisar abrir chamados, reduzindo a carga de suporte."

---

### 7️⃣ Perfil do Usuário (2 minutos)

**Demonstrar:**

1. **Abrir FormPerfil**
   - Clicar em "Meu Perfil"

2. **Visualizar informações**
   - Avatar com iniciais
   - Dados do usuário
   - Badge de role

3. **Editar perfil**
   - Clicar em "Editar Perfil"
   - Mostrar campos editáveis
   - Seção de alteração de senha

---

### 8️⃣ Funcionalidades Admin/Tech (5 minutos)

**Demonstrar:**

1. **Fazer logout**
   - Clicar em "Sair"
   - Confirmar

2. **Login como Admin**
   - Logar como `admin` / `admin123`
   - Mostrar dashboard diferente

3. **Todos os Chamados**
   - Mostrar lista completa
   - Filtros avançados

4. **Aprovar Chamados**
   - Abrir FormAprovarChamados
   - Demonstrar ações:
     - Atribuir a si
     - Marcar como resolvido
     - Escalar
     - Fechar

5. **Gerenciar FAQ (Admin)**
   - Abrir FormFAQAdmin
   - Criar nova FAQ
   - Editar existente

6. **Criar Usuário (Admin)**
   - Abrir FormCriarUsuario
   - Mostrar seleção de role

**Falar:**
> "Administradores e técnicos têm acesso a funcionalidades adicionais. Podem ver todos os chamados, atribuir a si mesmos, gerenciar a base de conhecimento e criar novos usuários."

---

### 9️⃣ Conclusão (2 minutos)

**Resumir:**

> "O ClickDesk Desktop oferece:
> - Interface moderna e intuitiva
> - Integração completa com API REST
> - Resolução automática com IA
> - Sistema de permissões (USER, TECH, ADMIN)
> - Base de conhecimento pesquisável
> - Gestão completa de chamados"

**Perguntas frequentes:**

**P: A IA resolve todos os chamados?**
> "Não, a IA tenta resolver chamados de severidade baixa e média. Chamados críticos são automaticamente escalados para técnicos."

**P: Os dados são persistidos onde?**
> "Todos os dados são armazenados no servidor via API. A aplicação desktop é apenas uma interface."

**P: É possível usar offline?**
> "Não, a aplicação requer conexão com a API para funcionar."

---

## 🎨 Destaques Visuais

### Paleta de Cores
- **Azul (#2563eb)**: Ações principais, links
- **Verde (#10b981)**: Sucesso, resolvido
- **Vermelho (#ef4444)**: Erro, crítico
- **Amarelo (#f59e0b)**: Aviso, em andamento
- **Cinza (#1f2937)**: Sidebar, backgrounds

### Layout Consistente
- Sidebar fixa de 260px
- Cards de estatísticas com barra colorida
- DataGridViews estilizados
- Botões com hover effects

---

## ❓ Perguntas para Avaliadores

Se solicitarem demonstração específica:

### "Mostre o fluxo completo de um chamado"
1. Login → Dashboard → Novo Chamado
2. Preencher e enviar
3. IA tenta resolver
4. Feedback do usuário
5. Visualizar em Meus Chamados
6. (Como admin) Gerenciar em Aprovar Chamados

### "Mostre a integração com a API"
1. Abrir Network/Fiddler para ver requisições
2. Mostrar headers com token JWT
3. Demonstrar resposta JSON da API

### "Mostre tratamento de erros"
1. Desligar API (simular)
2. Mostrar mensagem de erro de conexão
3. Tentar acessar recurso sem permissão

---

## 📊 Métricas para Mencionar

- **14 telas** implementadas
- **6 serviços** de API
- **7 modelos** de dados
- **3 níveis** de permissão
- Integração com **IA** para resolução automática
- Código **bem comentado** para manutenibilidade

---

## 🏁 Checklist Final

Antes de terminar a apresentação:

- [ ] Mostrou login/logout
- [ ] Demonstrou criação de chamado
- [ ] Mostrou integração com IA
- [ ] Navegou pelas principais telas
- [ ] Demonstrou funcionalidades admin
- [ ] Respondeu perguntas

---

**Boa apresentação! 🎯**
