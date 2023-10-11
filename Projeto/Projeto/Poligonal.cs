namespace Projeto;

public partial class Poligonal
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
  //todo ajustar calculo azimute que possui um erro
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

  public void Inserir()
  {
    Console.Clear();
    Console.WriteLine("Inserir Nova Estação");
    Estacao novaEstacao = new Estacao();

    Console.Write("Ângulo da Estação (Graus): ");
    //todo validar limites graus (0-359/dígito)
    novaEstacao.AngEstacao.Graus = int.Parse(Console.ReadLine());
    //todo validar limites minuto e segundo (0-59/digito)
    Console.Write("Ângulo da Estação (Minutos): ");
    novaEstacao.AngEstacao.Minutos = int.Parse(Console.ReadLine());
    //todo idem
    Console.Write("Ângulo da Estação (Segundos): ");
    novaEstacao.AngEstacao.Segundos = int.Parse(Console.ReadLine());
    //todo validar limites float (dígito)
    Console.Write("Distância (metros): ");
    novaEstacao.Distancia = float.Parse(Console.ReadLine());
    //todo validar se resultado desvia das opções/lowercase
    Console.Write("Deflexão ('D' ou 'E'): ");
    novaEstacao.Deflexao = char.Parse(Console.ReadLine());
    Estacoes.Add(novaEstacao);
  }

  public void Editar()
  {
    Console.Clear();
    Console.WriteLine("Alterar Estação");

    Console.WriteLine("Informe o número da estação que deseja alterar: ");
    if (int.TryParse(Console.ReadLine(), out int estacaoNumero) && estacaoNumero >= 1 && estacaoNumero <= Estacoes.Count)
    {
      Estacao estacao = Estacoes[estacaoNumero - 1];

      Console.WriteLine($"Estação {estacaoNumero} - Dados Atuais: {estacao}");
      Console.WriteLine("Informe os novos dados da estação:");

      Console.WriteLine("Ângulo da Estação (Graus): ");
      if (int.TryParse(Console.ReadLine(), out int grausEstacao))
      {
        estacao.AngEstacao.Graus = grausEstacao;
      }

      Console.WriteLine("Ângulo da Estação (Minutos): ");
      if (int.TryParse(Console.ReadLine(), out int minutosEstacao))
      {
        estacao.AngEstacao.Minutos = minutosEstacao;
      }

      Console.WriteLine("Ângulo da Estação (Segundos): ");
      if (int.TryParse(Console.ReadLine(), out int segundosEstacao))
      {
        estacao.AngEstacao.Segundos = segundosEstacao;
      }

      Console.WriteLine("Deflexão (D ou E): ");
      char deflexao;
      if (char.TryParse(Console.ReadLine(), out deflexao) && (deflexao == 'D' || deflexao == 'E'))
      {
        estacao.Deflexao = deflexao;
      }

      Console.WriteLine("Distância (metros): ");
      if (float.TryParse(Console.ReadLine(), out float distancia))
      {
        estacao.Distancia = distancia;
      }
      Console.WriteLine("Estação alterada com sucesso!");
    }
    else
    {
      Console.WriteLine("Número de estação inválido. Pressione Enter para voltar.");
    }

    Console.ReadKey();
  }

  public void Excluir()
  {
    Console.Clear();
    for (int i = 0; i < Estacoes.Count; i++)
    {
      Console.WriteLine($"{i + 1:D4} {Estacoes[i]}");
    }

    Console.Write("Digite o número da estação que deseja excluir: ");

    if (int.TryParse(Console.ReadLine(), out int estacaoIndex) && estacaoIndex >= 1 && estacaoIndex <= Estacoes.Count)
    {
      estacaoIndex--;
      Estacao estacaoExcluir = Estacoes[estacaoIndex];

      Console.WriteLine($"Você deseja realmente excluir a estação {estacaoIndex + 1:D4}?");
      Console.Write("Pressione 'S' para confirmar ou qualquer outra tecla para cancelar: ");

      if (Console.ReadKey().Key == ConsoleKey.S)
      {
        Estacoes.RemoveAt(estacaoIndex);
      }
    }
    Console.WriteLine("\nPressione qualquer tecla para continuar...");
    Console.ReadKey();
  }

  public void Listar()
  {
    ConsoleKeyInfo key;
    do
    {
      ImprimeTelaListar();

      key = Console.ReadKey();

      switch (key.Key)
      {
        case ConsoleKey.F1:
          Inserir();
          break;
        case ConsoleKey.F2:
          Editar();
          break;
        case ConsoleKey.F3:
          Excluir();
          break;
        case ConsoleKey.Escape:
          break;
        case ConsoleKey.S:
          if (key.Modifiers.HasFlag(ConsoleModifiers.Control))
          {
            SalvarDados();
          }
          break;
        case ConsoleKey.PageDown:
          //todo voltar paginação
          break;
        case ConsoleKey.PageUp:
          //todo avançar paginação
          break;
      }
    } while (key.Key != ConsoleKey.Escape);
  }
  
  private void SalvarDados()
  {
    //todo validar limites nome (caracteres especiais/arquivo já existe)
    Console.Write("Nome do arquivo para salvar os dados: ");
    string nomeArquivo = Console.ReadLine();

    using (StreamWriter sw = new StreamWriter(nomeArquivo + ".csv"))
    {
      // Escreve os dados da Poligonal na primeira linha
      sw.WriteLine(this.ToString());
      sw.WriteLine("Estação;Ângulo lido;Deflexão;Distância(m);Azimute");
      var i = 0;
      // Escreve os dados das estações nas linhas subsequentes
      foreach (var estacao in Estacoes)
      {
        sw.WriteLine(estacao.StringParaImpressao(i));
        i++;
      }
    }

    Console.WriteLine("Dados salvos com sucesso!");
    Console.ReadKey();
  }

  public override string ToString()
  {
    return $"{Descricao};{AzGraus};{AzMinutos};{AzSegundos}";
  }
}
