using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace diferença_finita_1
{
    partial class Program
    {



        static void Main(string[] args)
        {


            Matriz_Simples Matriz_Problema;
            Pontos MatrizTotal;

            Inicializar_Matriz(out MatrizTotal);

            Mostrar_Matriz(ref MatrizTotal, "\n\n A mantiz original era: \n\n\n");


            Criar_Matriz_de_Cálculo(ref MatrizTotal, out double[,] Matriz_Cálculo, out Matriz_Problema);

            Mostrar_Matriz(ref Matriz_Cálculo, "\n\n A mantiz de Cálculo que foi instanciada é: \n\n");

            Preencher_Matriz_de_Cálculo(ref MatrizTotal, ref Matriz_Cálculo, ref Matriz_Problema);

            Mostrar_Matriz(ref Matriz_Cálculo, "\n\n A mantiz de Cálculo que foi encontrada é: \n\n");

            Verificar_critério_de_Sassenfeld(ref Matriz_Cálculo);

            Gauss_Siedel(ref Matriz_Cálculo, ref MatrizTotal);

            int Linha = 0;
            int Coluna = 0;
            StreamWriter writer = new StreamWriter("saida.txt");
            foreach (Linha_da_Matriz linha in MatrizTotal.Linha)
            {
                foreach (Coluna_da_Matriz coluna in linha.Coluna)
                {
                    writer.Write("{0} {1} {2} \r\n", Linha, Coluna, coluna.valor);
                    Coluna++;
                }
                Linha++;
                writer.Write("\r\n");
                Coluna = 0;
            }
            writer.Close();


        }

        private static void Criar_Matriz_de_Cálculo(ref Pontos matrizTotal, out double[,] matriz_Cálculo, out Matriz_Simples matriz_Problema)
        {
            int Número_de_pontos = 0;
            for (int i = 0; i < matrizTotal.Linha.Length; i++)
            {
                for (int j = 0; j < matrizTotal.Linha[i].Coluna.Length; j++)
                {
                    if (matrizTotal.Linha[i].Coluna[j].nome >= 0)
                    {
                        matrizTotal.Linha[i].Coluna[j].nome = Número_de_pontos;
                        Número_de_pontos++;
                    }
                }
            }
            matriz_Problema = new Matriz_Simples(Número_de_pontos);
            matriz_Cálculo = new double[Número_de_pontos, Número_de_pontos + 1];
        }

        /// <summary>
        /// Preenche a matriz de Cálculo com  os coeficientes da Matriz Total
        /// </summary>
        /// <param name="MatrizTotal">Matriz Tipo Pontos que contêm todos os dados do Problema proposto</param>
        /// <param name="Matriz_Cálculo">Matriz onde se encontram todos os coeficientes</param>
        private static void Preencher_Matriz_de_Cálculo(ref Pontos MatrizTotal, ref double[,] Matriz_Cálculo, ref Matriz_Simples matriz_Problema )
        {
            int Nome_do_ponto = 0;
            int N = matriz_Problema.Número_de_Variáveis;
            for (int i = 0; i < MatrizTotal.Linha.Length; i++)
            {
                for (int j = 0; j < MatrizTotal.Linha[i].Coluna.Length; j++)
                {
                    if (MatrizTotal.Linha[i].Coluna[j].nome >= 0)
                    {
                        // Central
                        Matriz_Cálculo[Nome_do_ponto, MatrizTotal.Linha[i].Coluna[j].nome] = 1 * -4;

                        // Direita
                        if (MatrizTotal.Linha[i].Coluna[j - 1].nome < 0)
                        {
                            Matriz_Cálculo[Nome_do_ponto, N] += -MatrizTotal.Linha[i].Coluna[j - 1].valor;
                        }
                        else
                        {
                            Matriz_Cálculo[Nome_do_ponto, MatrizTotal.Linha[i].Coluna[j - 1].nome] = 1;
                        }
                        // Esquerda
                        if (MatrizTotal.Linha[i].Coluna[j + 1].nome < 0)
                        {
                            Matriz_Cálculo[Nome_do_ponto, N] += -MatrizTotal.Linha[i].Coluna[j + 1].valor;
                        }
                        else
                        {
                            Matriz_Cálculo[Nome_do_ponto, MatrizTotal.Linha[i].Coluna[j + 1].nome] = 1;
                        }
                        // Em cima
                        if (MatrizTotal.Linha[i + 1].Coluna[j].nome < 0)
                        {
                            Matriz_Cálculo[Nome_do_ponto, N] += -MatrizTotal.Linha[i + 1].Coluna[j].valor;
                        }
                        else
                        {
                            Matriz_Cálculo[Nome_do_ponto, MatrizTotal.Linha[i + 1].Coluna[j].nome] = 1;
                        }
                        // Em baixo
                        if (MatrizTotal.Linha[i - 1].Coluna[j].nome < 0)
                        {
                            Matriz_Cálculo[Nome_do_ponto, N] += -MatrizTotal.Linha[i - 1].Coluna[j].valor;
                        }
                        else
                        {
                            Matriz_Cálculo[Nome_do_ponto, MatrizTotal.Linha[i - 1].Coluna[j].nome] = 1;
                        }
                        Nome_do_ponto++;
                    }
                }
            }
        }

        /// <summary>
        /// Conta o númeor de pontos que realmente fazem parte da matriz de cálculo e instancia a matriz de calculo
        /// </summary>
        /// <param name="MatrizTotal"></param>
        /// <param name="Matriz_Cálculo"></param>
        static public void Criar_Matriz_de_Cálculo(ref Pontos MatrizTotal, out double[,] Matriz_Cálculo)
        {
            int Número_de_pontos = 0;
            for (int i = 0; i < MatrizTotal.Linha.Length; i++)
            {
                for (int j = 0; j < MatrizTotal.Linha[i].Coluna.Length; j++)
                {
                    if (MatrizTotal.Linha[i].Coluna[j].nome >= 0)
                    {
                        MatrizTotal.Linha[i].Coluna[j].nome = Número_de_pontos;
                        Número_de_pontos++;
                    }
                }
            }
            Matriz_Cálculo = new double[Número_de_pontos, Número_de_pontos + 1];
        }

        /// <summary>
        /// Inicializa de forma vagabunda os dados da matriz onde todas as informações são armazenadas
        /// </summary>
        /// <param name="MatrizTotal">Matriz vazia onde vão ser armazenadas todos os pontos</param>
        public static void Inicializar_Matriz(out Pontos MatrizTotal)
        {
            string[] Linhas = File.ReadAllLines(@"caso muito grande.txt", Encoding.UTF8);
            Matriz_de_Temp Matriz_de_Temperaturas = new Matriz_de_Temp();
            Matriz_de_Temperaturas.Adicionar_Ponto(0, 0);
            int Caso = 0;
            int Número_de_Linhas = 0;
            int Char_branco = 0;
            List<int> Número_de_Colunas = new List<int>();
            #region Recolhe a Matriz de temperaturas
            foreach (string linha in Linhas)
            {
                if (linha[0] == '#')
                {
                    Caso++;
                }
                else
                {
                    string[] Valores;
                    switch (Caso)
                    {
                        case 1:
                            {
                                Valores = linha.Split(' ');
                                Matriz_de_Temperaturas.Adicionar_Ponto(Convert.ToInt32(Valores[0]), Convert.ToDouble(Valores[1], System.Globalization.CultureInfo.CurrentCulture));

                                break;
                            }
                        case 2:
                            {
                                Valores = linha.Split(' ');
                                Número_de_Linhas++;
                                foreach (string t in Valores)
                                {
                                    if (t == "") Char_branco++;

                                }
                                Número_de_Colunas.Add(Valores.Length - Char_branco);
                                Char_branco = 0;
                                break;
                            }
                        case 3:
                            {
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("Você não deveria estar lendo isto.");
                                break;
                            }
                    }
                }
            }
            #endregion
            #region Recolhe todas as outras informações da Matriz
            Caso = 0;
            MatrizTotal = new Pontos(Número_de_Linhas, Número_de_Colunas.ToArray());
            int contador1 = 0;
            int contador2 = 0;
            foreach (string linha in Linhas)
            {
                if (linha[0] == '#')
                {
                    Caso++;
                    contador1 = 0;
                    contador2 = 0;
                }
                else
                {
                    string[] Valores;
                    switch (Caso)
                    {
                        case 1:
                            {
                                break;
                            }
                        case 2:
                            {
                                contador2 = 0;
                                Valores = linha.Split(' ');
                                foreach (string valor in Valores)
                                {
                                    if (valor != "")
                                    {
                                        MatrizTotal.Linha[contador1].Coluna[contador2].valor = Matriz_de_Temperaturas.Temperatura[Convert.ToInt32(valor)].Temperatura;
                                        contador2++;
                                    }
                                }
                                contador1++;
                                break;
                            }
                        case 3:
                            {
                                contador2 = 0;
                                Valores = linha.Split(' ');
                                foreach (string valor in Valores)
                                {
                                    if (valor != "")
                                    {
                                        MatrizTotal.Linha[contador1].Coluna[contador2].nome = Convert.ToInt32(valor) * -1;
                                        contador2++;
                                    }
                                }
                                contador1++;
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("Você não deveria estar lendo isto.");
                                break;
                            }
                    }
                }
            }
            #endregion
        }

        /// <summary>
        /// Mostra no Console como ficara a matriz com todos os pontos da simulação.
        /// </summary>
        /// <param name="Matriz">Matriz tipo Pontos que vai ser exebida</param>
        /// <param name="texto">Texto opcional</param>
        public static void Mostrar_Matriz(ref Pontos Matriz, string texto = "\n Matriz Gerada\n\n\n")
        {
            Console.Write(texto);
            for (int i = 0; i < Matriz.Linha.Length; i++)
            {
                Console.Write("\t");
                for (int j = 0; j < Matriz.Linha[i].Coluna.Length; j++)
                {
                    if (Matriz.Linha[i].Coluna[j].nome < 0 && Matriz.Linha[i].Coluna[j].valor == 0)
                        Console.Write("       ");
                    else
                    if (Matriz.Linha[i].Coluna[j].valor >= 0)
                        Console.Write("  {0:00.00}", Matriz.Linha[i].Coluna[j].valor);
                    else
                        Console.Write(" {0:00.00}", Matriz.Linha[i].Coluna[j].valor);
                }
                Console.Write("\n\n");
            }
            Console.Write("\n\n\n Tecle calquer tecla para continuar:\n");
            Console.ReadKey();
        }
        public static void Mostrar_Matriz(ref double[,] m, string texto = "\n Matriz Gerada\n\n")
        {
            Console.Write(texto);
            for (int i = 0; i < m.GetLength(0); i++)
            {
                Console.Write("\t");
                for (int j = 0; j < m.GetLength(1); j++)
                {
                    Console.Write(" {0}", m[i, j]);
                }
                Console.Write("\n");
            }
            Console.Write("\n Tecle calquer tecla para continuar:\n");
            Console.ReadKey();
        }
        public static void Mostrar_Matriz(ref double[] m, string texto = "\n Matriz Gerada\n\n")
        {
            Console.Write(texto);
            for (int i = 0; i < m.Length; i++)
            {
                Console.Write("\t");

                Console.Write(" {0}", m[i]);

                Console.Write("\n");
            }
            Console.Write("\n Tecle calquer tecla para continuar:\n");
            Console.ReadKey();
        }


        /// <summary>
        /// Verifica se a matriz de Cálculo tem solução e se converge para ela.
        /// </summary>
        /// <param name="Matriz_Cálculo"></param>
        public static void Verificar_critério_de_Sassenfeld(ref double[,] Matriz_Cálculo)
        {
            int N = Convert.ToInt32(Math.Truncate(Math.Sqrt(Matriz_Cálculo.Length)));
            double[] Sassen = new double[N];

            // Instancia um array com todos os valores pré-setados em 1
            int contador = 0;
            foreach (double valor in Sassen)
            {
                Sassen[contador] = 1;
                contador++;
            }
            for (int i = 0; i < N; i++)
            {
                double Novo_Sassen = 0;
                for (int j = 0; j < N; j++)
                {
                    if (i != j)
                    {
                        Novo_Sassen += Sassen[j] * Math.Abs(Matriz_Cálculo[i, j]);
                    }
                }
                Sassen[i] = Novo_Sassen / Math.Abs(Matriz_Cálculo[i, i]);
            }
            Console.Write("\n Para a Matriz de Cálculo convergir para a solução,\n o vetor de Sassenfeld tem que ter todos os valores menores que 1");
            Mostrar_Matriz(ref Sassen, "\n\n O vetor de Sassenfeld que foi encontrado é: \n\n");
            contador = 0;
            bool flag = false;
            foreach (double valor in Sassen)
            {
                if (Sassen[contador] >= 1)
                {
                    flag = true;
                }
                contador++;
            }
            if (flag)
            {
                Console.Write("\n A Matriz Não Passou no critério de Sassenfeld\n\n");
            }
            else
            {
                Console.Write("\n OK. A Matriz Passou no critério de Sassenfeld\n\n");
            }
            Console.Write("\n Tecle calquer tecla para continuar:\n");
            Console.ReadKey();
        }

        public static void Gauss_Siedel(ref double[,] Matriz_Cálculo, ref Pontos Matriz)
        {
            // determina o número de variáveis da matriz de cálculo 
            int N = Convert.ToInt32(Math.Truncate(Math.Sqrt(Matriz_Cálculo.Length)));
            double[] X = new double[N];
            int contador = 0;
            foreach (double valor in X)
            {
                X[contador] = 1;
                contador++;
            }
            double[] B = new double[N];
            contador = 0;
            foreach (double valor in X)
            {
                B[contador] = Matriz_Cálculo[contador, N];
                contador++;
            }

            int k = 0;
            int tol = 10000;
            while (k < tol)
            {
                for (int i = 0; i <= N - 1; i++)
                {
                    X[i] = (B[i] - somat(i, N, ref Matriz_Cálculo, ref X) + Matriz_Cálculo[i, i] * X[i]) / Matriz_Cálculo[i, i];
                }
                k++;
            }

            Mostrar_Matriz(ref X, "\n\n O vetor Solução que foi encontrado é: \n\n");

            int Contador = 0;
            for (int i = 0; i < Matriz.Linha.Length; i++)
            {
                for (int j = 0; j < Matriz.Linha[i].Coluna.Length; j++)
                {
                    if (Matriz.Linha[i].Coluna[j].nome >= 0)
                    {
                        Matriz.Linha[i].Coluna[j].valor = X[Contador];
                        Contador++;
                    }
                }
            }
            Mostrar_Matriz(ref Matriz, "\n\n A mantiz final foi: \n\n\n\n");
        }

        public static double somat(int i, int N, ref double[,] Matriz_Cálculo, ref double[] X)
        {
            {
                double soma = 0;
                for (int j = 0; j <= N - 1; j++)
                {
                    soma += Matriz_Cálculo[i, j] * X[j];
                }
                return soma;
            }
        }
    }
}

