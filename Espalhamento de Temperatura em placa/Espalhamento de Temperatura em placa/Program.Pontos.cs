namespace Espalhamento_de_Temperatura_em_placa
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
                    if (colunas > Número_Max_de_Colunas) Número_Max_de_Colunas = colunas;
                    i++;
                }
                Número_Max_de_Linhas = linha;
            }



            private int número_max_de_colunas;

            public int Número_Max_de_Colunas
            {
                get { return número_max_de_colunas; }
                internal set { número_max_de_colunas = value; }
            }


            private int número_max_de_Linhas;

            public int Número_Max_de_Linhas
            {
                get { return número_max_de_Linhas; }
                internal set { número_max_de_Linhas = value; }
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

