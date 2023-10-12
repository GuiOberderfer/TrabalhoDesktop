namespace Projeto;

public partial class Poligonal
{
  public int pagina { get; set; } = 0;
  private int itensPorPagina = 10;

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
    Console.Clear();
    Console.WriteLine("Engenharia Cartográfica             Sistema de Poligonais             Data: " +
                      DateTime.Now.ToString("dd/MM/yyyy"));
    Console.WriteLine("======================================================================================");
    Console.WriteLine($"Poligonal: {Descricao}");
    Console.WriteLine("--------------------------------------------------------------------------------------");

    // Cabeçalho da tabela
    Console.WriteLine("Estação      Ângulo lido      Deflexão       Distância(m)     Azimute");
    Console.WriteLine("======================================================================================");
    var teto = pagina * itensPorPagina + itensPorPagina > Estacoes.ToArray().Length
        ? Estacoes.ToArray().Length - (pagina * itensPorPagina)
        : itensPorPagina;

    for (int i = pagina * itensPorPagina; i < pagina * itensPorPagina + teto; i++)
    {
      var estacao = Estacoes[i];
      Console.WriteLine($"{i + 1:D4}         {estacao.AngEstacao,-14:F2}     {estacao.Deflexao,-14:F2}   {estacao.Distancia,-14:F2}     {estacao.Azimute,-14:F2}");
    }

    Console.WriteLine("======================================================================================");
    Console.WriteLine($"Perímetro: {Perimetro():F2} metros");
    Console.WriteLine($"Pagina {pagina + 1} de {Math.Ceiling((float)Estacoes.ToArray().Length / itensPorPagina)}");
    Console.WriteLine(
        "Pressione <Esc> para Sair | <F1> Inserir | <F2> Alterar | <F3> Excluir | <CTRL + S> Salvar | <PgDn> Proxima pagina | <PgUp> Pagina anterior");
  }
}
