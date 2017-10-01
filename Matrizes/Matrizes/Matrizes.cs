using System;

namespace Matrizes
    {

    /// <summary>
    /// Esta Classe define alguns tipos mais básicos de matrizes lineares
    /// </summary>
    public class Matriz_Simples : IMatrizes
        {
        /// <summary>
        /// Controi as Mtrizes simples A[N,N] e B[N] e X[N].
        /// </summary>
        /// <param name="Número_de_Variáveis">Númeor de Variáveis da Matiriz</param>
        /// <param name="Dimensão_X">Ainda não implementado</param>
        public Matriz_Simples(int Número_de_Variáveis, decimal Dimensão_X = 1)
            {
            if ( Número_de_Variáveis <= 0 ) throw new ArgumentOutOfRangeException("Número_de_Variáveis", Número_de_Variáveis, "Insira um númeor de Variáveis Acima de 0.");
            if ( Dimensão_X != 1 ) throw new NotImplementedException("Ainda Não implementei para dimenões maiores de X[].");
            número_de_Variáveis = Número_de_Variáveis;
            a = new double[ Número_de_Variáveis, Número_de_Variáveis ];
            b = new double[ Número_de_Variáveis ];
            Sassen = new double[ Número_de_Variáveis ];
            if ( Dimensão_X == 1 )
                {
                x = new double[ Número_de_Variáveis ];
                }
            }

        /// <summary>
        /// informa a quantidade de interações até alcançar a solução com o número de casas decimais necessários
        /// </summary>
        private int número_de_Tentativas = 0;
        /// <summary>
        /// informa a quantidade de interações até alcançar a solução com o número de casas decimais necessários
        /// </summary>
        public int Número_de_Tentativas
            {
            get { return número_de_Tentativas = 0; }
            internal set { número_de_Tentativas = value; }
            }


        /// <summary>
        /// Númeor de Interações. Padrão 10.000
        /// </summary>
        private int interações = 100000;
        /// <summary>
        /// Númeor de Interações. Padrão 10.000
        /// </summary>
        public int Interações
            {
            get { return interações; }
            set { interações = value; }
            }

        /// <summary>
        /// Númeor de casas
        /// </summary>
        private int precisão = 4;
        /// <summary>
        /// Númeor de casas
        /// </summary>
        public int Precisão
            {
            get { return precisão; }
            set
                {
                if ( value < 0 || value > 12 ) throw new ArgumentOutOfRangeException("Precissão", value, "Insira um número de casas decimais de precisão entre 0 e 11.");
                precisão = value;
                }
            }


        /// <summary>
        /// Valores de Sassenfield para o critério do método de Gaus-Siedel
        /// </summary>
        private double[] sassen;
        /// <summary>
        /// Valores de Sassenfield para o critério do método de Gaus-Siedel
        /// </summary>
        public double[] Sassen
            {
            get { return sassen; }
            internal set { sassen = value; }
            }

        /// <summary>
        /// Matriz de Coeficientes
        /// </summary>
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

        /// <summary>
        /// Verifica se a matriz de Cálculo tem solução e se converge para ela.
        /// </summary>
        public bool Verificar_critério_de_Sassenfeld()
            {
            // Instancia um array com todos os valores pré-setados em 1
            int contador = 0;
            foreach ( double valor in Sassen )
                {
                Sassen[ contador ] = 1;
                contador++;
                }
            for ( int i = 0; i < Número_de_Variáveis; i++ )
                {
                double Novo_Sassen = 0;
                for ( int j = 0; j < Número_de_Variáveis; j++ )
                    {
                    if ( i != j )
                        {
                        Novo_Sassen += Sassen[ j ] * Math.Abs(A[ i, j ]);
                        }
                    }
                Sassen[ i ] = Novo_Sassen / Math.Abs(A[ i, i ]);
                }
            contador = 0;
            bool flag = false;
            foreach ( double valor in Sassen )
                {
                if ( Sassen[ contador ] >= 1 )
                    {
                    flag = true;
                    return flag;
                    }
                contador++;
                }
            return flag;
            }

        /// <summary>
        /// Fução responsável por solucionar a matriz A atraves de Gaus-Siedel
        /// </summary>
        public void Solucionar_matriz()
            {
            // determina o número de variáveis da matriz de cálculo 
            int N = Número_de_Variáveis;
            int contador = 0;
            foreach ( double valor in x )
                {
                X[ contador ] = 1;
                contador++;
                }
            double[] Erro = new double[ Número_de_Variáveis ];
            bool Para_interação = true;


            double ValordePrecisão = 1 / Math.Pow(10, Precisão + 1);
            int k = 0;
            while ( k < Interações )
                {
                for ( int i = 0; i < Número_de_Variáveis; i++ )
                    {
                    Erro[ i ] = X[ i ];
                    X[ i ] = ( B[ i ] - Somat(i) + A[ i, i ] * X[ i ] ) / A[ i, i ];
                    Erro[ i ] = Math.Abs(Erro[ i ] - X[ i ]);
                    }
                Para_interação = true;
                for ( int i = 0; i < Número_de_Variáveis; i++ )
                    {
                    if ( Erro[ i ] > ValordePrecisão )
                        {
                        Para_interação = false;
                        break;
                        }
                    }
                if ( Para_interação ) break;
                k++;
                }
            Número_de_Tentativas = k;
            }

        private double Somat(int i)
            {

            double soma = 0;
            for ( int j = 0; j < Número_de_Variáveis; j++ )
                {
                soma += A[ i, j ] * X[ j ];
                }
            return soma;

            }

        }
    }

