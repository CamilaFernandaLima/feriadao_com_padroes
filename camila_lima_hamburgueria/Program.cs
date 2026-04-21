using System;
using System.Collections.Generic;

// 1. singleton: configuração da loja
public class  ConfiguracaoLoja
{
    private static ConfiguracaoLoja instance;

    public string Status = "Aberta";
    public double TaxaBase = 3.0;

    private ConfiguracaoLoja() { }
    public static ConfiguracaoLoja GetInstance()
    {
        if (instance == null)
        {
            instance = new ConfiguracaoLoja();
        }
        return instance;
    }
}

// 2. padrão factory: criação dos lanches
public interface  IItemPedido
{
    string GetDescricao();
    double GetPreco();
}
public class XBurguer : IItemPedido
{
    public string GetDescricao() => "X-Burguer";
    public double GetPreco() => 18.0;
}
public class XSalada : IItemPedido
{
    public string GetDescricao() => "X-Salada";
    public double GetPreco() => 20.0;
}
public class  Vegano : IItemPedido
{
    public string GetDescricao() => "Vegano";
    public double GetPreco() => 23.0;
}
public class  BatataBacon : IItemPedido
{
    public string GetDescricao() => "Porção de Batata com Bacon";
    public double GetPreco() => 14.0;
}

// a factory:
public class  LanchaFactory
{
    public static IItemPedido Criar(string tipo)
    {
        Console.WriteLine("Qual item você deseja?");
        Console.WriteLine("(1) X-Burguer, (2) X-Salada, (3) Vegano, (4) Porção de Batata com Bacon");
        
        if (tipo == "1")
            return new XBurguer();
        else if (tipo == "2")
            return new XSalada();
        else if (tipo == "3")
            return new Vegano();
        else if (tipo == "4")
            return new BatataBacon();
        else
        {
            Console.WriteLine("Erro: seu pedido não existe no cardápio.");
            return null;
        }
    }
}

// 3. padrão adapter: adaptador de pagamento
//sistema antigo, com o método de pagamento antigo
public class SistemaAntigo
{
    public void Transacao(double valor)
    {
        Console.WriteLine($"Processando pagamento de R${valor:F2} no sistema antigo.");
    }
}

//interface moderna usada pelo novo sistema
public interface IPagamento
{
    void Pagar(double valor);
}
//adaptador (pega o valor antigo e adapta a transação para o novo sistema)
public class  AdaptadorPagamento : IPagamento
{
    private SistemaAntigo sistemaAntigo;

    public AdaptadorPagamento(SistemaAntigo sistema)
    {
        this.sistemaAntigo = sistema;
    }
    public void Pagar(double valor)
    {
        sistemaAntigo.Transacao(valor);
    }
}

// 4. padrão proxy: controle de cancelamentos no sistema de pedidos
public interface ICancelamento
{
    void CancelarPedido();
}
public class CancelamentoReal : ICancelamento
{
    public void CancelarPedido()
    {
        Console.WriteLine("Pedido cancelado com sucesso.");
    }
}
public class ProxyCancelamento : ICancelamento
{
    private ICancelamento sistemaReal;
    private string senhaGerente;

    public ProxyCancelamento(ICancelamento sistema, string senha)
    {
        this.sistemaReal = sistema;
        this.senhaGerente = senha;
    }

    public void CancelarPedido()
    {
        if (senhaGerente == "1234")
        {
            Console.WriteLine("Acesso concedido: senha correta. Cancelando pedido...");
            sistemaReal.CancelarPedido();
        }
        else
        {
            Console.WriteLine("Acesso negado: senha incorreta. Pedido não cancelado.");
        }
    }
}

// 5. padrão decorator: para adicionar complementos
public abstract class ItemPedidoDecorator : IItemPedido
{
    protected IItemPedido itemAtual;
    public ItemPedidoDecorator(IItemPedido item)
    {
        this.itemAtual = item;
    }
    public virtual string GetDescricao() => itemAtual.GetDescricao();
    public virtual double GetPreco() => itemAtual.GetPreco();
}
public class QueijoExtra : ItemPedidoDecorator
{
    public QueijoExtra(IItemPedido item) : base(item) { }

    public override string GetDescricao() => base.itemAtual.GetDescricao() + " + Queijo Extra";
    public override double GetPreco() => base.itemAtual.GetPreco() + 3.0;

}
public class Bacon : ItemPedidoDecorator
{
    public Bacon(IItemPedido item) : base(item) { }

    public override string GetDescricao() => base.itemAtual.GetDescricao() + " + Bacon";
    public override double GetPreco() => base.itemAtual.GetPreco() + 4.0;

}
public class CebolaCrispy : ItemPedidoDecorator
{
    public CebolaCrispy(IItemPedido item) : base(item) { }

    public override string GetDescricao() => base.itemAtual.GetDescricao() + " + Cebola Crispy";
    public override double GetPreco() => base.itemAtual.GetPreco() + 2.0;

}

// 6. padrão strategy: calcular frete
public interface ICalculoFrete
{
    double Calcular();
}
public class FreteNormal : ICalculoFrete
{
    public double Calcular() { Console.WriteLine("Frete: Normal"); return 5.00; }
}
public class FreteRapido : ICalculoFrete
{
    public double Calcular() { Console.WriteLine("Frete: Rápido"); return 10.00; }
}
public class RetirarBalcao : ICalculoFrete
{
    public double Calcular() { Console.WriteLine("Frete: Retirada (Grátis)"); return 0.00; }
}

// 7. padrão observer: para restrear os pedidos
public interface IObservador
{
    void Atualizar(string status);
}
public class AppCliente : IObservador
{
    public void Atualizar(string status)
    {
        Console.WriteLine($"[App do Cliente] Status do pedido atualizado: {status}");
    }
}
public class PainelCozinha : IObservador
{
    public void Atualizar(string status)
    {
        Console.WriteLine($"[Painel da Cozinha] Status do pedido atualizado: {status}");
    }
}
public class Pedido
{
    private List<IObservador> abservadores = new List<IObservador>();
    private string statusAtual;

    public void Adicionar(IObservador obs) => abservadores.Add(obs);
    public void MudarStatus(string novoStatus)
    {
        this.statusAtual = novoStatus;
        foreach (var obs in abservadores)
        {
            obs.Atualizar(statusAtual);
        }
    }
}