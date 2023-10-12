namespace Projeto;

public partial class Poligonal
{
    void ImprimeTelaListar()
    {
        //todo ajustar tela de listagem
        //todo adicionar paginação, limitar itens a 10
        //todo utilizar funções de espaçamento
        Console.Clear();
        Console.WriteLine("Engenharia Cartográfica             Sistema de Poligonais             Data: " + DateTime.Now.ToString("dd/MM/yyyy"));
        Console.WriteLine("======================================================================================");
        Console.WriteLine($"Poligonal: {Descricao}");
        Console.WriteLine("--------------------------------------------------------------------------------------");

        // Cabeçalho da tabela
        Console.WriteLine("Estação      Ângulo lido      Deflexão       Distância(m)     Azimute");
        Console.WriteLine("======================================================================================");

        int estacaoNum = 1;
        foreach (var estacao in Estacoes)
        {
            // Use formatação para alinhar os valores
            Console.WriteLine($"{estacaoNum:D4}         {estacao.AngEstacao,-14:F2}     {estacao.Deflexao,-14:F2}   {estacao.Distancia,-14:F2}     {estacao.Azimute,-14:F2}");
            estacaoNum++;
        }

        Console.WriteLine("======================================================================================");
        Console.WriteLine($"Perímetro: {Perimetro():F2} metros");
        Console.WriteLine("Pressione <Esc> para Sair | <F1> Inserir | <F2> Alterar | <F3> Excluir | <CTRL + S> Salvar | <PgDn> | <PgUp>");
    }
}
}
