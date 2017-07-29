namespace diferença_finita_1
{
    partial class Program
    {
        public class Pontos
        {
            public Linha_da_Matriz[] Linha;
            public Pontos(int linha, int[] Coluna)
            {
                Linha = new Linha_da_Matriz[linha];
                int i = 0;
                foreach (int colunas in Coluna)
                {
                    Linha[i] = new Linha_da_Matriz(colunas);
                    i++;
                }
            }
        }

        public class Linha_da_Matriz 
        {
            public Coluna_da_Matriz[] Coluna;
            public Linha_da_Matriz(int coluna)
            {
                Coluna = new Coluna_da_Matriz[coluna];
            }
        }

        public struct Coluna_da_Matriz : IColuna_da_Matriz
        {
            public double Valor { get; set; }
            public int Nome { get; set; }
        }

    }
}

