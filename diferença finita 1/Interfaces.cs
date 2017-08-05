namespace diferença_finita_1
{
        public interface Interfaces 
    {
        double[,] A { get; set; }
        double[] B { get; set; }
        int Número_de_Variáveis { get; }
        double[] X { get; set; }
        int Número_de_Tentativas { get; }
        void Solucionar_matriz();
        double[] Sassen {get; }
        bool Verificar_critério_de_Sassenfeld();
        int Interações { get; set; }
        int Precisão {get; set; }    

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