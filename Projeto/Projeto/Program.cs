namespace Projeto;

public static class Program
{
  static void Main(string[] args)
  {
    Poligonal poligonal = new Poligonal("Fazenda Rio Verde", 225, 32, 48);
    poligonal.Estacoes.AddRange(TestaPaginaçãoEstacoes());
    poligonal.Listar();
  }
  
  private static List<Estacao> TestaPaginaçãoEstacoes()
  {
      var estacoes = new List<Estacao>();
      estacoes.Add(new Estacao(new Angulo(60, 30, 20), 90.76f, 'E'));
      estacoes.Add(new Estacao(new Angulo(60, 52, 23), 560.0f, 'E'));
      estacoes.Add(new Estacao(new Angulo(57, 22,17), 712.36f, 'E'));
      estacoes.Add(new Estacao(new Angulo(16, 16, 16), 30.2f, 'D'));
      estacoes.Add(new Estacao(new Angulo(32, 32, 32), 30.2f, 'E'));
      estacoes.Add(new Estacao(new Angulo(64, 59, 59), 4.5f, 'D'));
      estacoes.Add(new Estacao(new Angulo(128, 59, 59), 10.2f, 'e'));
      estacoes.Add(new Estacao(new Angulo(256, 59, 59), 10.2f, 'D'));
      estacoes.Add(new Estacao(new Angulo(2, 2, 2), 10.2f, 'E'));
      estacoes.Add(new Estacao(new Angulo(360, 4, 4), 4.5f, 'D'));
      estacoes.Add( new Estacao(new Angulo(256, 59, 59), 10.2f, 'E'));
      return estacoes;
  }
}

