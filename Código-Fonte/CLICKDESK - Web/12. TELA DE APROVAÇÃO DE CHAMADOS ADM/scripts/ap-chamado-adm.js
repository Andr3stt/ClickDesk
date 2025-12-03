const chamados = [
  { no:"1234", assunto:"Problema no login", status:"progresso", statusLabel:"Em progresso", categoria:"Suporte técnico", data:"23/05/25", avaliacao:0 },
  { no:"1124", assunto:"Lentidão", status:"espera", statusLabel:"Em espera", categoria:"Suporte técnico", data:"24/05/25", avaliacao:0 },
  { no:"1224", assunto:"Sem acesso", status:"fechado", statusLabel:"Fechado", categoria:"Suporte técnico", data:"13/04/25", avaliacao:4 },
  { no:"1244", assunto:"Rede instável", status:"progresso", statusLabel:"Em progresso", categoria:"Suporte de rede", data:"23/05/25", avaliacao:0 },
  { no:"1114", assunto:"Lentidão", status:"progresso", statusLabel:"Em progresso", categoria:"Suporte técnico", data:"22/05/25", avaliacao:0 }
];

function renderTabela() {
  const tbody = document.getElementById('chamados-tbody');
  const info = document.getElementById('info-entradas');
  if (!tbody) return;

  tbody.innerHTML = '';
  chamados.forEach((c) => {
    const tr = document.createElement('tr');
    tr.innerHTML = `
      <td><a href="#">${c.no}</a></td>
      <td>${c.assunto}</td>
      <td><span class="chamados-status ${c.status}">${c.statusLabel}</span></td>
      <td>${c.categoria}</td>
      <td>${c.data}</td>
      <td>${[1,2,3,4,5].map(j =>
        '<span class="chamados-estrela' + (j <= c.avaliacao ? '' : ' off') + '">&#9733;</span>'
      ).join('')}</td>
    `;
    tbody.appendChild(tr);
  });

  if (info) {
    info.textContent = `Mostrando 1 de ${chamados.length} de ${chamados.length} entradas`;
  }
}

document.addEventListener('DOMContentLoaded', renderTabela);
