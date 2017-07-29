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



            Inicializar_Matriz(out Pontos MatrizTotal);

            Mostrar_Matriz(ref MatrizTotal, "\n\n A mantiz original era: \n\n\n");

            Criar_Matriz_de_Cálculo(ref MatrizTotal, out Matriz_Simples Matriz_Problema);

            Mostrar_Matriz(ref Matriz_Problema, "\n\n A mantiz de Cálculo que foi instanciada é: \n\n");

            Preencher_Matriz_de_Cálculo(ref MatrizTotal, ref Matriz_Problema);

            Mostrar_Matriz(ref Matriz_Problema, "\n\n A mantiz de Cálculo que foi encontrada é: \n\n");

            Matriz_Problema.Verificar_critério_de_Sassenfeld();

            Matriz_Problema.Solucionar_matriz();



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

        private static void Criar_Matriz_de_Cálculo(ref Pontos matrizTotal, out Matriz_Simples matriz_Problema)
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
            matriz_Problema = new Matriz_Simples(Número_de_pontos, ref matrizTotal);
        }

        /// <summary>
        /// Preenche a matriz de Cálculo com  os coeficientes da Matriz Total
        /// </summary>
        /// <param name="MatrizTotal">Matriz Tipo Pontos que contêm todos os dados do Problema proposto</param>
        /// <param name="Matriz_Cálculo">Matriz onde se encontram todos os coeficientes</param>
        private static void Preencher_Matriz_de_Cálculo(ref Pontos MatrizTotal, ref Matriz_Simples m)
        {
            /// Este código vai varrer ponto por ponto e observar os seus vizinhos para preencher a matriz de cálculo
            int Nome_do_ponto = 0;
            int N = m.Número_de_Variáveis;
            for (int i = 0; i < MatrizTotal.Linha.Length; i++)
            {
                for (int j = 0; j < MatrizTotal.Linha[i].Coluna.Length; j++)
                {
                    if (MatrizTotal.Linha[i].Coluna[j].nome >= 0)
                    {
                        // Central
                        m.A[Nome_do_ponto, MatrizTotal.Linha[i].Coluna[j].nome] = -4;

                        // Direita
                        if (MatrizTotal.Linha[i].Coluna[j - 1].nome < 0)
                        {
                            m.B[Nome_do_ponto] += -MatrizTotal.Linha[i].Coluna[j - 1].valor;
                        }
                        else
                        {
                            m.A[Nome_do_ponto, MatrizTotal.Linha[i].Coluna[j - 1].nome] = 1;
                        }
                        // Esquerda
                        if (MatrizTotal.Linha[i].Coluna[j + 1].nome < 0)
                        {
                            m.B[Nome_do_ponto] += -MatrizTotal.Linha[i].Coluna[j + 1].valor;
                        }
                        else
                        {
                            m.A[Nome_do_ponto, MatrizTotal.Linha[i].Coluna[j + 1].nome] = 1;
                        }
                        // Em cima
                        if (MatrizTotal.Linha[i + 1].Coluna[j].nome < 0)
                        {
                            m.B[Nome_do_ponto] += -MatrizTotal.Linha[i + 1].Coluna[j].valor;
                        }
                        else
                        {
                            m.A[Nome_do_ponto, MatrizTotal.Linha[i + 1].Coluna[j].nome] = 1;
                        }
                        // Em baixo
                        if (MatrizTotal.Linha[i - 1].Coluna[j].nome < 0)
                        {
                            m.B[Nome_do_ponto] += -MatrizTotal.Linha[i - 1].Coluna[j].valor;
                        }
                        else
                        {
                            m.A[Nome_do_ponto, MatrizTotal.Linha[i - 1].Coluna[j].nome] = 1;
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
        public static void Mostrar_Matriz(ref Matriz_Simples Matriz, string texto = "\n Matriz Gerada\n\n\n")
        {
            Console.Write(texto);
            for (int i = 0; i < Matriz.Número_de_Variáveis; i++)
            {
                Console.Write("  ");
                for (int j = 0; j < Matriz.Número_de_Variáveis; j++)
                {
                    if (j == 0)
                    {
                        Console.Write("| {0:#0.##}", Matriz.A[i, j]);
                    }

                    if (j != Matriz.Número_de_Variáveis - 1)
                        Console.Write(" {0:#0.##}", Matriz.A[i, j]);
                    else
                        Console.Write("{0:#0.##} | | {1:#0.##} |  | {2:#0.##} |", Matriz.A[i, j], Matriz.X[i], Matriz.B[i]);
                }
                Console.Write("\n\n");
            }
            Console.Write("\n\n\n Tecle calquer tecla para continuar:\n");
            Console.ReadKey();
        }

    }
}

