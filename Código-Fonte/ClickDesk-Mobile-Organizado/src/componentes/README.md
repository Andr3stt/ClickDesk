# 🧩 Componentes Reutilizáveis

Este diretório contém componentes reutilizáveis organizados por categoria.

## 📂 Estrutura

```
componentes/
├── comum/              # Componentes comuns e genéricos
│   ├── Botao.js       # Botão customizável
│   ├── Input.js       # Input de texto customizável
│   └── Cartao.js      # Card/Container genérico
│
├── layout/             # Componentes de layout
│   ├── Cabecalho.js   # Header da aplicação
│   ├── MenuLateral.js # Menu lateral/drawer
│   └── Rodape.js      # Footer
│
└── chamados/           # Componentes específicos de chamados
    ├── CartaoChamado.js      # Card de chamado
    ├── ListaChamados.js      # Lista de chamados
    └── FormularioChamado.js  # Formulário de chamado
```

## 🎯 Diretrizes

### 1. Componentes Devem Ser
- **Reutilizáveis**: Podem ser usados em múltiplos lugares
- **Configuráveis**: Aceitam props para customização
- **Documentados**: Comentados em PT-BR
- **Testáveis**: Fáceis de testar isoladamente

### 2. Padrão de Componente
```javascript
/**
 * Componente [Nome]
 * 
 * Descrição do que o componente faz
 * 
 * @param {Object} props - Propriedades do componente
 * @param {string} props.prop1 - Descrição da prop1
 */
export default function Componente({ prop1, prop2 }) {
  return (
    // JSX
  );
}
```

### 3. Uso de Props
Sempre valide e forneça valores padrão:
```javascript
export default function Botao({ 
  titulo = 'Botão', 
  onPress, 
  tipo = 'primary',
  desabilitado = false 
}) {
  // ...
}
```

## 📝 Status

Todos os componentes estão pendentes de implementação.
Implemente conforme necessário durante o desenvolvimento das telas.
