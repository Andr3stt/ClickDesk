# Banco de dados – ClickDesk (Azure SQL)

O ClickDesk utiliza **Microsoft Azure SQL Database** como sistema gerenciador de dados em produção. Os scripts de criação (`schema.sql`, `seed.sql`) devem ser versionados nesta pasta conforme forem concluídos.

## Papel do banco na arquitetura

- Armazena usuários, chamados, status, prioridades/criticidade, FAQ e feedback.
- É acessado **somente pela API REST** hospedada no Azure (não pelos clientes Web, Desktop ou Mobile).
- Garante que todas as plataformas exibem os mesmos dados após integração com a API.

## Integração com a API

1. A API lê a **connection string** das configurações do App Service (ou Key Vault).
2. Conexões utilizam **criptografia TLS** até o Azure SQL.
3. Operações de negócio (abrir chamado, listar, atualizar status) executam transações SQL ou comandos via ORM.
4. O resultado da classificação por IA é persistido no registro do chamado para consultas e filtros.

Para a redação completa destinada ao relatório do PIM, consulte [Integração API e Azure](../Documentacao%20do%20projeto/Integracao_API_e_Azure.md).

## Conteúdo esperado nesta pasta (entrega)

| Arquivo | Descrição |
|---------|-----------|
| `schema.sql` | Tabelas, chaves e índices |
| `seed.sql` | Dados iniciais para demonstração (usuários de teste, FAQs) |
| `diagrama-er.png` | Modelo entidade-relacionamento (opcional) |

*Substituir nomes de servidor e banco pelos recursos reais criados no portal Azure na documentação impressa.*
