# 📱 Telas do ClickDesk Mobile

Este diretório contém todas as telas da aplicação, organizadas por funcionalidade.

## 📂 Estrutura

```
telas/
├── autenticacao/        # Telas de autenticação e acesso
│   ├── TelaLogin.js     # ✅ Login com JWT
│   ├── TelaRegistro.js  # Registro de novo usuário
│   ├── TelaTermos.js    # Termos de uso
│   └── TelaLogout.js    # Logout
│
├── usuario/             # Telas do usuário comum
│   ├── TelaDashboard.js           # Dashboard principal
│   ├── TelaMeusChamados.js        # Lista de chamados do usuário
│   ├── TelaDetalhesChamado.js    # Detalhes de um chamado
│   └── TelaNovoChamado.js         # Criar novo chamado
│
├── administrador/       # Telas exclusivas de admin/tech
│   ├── TelaDashboardAdmin.js      # Dashboard administrativo
│   ├── TelaChamadosAdmin.js       # Gerenciar todos os chamados
│   └── TelaFAQAdmin.js            # Gerenciar FAQs
│
└── compartilhadas/      # Telas compartilhadas por todos
    ├── TelaFAQ.js              # Visualizar FAQs
    └── TelaListaChamados.js    # Lista geral de chamados
```

## 🎯 Padrões de Desenvolvimento

### 1. Nomenclatura
- Use nomes descritivos em português: `TelaLogin`, `TelaDashboard`
- Sempre comece com "Tela" seguido do nome da funcionalidade

### 2. Estrutura do Arquivo
```javascript
/**
 * Tela de [Nome]
 * 
 * Descrição do que a tela faz
 */

import React, { useState, useEffect } from 'react';
import { View, Text, StyleSheet } from 'react-native';
// Importar serviços necessários
// Importar estilos e componentes

export default function Tela[Nome]({ navigation, route }) {
  // Estados
  // Efeitos
  // Handlers
  // Renderização
  
  return (
    <View style={styles.container}>
      {/* Conteúdo */}
    </View>
  );
}

// Estilos
const styles = StyleSheet.create({
  // ...
});
```

### 3. Serviços de API
Sempre use os serviços organizados:
```javascript
import autenticacaoService from '../../servicos/api/autenticacaoService';
import chamadosService from '../../servicos/api/chamadosService';
```

### 4. Validação
Use os validadores centralizados:
```javascript
import { validarEmail, validarCampoObrigatorio } from '../../servicos/utilitarios/validadores';
```

### 5. Formatação
Use os formatadores centralizados:
```javascript
import { formatarData, formatarTempoRelativo } from '../../servicos/utilitarios/formatadores';
```

### 6. Estilos
Use as cores e estilos globais:
```javascript
import { Cores } from '../../estilos/cores';
import { EstilosGlobais } from '../../estilos/global';
```

## 📝 Notas

- Todas as telas devem ser comentadas em PT-BR
- Sempre trate erros adequadamente
- Use ActivityIndicator para estados de carregamento
- Implemente navegação corretamente
- Teste em dispositivos reais

## 🔄 Status de Implementação

| Tela | Status | Notas |
|------|--------|-------|
| TelaLogin | ✅ Implementada | Com serviço de autenticação |
| TelaRegistro | ⏳ Pendente | Baseada em RegisterScreen.js |
| TelaTermos | ⏳ Pendente | Baseada em TermsScreen.js |
| TelaLogout | ⏳ Pendente | Baseada em LogoutScreen.js |
| TelaDashboard | ⏳ Pendente | Baseada em DashboardScreen.js |
| TelaMeusChamados | ⏳ Pendente | Baseada em MyTicketScreen.js |
| TelaDetalhesChamado | ⏳ Pendente | Baseada em TicketDetailsScreen.js |
| TelaNovoChamado | ⏳ Pendente | Baseada em NewTicketScreen.js |
| TelaDashboardAdmin | ⏳ Pendente | Baseada em AdminDashboardScreen.js |
| TelaChamadosAdmin | ⏳ Pendente | Baseada em MyTicketsAdminScreen.js |
| TelaFAQAdmin | ⏳ Pendente | Baseada em FAQAdminScreen.js |
| TelaFAQ | ⏳ Pendente | Baseada em FAQScreen.js |
| TelaListaChamados | ⏳ Pendente | Baseada em TicketListScreen.js |
