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