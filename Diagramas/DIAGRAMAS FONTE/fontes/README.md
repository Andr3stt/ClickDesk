# Fontes Mermaid (`fontes/`)

Gera os PNG em `../` e, em seguida, copie para as pastas `SPRINT …` (ver [`../../ORGANIZACAO_E_COERENCIA.md`](../../ORGANIZACAO_E_COERENCIA.md)).

```powershell
Set-Location "Diagramas/DIAGRAMAS FONTE/fontes"
$base = ".."
$outputs = @(
  "01-implantacao.mmd","Implantacao.png",
  "01-implantacao.mmd","Implamentaçao.png",
  "02-estrutura.mmd","Estrutura.png",
  "03-sequencia-abertura-chamado.mmd","Sequencia Abertura de Chamado .png",
  "04-pacotes.mmd","Pacotes.png",
  "05-sugestao-ia.mmd","Sugestao IA .png",
  "06-caso-de-uso.mmd","Caso de uso.png",
  "07-classe-abertura-chamado.mmd","Classe Abertura de chamado .png",
  "08-objeto.mmd","OBJETO.png",
  "09-comunicacao-abertura.mmd","Comunicacao Abertura de Chamado.png",
  "09-comunicacao-abertura.mmd","Cominicaçao Abertura de Chamado .png",
  "10-sequencia-atualizar.mmd","Sequencia Atualizar Chamado.png",
  "11-comunicacao-atualizar-status.mmd","Comunicacao Atualizar Status.png",
  "12-classe-consultar-chamado.mmd","Classe consultar chamado.png",
  "13-classe-aprimorar-modelo-ia.mmd","Classe Aprimorar  Modelo IA .png",
  "14-classe-controle-status.mmd","Classe controle de status .png",
  "15-comunicacao-controle-configuracao.mmd","Comunicacao Controle Configuracao.png",
  "15-comunicacao-controle-configuracao.mmd","Comunicaçao Controle Configuraçao.png",
  "16-comunicacao-tela-consulta.mmd","Comunicacao Tela de consulta.png",
  "16-comunicacao-tela-consulta.mmd","Comunicaçao Tela de consulta .png",
  "17-controle-consulta.mmd","Controle de consulta .png"
)
for ($i = 0; $i -lt $outputs.Length; $i += 2) {
  npx --yes @mermaid-js/mermaid-cli -c mermaid-config.json -b white -s 2 -i $outputs[$i] -o (Join-Path $base $outputs[$i+1])
}
```

Depois copie de `DIAGRAMAS FONTE` para cada pasta `SPRINT n` conforme o mapa em `ORGANIZACAO_E_COERENCIA.md`.
