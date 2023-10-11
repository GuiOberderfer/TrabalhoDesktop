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

  public Angulo() { }

  public Angulo(int graus, int minutos, int segundos)
  {
    Graus = graus;
    Minutos = minutos;
    Segundos = segundos;
  }
}
