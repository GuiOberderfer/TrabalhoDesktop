namespace Projeto;

using System;

public class Program
{
  static void Main(string[] args)
  {
    Poligonal poligonal = new Poligonal("Fazenda Rio Verde", 225, 32, 48);

    ConsoleKeyInfo key;
    do
    {
      Console.Clear();
      Console.WriteLine("Engenharia Cartográfica - Sistema de Poligonais");
      Console.WriteLine($"Poligonal: {poligonal.Descricao}");
      Console.WriteLine("===================================================");
      Console.WriteLine("Estação\tÂngulo lido\tDeflexão\tDistância(m)\tAzimute");
      Console.WriteLine("===================================================");

      int estacaoNum = 1;
      foreach (var estacao in poligonal.Estacoes)
      {
        Console.WriteLine($"{estacaoNum:D4}\t{estacao.AngEstacao}\t{estacao.Deflexao}\t{estacao.Distancia}\t{estacao.Azimute}");
        estacaoNum++;
      }

      Console.WriteLine("===================================================");
      Console.WriteLine($"Perímetro: {poligonal.Perimetro():F2} metros");

      Console.WriteLine("Pressione <Esc> para Sair | <F1> Inserir | <F2> Alterar | <F3> Excluir | <s> Salvar | <PgDn> | <PgUp>");
      key = Console.ReadKey();

      switch (key.Key)
      {
        case ConsoleKey.F1: // Inserir
          Console.Clear();
          Console.WriteLine("Inserir Nova Estação");
          Estacao novaEstacao = new Estacao();

          Console.Write("Ângulo da Estação (Graus): ");
          novaEstacao.AngEstacao.Graus = int.Parse(Console.ReadLine());

          Console.Write("Ângulo da Estação (Minutos): ");
          novaEstacao.AngEstacao.Minutos = int.Parse(Console.ReadLine());

          Console.Write("Ângulo da Estação (Segundos): ");
          novaEstacao.AngEstacao.Segundos = int.Parse(Console.ReadLine());

          Console.Write("Distância (metros): ");
          novaEstacao.Distancia = float.Parse(Console.ReadLine());

          Console.Write("Deflexão ('D' ou 'E'): ");
          novaEstacao.Deflexao = char.Parse(Console.ReadLine());

          poligonal.Estacoes.Add(novaEstacao);
          break;
        case ConsoleKey.F2: // Alterar
          AlterarEstacao(poligonal);
          break;
        case ConsoleKey.F3: // Excluir
          Excluir(poligonal);
          break;
        case ConsoleKey.Escape: // Sair
          break;
        case ConsoleKey.S:
          SalvarDados(poligonal);
          break;
          // Implementar a lógica de paginação (<PgDn> e <PgUp>)
      }
    } while (key.Key != ConsoleKey.Escape);
  }

  static void Excluir(Poligonal poligonal)
  {
    Console.Clear();
    for (int i = 0; i < poligonal.Estacoes.Count; i++)
    {
      Console.WriteLine($"{i + 1:D4} {poligonal.Estacoes[i]}");
    }

    Console.Write("Digite o número da estação que deseja excluir: ");

    if (int.TryParse(Console.ReadLine(), out int estacaoIndex) && estacaoIndex >= 1 && estacaoIndex <= poligonal.Estacoes.Count)
    {
      estacaoIndex--;
      Estacao estacaoExcluir = poligonal.Estacoes[estacaoIndex];

      Console.WriteLine($"Você deseja realmente excluir a estação {estacaoIndex + 1:D4}?");
      Console.Write("Pressione 'S' para confirmar ou qualquer outra tecla para cancelar: ");

      if (Console.ReadKey().Key == ConsoleKey.S)
      {
        poligonal.Estacoes.RemoveAt(estacaoIndex);
      }
    }
    Console.WriteLine("\nPressione qualquer tecla para continuar...");
    Console.ReadKey();
  }

  static void AlterarEstacao(Poligonal poligonal)
  {
    Console.Clear();
    Console.WriteLine("Alterar Estação");

    Console.WriteLine("Informe o número da estação que deseja alterar: ");
    if (int.TryParse(Console.ReadLine(), out int estacaoNumero) && estacaoNumero >= 1 && estacaoNumero <= poligonal.Estacoes.Count)
    {
      Estacao estacao = poligonal.Estacoes[estacaoNumero - 1];

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

  static void SalvarDados(Poligonal poligonal)
  {
    Console.Write("Nome do arquivo para salvar os dados: ");
    string nomeArquivo = Console.ReadLine();

    using (StreamWriter sw = new StreamWriter(nomeArquivo + ".csv"))
    {
      // Escreve os dados da Poligonal na primeira linha
      string poligonalData = $"{poligonal.Descricao};{poligonal.AzGraus};{poligonal.AzMinutos};{poligonal.AzSegundos}";
      sw.WriteLine(poligonalData);
      sw.WriteLine("Estação;Ângulo lido;Deflexão;Distância(m);Azimute");
      var i = 0;
      // Escreve os dados das estações nas linhas subsequentes
      foreach (var estacao in poligonal.Estacoes)
      {
        string estacaoData = $"{i:D4};{estacao.AngEstacao};{estacao.Deflexao};{estacao.Distancia};{estacao.Azimute}";
        sw.WriteLine(estacaoData);
        i++;
      }
    }

    Console.WriteLine("Dados salvos com sucesso!");
    Console.ReadKey();
  }
}

