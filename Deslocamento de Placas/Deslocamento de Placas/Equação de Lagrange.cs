using System;
using Matrizes;


namespace Deslocamento_de_Placas
    {
    partial class Program
        {
        delegate Double FunçãoMatemática(double X, double N);

        /// <summary>
        /// Preenche a matriz de Cálculo com  os coeficientes da Matriz Total
        /// </summary>
        /// <param name="MatrizTotal">Matriz Tipo Pontos que contêm todos os dados do Problema proposto</param>
        /// <param name="Matriz">Matriz onde se encontram todos os coeficientes</param>
        private static void Preencher_Matriz_de_Cálculo(ref Pontos MatrizTotal, ref Matriz_Simples Matriz)
            {
            FunçãoMatemática Pow = Math.Pow;
            /// Este código vai varrer ponto por ponto e observar os seus vizinhos para preencher a matriz de cálculo
            int Nome_do_ponto = 0;
            int N = Matriz.Número_de_Variáveis;
            DX = X_total / ( MatrizTotal.Número_Max_de_Colunas - 1 );
            DY = Y_total / ( MatrizTotal.Número_Max_de_Linhas - 1 );
            for ( int i = 0; i < MatrizTotal.Linha.Length; i++ )
                {
                for ( int j = 0; j < MatrizTotal.Linha[ i ].Coluna.Length; j++ )
                    {
                    if ( MatrizTotal.Linha[ i ].Coluna[ j ].Nome >= 0 )
                        {
                        // Central
                        Matriz.A[ Nome_do_ponto, MatrizTotal.Linha[ i ].Coluna[ j ].Nome ] = ( ( 6 / Pow(DX, 4) ) + ( 6 / Pow(DY, 4) ) + ( 8 / ( Pow(DX, 2) * Pow(DY, 2) ) ) );


                        // Esquerda
                        if ( ( j - 1 ) >= 0 && MatrizTotal.Linha[ i ].Coluna[ j - 1 ].Nome >= 0 )
                            {

                            Matriz.A[ Nome_do_ponto, MatrizTotal.Linha[ i ].Coluna[ j - 1 ].Nome ] += -( 4 / ( Pow(DY, 2) ) ) - ( 4 / ( Pow(DY, 2) * Pow(DX, 2) ) );

                            }
                        // 2 X Esquerda
                        if ( ( j - 2 ) >= 0 && MatrizTotal.Linha[ i ].Coluna[ j - 2 ].Nome >= 0 )
                            {
                            Matriz.A[ Nome_do_ponto, MatrizTotal.Linha[ i ].Coluna[ j - 2 ].Nome ] += ( 1 / ( Pow(DX, 4) ) );
                            }
                        // Direita
                        if ( ( j + 1 ) < MatrizTotal.Linha[ i ].Coluna.Length && MatrizTotal.Linha[ i ].Coluna[ j + 1 ].Nome >= 0 )
                            {
                            Matriz.A[ Nome_do_ponto, MatrizTotal.Linha[ i ].Coluna[ j + 1 ].Nome ] += -( 4 / ( Pow(DX, 2) ) ) - ( 4 / ( Pow(DX, 2) * Pow(DY, 2) ) );
                            }
                        // 2 X Direita
                        if ( ( j + 2 ) < MatrizTotal.Linha[ i ].Coluna.Length && MatrizTotal.Linha[ i ].Coluna[ j + 2 ].Nome >= 0 )
                            {
                            Matriz.A[ Nome_do_ponto, MatrizTotal.Linha[ i ].Coluna[ j + 2 ].Nome ] += ( 1 / ( Pow(DX, 4) ) );
                            }
                        // Em cima
                        if ( MatrizTotal.Linha[ i + 1 ].Coluna[ j ].Nome >= 0 )
                            {
                            Matriz.A[ Nome_do_ponto, MatrizTotal.Linha[ i + 1].Coluna[ j + 2 ].Nome ] += -( 4 / ( Pow(DX, 2) ) ) - ( 4 / ( Pow(DX, 2) * Pow(DY, 2) ) );
                            }
                        else
                            {
                            Matriz.A[ Nome_do_ponto, MatrizTotal.Linha[ i + 1 ].Coluna[ j ].Nome ] = 1;
                            }
                        // Em baixo
                        if ( MatrizTotal.Linha[ i - 1 ].Coluna[ j ].Nome < 0 )
                            {
                            Matriz.B[ Nome_do_ponto ] += -MatrizTotal.Linha[ i - 1 ].Coluna[ j ].Valor;
                            }
                        else
                            {
                            Matriz.A[ Nome_do_ponto, MatrizTotal.Linha[ i - 1 ].Coluna[ j ].Nome ] = 1;
                            }

                        // Valor deslocamento
                        Matriz.B[ Nome_do_ponto ] = MatrizTotal.Linha[ i ].Coluna[ j ].Valor / D;
                        Nome_do_ponto++;
                        }
                    }
                }
            }
        }
    }