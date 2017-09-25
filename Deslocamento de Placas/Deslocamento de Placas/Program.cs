using System;
using Matrizes;

namespace Deslocamento_de_Placas
    {
    partial class Program
        {

        static void Main(string[] args)
            {

            Console.WriteLine("Diga o tamanho máximo no eixo X:");
            Console.WriteLine("X=1 metros");
            X_total = 1;
            Console.WriteLine("Diga o tamanho máximo no eixo Y:");
            Console.WriteLine("Y=1 metros");
            Y_total = 1;

            Apresentação();

            Inicializar_Matriz(out Pontos MatrizTotal);

            Graficar_MatrizTotal(ref MatrizTotal);

            Criar_Matriz_de_Cálculo(ref MatrizTotal, out Matriz_Simples Matriz_Problema);

            Preencher_Matriz_de_Cálculo(ref MatrizTotal, ref Matriz_Problema);

            Matriz_Problema.Verificar_critério_de_Sassenfeld();

            Matriz_Problema.Solucionar_matriz();

            Mostrar_Matriz(Matriz_Problema.A);
            //Console.ReadKey();
            Salvar_Solução(ref Matriz_Problema, ref MatrizTotal);
            Mostrar_Matriz(ref MatrizTotal);
            // Console.ReadKey();
            Graficar_MatrizTotal(ref MatrizTotal);

            }
        private static void Apresentação()
            {
            Console.WriteLine("\t\t------------------------------------------------");
            Console.WriteLine("\t\t|          Alguns valores de Exemplo           |");
            Console.WriteLine("\t\t------------------------------------------------");
            Console.WriteLine("\t\t|                  Concreto                    |");
            Console.WriteLine("\t\t------------------------------------------------");
            Console.WriteLine("\t\t| Concreto Coef.de Poisson 0.1 <= v <= 0.15    |");
            Console.WriteLine("\t\t| Concreto Módulo de Young 14GPa <= E <= 30GPa |");
            Console.WriteLine("\t\t------------------------------------------------");
            Console.WriteLine("\t\t| Concreto de média resistência E = 25GPa      |");
            Console.WriteLine("\t\t| Concreto de Alta resistência  E = 30 GPa     |");
            Console.WriteLine("\t\t------------------------------------------------");
            Console.WriteLine("\t\t|                Ferro fundido                 |");
            Console.WriteLine("\t\t------------------------------------------------");
            Console.WriteLine("\t\t| Coef.de Poisson v =0.25                      |");
            Console.WriteLine("\t\t| Módulo de Young 80G Pa <= E <= 170 GPa       |");
            Console.WriteLine("\t\t------------------------------------------------");
            Console.WriteLine("\n\n\t\t------------------------------------------------");
            Console.WriteLine("\t\t|                   ATENÇÃO!                   |");
            Console.WriteLine("\t\t------------------------------------------------");
            Console.WriteLine("\t\t------------------------------------------------");
            Console.WriteLine("\t\t|            Você esta no modo Debug           |");
            Console.WriteLine("\t\t|        Não é possível alterar valores        |");
            Console.WriteLine("\t\t------------------------------------------------");


            E_Placa = 25 * Math.Pow(10, 9);
            v = 0.12;
            if ( X_total >= Y_total )
                {
                l_Laje = X_total;
                }
            else
                {
                l_Laje = Y_total;
                }

            t_placa = 0.001;

            D = E_Placa * ( Math.Pow(t_placa, 3) ) / ( 12 * ( 1 - Math.Pow(v, 2) ) );

            Console.WriteLine("\t\t------------------------------------------------");
            Console.WriteLine("\t\t|               Valores Definidos              |");
            Console.WriteLine("\t\t------------------------------------------------");
            Console.WriteLine("\t\t| Largura da placa na direção X   {0:0.00}         |", X_total);
            Console.WriteLine("\t\t| Largura da placa na direção Y   {0:0.00}         |", Y_total);
            Console.WriteLine("\t\t| Coef.de Poisson   {0:0.00}                       |", v);
            Console.WriteLine("\t\t| Módulo de Young   {0:0.00}  GPa                 |", E_Placa / 1000000000);
            Console.WriteLine("\t\t| lagura da Laje    {0:0.000}  m                   |", l_Laje);
            Console.WriteLine("\t\t| Espessura da Laje {0:0.000}  cm                  |", t_placa * 100);
            Console.WriteLine("\t\t| Razão Largura/espessura {0:0.0}               |", l_Laje / t_placa);
            Console.WriteLine("\t\t| Modulo de rigidez à flexão {0:0.00000}           |", D);
            Console.WriteLine("\t\t------------------------------------------------");
            }
        }
    }
