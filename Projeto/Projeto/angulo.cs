using System;
using System.Collections.Generic;
namespace Projeto;

public class Angulo
{
    public int Graus { get; set; }
    public int Minutos { get; set; }
    public int Segundos { get; set; }

    public Angulo(int graus, int minutos, int segundos)
    {
        this.Graus = graus;
        this.Minutos = minutos;
        this.Segundos = segundos;
    }

    public Angulo() { }

    public override string ToString()
    {
        return $"{Graus}°{Minutos}'{Segundos}''";
    }
}