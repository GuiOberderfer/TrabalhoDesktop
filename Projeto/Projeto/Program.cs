using estacao;
using System;
using System.Globalization;

class Program
{
    static void Main(string[] args)
    {
       
        Poligonal poligonal = new Poligonal(  "Fazenda Rio Verde", 225, 32, 48);

        ConsoleKeyInfo keyInfo;

        do
        {
            Console.Clear();
            Console.WriteLine("Engenharia Cartográfica             Sistema de Poligonais                   Data: " + DateTime.Now.ToString("dd/MM/yyyy"));
            Console.WriteLine("============================================================================================");
            Console.WriteLine($"Poligonal: {poligonal.Descricao}");
            Console.WriteLine("———————————————————————————————————————————————————————————————————————————————————————————-");
            Console.WriteLine("Estação       Ângulo lido       Deflexão       Distância(m)       Azimute");
            Console.WriteLine("============================================================================================");


           
            int estacaoNumero = 1;
            foreach (Estacao estacao in poligonal.Estacoes)
            {
                Console.WriteLine($"{estacaoNumero:D4} {estacao.AngEstacao} {estacao.Deflexao} {estacao.Distancia:F2} {estacao.Azimute}");
                estacaoNumero++;
            }

            Console.WriteLine("============================================================================================");

            float perimetro = poligonal.Perimetro();
            int paginaAtual = 1;
            int totalPaginas = 1; 

            Console.WriteLine($"Perímetro: {perimetro:F2} metros                                                        Pag.: {paginaAtual:D2} de {totalPaginas:D2}");
            Console.WriteLine("<Esc> Sair <F1> Inserir <F2> Alterar <F3> Excluir <PgDn> <PgUp>");

            keyInfo = Console.ReadKey();

            if (keyInfo.Key == ConsoleKey.F1)
            {

            }

            

        } while (keyInfo.Key != ConsoleKey.Escape);

        
    }
}

