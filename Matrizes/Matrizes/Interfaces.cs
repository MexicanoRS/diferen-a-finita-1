namespace Matrizes
{
    /// <summary>
    /// Interface da classe Metrizes
    /// </summary>
    public interface IMatrizes
    {
        /// <summary>
        /// Matriz de coeficientes
        /// </summary>
        double[,] A { get; set; }
        /// <summary>
        /// Vetor Solução
        /// </summary>
        double[] B { get; set; }
        /// <summary>
        /// Númeoro de variáveis da equação Linear
        /// </summary>
        int Número_de_Variáveis { get; }
        /// <summary>
        /// Vetor de Icôgnitas
        /// </summary>
        double[] X { get; set; }
        /// <summary>
        /// Númerode de Tentativas antes do programa desistir
        /// </summary>
        int Número_de_Tentativas { get; }
        /// <summary>
        /// Função responsável por solucionar a matriz 
        /// </summary>
        void Solucionar_matriz();
        /// <summary>
        /// Vetor usado para testar se a Matriz A convege para solução
        /// </summary>
        double[] Sassen { get; }
        /// <summary>
        /// Função responsável por testar se a matriz A Converge para a solução atravez do método de Gaus-Siedel
        /// </summary>
        /// <returns>Retorna se a função tem solução ou não.</returns>
        bool Verificar_critério_de_Sassenfeld();
        /// <summary>
        /// Número de interações até chegar na solução
        /// </summary>
        int Interações { get; set; }
        /// <summary>
        /// Número de Casas de precissão
        /// </summary>
        int Precisão { get; set; }
    }
}