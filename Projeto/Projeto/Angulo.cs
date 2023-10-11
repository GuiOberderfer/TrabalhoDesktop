namespace Projeto;

public class Angulo
{
  public int Graus { get; set; }
  public int Minutos { get; set; }
  public int Segundos { get; set; }

  public override string ToString()
  {
    return $"{Graus}º {Minutos}' {Segundos}''";
  }
}
