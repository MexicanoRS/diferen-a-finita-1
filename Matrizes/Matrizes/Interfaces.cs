namespace Matrizes
{
    public interface Interfaces
    {
        double[,] A { get; set; }
        double[] B { get; set; }
        int Número_de_Variáveis { get; }
        double[] X { get; set; }
        int Número_de_Tentativas { get; }
        void Solucionar_matriz();
        double[] Sassen { get; }
        bool Verificar_critério_de_Sassenfeld();
        int Interações { get; set; }
        int Precisão { get; set; }
    }
}