(function(){
  'use strict';
  const $ = (s,r=document)=> r.querySelector(s);
  const on = (el,ev,fn,opt)=> el && el.addEventListener(ev,fn,opt);

  function fmtDate(iso){
    try{
      const d = new Date(iso);
      const dd = String(d.getDate()).padStart(2,'0');
      const mm = String(d.getMonth()+1).padStart(2,'0');
      const yy = String(d.getFullYear()).slice(-2);
      return `${dd}/${mm}/${yy}`;
    }catch(_){ return iso; }
  }
  function statusLabel(s){
    switch(String(s||'').toLowerCase()){
      case 'open': return 'Aberto';
      case 'progress': return 'Em progresso';
      case 'waiting': return 'Em espera';
      case 'closed': return 'Fechado';
      default: return s || '—';
    }
  }
  function statusClass(s){
    switch(String(s||'').toLowerCase()){
      case 'open': return 'open';
      case 'progress': return 'progress';
      case 'waiting': return 'waiting';
      case 'closed': return 'closed';
      default: return 'open';
    }
  }
  function normalize(d){
    return {
      id: String(d.id || '').replace(/^CD-?/i,'') || '—',
      date: d.date || d.createdAt || d.updatedAt || '',
      name: d.name || d.requester || '—',
      department: d.department || d.dept || '—',
      subject: d.subject || d.title || '—',
      description: d.description || d.details || '—',
      category: d.category || '—',
      status: (d.status || 'open')
    };
  }
  async function fetchTicket(id){
    try{
      const resp = await fetch(`/api/tickets/${encodeURIComponent(id)}`, { headers:{Accept:'application/json'} });
      if(!resp.ok) throw new Error('HTTP '+resp.status);
      const data = await resp.json();
      return normalize(data);
    }catch(_){
      // Fallback mock
      return normalize({
        id, date:'2025-05-24', name:'Sarah', department:'Administração',
        subject:'Lentidão', description:'Meu computador está lento para ligar',
        category:'Hardware', status:'waiting'
      });
    }
  }
  function render(t){
    $('#tktId').textContent = `CD-${t.id}`;
    $('#tktDate').textContent = t.date ? fmtDate(t.date) : '—';
    $('#tktName').textContent = t.name;
    $('#tktDept').textContent = t.department;
    $('#tktSubject').textContent = t.subject;
    $('#tktDescription').textContent = t.description;
    $('#tktCategory').textContent = t.category;

    const st = $('#tktStatus');
    st.textContent = statusLabel(t.status);
    st.className = `badge ${statusClass(t.status)}`;
    const dot = document.createElement('span'); dot.className='dot'; dot.setAttribute('aria-hidden','true');
    st.prepend(dot);
  }
  function goBack(){
    if (document.referrer && /lista-chamado\.html/i.test(document.referrer)){
      history.back();
    }else{
      location.href = '../lista-chamado.html';
    }
  }

  async function loadIASuggestion(ticketId) {
    try {
      const response = await fetch(`/api/tickets/${encodeURIComponent(ticketId)}/ia-suggestion`, {
        headers: { Accept: 'application/json' }
      });
      
      if (response.ok) {
        const data = await response.json();
        $('#iaSuggestion').innerHTML = data.suggestion || 'Nenhuma sugestão disponível no momento.';
      } else {
        // Fallback mock para demonstração
        $('#iaSuggestion').innerHTML = `
          <p style="margin: 0 0 12px 0; font-weight: 600;">Com base na análise do problema relatado, recomendamos verificar os seguintes itens:</p>
          <ol style="margin: 0; padding-left: 20px; line-height: 1.8;">
            <li>Realizar limpeza de arquivos temporários no disco rígido</li>
            <li>Verificar se há atualizações pendentes do sistema operacional</li>
            <li>Executar uma varredura de malware/vírus</li>
            <li>Verificar se o disco está com mais de 80% de uso</li>
          </ol>
          <p style="margin: 12px 0 0 0; font-style: italic; color: #6F6F6F;">Essas ações podem resolver o problema de lentidão sem necessidade de intervenção técnica.</p>
        `;
      }
    } catch (error) {
      console.error('Erro ao carregar sugestão da IA:', error);
      // Fallback mock
      $('#iaSuggestion').innerHTML = `
        <p style="margin: 0 0 12px 0; font-weight: 600;">Com base na análise do problema relatado, recomendamos verificar os seguintes itens:</p>
        <ol style="margin: 0; padding-left: 20px; line-height: 1.8;">
          <li>Realizar limpeza de arquivos temporários no disco rígido</li>
          <li>Verificar se há atualizações pendentes do sistema operacional</li>
          <li>Executar uma varredura de malware/vírus</li>
          <li>Verificar se o disco está com mais de 80% de uso</li>
        </ol>
        <p style="margin: 12px 0 0 0; font-style: italic; color: #6F6F6F;">Essas ações podem resolver o problema de lentidão sem necessidade de intervenção técnica.</p>
      `;
    }
  }

  document.addEventListener('DOMContentLoaded', async ()=>{
    const params = new URLSearchParams(location.search);
    const id = params.get('id') || '1124';

    let model = await fetchTicket(id);
    render(model);

    on($('#refreshBtn'),'click', async ()=>{
      model = await fetchTicket(id);
      render(model);
    });
    on($('#backBtn'),'click', goBack);
    on(document,'keydown',(e)=>{ if(e.key==='Escape') goBack(); });

    // Carregar sugestão da IA
    await loadIASuggestion(id);

    // Botão "Resolvido com IA"
    on($('#resolvedWithIABtn'), 'click', async () => {
      try {
        const response = await fetch(`/api/tickets/${encodeURIComponent(id)}/resolve-with-ia`, {
          method: 'POST',
          headers: { 
            'Content-Type': 'application/json',
            'Accept': 'application/json'
          }
        });
        
        if (response.ok) {
          alert('Chamado resolvido com sucesso pela IA!');
          // Atualizar status do chamado
          model = await fetchTicket(id);
          render(model);
        } else {
          alert('Erro ao resolver chamado com IA. Tente novamente.');
        }
      } catch (error) {
        console.error('Erro ao resolver com IA:', error);
        alert('Erro ao processar a solicitação. Tente novamente.');
      }
    });

    // Botão "Esperar Atendimento do Técnico"
    on($('#waitTechnicianBtn'), 'click', async () => {
      try {
        const response = await fetch(`/api/tickets/${encodeURIComponent(id)}/assign-technician`, {
          method: 'POST',
          headers: { 
            'Content-Type': 'application/json',
            'Accept': 'application/json'
          }
        });
        
        if (response.ok) {
          alert('Chamado encaminhado ao técnico com sucesso!');
          // Atualizar status do chamado
          model = await fetchTicket(id);
          render(model);
        } else {
          alert('Erro ao encaminhar chamado. Tente novamente.');
        }
      } catch (error) {
        console.error('Erro ao encaminhar ao técnico:', error);
        alert('Erro ao processar a solicitação. Tente novamente.');
      }
    });

    // Menu do usuário
    const userMenuBtn = $('#userMenuBtn');
    const userDropdown = $('#userDropdown');

    if (userMenuBtn && userDropdown) {
      on(userMenuBtn, 'click', (e) => {
        e.stopPropagation();
        const isHidden = userDropdown.hasAttribute('hidden');
        if (isHidden) {
          userDropdown.removeAttribute('hidden');
        } else {
          userDropdown.setAttribute('hidden', '');
        }
      });

      // Fechar dropdown ao clicar fora
      on(document, 'click', () => {
        userDropdown.setAttribute('hidden', '');
      });

      // Prevenir fechamento ao clicar no dropdown
      on(userDropdown, 'click', (e) => {
        e.stopPropagation();
      });
    }
  });
})();