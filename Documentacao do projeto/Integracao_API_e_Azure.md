# Integração do ClickDesk com a API e o banco de dados no Azure

Este documento descreve, em linguagem adequada ao relatório do PIM IV, **como as três aplicações do ClickDesk se conectam à API REST** e **como essa API persiste e consulta dados no Microsoft Azure SQL Database**. O foco é a arquitetura e o fluxo de integração; não é necessário detalhar implementação linha a linha do back-end na apresentação.

---

## 1. Visão geral da arquitetura

O ClickDesk adota uma arquitetura **cliente–servidor** em três camadas lógicas:

1. **Camada de apresentação** — três clientes independentes que compartilham o mesmo contrato de API:
   - **Web** (HTML, CSS e JavaScript), executada no navegador;
   - **Desktop** (C# / Windows Forms), instalada na estação do usuário;
   - **Mobile** (React Native / Expo), executada em smartphones.

2. **Camada de serviços (API REST)** — aplicação central hospedada na nuvem (Azure), responsável por autenticação, regras de negócio, chamados, FAQ, usuários e **classificação de criticidade/prioridade com apoio de IA**.

3. **Camada de dados** — **Azure SQL Database**, onde ficam usuários, chamados, histórico, prioridades definidas pela IA e demais entidades modeladas no diagrama de classes.

Nenhum cliente acessa o banco diretamente. Toda leitura e gravação passam pela API, o que centraliza segurança, validação e auditoria.

---

## 2. Como cada cliente se conecta à API

### 2.1 Contrato comum: REST sobre HTTP/HTTPS

A comunicação utiliza **JSON** no corpo das requisições e respostas. Os verbos HTTP seguem o padrão REST (por exemplo: `POST` para criar chamado, `GET` para listar, `PUT` para atualizar status).

A URL base da API é configurável por ambiente:

| Cliente | Onde se configura | Desenvolvimento | Produção (exemplo) |
|--------|-------------------|-----------------|---------------------|
| Web | scripts ou variável de ambiente | `http://localhost:8080` | URL pública do App Service no Azure |
| Desktop | `App.config` (`ApiBaseUrl`) | `http://localhost:8080` | mesma URL HTTPS da API |
| Mobile | `.env` / `REACT_APP_API_URL` | `http://localhost:8080` (ou IP do emulador) | URL HTTPS da API |

Em produção, a equipe utiliza **HTTPS** entre clientes e API, eliminando exposição de credenciais e tokens em texto claro na rede.

### 2.2 Autenticação (JWT)

O fluxo de login é padronizado nos três clientes:

1. O usuário envia credenciais para o endpoint de autenticação (por exemplo, `POST /auth/login`).
2. A API valida usuário e senha contra o **Azure SQL** e devolve um **token JWT**.
3. Os clientes armazenam o token (sessão no Desktop, `AsyncStorage` no Mobile; na Web, após integração completa, armazenamento seguro de sessão) e enviam o header `Authorization: Bearer <token>` em todas as requisições subsequentes.
4. Endpoints administrativos e de técnico validam o **perfil (role)** contido no token ou consultado na base.

O **Mobile** já implementa esse padrão por meio de um cliente HTTP com interceptadores que anexam o JWT automaticamente. O **Desktop** concentra as chamadas em serviços (`AuthService`, `ChamadoService`, `IAService`) que utilizam um `HttpClient` compartilhado. A **Web** deve seguir o mesmo contrato ao substituir o uso atual de `localStorage` apenas para protótipo.

### 2.3 Principais grupos de endpoints (visão funcional)

Estes grupos estão alinhados ao modelo do projeto e ao `ApiConfig` do Desktop:

| Área | Função para o usuário | Exemplos de rota |
|------|------------------------|------------------|
| Autenticação | Login, registro, logout | `/auth/login`, `/auth/register` |
| Chamados | Abrir, listar, detalhar, feedback | `/api/chamados`, `/api/chamados/meus` |
| IA | Definir criticidade e sugerir solução | `/api/ia/assist` |
| FAQ | Consulta e gestão (admin/técnico) | `/api/faqs` |
| Usuários / perfil | Dados do perfil e alteração de senha | `/api/users/profile` |

Na abertura de chamado, o cliente envia título, categoria, departamento e descrição. A API persiste o registro no Azure SQL e aciona o módulo de IA para **atribuir criticidade/prioridade** (requisitos RF003, RF008 e RF009 do backlog), retornando o chamado já classificado para exibição nas três interfaces.

---

## 3. Integração da API com o banco de dados no Azure

### 3.1 Papel do Azure SQL Database

O **Azure SQL Database** é o repositório relacional oficial do ClickDesk. Ele armazena, entre outros:

- credenciais e perfis de usuário (com papéis USER, TECH, ADMIN);
- chamados, status, datas e histórico;
- campo de **prioridade/criticidade** preenchido ou validado pelo fluxo de IA;
- registros de FAQ e feedback.

A API é o **único componente** que mantém conexão com o banco em produção. Isso atende ao requisito não funcional de criação e uso centralizado do banco (RNF002) e evita que a aplicação Desktop, a Web ou o Mobile precisem abrir firewall ou connection string.

### 3.2 Como a API se conecta ao Azure SQL (conceitual)

1. **Provisionamento**: cria-se um servidor lógico e um banco no Azure SQL (região definida pela equipe, por exemplo Brazil South).
2. **Connection string**: a API recebe a string de conexão via configuração do ambiente no **Azure App Service** (Application Settings) ou **Azure Key Vault**, sem commitar segredos no GitHub.
3. **Driver e protocolo**: a API utiliza provedor **ADO.NET** ou ORM (Entity Framework, conforme implementação do back-end) com **conexão criptografada (TLS)** ao Azure SQL.
4. **Operações**: cada endpoint da API traduz a requisição HTTP em comandos SQL transacionais (inserir chamado, consultar lista por usuário, atualizar status).
5. **IA e persistência**: após o processamento do texto do chamado, o resultado da criticidade é **gravado na mesma transação ou em atualização imediata** na tabela de chamados, permitindo filtros e relatórios consistentes em todas as plataformas.

### 3.3 Segurança e rede no Azure

- **Firewall do Azure SQL**: liberam-se apenas IPs do App Service (ou regra de serviços Azure) e IPs da equipe para desenvolvimento.
- **Identidade**: em cenários avançados, pode-se usar **Managed Identity** do App Service para autenticar no SQL sem senha em texto na configuração (opcional no relatório como evolução).
- **Backup e disponibilidade**: o Azure SQL oferece backup automático e SLA de disponibilidade, citados no relatório como benefício da escolha da nuvem para o PIM.

---

## 4. Fluxo integrado: abertura de chamado com criticidade (IA)

Descrição textual para o relatório ou para derivar diagrama de sequência:

1. Usuário preenche o formulário em qualquer cliente (Web, Desktop ou Mobile).
2. Cliente envia `POST /api/chamados` com JSON e header `Authorization`.
3. API valida token e dados de entrada.
4. API invoca o serviço de IA (endpoint interno ou `/api/ia/assist`) com a descrição do problema.
5. IA retorna nível de criticidade/prioridade (e opcionalmente sugestão de solução).
6. API grava chamado e prioridade no **Azure SQL**.
7. API responde ao cliente com o objeto chamado completo.
8. Cliente exibe prioridade na tela (requisito de demonstração apontado na avaliação).

Esse fluxo deve aparecer de forma explícita no **diagrama de sequência da abertura de chamado** e no **diagrama de implantação** (nós: cliente, API no Azure, banco no Azure).

---

## 5. Ambientes: desenvolvimento e produção

| Ambiente | API | Banco | Clientes |
|----------|-----|-------|----------|
| Desenvolvimento local | API rodando na máquina do desenvolvedor (`localhost:8080`) | SQL Server local ou Azure SQL de desenvolvimento | Apontam para `localhost` |
| Produção / demonstração | Azure App Service (HTTPS) | Azure SQL Database | Web, Desktop e Mobile com URL pública da API |

A equipe pode desenvolver localmente e publicar a API no Azure para a banca, desde que os três clientes estejam configurados para a **mesma URL base** no momento da apresentação.

---

## 6. Relação com os diagramas UML do repositório

| Diagrama | O que deve refletir após a revisão |
|----------|-----------------------------------|
| Implantação (Sprint 7) | Três clientes, API no Azure, Azure SQL, protocolos HTTPS e JDBC/ADO.NET |
| Estrutura / componentes (Sprint 8) | API como componente central; interfaces REST expostas; dependência da API em persistência |
| Pacote (Sprint 6) | Pacotes `Apresentacao`, `API`, `Dominio`, `Persistencia`, `IA` |
| Sequência – abertura de chamado | Mensagens até a API, IA e banco |
| Classe | Entidade `Chamado` com atributo prioridade/criticidade coerente com o banco |

O arquivo `Diagramas/GUIA_REFATORACAO_DIAGRAMAS.md` complementa este texto com checklist de figuras a refazer.

---

## 7. Texto sugerido para o relatório impresso (parágrafo único)

> O ClickDesk foi estruturado em arquitetura cliente–servidor: as aplicações Web, Desktop e Mobile atuam exclusivamente como camadas de apresentação, consumindo uma API REST hospedada no Microsoft Azure. Essa API concentra autenticação via JWT, o gerenciamento de chamados e a integração com o módulo de inteligência artificial responsável pela definição da criticidade dos atendimentos. Os dados persistentes são armazenados no Azure SQL Database, acessado somente pela API por meio de conexão segura configurada no ambiente de nuvem, garantindo unicidade das regras de negócio e alinhamento entre as três plataformas entregues no projeto.

---

*Documento elaborado para o PIM IV – ADS (2025). Atualizar nomes exatos dos recursos Azure (App Service, servidor SQL, resource group) na versão final do relatório.*
