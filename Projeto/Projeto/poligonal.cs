using System;
using System.Collections.Generic;
using angulo;
using estacao;


class Poligonal
{
    public string Descricao { get; set; }
    public int AzGraus { get; set; }
    public int AzMinutos { get; set; }
    public int AzSegundos { get; set; }
    public List<Estacao> Estacoes { get; set; }

    public Poligonal(string descricao, int azGraus, int azMinutos, int azSegundos)
    {
        Descricao = descricao;
        AzGraus = azGraus;
        AzMinutos = azMinutos;
        AzSegundos = azSegundos;
        Estacoes = new List<Estacao>();
    }

    public float Perimetro()
    {
        float perimetro = 0;
        foreach (Estacao estacao in Estacoes)
        {
            perimetro += estacao.Distancia;
        }
        return perimetro;
    }

    public void Inserir()
    {
       
    }

    public void Editar()
    {
      
    }

    public void Excluir()
    {
    
    }

    public void Listar()
    {
        
    }

    public void CalcularAzimutes()
    {
 
    }
}
