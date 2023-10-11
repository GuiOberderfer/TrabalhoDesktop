namespace Projeto;

public partial class Poligonal
{
     void ImprimeTelaListar()
    {
      //todo ajustar tela de listagem
      //todo adicionar paginação, limitar itens a 10
      //todo utilizar funções de espaçamento
      Console.Clear();
      Console.WriteLine("Engenharia Cartográfica - Sistema de Poligonais");
      Console.WriteLine($"Poligonal: {Descricao}");
      Console.WriteLine("===================================================");
      Console.WriteLine("Estação\tÂngulo lido\tDeflexão\tDistância(m)\tAzimute");
      Console.WriteLine("===================================================");
      
      int estacaoNum = 1;
      foreach (var estacao in Estacoes)
      {
          Console.WriteLine($"{estacaoNum:D4}\t{estacao.AngEstacao}\t{estacao.Deflexao}\t{estacao.Distancia}\t{estacao.Azimute}");
          estacaoNum++;
      }

      Console.WriteLine("===================================================");
      Console.WriteLine($"Perímetro: {Perimetro():F2} metros");
      Console.WriteLine("Pressione <Esc> para Sair | <F1> Inserir | <F2> Alterar | <F3> Excluir | <CTRL + S> Salvar | <PgDn> | <PgUp>");
    }
}