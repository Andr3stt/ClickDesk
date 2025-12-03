# 📱 Guia de Navegação - ClickDesk Mobile

## 🎯 Estrutura de Navegação Implementada

O app agora possui navegação completa entre todas as telas!

---

## 🚀 Como Navegar Entre Telas

### 📍 Tela Atual: **Login**

A partir da tela de Login, você pode:

1. **Fazer Login** → Vai para `Dashboard` (usuário) ou `DashboardAdmin` (admin)
2. **Criar Conta** → Botão "Cadastre-se" vai para `Registro`
3. **Ver Termos** → Link nos termos vai para `Termos`

---

## 🔑 Navegação por Código

### No TelaLogin.js

Para navegar após login bem-sucedido:

```javascript
// Após login bem-sucedido
if (userData.role === 'admin') {
  navigation.replace('DashboardAdmin');
} else {
  navigation.replace('Dashboard');
}
```

Para ir para registro:

```javascript
// Botão de cadastro
navigation.navigate('Registro');
```

---

## 📋 Todas as Rotas Disponíveis

### 🔐 Autenticação
- `Login` - Tela de login
- `Registro` - Criar nova conta
- `Termos` - Termos de uso

### 👤 Usuário
- `Dashboard` - Dashboard do usuário
- `MeusChamados` - Lista de chamados do usuário
- `NovoChamado` - Criar novo chamado
- `DetalhesChamado` - Ver detalhes de um chamado
- `EditarPerfil` - Editar perfil do usuário

### 🔧 Compartilhadas
- `FAQ` - Perguntas frequentes
- `ListaChamados` - Lista geral de chamados
- `CriarPerfil` - Criar perfil completo

### 👨‍💼 Administrador
- `DashboardAdmin` - Dashboard do admin
- `ChamadosAdmin` - Gerenciar todos os chamados
- `AprovarChamados` - Aprovar chamados pendentes
- `FAQAdmin` - Gerenciar FAQ

---

## 💡 Exemplos de Uso

### 1. Navegar do Dashboard para Novo Chamado

```javascript
// No TelaDashboard.js
<TouchableOpacity onPress={() => navigation.navigate('NovoChamado')}>
  <Text>Novo Chamado</Text>
</TouchableOpacity>
```

### 2. Navegar para Detalhes passando dados

```javascript
// Passar ID do chamado
navigation.navigate('DetalhesChamado', { 
  chamadoId: 123,
  titulo: 'Problema no sistema'
});

// Na tela de destino, receber os dados:
const { chamadoId, titulo } = route.params;
```

### 3. Voltar para tela anterior

```javascript
// Botão de voltar personalizado
<TouchableOpacity onPress={() => navigation.goBack()}>
  <Text>Voltar</Text>
</TouchableOpacity>
```

### 4. Substituir tela atual (sem voltar)

```javascript
// Após logout, não permitir voltar
navigation.replace('Login');
```

### 5. Voltar para tela inicial

```javascript
// Voltar para o início da stack
navigation.popToTop();
```

---

## 🎨 Personalização de Headers

O header é configurado automaticamente com:
- Cor de fundo laranja (`Cores.brand`)
- Texto branco
- Botão de voltar

Para esconder o header em uma tela específica:

```javascript
// No NavegadorPrincipal.js
<Stack.Screen 
  name="Dashboard" 
  component={TelaDashboard}
  options={{ headerShown: false }}
/>
```

---

## 🔄 Fluxo de Navegação Recomendado

### Para Usuário:
```
Login → Dashboard → MeusChamados → DetalhesChamado
                 → NovoChamado
                 → FAQ
                 → EditarPerfil
```

### Para Admin:
```
Login → DashboardAdmin → ChamadosAdmin → DetalhesChamado
                      → AprovarChamados
                      → FAQAdmin
```

---

## 🛠️ Implementação Prática

### 1. Na Tela de Login (TelaLogin.js)

Encontre a função `handleLogin` e adicione após login bem-sucedido:

```javascript
const handleLogin = async () => {
  setCarregando(true);
  
  try {
    const response = await login(username, password);
    
    if (response.success) {
      // Salvar dados do usuário
      // await armazenarDados('userData', response.user);
      
      // Navegar baseado no tipo de usuário
      if (response.user.role === 'admin') {
        navigation.replace('DashboardAdmin');
      } else {
        navigation.replace('Dashboard');
      }
    }
  } catch (error) {
    Alert.alert('Erro', error.message);
  } finally {
    setCarregando(false);
  }
};
```

### 2. No Dashboard (TelaDashboard.js)

Adicione navegação aos cards:

```javascript
// Card de Novo Chamado
<TouchableOpacity 
  style={styles.card}
  onPress={() => navigation.navigate('NovoChamado')}
>
  <MaterialCommunityIcons name="plus-circle" size={48} color={Cores.brand} />
  <Text style={styles.cardTitle}>Novo Chamado</Text>
</TouchableOpacity>

// Card de Meus Chamados
<TouchableOpacity 
  style={styles.card}
  onPress={() => navigation.navigate('MeusChamados')}
>
  <MaterialCommunityIcons name="ticket" size={48} color={Cores.primary} />
  <Text style={styles.cardTitle}>Meus Chamados</Text>
</TouchableOpacity>

// Card de FAQ
<TouchableOpacity 
  style={styles.card}
  onPress={() => navigation.navigate('FAQ')}
>
  <MaterialCommunityIcons name="help-circle" size={48} color={Cores.info} />
  <Text style={styles.cardTitle}>FAQ</Text>
</TouchableOpacity>
```

### 3. Menu de Perfil

```javascript
// No menu dropdown
<TouchableOpacity onPress={() => navigation.navigate('EditarPerfil')}>
  <Text>Editar Perfil</Text>
</TouchableOpacity>

<TouchableOpacity onPress={() => navigation.replace('Login')}>
  <Text>Sair</Text>
</TouchableOpacity>
```

---

## 🧪 Testando a Navegação

1. **Recarregue o app** no Expo Go (pressione `r` no terminal)
2. **Você verá a tela de Login**
3. **Teste os botões de navegação** que já existem nas telas
4. **Use o botão voltar** do sistema ou do header

---

## ⚠️ Importante

- ✅ Navegação configurada e funcionando
- ✅ Todas as telas já criadas e importadas
- ✅ Headers personalizados
- ✅ Gesture handler habilitado

**Próximo passo:** Adicionar a lógica de navegação nos botões das telas existentes!

---

## 🔍 Como Ver a Estrutura de Navegação

No terminal do Metro Bundler, você pode pressionar:
- `shift+m` → More tools
- Ver logs de navegação

Ou adicione logs nas telas:

```javascript
useEffect(() => {
  console.log('Navegou para Dashboard');
}, []);
```

---

## 📞 Dica de Debug

Se a navegação não funcionar:

1. Verifique se `navigation` está sendo passado como prop
2. Verifique o nome exato da rota (case-sensitive)
3. Verifique se a tela está registrada no `NavegadorPrincipal.js`
4. Limpe o cache: `npx expo start -c`
