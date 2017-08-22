using System;
using Matrizes;


namespace Deslocamento_de_Placas
{
    partial class Program
    {
        /// <summary>
        /// Preenche a matriz de Cálculo com  os coeficientes da Matriz Total
        /// </summary>
        /// <param name="MatrizTotal">Matriz Tipo Pontos que contêm todos os dados do Problema proposto</param>
        /// <param name="Matriz">Matriz onde se encontram todos os coeficientes</param>
        private static void Preencher_Matriz_de_Cálculo(ref Pontos MatrizTotal, ref Matriz_Simples Matriz)
        {
            /// Este código vai varrer ponto por ponto e observar os seus vizinhos para preencher a matriz de cálculo
            int Nome_do_ponto = 0;
            int N = Matriz.Número_de_Variáveis;
            DX = X_total / (MatrizTotal.Número_Max_de_Colunas - 1);
            DY = Y_total / (MatrizTotal.Número_Max_de_Linhas - 1);
            for (int i = 0; i < MatrizTotal.Linha.Length; i++)
            {
                for (int j = 0; j < MatrizTotal.Linha[i].Coluna.Length; j++)
                {
                    if (MatrizTotal.Linha[i].Coluna[j].Nome >= 0)
                    {
                        // Central
                        Matriz.A[Nome_do_ponto, MatrizTotal.Linha[i].Coluna[j].Nome] = ((6 / Math.Pow(DX, 4)) + (6 / Math.Pow(DY, 4)) + (8 / (Math.Pow(DX, 2) * Math.Pow(DY, 2))));

                        // Direita
                        if (MatrizTotal.Linha[i].Coluna[j - 1].Nome < 0)
                        {
                            Matriz.B[Nome_do_ponto] += -MatrizTotal.Linha[i].Coluna[j - 1].Valor;
                        }
                        else
                        {
                            Matriz.A[Nome_do_ponto, MatrizTotal.Linha[i].Coluna[j - 1].Nome] = 1;
                        }
                        // Esquerda
                        if (MatrizTotal.Linha[i].Coluna[j + 1].Nome < 0)
                        {
                            Matriz.B[Nome_do_ponto] += -MatrizTotal.Linha[i].Coluna[j + 1].Valor;
                        }
                        else
                        {
                            Matriz.A[Nome_do_ponto, MatrizTotal.Linha[i].Coluna[j + 1].Nome] = 1;
                        }
                        // Em cima
                        if (MatrizTotal.Linha[i + 1].Coluna[j].Nome < 0)
                        {
                            Matriz.B[Nome_do_ponto] += -MatrizTotal.Linha[i + 1].Coluna[j].Valor;
                        }
                        else
                        {
                            Matriz.A[Nome_do_ponto, MatrizTotal.Linha[i + 1].Coluna[j].Nome] = 1;
                        }
                        // Em baixo
                        if (MatrizTotal.Linha[i - 1].Coluna[j].Nome < 0)
                        {
                            Matriz.B[Nome_do_ponto] += -MatrizTotal.Linha[i - 1].Coluna[j].Valor;
                        }
                        else
                        {
                            Matriz.A[Nome_do_ponto, MatrizTotal.Linha[i - 1].Coluna[j].Nome] = 1;
                        }
                        Nome_do_ponto++;
                    }
                }
            }
        }
    }
}