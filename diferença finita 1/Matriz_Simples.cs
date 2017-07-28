using System;

namespace diferença_finita_1
{
    partial class Program
    {
        /// <summary>
        /// Esta Classe define alguns tipos mais básicos de matrizes lineares
        /// </summary>
        public class Matriz_Simples : IMatriz_Simples
        {
            /// <summary>
            /// Controi as Mtrizes simples A[N,N] e B[N] e X[N].
            /// </summary>
            /// <param name="Número_de_Variáveis">Númeor de Variáveis da Matiriz</param>
            /// <param name="Dimensão_X">Ainda não implementado</param>
           public Matriz_Simples(int Número_de_Variáveis, decimal Dimensão_X = 1)
            {
                if (Número_de_Variáveis <= 0) throw new ArgumentOutOfRangeException("Número_de_Variáveis", Número_de_Variáveis, "Insira um númeor de Variáveis Acima de 0.");
                if (Dimensão_X != 1) throw new NotImplementedException("Ainda Não implementei para dimenões maiores de X[].");
                número_de_Variáveis = Número_de_Variáveis;

                a = new double[Número_de_Variáveis, Número_de_Variáveis];
                b = new double[Número_de_Variáveis];
                if (Dimensão_X == 1)
                {
                    x = new double[Número_de_Variáveis];
                }
            }


            private double[,] a;
            /// <summary>
            /// Matriz de Coeficientes
            /// </summary>
            public double[,] A
            {
                get { return a; }
                set { a = value; }
            }

            private double[] x;
            /// <summary>
            /// Matriz de Incógnitas
            /// </summary>
            public double[] X
            {
                get { return x; }
                set { x = value; }
            }

            private double[] b;
            /// <summary>
            /// Matriz Solução
            /// </summary>
            public double[] B
            {
                get { return b; }
                set { b = value; }
            }

            private int número_de_Variáveis;
            /// <summary>
            /// Número de Variáveis da minha matriz
            /// </summary>
            public int Número_de_Variáveis
            {
                get { return número_de_Variáveis; }
            }


        }
    }
}

