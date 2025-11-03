<h1> 📋 Definições de Pronto e Feito (DoR/DoD) </h1>
&nbsp;&nbsp;
## 📖 Nossos Conceitos

- **🚀 DoR (Definition of Ready - "Definição de Pronto"):**
Um checklist de pré-requisitos. Uma tarefa só pode ser iniciada pela equipe se todos os critérios do DoR forem atendidos. Garante que não vamos começar algo sem as informações necessárias.

- **🏁 DoD (Definition of Done - "Definição de Feito"):**
Um checklist de qualidade. Uma tarefa ou sprint só é considerada 100% concluída quando atende a todos os critérios do DoD. Garante que a entrega tem o padrão de qualidade esperado.

---

## 🎯 Sprint 1 — Estruturação do GitHub e Diagrama de Caso de Uso

**Período:** 19/08/2025 - 25/08/2025&nbsp;&nbsp;

### 🚀 DoR
- O repositório central do projeto foi inicializado no GitHub.
- As *guidelines* de versionamento (ex: padrão de commits, estratégia de branches) foram comunicadas à equipe.
- A ferramenta de modelagem UML (Astah, Lucidchart, etc.) foi escolhida e está acessível para todos.
- Os requisitos de alto nível e os atores do sistema foram levantados em reunião.

### 🏁 DoD
- A estrutura de pastas base do projeto (ex: `/documentos`, `/sprints`) foi criada e enviada ao repositório.
- O arquivo `README.md` principal foi preenchido com a descrição, objetivos e membros da equipe.
- O Diagrama de Caso de Uso foi validado, e o arquivo-fonte (ex: `.asta`) junto com uma imagem (`.png`) foram commitados na pasta da Sprint 1.

---

## 🎯 Sprint 2 — Diagrama de Classe de Implementação

**Período:** 26/08/2025 - 01/09/2025&nbsp;&nbsp;

### 🚀 DoR
- O Diagrama de Caso de Uso (Sprint 1) foi formalmente aprovado.
- Um rascunho inicial das principais entidades (classes) do sistema foi discutido.
- A equipe tem clareza sobre os requisitos funcionais que o diagrama de classes deve suportar.

### 🏁 DoD
- O diagrama identifica corretamente as classes, seus atributos (com tipos) e métodos principais.
- Os relacionamentos (herança, associação, agregação, composição) e suas respectivas multiplicidades foram definidos e revisados.
- O diagrama foi exportado e os arquivos (fonte e imagem) estão versionados na pasta correta no GitHub.

---

## 🎯 Sprint 3 — Diagrama de Objetos

**Período:** 02/09/2025 - 08/09/2025&nbsp;&nbsp;

### 🚀 DoR
- O Diagrama de Classe (Sprint 2) é considerado estável e validado.
- Pelo menos um cenário de uso específico (ex: "Usuário realiza login") foi selecionado para ser instanciado.
- Os dados de exemplo que serão usados para "preencher" os objetos foram definidos.

### 🏁 DoD
- O diagrama representa uma "fotografia" válida do sistema em um determinado momento, mostrando instâncias concretas das classes.
- Os valores dos atributos dos objetos são consistentes com o Diagrama de Classe (tipos, regras).
- O arquivo foi salvo na pasta da Sprint 3 e passou por uma revisão de pares (peer review).

---

## 🎯 Sprint 4 — Diagrama de Sequência

**Período:** 09/09/2025 - 15/09/2025&nbsp;&nbsp;

### 🚀 DoR
- Os cenários (Casos de Uso) que serão detalhados nesta sprint foram priorizados.
- Os objetos e classes envolvidos na interação (atores, controladores, entidades) são conhecidos.
- A ordem básica das mensagens (o "fluxo feliz") foi rascunhada pela equipe.

### 🏁 DoD
- O diagrama demonstra claramente a ordem temporal (linhas de vida) das mensagens trocadas entre os objetos.
- A sintaxe UML (setas síncronas/assíncronas, loops, fragmentos `alt`/`opt`) foi usada corretamente.
- O diagrama foi validado contra o Caso de Uso e o Diagrama de Classe para garantir consistência.
- O arquivo final foi enviado ao repositório.

---

## 🎯 Sprint 5 — Diagrama de Colaboração (Comunicação)

**Período:** 16/09/2025 - 22/09/2025&nbsp;&nbsp;

### 🚀 DoR
- O Diagrama de Sequência (Sprint 4) está completo e aprovado.
- A equipe entende que este diagrama é uma visão estrutural (foco nos links) do mesmo cenário visto no Diagrama de Sequência (foco no tempo).

### 🏁 DoD
- O diagrama foca na organização espacial dos objetos e como eles estão conectados (links).
- A numeração das mensagens está correta e é 100% consistente com a ordem definida no Diagrama de Sequência.
- O arquivo foi exportado e commitado na pasta da Sprint 5.

---

## 🎯 Sprint 6 — Diagrama de Pacote

**Período:** 23/09/2025 - 29/09/2025&nbsp;&nbsp;

### 🚀 DoR
- As principais classes e componentes do sistema (Sprints 2-5) estão identificados.
- A arquitetura do sistema (ex: arquitetura em camadas, microsserviços) foi discutida e definida pela equipe.
- As regras de dependência (o que "pode ver" o quê) foram estabelecidas.

### 🏁 DoD
- O diagrama agrupa logicamente as classes em pacotes (namespaces/módulos).
- As dependências (ex: `<<import>>` ou `<<access>>`) entre os pacotes estão claramente visíveis e seguem as regras de arquitetura.
- O diagrama foi validado pela equipe e o arquivo-fonte/imagem está no GitHub.

---

## 🎯 Sprint 7 — Diagrama de Implantação

**Período:** 30/09/2025 - 06/10/2025&nbsp;&nbsp;

### 🚀 DoR
- Os requisitos não-funcionais de infraestrutura (hardware, rede, SGBD, servidor de aplicação) foram levantados.
- Os artefatos de software que serão implantados (ex: `.jar`, `.war`, scripts de banco de dados) são conhecidos.

### 🏁 DoD
- O diagrama modela a arquitetura física (hardware, nós, servidores) e como os artefatos de software são distribuídos por ela.
- Os protocolos de comunicação (ex: HTTP, TCP/IP, JDBC) entre os nós estão identificados.
- O arquivo final foi revisado, exportado para um formato acessível (PDF/PNG) e enviado ao repositório.

---

## 🎯 Sprint 8 — Diagrama de Estrutura

**Período:** 07/10/2025 - 13/10/2025&nbsp;&nbsp;

### 🚀 DoR
- Os principais componentes lógicos do software foram identificados (baseado no Diagrama de Pacotes).
- As interfaces (APIs, pontos de conexão) entre os componentes foram rascunhadas.
- Todos os diagramas anteriores estão disponíveis para consulta.

### 🏁 DoD
- O Diagrama de Componentes (ou Estrutura Composta) mostra as partes modulares do sistema e suas interações.
- As interfaces (portas `provided` e `required`) estão claramente definidas, mostrando o "contrato" de cada componente.
- O diagrama final é consistente com o Diagrama de Pacotes e foi versionado no GitHub.