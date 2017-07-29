namespace diferença_finita_1
{
    public interface Interfaces 
    {
        double[,] A { get; set; }
        double[] B { get; set; }
        int Número_de_Variáveis { get; }
        double[] X { get; set; }

        void Solucionar_matriz(double Precisão = 2, int Núm_MaxInterações = 10000);
        void Verificar_critério_de_Sassenfeld();
    }

    public interface IColuna_da_Matriz
    {
        double Valor { get; set; }
        int Nome { get; set; }
    }

    interface IMatriz_de_Temp
    {
        void Adicionar_Ponto(int nome, double temperatura);
    }
    interface IPonto_Temp
    {
        int Nome { get; set; }
        double Temperatura { get; set; }
    }
}