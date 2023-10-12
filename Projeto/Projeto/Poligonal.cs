namespace Projeto;

public partial class Poligonal
{
    private string Descricao { get; set; }
    private int AzGraus { get; set; }
    private int AzMinutos { get; set; }
    private int AzSegundos { get; set; }
    public List<Estacao> Estacoes { get; set; }
    private Angulo? _coordenadaAtual;

    public Poligonal(string descricao, int azGraus, int azMinutos, int azSegundos)
    {
        Descricao = descricao;
        AzGraus = azGraus;
        AzMinutos = azMinutos;
        AzSegundos = azSegundos;
        Estacoes = new List<Estacao>();
    }

    private float Perimetro()
    {
        return Estacoes.Sum(estacao => estacao.Distancia);
    }

    public void CalcAzimute(Estacao estacao)
        //todo ajustar calculo azimute que possui um erro
    {
        if (estacao.Azimute != null)
        {
            return;
        }

        if (_coordenadaAtual == null)
        {
            estacao.Azimute = new Angulo(AzGraus, AzMinutos, AzSegundos);
            _coordenadaAtual = estacao.Azimute;
            return;
        }

        int segundos = _coordenadaAtual.Segundos;
        int minutos = _coordenadaAtual.Minutos;
        int graus = _coordenadaAtual.Graus;

        if (estacao.Deflexao == 'D')
        {
            segundos = calculoDeflexaoADireita(estacao, segundos, ref minutos, ref graus);
        }
        else
        {
            segundos = CalculoDeflexaoAEsquerda(estacao, segundos, ref minutos, ref graus);
        }

        estacao.Azimute = new Angulo { Graus = graus, Minutos = minutos, Segundos = segundos };
        _coordenadaAtual = estacao.Azimute;
    }

    private static int CalculoDeflexaoAEsquerda(Estacao estacao, int segundos, ref int minutos, ref int graus)
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

        return segundos;
    }

    private static int calculoDeflexaoADireita(Estacao estacao, int segundos, ref int minutos, ref int graus)
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

        return segundos;
    }

    public void Inserir()
    {
        Console.Clear();
        Console.WriteLine("Inserir Nova Estação");
        Estacao novaEstacao = new Estacao();

        Console.Write("Ângulo da Estação (Graus): ");
        novaEstacao.AngEstacao.Graus = ValidaEntradaGraus();

        Console.Write("Ângulo da Estação (Minutos): ");
        novaEstacao.AngEstacao.Minutos = ValidaEntradaMinutoESegundo();

        Console.Write("Ângulo da Estação (Segundos): ");
        novaEstacao.AngEstacao.Segundos = ValidaEntradaMinutoESegundo();

        Console.Write("Distância (metros): ");
        // todo validar limites float (dígito)
        bool metrosValidos = false;
        float metros = 0;
        do
        {
            metrosValidos = float.TryParse(Console.ReadLine(), out metros);
            if (metros < 0) metrosValidos = false;
            if (!metrosValidos) Console.WriteLine("Distancia inválida. Tente novamente.");
        } while (!metrosValidos);

        novaEstacao.Distancia = metros;

        while (true)
        {
            Console.Write("Deflexão ('D' ou 'E'): ");
            char deflexao = char.Parse(Console.ReadLine());
            if (char.ToUpper(deflexao) == 'D' || char.ToUpper(deflexao) == 'E')
            {
                novaEstacao.Deflexao = char.ToUpper(deflexao);
                break;
            }
            else
            {
                Console.WriteLine("Por favor, unsira uma Deflexão válida.");
            }
        }

        Estacoes.Add(novaEstacao);
    }

    private int ValidaEntradaGraus()
    {
        while (true)
        {
            var e = Console.ReadLine();
            if (int.TryParse(e, out var n) && n is >= 0 and < 360) return n;
            Console.WriteLine("Por favor, insira um inteiro de 0 a 359");
        }
    }

    private int ValidaEntradaMinutoESegundo()
    {
        while (true)
        {
            var e = Console.ReadLine();
            if (int.TryParse(e, out var n) && n is >= 0 and < 59) return n;
            Console.WriteLine("Por favor, insira um inteiro de 0 a 59");
        }
    }


    public void Editar()
    {
        Console.Clear();
        Console.WriteLine("Alterar Estação");

        Console.WriteLine("Informe o número da estação que deseja alterar: ");
        if (int.TryParse(Console.ReadLine(), out int estacaoNumero) && estacaoNumero >= 1 &&
            estacaoNumero <= Estacoes.Count)
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

        if (int.TryParse(Console.ReadLine(), out int estacaoIndex) && estacaoIndex >= 1 &&
            estacaoIndex <= Estacoes.Count)
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
            foreach (var estacao in Estacoes)
            {
                CalcAzimute(estacao);
            }

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
                    paginaAnterior();
                    break;
                case ConsoleKey.PageUp:
                    proximaPagina();
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
            sw.WriteLine("Estação;Ângulo lido;Deflexão;Distância(m);Azimute;");
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