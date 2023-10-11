namespace Projeto;

public class Estacao
{
  public Estacao()
  {
    AngEstacao = new Angulo();
  }

  public Angulo AngEstacao { get; set; }
  public Angulo Azimute { get; set; }
  public float Distancia { get; set; }
  public char Deflexao { get; set; }

  public override string ToString()
  {
    return $"{AngEstacao} - {Distancia} - {Deflexao}";
  }
}
