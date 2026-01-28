namespace GestaoFinanceiraApi.DTOs.Investimento
{
    public class ResgatarInvestimentoDTO
    {
        public decimal Valor { get; set; }
        public string Categoria { get; set; } = string.Empty;
        public DateTime DataResgate { get; set; }
    }
}
