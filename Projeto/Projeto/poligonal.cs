namespace Projeto;

class Poligonal
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
        foreach (Estacao estacao in Estacoes)
        {
            perimetro += estacao.Distancia;
        }
        return perimetro;
    }

    public void Inserir()
    {
       
    }

    public void Editar()
    {
      
    }

    public void Excluir()
    {
    
    }

    public void Listar()
    {
        Console.Clear();
        ImprimeHeader();
        Console.WriteLine($"Poligonal: {Descricao}");
        Console.WriteLine("———————————————————————————————————————————————————————————————————————————————————————————-");
        Console.WriteLine("Estação       Ângulo lido       Deflexão       Distância(m)       Azimute");
        Console.WriteLine("============================================================================================");
        
        int estacaoNumero = 1;
        foreach (Estacao estacao in Estacoes)
        {
            Console.WriteLine($"{estacaoNumero:D4}".PadRight(14) + $"{estacao.AngEstacao}".PadRight(18) + $"{estacao.Deflexao} {estacao.Distancia:F2} {estacao.Azimute}");
            estacaoNumero++;
        }

        Console.WriteLine("============================================================================================");

        float perimetro = Perimetro();
        int paginaAtual = 1;
        int totalPaginas = 1; 

        Console.WriteLine($"Perímetro: {perimetro:F2} metros                                                        Pag.: {paginaAtual:D2} de {totalPaginas:D2}");
        ImprimeFooter();

        ExecutaComandosListar();
    }
    
    public Angulo CalcularAzimutes(Estacao? estacao = null, Angulo? azimuteAnterior = null)
    {
        if (azimuteAnterior == null || Estacoes.Count == 1)
        { 
            return new Angulo(AzGraus, AzMinutos, AzSegundos);
        }

        var angFinal = new Angulo();
        
        switch (estacao.Deflexao)
        {
            case 'D':
                CalculoDeflexaoD(estacao, angFinal, azimuteAnterior);
                break;
            case 'E':
                CalculoDeflexaoE(estacao, angFinal, azimuteAnterior);
                break;
        }
        return angFinal;
    }

    private static void CalculoDeflexaoE(Estacao estacao, Angulo angFinal, Angulo azimuteAnterior)
    {
        angFinal.Segundos = azimuteAnterior.Segundos - estacao.AngEstacao.Segundos;

        if (angFinal.Segundos < 0)
        {
            angFinal.Minutos = azimuteAnterior.Minutos + estacao.AngEstacao.Minutos - 1;
            angFinal.Minutos += 60;
        }
        else
        {
            angFinal.Minutos = azimuteAnterior.Minutos - estacao.AngEstacao.Minutos;
        }

        if (angFinal.Minutos < 0)
        {
            angFinal.Graus = azimuteAnterior.Graus - estacao.AngEstacao.Graus - 1;
            angFinal.Minutos += 60;
        }
        else
        {
            angFinal.Graus = azimuteAnterior.Graus - estacao.AngEstacao.Graus;
        }

        if (angFinal.Graus < 0)
        {
            angFinal.Graus += 360;
        }
    }

    private static void CalculoDeflexaoD(Estacao estacao, Angulo angFinal, Angulo azimuteAnterior)
    {
        angFinal.Segundos = azimuteAnterior.Segundos + estacao.AngEstacao.Segundos;

        if (angFinal.Segundos > 60)
        {
            angFinal.Minutos = 1 + azimuteAnterior.Minutos + estacao.AngEstacao.Minutos;
            angFinal.Segundos -= 60;
        }
        else
        {
            angFinal.Minutos = 1 + azimuteAnterior.Minutos + estacao.AngEstacao.Minutos;
        }

        if (angFinal.Minutos > 60)
        {
            angFinal.Graus = 1 + azimuteAnterior.Minutos + estacao.AngEstacao.Minutos;
            angFinal.Minutos -= 60;
        }
        else
        {
            angFinal.Graus = azimuteAnterior.Graus + estacao.AngEstacao.Graus;
        }

        if (angFinal.Graus > 359)
        {
            angFinal.Graus -= 360;
        }
    }

    private void ExecutaComandosListar()
    {
        while (true)
        {
            var tecla = Console.ReadKey(true);

            switch (tecla.Key)
            {
                case ConsoleKey.Escape:
                {
                    Environment.Exit(0);
                    break;
                }

                case ConsoleKey.F1:
                {
                    Inserir();
                    break;
                }

                case ConsoleKey.F2:
                {
                    Editar();
                    break;
                }

                case ConsoleKey.F3:
                {
                    Excluir();
                    break;
                }

                case ConsoleKey.PageDown:
                {
                    //todo prevpageListar
                    break;
                }

                case ConsoleKey.PageUp:
                {
                    //todo nextpageListar
                    break;
                }

                case ConsoleKey.S:
                {
                    if (tecla.Modifiers.HasFlag(ConsoleModifiers.Control))
                    {
                        SalvarLista();
                    }
                    break;
                }

                default:
                    continue;
            }
            
            Listar();
        }
    }

    private void SalvarLista()
    {
        
    }

    private void ImprimeHeader()
    {
        Console.WriteLine("Engenharia Cartográfica             Sistema de Poligonais                   Data: " + DateTime.Now.ToString("dd/MM/yyyy"));
        Console.WriteLine("============================================================================================");
    }

    private void ImprimeFooter()
    {
        Console.WriteLine("<Esc> Sair <F1> Inserir <F2> Alterar <F3> Excluir <PgDn> <PgUp>");
    }
}
