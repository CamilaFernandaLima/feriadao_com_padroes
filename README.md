# Sistema de Delivery de uma Hamburgueria

*CONTEXTO:* 

Uma hamburgueria está com problemas para organizar e realizar pedidos feitos pelo WhatsApp, e deseja criar seu próprio aplicativo de pedidos.
O sistema precisa ser capaz de: 
- gerenciar as configurações do estabelecimento;
- criar diferentes tipos de lanche;
- permitir que o cliente adicione os complementos desejados;
- calcular o frete (dependendo de alguns fatores);
- processar pagamentos;
- avisar o cliente quando o lanche sai para entrega;
- proteger o sistema contra canvcelamentos não autorizados de pedidos.

*A SOLUÇÃO: implementação dos 8 padrões de projetos estudados até o momento*
1. Singleton: usado para configuração da loja. Uma classe ConfiguracaoLoja é criada, e ela armazena informações sobre o status ("aberta" ou "fechada") e a taxa base de operação. O sistema intiro compartilha essa mesma instância.
2. Factory: fábrica de criação de lanches. O cliente pede a criação de algum objeto e esta é realizada pela LancheFactory (ele não deve fazer "new XBurger()", por exemplo).
3. Decorator: se o cliente quiser um hambúrguer com complementos extras, usa-se o decorator para adicioná-los e somar os preços de maneira dinâmica.
4. Strategy: usado para o cálculo da taxa de entrega, dependendo da preferência do cliente. Serão implementadas três diferentes estratégias: FreteNormal (mais barato, mas demora mais), FreteRapido (mais rápido e mais caro), RetirarBalcao (não cobrado), todas a partir da interface IClaculoFrete.
5. Adapter: vou supor que a loja ainda tem contrato com um sistema de cartão antigo, que usa um método chamado Transacao(). Assim, foi criado um AdaptadorPagamento, para uqe ele se encaixe na interface que simplesmente pede um Pagar().
6. Observer: como o sujeito principal é o pedido, teremos dois observadores: o AppCliente e o PainelCozinha. Quando o status do pedido é modificado, ambos recebem notificações e atualizam as telas.
7. Proxy:  
