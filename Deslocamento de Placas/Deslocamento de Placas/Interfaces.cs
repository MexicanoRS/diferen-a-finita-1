namespace Deslocamento_de_Placas
{

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