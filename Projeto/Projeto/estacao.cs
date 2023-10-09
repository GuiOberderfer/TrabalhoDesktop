using System;
using System.Collections.Generic;
namespace Projeto;

class Estacao
{
    public Angulo AngEstacao { get; set; }
    public Angulo Azimute { get; set; }
    public float Distancia { get; set; }
    public char Deflexao { get; set; }

    public override string ToString()
    {
        return $"{AngEstacao}-{Distancia}-{Deflexao}-";
    }
}
