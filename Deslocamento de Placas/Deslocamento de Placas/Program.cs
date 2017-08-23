using System;
using Matrizes;


namespace Deslocamento_de_Placas
{
    partial class Program
    {

        static void Main(string[] args)
        {

            Console.WriteLine("Diga o tamanho máximo no eixo X:");
            Console.WriteLine("X=5 metros");
            X_total = 5;
            Console.WriteLine("Diga o tamanho máximo no eixo Y:");
            Console.WriteLine("Y=5 metros");
            Y_total = 5;

            Apresentação();

            Inicializar_Matriz(out Pontos MatrizTotal);

            Graficar_MatrizTotal(ref MatrizTotal);

            Criar_Matriz_de_Cálculo(ref MatrizTotal, out Matriz_Simples Matriz_Problema);

            Preencher_Matriz_de_Cálculo(ref MatrizTotal, ref Matriz_Problema);

            Matriz_Problema.Verificar_critério_de_Sassenfeld();

            Matriz_Problema.Solucionar_matriz();

            Salvar_Solução(ref Matriz_Problema, ref MatrizTotal);

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
#if(DEBUG)
            Console.WriteLine("\n\n\t\t------------------------------------------------");
            Console.WriteLine("\t\t|            Você esta no modo Debug           |");
            Console.WriteLine("\t\t|        Não é possível alterar valores        |");
            Console.WriteLine("\t\t------------------------------------------------");
#else

#endif
        }
    }
}
