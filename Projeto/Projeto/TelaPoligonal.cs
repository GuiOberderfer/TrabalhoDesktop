namespace Projeto;

public partial class Poligonal
{
  public int pagina { get; set; } = 0;
  private int itensPorPagina = 5;

  void proximaPagina()
  {
    if (pagina < Math.Ceiling((float)Estacoes.ToArray().Length / itensPorPagina) - 1)
      pagina++;
  }

  void paginaAnterior()
  {
    if (pagina > 0)
      pagina--;
  }

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


    var teto = pagina * itensPorPagina + itensPorPagina > Estacoes.ToArray().Length ? Estacoes.ToArray().Length - (pagina * itensPorPagina) : itensPorPagina;
    Console.WriteLine(teto);
    for (int i = pagina * itensPorPagina; i < pagina * itensPorPagina + teto; i++)
    {
      var estacao = Estacoes[i];
      CalcAzimute(Estacoes[i]);
      Console.WriteLine($"{i + 1:D4}\t{estacao.AngEstacao}\t{estacao.Deflexao}\t{estacao.Distancia}\t{estacao.Azimute}");
    }

    Console.WriteLine("===================================================");
    Console.WriteLine($"Perímetro: {Perimetro():F2} metros");
    Console.WriteLine("Pressione <Esc> para Sair | <F1> Inserir | <F2> Alterar | <F3> Excluir | <CTRL + S> Salvar | <PgDn> | <PgUp>");
  }
}
