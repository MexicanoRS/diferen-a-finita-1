using System;
using Matrizes;


namespace Deslocamento_de_Placas
    {
    partial class Program
        {


        delegate Double FunçãoMatemática(double X, double N);
        static int PosLinha = 0;
        static int PosColuna = 0;
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
                    PosLinha = i;
                    PosColuna = j;
                    if ( MatrizTotal.Linha[ i ].Coluna[ j ].Nome >= 0 )
                        {

                        // Central
                        //         ┌───┐
                        //         │   │
                        //     ┌───┼───┼───┐
                        //     │   │   │   │
                        // ┌───┼───┼───┼───┼───┐
                        // │   │   │ █ │   │   │
                        // └───┼───┼───┼───┼───┘
                        //     │   │   │   │   
                        //     └───┼───┼───┘
                        //         │   │ 
                        //         └───┘
                        Matriz.A[ Nome_do_ponto, MatrizTotal.Linha[ i ].Coluna[ j ].Nome ] = ( ( 6 / Pow(DX, 4) ) + ( 6 / Pow(DY, 4) ) + ( 8 / ( Pow(DX, 2) * Pow(DY, 2) ) ) );


                        // Esquerda
                        //         ┌───┐
                        //         │   │
                        //     ┌───┼───┼───┐
                        //     │   │   │   │
                        // ┌───┼───┼───┼───┼───┐
                        // │   │ █ │ □ │   │   │
                        // └───┼───┼───┼───┼───┘
                        //     │   │   │   │   
                        //     └───┼───┼───┘
                        //         │   │  
                        //         └───┘
                        if ( ( j - 1 ) >= 0 && MatrizTotal.Linha[ i ].Coluna[ j - 1 ].Nome >= 0 )
                            {
                            Matriz.A[ Nome_do_ponto, MatrizTotal.Linha[ i ].Coluna[ j - 1 ].Nome ] += -( 4 / Pow(DX, 4) ) - ( 4 / ( Pow(DX, 2) * Pow(DY, 2) ) );
                            }
                        // 2 X Esquerda
                        //         ┌───┐
                        //         │   │
                        //     ┌───┼───┼───┐
                        //     │   │   │   │
                        // ┌───┼───┼───┼───┼───┐
                        // │ █ │   │ □ │   │   │
                        // └───┼───┼───┼───┼───┘
                        //     │   │   │   │   
                        //     └───┼───┼───┘
                        //         │   │  
                        //         └───┘
                        if ( ( j - 2 ) >= 0 && MatrizTotal.Linha[ i ].Coluna[ j - 2 ].Nome >= 0 )
                            {
                            Matriz.A[ Nome_do_ponto, MatrizTotal.Linha[ i ].Coluna[ j - 2 ].Nome ] += ( 1 / ( Pow(DX, 4) ) );
                            }
                        else
                            {
                            if ( ( j - 2 ) < 0 )
                                {
                                switch ( MatrizTotal.Linha[ i ].Coluna[ j ].Nome )
                                    {
                                    case -1:
                                        Matriz.A[ Nome_do_ponto, MatrizTotal.Linha[ i ].Coluna[ j ].Nome ] += -( 1 / ( Pow(DX, 4) ) );
                                        break;
                                    case -2:
                                        Matriz.A[ Nome_do_ponto, MatrizTotal.Linha[ i ].Coluna[ j ].Nome ] += -( 1 / ( Pow(DX, 4) ) );
                                        Matriz.A[ Nome_do_ponto, MatrizTotal.Linha[ i ].Coluna[ j ].Nome ] += -( 1 / ( Pow(DX, 4) ) );
                                        Matriz.A[ Nome_do_ponto, MatrizTotal.Linha[ i ].Coluna[ j ].Nome ] += -( 1 / ( Pow(DX, 4) ) );
                                        break;
                                    default:
                                        break;
                                    }
                                Matriz.A[ Nome_do_ponto, MatrizTotal.Linha[ i ].Coluna[ j ].Nome ] += -( 1 / ( Pow(DX, 4) ) );
                                }
                            }
                        // Direita
                        //         ┌───┐
                        //         │   │
                        //     ┌───┼───┼───┐
                        //     │   │   │   │
                        // ┌───┼───┼───┼───┼───┐
                        // │   │   │ □ │ █ │   │
                        // └───┼───┼───┼───┼───┘
                        //     │   │   │   │   
                        //     └───┼───┼───┘
                        //         │   │  
                        //         └───┘
                        if ( ( j + 1 ) < MatrizTotal.Linha[ i ].Coluna.Length && MatrizTotal.Linha[ i ].Coluna[ j + 1 ].Nome >= 0 )
                            {
                            Matriz.A[ Nome_do_ponto, MatrizTotal.Linha[ i ].Coluna[ j + 1 ].Nome ] += -( 4 / Pow(DX, 4) ) - ( 4 / ( Pow(DX, 2) * Pow(DY, 2) ) );
                            }
                        // 2 X Direita
                        //         ┌───┐
                        //         │   │
                        //     ┌───┼───┼───┐
                        //     │   │   │   │
                        // ┌───┼───┼───┼───┼───┐
                        // │   │   │ □ │   │ █ │
                        // └───┼───┼───┼───┼───┘
                        //     │   │   │   │   
                        //     └───┼───┼───┘
                        //         │   │  
                        //         └───┘
                        if ( ( j + 2 ) < MatrizTotal.Linha[ i ].Coluna.Length && MatrizTotal.Linha[ i ].Coluna[ j + 2 ].Nome >= 0 )
                            {
                            Matriz.A[ Nome_do_ponto, MatrizTotal.Linha[ i ].Coluna[ j + 2 ].Nome ] += ( 1 / ( Pow(DX, 4) ) );
                            }
                        else
                            {
                            if ( ( j + 2 ) >= MatrizTotal.Linha[ i ].Coluna.Length )
                                Matriz.A[ Nome_do_ponto, MatrizTotal.Linha[ i ].Coluna[ j ].Nome ] += -( 1 / ( Pow(DX, 4) ) );
                            }
                        // Em cima
                        //         ┌───┐
                        //         │   │
                        //     ┌───┼───┼───┐
                        //     │   │ █ │   │
                        // ┌───┼───┼───┼───┼───┐
                        // │   │   │ □ │   │   │
                        // └───┼───┼───┼───┼───┘
                        //     │   │   │   │   
                        //     └───┼───┼───┘
                        //         │   │  
                        //         └───┘
                        if ( ( i - 1 ) >= 0 && MatrizTotal.Linha[ i - 1 ].Coluna[ j ].Nome >= 0 )
                            {
                            Matriz.A[ Nome_do_ponto, MatrizTotal.Linha[ i - 1 ].Coluna[ j ].Nome ] += -( 4 / Pow(DY, 4) ) - ( 4 / ( Pow(DX, 2) * Pow(DY, 2) ) );
                            }
                        // 2 X Em cima
                        //         ┌───┐
                        //         │ █ │
                        //     ┌───┼───┼───┐
                        //     │   │   │   │
                        // ┌───┼───┼───┼───┼───┐
                        // │   │   │ □ │   │   │
                        // └───┼───┼───┼───┼───┘
                        //     │   │   │   │   
                        //     └───┼───┼───┘
                        //         │   │  
                        //         └───┘
                        if ( ( i - 2 ) >= 0 && MatrizTotal.Linha[ i - 2 ].Coluna[ j ].Nome >= 0 )
                            {
                            Matriz.A[ Nome_do_ponto, MatrizTotal.Linha[ i - 2 ].Coluna[ j ].Nome ] += ( 1 / ( Pow(DY, 4) ) );
                            }
                        else
                            {
                            if ( ( i - 2 ) < 0 )
                                Matriz.A[ Nome_do_ponto, MatrizTotal.Linha[ i ].Coluna[ j ].Nome ] += -( 1 / ( Pow(DY, 4) ) );
                            }
                        // Em baixo
                        //         ┌───┐
                        //         │   │
                        //     ┌───┼───┼───┐
                        //     │   │   │   │
                        // ┌───┼───┼───┼───┼───┐
                        // │   │   │ □ │   │   │
                        // └───┼───┼───┼───┼───┘
                        //     │   │ █ │   │   
                        //     └───┼───┼───┘
                        //         │   │  
                        //         └───┘
                        if ( ( i + 1 ) < MatrizTotal.Linha.Length && MatrizTotal.Linha[ i + 1 ].Coluna[ j ].Nome >= 0 )
                            {
                            Matriz.A[ Nome_do_ponto, MatrizTotal.Linha[ i + 1 ].Coluna[ j ].Nome ] += -( 4 / Pow(DY, 4) ) - ( 4 / ( Pow(DX, 2) * Pow(DY, 2) ) );
                            }
                        // 2 X Em baixo
                        //         ┌───┐
                        //         │   │
                        //     ┌───┼───┼───┐
                        //     │   │   │   │
                        // ┌───┼───┼───┼───┼───┐
                        // │   │   │ □ │   │   │
                        // └───┼───┼───┼───┼───┘
                        //     │   │   │   │   
                        //     └───┼───┼───┘
                        //         │ █ │  
                        //         └───┘
                        if ( ( i + 2 ) < MatrizTotal.Linha.Length && MatrizTotal.Linha[ i + 2 ].Coluna[ j ].Nome >= 0 )
                            {
                            Matriz.A[ Nome_do_ponto, MatrizTotal.Linha[ i + 2 ].Coluna[ j ].Nome ] += ( 1 / ( Pow(DY, 4) ) );
                            }
                        else
                            {
                            if ( ( i + 2 ) >= MatrizTotal.Linha.Length )
                                Matriz.A[ Nome_do_ponto, MatrizTotal.Linha[ i ].Coluna[ j ].Nome ] += -( 1 / ( Pow(DY, 4) ) );
                            }
                        // Em Baixo e Direita 
                        //         ┌───┐
                        //         │   │
                        //     ┌───┼───┼───┐
                        //     │   │   │   │
                        // ┌───┼───┼───┼───┼───┐
                        // │   │   │ □ │   │   │
                        // └───┼───┼───┼───┼───┘
                        //     │   │   │ █ │   
                        //     └───┼───┼───┘
                        //         │   │  
                        //         └───┘
                        if ( ( i + 1 ) < MatrizTotal.Linha.Length && ( j + 1 ) < MatrizTotal.Linha[ i ].Coluna.Length && EhPontoInterno(1, 1, ref MatrizTotal) )
                            {
                            Matriz.A[ Nome_do_ponto, MatrizTotal.Linha[ i + 1 ].Coluna[ j + 1 ].Nome ] += ( 2 / ( Pow(DX, 2) * Pow(DY, 2) ) );
                            }
                        // Em Baixo e Esquerda 
                        //         ┌───┐
                        //         │   │
                        //     ┌───┼───┼───┐
                        //     │   │   │   │
                        // ┌───┼───┼───┼───┼───┐
                        // │   │   │ □ │   │   │
                        // └───┼───┼───┼───┼───┘
                        //     │ █ │   │   │   
                        //     └───┼───┼───┘
                        //         │   │  
                        //         └───┘
                        if ( ( i + 1 ) < MatrizTotal.Linha.Length && ( j - 1 ) > 0 && EhPontoInterno(1, -1, ref MatrizTotal) )
                            {
                            Matriz.A[ Nome_do_ponto, MatrizTotal.Linha[ i + 1 ].Coluna[ j - 1 ].Nome ] += ( 2 / ( Pow(DX, 2) * Pow(DY, 2) ) );
                            }
                        // Em Cima e Esquerda 
                        //         ┌───┐
                        //         │   │
                        //     ┌───┼───┼───┐
                        //     │ █ │   │   │
                        // ┌───┼───┼───┼───┼───┐
                        // │   │   │ □ │   │   │
                        // └───┼───┼───┼───┼───┘
                        //     │   │   │   │   
                        //     └───┼───┼───┘
                        //         │   │  
                        //         └───┘
                        if ( ( i - 1 ) >= 0 && ( j - 1 ) > 0 && EhPontoInterno(-1, -1, ref MatrizTotal) )
                            {
                            Matriz.A[ Nome_do_ponto, MatrizTotal.Linha[ i - 1 ].Coluna[ j - 1 ].Nome ] += ( 2 / ( Pow(DX, 2) * Pow(DY, 2) ) );
                            }
                        // Em Cima e Direita 
                        //         ┌───┐
                        //         │   │
                        //     ┌───┼───┼───┐
                        //     │   │   │ █ │
                        // ┌───┼───┼───┼───┼───┐
                        // │   │   │ □ │   │   │
                        // └───┼───┼───┼───┼───┘
                        //     │   │   │   │   
                        //     └───┼───┼───┘
                        //         │   │  
                        //         └───┘
                        if ( ( i - 1 ) >= 0 && ( j + 1 ) < MatrizTotal.Linha[ i ].Coluna.Length && EhPontoInterno(-1, 1, ref MatrizTotal) )
                            {
                            Matriz.A[ Nome_do_ponto, MatrizTotal.Linha[ i - 1 ].Coluna[ j + 1 ].Nome ] += ( 2 / ( Pow(DX, 2) * Pow(DY, 2) ) );
                            }
                        // Valor deslocamento
                        Matriz.B[ Nome_do_ponto ] = MatrizTotal.Linha[ i ].Coluna[ j ].Valor / D;

                        Nome_do_ponto++;

                        }

                    }
                }
            }

        static bool EhPontoInterno(int Deslocamento_Linha, int Deslocamento_Coluna, ref Pontos MatrizTotal)
            {
            return ( MatrizTotal.Linha[ PosLinha + Deslocamento_Linha ].Coluna[ PosColuna + Deslocamento_Coluna ].Nome >= 0 );
            }
        }

    }