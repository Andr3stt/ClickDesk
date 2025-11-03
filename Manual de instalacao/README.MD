## ⏯️ Guia de Uso, Visualização e Validação do Projeto ⏯️

Este projeto consiste em artefatos de modelagem de sistema (Diagramas UML) e não em código-fonte executável. As seções abaixo detalham como visualizar, interpretar e validar este trabalho.

### ⚙️ 1. Como Visualizar os Modelos

Para abrir e interagir com os diagramas, siga estes passos.

**Pré-requisito Essencial:**
* É **obrigatório** ter o software **Astah** (Professional, UML ou Community) instalado em sua máquina. Os arquivos-fonte `.asta` só podem ser abertos com ele.

**Passos para Visualização:**
1.  **Acesse os Arquivos-Fonte:** Navegue até a pasta de diagramas no repositório (ex: `/ClickDesk/Diagramas/` ou a pasta de cada Sprint).
2.  **Identifique os Arquivos:** Dentro de cada subpasta de Sprint (ex: `SPRINT 1...`, `SPRINT 2...`), você encontrará o arquivo-fonte principal com a extensão `.asta`.
3.  **Obtenha o Arquivo:** Você pode clonar o repositório inteiro (`git clone ...`) ou baixar o arquivo `.asta` individualmente.
4.  **Abra no Astah:** Execute o software Astah em seu computador e abra o arquivo `.asta` baixado para explorar, editar ou exportar os diagramas.

### 🧠 2. Como Interpretar e Utilizar os Diagramas

Cada diagrama é uma "view" do sistema com um propósito específico. Use-os para obter respostas concretas:

* **Diagrama de Caso de Uso (Sprint 1):**
    * **Propósito:** Entender **O QUE** o sistema faz (funcionalidades) e **QUEM** interage com ele (atores).
    * **Use para:** Ter uma visão macro das fronteiras e do escopo total do projeto.

* **Diagrama de Classe (Sprint 2):**
    * **Propósito:** Ver a **estrutura estática** e o "dicionário de dados" do sistema.
    * **Use para:** Identificar as entidades principais, seus atributos (dados que armazenam) e métodos (ações que executam), bem como os relacionamentos (herança, associação) entre elas.

* **Diagramas de Sequência e Colaboração (Sprints 4 e 5):**
    * **Propósito:** Mostrar a **dinâmica e o fluxo** (troca de mensagens) entre objetos ao longo do tempo para realizar um Caso de Uso.
    * **Use para:** Rastrear "passo a passo" como os componentes de software colaboram para completar uma funcionalidade (ex: como um `ControladorDeLogin` valida dados em um `UsuarioDAO`).

* **Diagramas de Pacote e Implantação (Sprints 6 e 7):**
    * **Propósito:** Modelar a **arquitetura lógica** (módulos) e **física** (hardware).
    * **Use para:** Entender como o sistema é organizado (camadas, serviços) e onde cada componente será fisicamente executado (servidores de aplicação, bancos de dados).

### 🧪 3. Como Testar e Validar os Modelos

O "teste" de um modelo UML é um processo de validação de consistência e lógica. Verifique os seguintes pontos:

1.  **Validação Cruzada (Coerência Interna):** Este é o teste mais crítico. Os diagramas devem ser consistentes entre si.
    * **Exemplo 1:** As classes mostradas no **Diagrama de Classe** (ex: `Cliente`, `Pedido`) são as mesmas que aparecem como objetos/lifelines no **Diagrama de Sequência**?
    * **Exemplo 2:** As mensagens enviadas no **Diagrama de Sequência** (ex: `calcularTotal()`) correspondem a métodos que realmente existem na classe de destino no **Diagrama de Classe**?
    * **Exemplo 3:** O **Diagrama de Caso de Uso** (ex: "Efetuar Compra") está sendo corretamente detalhado por um **Diagrama de Sequência** de mesmo nome?

2.  **Aderência aos Requisitos (Validação Externa):**
    * O diagrama atende ao objetivo da sua Sprint? (ex: O Diagrama de Implantação reflete a infraestrutura real do projeto?).
    * Todos os requisitos funcionais levantados estão cobertos por pelo menos um Caso de Uso?

3.  **Padrão UML:**
    * A sintaxe e a semântica UML estão sendo usadas corretamente? (ex: Setas de herança vs. associação, uso correto de multiplicidade `1..*`, etc.).