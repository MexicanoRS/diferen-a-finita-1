namespace diferença_finita_1
{
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