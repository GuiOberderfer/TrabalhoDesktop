namespace Projeto
{
    static class Program
    {
        static void Main(string[] args)
        {
            Poligonal poligonal = new Poligonal("Fazenda Rio Verde", 225, 32, 48);
            var estacao = new Estacao(new Angulo(2, 2, 2), 4.5f, 'D');
            estacao.Azimute = poligonal.CalcularAzimutes(estacao);
            poligonal.Estacoes.Add(estacao);
            poligonal.Listar();
        }
    }
}



