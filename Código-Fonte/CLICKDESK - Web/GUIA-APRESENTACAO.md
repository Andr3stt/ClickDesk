# 🎯 ClickDesk - Guia Rápido para Apresentação

## 🚀 Como Iniciar o Sistema

```powershell
# No diretório do projeto, execute:
Start-Process "index.html"
```

Ou simplesmente **clique duas vezes** no arquivo `index.html`

---

## 🔐 Credenciais de Acesso

### 👤 Usuário Comum
- **Usuário:** `vini` | **Senha:** `123`
- **Usuário:** `usuario` | **Senha:** `123`

### 🔧 Administrador/Técnico
- **Usuário:** `admin` | **Senha:** `admin`
- **Usuário:** `tecnico` | **Senha:** `tecnico`

---

## 📋 Fluxo de Demonstração Sugerido

### 1️⃣ **Login e Autenticação** (2 min)
- Mostrar tela de login
- Fazer login como usuário comum (`vini` / `123`)
- Mostrar redirecionamento automático para dashboard

### 2️⃣ **Dashboard do Usuário** (3 min)
- KPIs com métricas de chamados
- Filtro de período (Hoje, 7 dias, 30 dias)
- Chamados recentes com status
- Ações rápidas (Novo Chamado, Meus Chamados, FAQ)

### 3️⃣ **Criar Novo Chamado** (2 min)
- Preencher formulário completo
- Upload de anexos
- Categorias e prioridades
- Confirmar criação

### 4️⃣ **Meus Chamados** (2 min)
- Lista de chamados do usuário
- Filtros por status e categoria
- Visualizar detalhes de um chamado
- Sistema de avaliação (estrelas)

### 5️⃣ **Logout e Login Admin** (1 min)
- Fazer logout
- Login como admin (`admin` / `admin`)

### 6️⃣ **Dashboard Administrativo** (4 min)
- Visão geral de todos os chamados
- Gráficos de status e categorias
- Chamados pendentes de aprovação
- Métricas de tempo médio

### 7️⃣ **Aprovação de Chamados** (2 min)
- Lista de chamados aguardando aprovação
- Aprovar ou rejeitar chamados
- Atribuir técnico responsável

### 8️⃣ **Gestão de Chamados** (2 min)
- Ver todos os chamados do sistema
- Filtros avançados
- Editar status e prioridade
- Adicionar comentários

### 9️⃣ **FAQ** (1 min)
- Perguntas frequentes para usuários
- Modo admin para gerenciar FAQ

### 🔟 **Extras** (1 min)
- Perfil do usuário
- Sistema de notificações
- Design responsivo

---

## ✨ Recursos Implementados

✅ **Autenticação completa** com tipos de usuário
✅ **Dashboards** para usuário e admin
✅ **CRUD completo** de chamados
✅ **Sistema de aprovação** de chamados
✅ **Filtros e busca** em todas as listagens
✅ **Upload de arquivos** nos chamados
✅ **Sistema de avaliação** por estrelas
✅ **FAQ** para usuários e admin
✅ **Perfis customizáveis**
✅ **Design moderno** e responsivo
✅ **Navegação fluida** entre todas as telas
✅ **LocalStorage** para persistência de dados

---

## 🎨 Telas Implementadas (16 telas)

1. **Login** - Autenticação de usuários
2. **Registro** - Cadastro de novos usuários
3. **Esqueci Senha** - Recuperação de senha
4. **Termos de Uso** - Aceite de termos
5. **Perfil Usuário** - Edição de perfil
5.6. **Perfil Admin** - Edição de perfil admin
6. **Dashboard** - Visão geral do usuário
7. **Novo Chamado** - Criação de tickets
7.5. **Detalhes Chamado** - Visualização de ticket
8. **Lista Chamados** - Todos os chamados
9. **Meus Chamados** - Chamados do usuário
10. **FAQ** - Perguntas frequentes
10.1. **FAQ Admin** - Gestão de FAQ
11. **Dashboard Admin** - Painel administrativo
12. **Aprovação Chamados** - Aprovar/rejeitar tickets
13. **Meus Chamados Admin** - Gestão de todos os tickets
14. **Detalhes Admin** - Visualização detalhada
14.1. **Editar Chamado** - Edição de ticket
15. **Logout** - Sair do sistema

---

## 🐛 Troubleshooting

### Problema: Links não funcionam
**Solução:** Recarregue a página (F5) - todos os links usam encodeURI para lidar com espaços

### Problema: Dados não aparecem
**Solução:** Os dados são mockados (simulados). Se não aparecerem, abra o console (F12) para ver logs

### Problema: Login não redireciona
**Solução:** Verifique se usou as credenciais corretas e recarregue a página

---

## 💡 Dicas para Apresentação

1. **Abra o console do navegador (F12)** antes de começar para mostrar os logs
2. **Teste o fluxo completo** uma vez antes da apresentação
3. **Mantenha as credenciais à mão** para login rápido
4. **Destaque as transições suaves** entre as telas
5. **Mostre os filtros funcionando** nos dashboards
6. **Demonstre a diferença** entre usuário comum e admin

---

## 🎯 Pontos Fortes para Destacar

- ✅ Interface moderna e intuitiva
- ✅ Separação clara de perfis (usuário/admin)
- ✅ Sistema completo de gestão de chamados
- ✅ Feedback visual em todas as ações
- ✅ Navegação consistente e fácil
- ✅ Design responsivo e acessível
- ✅ Código organizado e padronizado

---

**Boa apresentação! 🚀**
