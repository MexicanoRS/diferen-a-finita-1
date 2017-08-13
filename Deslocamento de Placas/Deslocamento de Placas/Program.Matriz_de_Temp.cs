using System.Collections.Generic;

namespace Deslocamento_de_Placas
{
    partial class Program
    {

        /// <summary>
        /// Classe criada para ajudar a escrever pequenas matrizes de calor a mão eu de forma simplificada
        /// </summary>
        class Matriz_de_Temp : IMatriz_de_Temp
        {
            public List<Ponto_Temp> Temperatura = new List<Ponto_Temp>();
            public void Adicionar_Ponto(int nome, double temperatura)
            {
                Ponto_Temp ponto_novo = new Ponto_Temp(nome, temperatura);
                Temperatura.Add(ponto_novo);
            }

        }

        /// <summary>
        /// Local específico que armazena as temperaturas na LIST mãe
        /// </summary>
        class Ponto_Temp : IPonto_Temp
        {
            public double Temperatura
            { get; set; }
            public int Nome
            { get; set; }
            public Ponto_Temp(int Nome, double Temperatura)
            {
                this.Temperatura = Temperatura;
                this.Nome = Nome;
            }
        }
    }
}

