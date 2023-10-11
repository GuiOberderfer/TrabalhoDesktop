namespace Projeto;

public class Poligonal
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
    Angulo azimuteInicial = new Angulo { Graus = AzGraus, Minutos = AzMinutos, Segundos = AzSegundos };

    foreach (var estacao in Estacoes)
    {
      perimetro += estacao.Distancia;
      CalcAzimute(estacao, azimuteInicial);
      azimuteInicial = estacao.Azimute;
    }

    return perimetro;
  }

  public void CalcAzimute(Estacao estacao, Angulo azimuteInicial)
  {
    if (Estacoes.Count == 0)
    {
      estacao.Azimute = new Angulo { Graus = AzGraus, Minutos = AzMinutos, Segundos = AzSegundos };
      return;
    }

    int segundos = azimuteInicial.Segundos;
    int minutos = azimuteInicial.Minutos;
    int graus = azimuteInicial.Graus;

    if (estacao.Deflexao == 'D')
    {
      segundos += estacao.AngEstacao.Segundos;
      if (segundos >= 60)
      {
        minutos += 1;
        segundos -= 60;
      }

      minutos += estacao.AngEstacao.Minutos;
      if (minutos >= 60)
      {
        graus += 1;
        minutos -= 60;
      }

      graus += estacao.AngEstacao.Graus;
      if (graus >= 360)
      {
        graus -= 360;
      }
    }
    else
    {
      segundos -= estacao.AngEstacao.Segundos;
      if (segundos < 0)
      {
        minutos -= 1;
        segundos += 60;
      }

      minutos -= estacao.AngEstacao.Minutos;
      if (minutos < 0)
      {
        graus -= 1;
        minutos += 60;
      }

      graus -= estacao.AngEstacao.Graus;
      if (graus < 0)
      {
        graus += 360;
      }
    }

    estacao.Azimute = new Angulo { Graus = graus, Minutos = minutos, Segundos = segundos };
  }
}
