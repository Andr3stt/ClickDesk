# 📦 Guia de Migração - ClickDesk Mobile

## 🎯 Objetivo da Reorganização

Este documento descreve as mudanças realizadas na reorganização completa do projeto mobile ClickDesk, migrando de uma estrutura desorganizada em inglês para uma estrutura profissional 100% em PT-BR com integração completa com API Backend.

---

## 🔄 Estrutura Antiga vs Nova

### Estrutura Antiga (❌ Descontinuada)
```
Código-Fonte/ClickDesk-Mobile/
└── Mobile/
    └── Mobile/
        └── mobile/
            └── src/
                ├── screens/           # ❌ Todas as telas misturadas
                ├── navigation/
                ├── components/
                └── styles/
```

### Nova Estrutura (✅ Atual)
```
Código-Fonte/ClickDesk/Clickdesk - Mobile/
├── src/
│   ├── configuracao/              # ✅ Configurações centralizadas
│   ├── servicos/                  # ✅ Camada de serviços com API
│   ├── telas/                     # ✅ Telas organizadas por função
│   ├── componentes/               # ✅ Componentes reutilizáveis
│   ├── contextos/                 # ✅ Context API
│   ├── hooks/                     # ✅ Custom Hooks
│   ├── navegacao/                 # ✅ Navegação em PT-BR
│   ├── estilos/                   # ✅ Estilos globais
│   └── modelos/                   # ✅ Modelos de dados
├── package.json                   # ✅ Dependências do React Native/Expo
├── app.json                       # ✅ Configuração do Expo
├── .env.example                   # ✅ Exemplo de variáveis de ambiente
└── README.md                      # ✅ Documentação completa
```

---

## 📋 Mapeamento de Arquivos

### Telas de Autenticação

| Arquivo Antigo | Novo Arquivo | Localização |
|----------------|--------------|-------------|
| `screens/LoginScreen.js` | `TelaLogin.js` | `src/telas/autenticacao/` |
| `screens/RegisterScreen.js` | `TelaRegistro.js` | `src/telas/autenticacao/` |
| `screens/TermsScreen.js` | `TelaTermos.js` | `src/telas/autenticacao/` |
| `screens/LogoutScreen.js` | `TelaLogout.js` | `src/telas/autenticacao/` |
| `screens/ForgotPasswordScreen.js` | `TelaEsqueciSenha.js` | `src/telas/autenticacao/` |

### Telas de Usuário

| Arquivo Antigo | Novo Arquivo | Localização |
|----------------|--------------|-------------|
| `screens/DashboardScreen.js` | `TelaDashboard.js` | `src/telas/usuario/` |
| `screens/MyTicketScreen.js` | `TelaMeusChamados.js` | `src/telas/usuario/` |
| `screens/TicketDetailsScreen.js` | `TelaDetalhesChamado.js` | `src/telas/usuario/` |
| `screens/NewTicketScreen.js` | `TelaNovoChamado.js` | `src/telas/usuario/` |
| `screens/EditProfileScreen.js` | `TelaEditarPerfil.js` | `src/telas/usuario/` |
| `screens/CreateProfileScreen.js` | `TelaCriarPerfil.js` | `src/telas/usuario/` |

### Telas de Administrador

| Arquivo Antigo | Novo Arquivo | Localização |
|----------------|--------------|-------------|
| `screens/AdminDashboardScreen.js` | `TelaDashboardAdmin.js` | `src/telas/administrador/` |
| `screens/MyTicketsAdminScreen.js` | `TelaChamadosAdmin.js` | `src/telas/administrador/` |
| `screens/FAQAdminScreen.js` | `TelaFAQAdmin.js` | `src/telas/administrador/` |
| `screens/TicketApprovalScreen.js` | `TelaAprovacaoChamados.js` | `src/telas/administrador/` |

### Telas Compartilhadas

| Arquivo Antigo | Novo Arquivo | Localização |
|----------------|--------------|-------------|
| `screens/FAQScreen.js` | `TelaFAQ.js` | `src/telas/compartilhadas/` |
| `screens/TicketListScreen.js` | `TelaListaChamados.js` | `src/telas/compartilhadas/` |

### Navegação

| Arquivo Antigo | Novo Arquivo | Localização |
|----------------|--------------|-------------|
| `navigation/AppNavigator.js` | `NavegadorPrincipal.js` | `src/navegacao/` |
| ❌ Não existia | `RotaProtegida.js` | `src/navegacao/` |

---

## 🆕 Novos Arquivos Criados

### Configuração
- ✅ `src/configuracao/constantes.js` - Endpoints, enums e constantes da API
- ✅ `src/configuracao/ambiente.js` - Configurações de ambiente

### Serviços
- ✅ `src/servicos/api/clienteHttp.js` - Cliente Axios com JWT
- ✅ `src/servicos/api/autenticacaoService.js` - Serviço de autenticação
- ✅ `src/servicos/api/chamadosService.js` - Serviço de chamados
- ✅ `src/servicos/api/faqService.js` - Serviço de FAQs
- ✅ `src/servicos/api/iaService.js` - Serviço de IA
- ✅ `src/servicos/utilitarios/armazenamentoLocal.js` - Wrapper AsyncStorage
- ✅ `src/servicos/utilitarios/validadores.js` - Validações
- ✅ `src/servicos/utilitarios/formatadores.js` - Formatação de dados

### Modelos
- ✅ `src/modelos/Usuario.js` - Modelo de usuário
- ✅ `src/modelos/Chamado.js` - Modelo de chamado
- ✅ `src/modelos/Enums.js` - Enums e constantes

### Estilos
- ✅ `src/estilos/cores.js` - Paleta de cores
- ✅ `src/estilos/global.js` - Estilos globais reutilizáveis

### Navegação
- ✅ `src/navegacao/RotaProtegida.js` - HOC para rotas protegidas

---

## 🔧 Mudanças Principais

### 1. Mudança de localStorage para AsyncStorage
```javascript
// ❌ Antigo (Web)
localStorage.getItem('token');
localStorage.setItem('token', token);

// ✅ Novo (React Native)
import AsyncStorage from '@react-native-async-storage/async-storage';
await AsyncStorage.getItem('token');
await AsyncStorage.setItem('token', token);
```

### 2. Atualização de Imports
```javascript
// ❌ Antigo
import LoginScreen from '../screens/LoginScreen';

// ✅ Novo
import TelaLogin from '../telas/autenticacao/TelaLogin';
```

### 3. Cliente HTTP com JWT
```javascript
// Agora com interceptadores automáticos
import clienteHttp from '../servicos/api/clienteHttp';

// Token é adicionado automaticamente
const resposta = await clienteHttp.get('/api/chamados');
```

### 4. Gerenciamento de Estado
```javascript
// Usando utilitários centralizados
import { salvarToken, obterToken, removerToken } from '../servicos/utilitarios/armazenamentoLocal';

await salvarToken(token);
const token = await obterToken();
await removerToken();
```

---

## 📦 Dependências Atualizadas

### Removidas (Web)
```json
"react-dom": "^18.2.0"
"react-router-dom": "^6.20.0"
"react-scripts": "5.0.1"
```

### Adicionadas (React Native/Expo)
```json
"@expo/vector-icons": "^15.0.3"
"@react-navigation/native": "^6.1.9"
"@react-navigation/native-stack": "^6.9.17"
"@react-native-async-storage/async-storage": "^1.19.0"
"expo": "~54.0.0"
"react-native": "0.81.5"
```

---

## 🚀 Passos para Migração (Se Necessário)

### 1. Atualizar Imports nas Telas
Se você criou telas customizadas, atualize os imports:
```javascript
// Atualize imports de navegação
navigation.navigate('Login') → navigation.navigate('Login')  // Mantém igual

// Atualize imports de utilitários
import { obterToken } from '../servicos/utilitarios/armazenamentoLocal';
```

### 2. Adaptar Código Web para React Native
```javascript
// ❌ Web
window.location.href = '/login';

// ✅ React Native
navigation.replace('Login');
```

### 3. Usar Novos Estilos Globais
```javascript
import { EstilosGlobais } from '../estilos/global';
import { CoresPrincipais } from '../estilos/cores';

<View style={EstilosGlobais.cartao}>
  <Text style={EstilosGlobais.titulo}>Título</Text>
</View>
```

---

## ✅ Checklist de Migração

Para desenvolvedores que precisam migrar código customizado:

- [ ] Mover arquivos para a nova estrutura de pastas
- [ ] Renomear arquivos seguindo o padrão PT-BR (Tela*.js)
- [ ] Atualizar imports de telas
- [ ] Substituir localStorage por AsyncStorage
- [ ] Usar clienteHttp ao invés de fetch direto
- [ ] Aplicar estilos globais quando possível
- [ ] Adicionar comentários em PT-BR
- [ ] Testar navegação entre telas
- [ ] Validar integração com API Backend

---

## 🐛 Problemas Comuns e Soluções

### Erro: Cannot find module '../screens/...'
**Solução**: Atualizar imports para a nova estrutura:
```javascript
// De:
import TelaLogin from '../screens/LoginScreen';
// Para:
import TelaLogin from '../telas/autenticacao/TelaLogin';
```

### Erro: localStorage is not defined
**Solução**: Usar AsyncStorage:
```javascript
import { salvarToken, obterToken } from '../servicos/utilitarios/armazenamentoLocal';
```

### Erro: Axios 401 Unauthorized
**Solução**: Verificar se o token está sendo salvo e o clienteHttp está sendo usado:
```javascript
import clienteHttp from '../servicos/api/clienteHttp';
// clienteHttp já adiciona o token automaticamente
```

---

## 📚 Recursos Adicionais

- [Documentação React Native](https://reactnative.dev/)
- [Documentação Expo](https://docs.expo.dev/)
- [React Navigation](https://reactnavigation.org/)
- [AsyncStorage](https://react-native-async-storage.github.io/async-storage/)

---

## 📞 Suporte

Para dúvidas sobre a nova estrutura:
1. Consulte o README.md principal
2. Verifique os comentários nos arquivos de código
3. Consulte a equipe de desenvolvimento

---

**Data da Migração**: Dezembro 2024  
**Versão**: 1.0.0  
**Status**: ✅ Completo
