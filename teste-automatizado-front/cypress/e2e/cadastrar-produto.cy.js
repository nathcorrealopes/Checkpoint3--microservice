function gerarStringAleatoria(tamanho) {
  const caracteres = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
  let resultado = '';
  
  for (let i = 0; i < tamanho; i++) {
    const indice = Math.floor(Math.random() * caracteres.length);
    resultado += caracteres[indice];
  }
  
  return resultado;
}

function gerarNumeroAleatorio(min, max) {
  return Math.floor(Math.random() * (max - min + 1)) + min;
}

function gerarPrecoAleatorio(min, max) {
  const preco = (Math.random() * (max - min) + min).toFixed(2);
  return parseFloat(preco);
}

function gerarDataAleatoriaFormatada(inicio, fim) {
  const inicioTimestamp = new Date(inicio).getTime();
  const fimTimestamp = new Date(fim).getTime();
  const timestampAleatorio = Math.floor(Math.random() * (fimTimestamp - inicioTimestamp + 1)) + inicioTimestamp;
  const dataAleatoria = new Date(timestampAleatorio);
  const ano = dataAleatoria.getFullYear();
  const mes = String(dataAleatoria.getMonth() + 1).padStart(2, '0'); 
  const dia = String(dataAleatoria.getDate()).padStart(2, '0'); 

  return `${ano}-${mes}-${dia}`;
}


describe('template spec', () => {
  it('passes', () => {
    cy.visit('http://127.0.0.1:5500/cadastro-produto.html')
    const id = gerarNumeroAleatorio(1, 100)
    const nome = gerarStringAleatoria(8)
    const preco = gerarPrecoAleatorio(1, 100)
    const qtd_estoque = gerarNumeroAleatorio (1, 100)
    const data = gerarDataAleatoriaFormatada('2024-01-01', '2024-10-24')
    cy.get('#registerId').type(id)
    cy.get('#registerName').type(nome)
    cy.get('#registerPrice').type(preco)
    cy.get('#registerQtd').type(qtd_estoque)
    cy.get('input[type="date"]').type(data);
    cy.get('#btnClick').click()
    cy.get('h2').should('contain', 'Cadastrar')  
  })
})