# Diagramas-fonte (`DIAGRAMAS FONTE`)

## Ferramentas (Astah não é obrigatório)

O PIM exige **diagramas UML coerentes** e **imagens no repositório**; a ferramenta é escolha da equipe. O README principal do projeto cita **Astah** e **Lucidchart**; também servem Draw.io, StarUML, Visual Paradigm ou outro editor UML, desde que:

- a notação UML esteja correta;
- os textos estejam em **português brasileiro** com ortografia revisada;
- o visual siga o padrão do grupo (como em `Diagramas/ANTIGOS/`), sem aparência de export automático genérico.

## O que é o `Completo.asta`

Arquivo **opcional**: projeto consolidado criado no **Astah** em algum momento do grupo. Se alguém usar Astah, pode continuar editando esse arquivo e exportar PNG. Quem usa Lucidchart ou outra ferramenta **não precisa** do `.asta` — basta manter os **PNG** (e, se possível, o arquivo editável da ferramenta escolhida: `.drawio`, projeto Lucidchart, etc.).

Dentro do `Completo.asta` há, entre outros, sequências de abertura/atualização de chamado e elementos como `ControleAberturaChamado`, `Chamado`, `Usuario`. Abre apenas no Astah; serve de referência para quem for refazer no mesmo software.

## Pasta `fontes/` (Mermaid)

Arquivos `.mmd` geram os PNG desta pasta (comando no `fontes/README.md`). **Última geração:** API no Azure, Azure SQL, módulo de IA e textos em português. Se refizerem no Lucidchart, use os `.mmd` só como roteiro do conteúdo.

## Fluxo sugerido

1. Escolher a ferramenta (Lucidchart, Astah, Draw.io, …).
2. Seguir [`../PADRAO_DIAGRAMAS_E_REVISAO.md`](../PADRAO_DIAGRAMAS_E_REVISAO.md) (conteúdo + português).
3. Exportar **PNG** para esta pasta.
4. **Copiar** o mesmo PNG para a pasta `SPRINT n - …` correspondente (ver mapa em [`../ORGANIZACAO_E_COERENCIA.md`](../ORGANIZACAO_E_COERENCIA.md)).
5. Atualizar o README da sprint e, se usar Astah, commitar `Completo.asta` junto.

## Nomes de arquivo (correções)

| Legado no repo | Preferir ao exportar |
|----------------|----------------------|
| `Implamentaçao.png` | `Implantacao.png` |
| `Cominicaçao …` | `Comunicacao …` |

Legendas no **Word** sempre com acentuação correta (Implantação, Comunicação, Descrição).
