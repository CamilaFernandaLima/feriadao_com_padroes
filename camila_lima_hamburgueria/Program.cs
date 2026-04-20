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