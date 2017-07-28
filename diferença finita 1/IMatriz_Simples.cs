namespace diferença_finita_1
{
    interface IMatriz_Simples
    {
        double[,] A { get; set; }
        double[] B { get; set; }
        int Número_de_Variáveis { get; }
        double[] X { get; set; }
    }
}