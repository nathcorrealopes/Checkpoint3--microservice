describe('template spec', () => {
  it('passes', () => {
    cy.visit('http://127.0.0.1:5500/index.html')
    cy.get('.btn').click()

    cy.get('#loginEmail').type('xxx@gmail.com')
    cy.get('#loginPassword').type('618401212')
    cy.get('.btn').click()
  })
})