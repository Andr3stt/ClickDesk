<p align="center">
  <img src="https://github.com/ViniFagundes-A/clickdesk/assets/clickdesk_logo.png" alt="Logo Clickdesk" width="180">
  <h2 align="center">Clickdesk</h2>
</p>

<p align="center">
  | <a href="#visao-geral">Visão Geral</a> |
  <a href="#backlog">Backlog do Produto</a> |
  <a href="#dor">DoR</a> |
  <a href="#dod">DoD</a> |
  <a href="#sprints">Sprints</a> |
  <a href="#tecnologias">Tecnologias</a> |
  <a href="#manual">Manual de Instalação</a> |
  <a href="#equipe">Equipe</a> |
</p>

> Status do Projeto: Em desenvolvimento 🚧
>
> Documentação: [docs/](docs/)
> 
> Estrutura de Diagramas: [diagramas/](diagramas/)
> 
> Roadmap e Backlog: [backlog/](backlog/)

---

## 🧩 Visão Geral <a id="visao-geral"></a>

O **Clickdesk** é um sistema de atendimento e suporte técnico (Helpdesk) voltado para pequenas e médias empresas que desejam otimizar o relacionamento com seus clientes por meio de chamados organizados, controle de SLA e histórico de atendimentos. Inspirado em líderes do mercado, como Zendesk, o Clickdesk entrega uma solução moderna, acessível e eficiente.

---

## 📋 Backlog do Produto <a id="backlog"></a>

| Rank | Prioridade | User Story                                                                                                    |
| :--: | :--------: | ------------------------------------------------------------------------------------------------------------ |
|  1   |    Alta    | Como cliente, quero criar chamados para registrar minhas solicitações e problemas.                           |
|  2   |    Alta    | Como agente, quero responder e encerrar chamados para controlar o atendimento e o SLA.                       |
|  3   |    Alta    | Como cliente, quero acompanhar o status dos meus chamados em tempo real.                                     |
|  4   |    Alta    | Como gestor, quero visualizar relatórios de atendimento para monitorar métricas e qualidade.                 |
|  5   |   Média    | Como cliente, quero interagir com um chatbot para resolução rápida de problemas simples.                     |
|  6   |   Média    | Como agente, quero categorizar chamados para facilitar o roteamento das solicitações.                        |
|  7   |   Baixa    | Como gestor, quero exportar o histórico de atendimentos para análise externa.                                |

---

## 🏃‍ DoR - Definition of Ready <a id="dor"></a>

- User Stories com **Critérios de Aceitação**
- Subtarefas divididas a partir das US
- Design no **Lucidchart/Astah**
- Modelagem do **Banco de Dados**
- Diagrama de **Fluxo de Atendimento**
- Documentação complementar disponível

---

## 🏆 DoD - Definition of Done <a id="dod"></a>

- Manual de Usuário e da Aplicação
- Documentação da API (REST/SQL)
- Código revisado e testado
- Vídeos/demos de cada etapa de entrega
- Integração validada e funcional
- Pronto para deploy e homologação

---

## 📅 Cronograma de Sprints <a id="sprints"></a>

| Sprint         | Período        | Documentação                        |
| -------------- | :------------: | ----------------------------------- |
| **SPRINT 1**   | 01/11 - 15/11  | [Sprint 1 Docs](docs/sprints/sprint-1/README.md) |
| **SPRINT 2**   | 16/11 - 30/11  | [Sprint 2 Docs](docs/sprints/sprint-2/README.md) |
| **SPRINT 3**   | 01/12 - 15/12  | [Sprint 3 Docs](docs/sprints/sprint-3/README.md) |

---

## 💻 Tecnologias <a id="tecnologias"></a>

<h4 align="center">
 <img src="https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white">
 <img src="https://img.shields.io/badge/Lucidchart-FF8000?style=for-the-badge&logo=lucidchart&logoColor=white">
 <img src="https://img.shields.io/badge/Astah-0096C7?style=for-the-badge">
 <img src="https://img.shields.io/badge/GitHub%20Projects-181717?style=for-the-badge&logo=github&logoColor=white">
</h4>

---

## 📖 Manual de Instalação <a id="manual"></a>

### 🛠 Pré-requisitos

- Git ([Download](https://git-scm.com/downloads))
- SQL Server ([Download](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads))
- Ferramenta de diagramas (Lucidchart/Astah)
- Editor de código (VS Code recomendado)

---

### 1. Clonar o Repositório

```bash
git clone https://github.com/ViniFagundes-A/clickdesk.git
cd clickdesk
```

### 2. Configuração do Banco de Dados

- Importe os scripts SQL localizados em `banco_de_dados/` no SQL Server Management Studio.
- Ajuste as credenciais de conexão conforme o ambiente.

### 3. Inicialização do Sistema

- Acesse o diretório `codigo_fonte/` para os módulos do sistema.
- Siga as instruções do manual técnico em `docs/`.

---

## 👥 Equipe <a id="equipe"></a>

<div align="center">
  <table>
    <tr>
      <th>Membro</th>
      <th>Função</th>
      <th>Github</th>
      <th>Linkedin</th>
    </tr>
    <tr>
      <td>Erika Cordeiro</td>
      <td>Dev Team</td>
      <td><a href="https://github.com/ErikaCordeiro"><img src="https://img.shields.io/badge/GitHub-100000?style=for-the-badge&logo=github&logoColor=white"></a></td>
      <td></td>
    </tr>
    <tr>
      <td>André Barbosa</td>
      <td>Product Owner</td>
      <td></td>
      <td></td>
    </tr>
    <tr>
      <td>Vinicius Fagundes</td>
      <td>Scrum Master</td>
      <td><a href="https://github.com/ViniFagundes-A"><img src="https://img.shields.io/badge/GitHub-100000?style=for-the-badge&logo=github&logoColor=white"></a></td>
      <td></td>
    </tr>
    <tr>
      <td>Kaique Uchoa</td>
      <td>Dev Team</td>
      <td></td>
      <td></td>
    </tr>
  </table>
</div>

---

> Dúvidas, sugestões ou colaboração? Fale com nossa equipe!
