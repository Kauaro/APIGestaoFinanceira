namespace GestaoFinanceiraApi.DTOs.Investimento
{
    public class AtualizarInvestimentoDTO
    {
        public string Descricao { get; set; }
        public string Categoria { get; set; }
        public decimal ValorAplicado { get; set; }
        public DateTime DataAplicacao { get; set; }
    }
}
