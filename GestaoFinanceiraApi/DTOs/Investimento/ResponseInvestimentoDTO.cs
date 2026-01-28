namespace GestaoFinanceiraApi.DTOs.Investimento
{
    public class ResponseInvestimentoDTO
    {
        public long Id { get; set; }
        public string Descricao { get; set; }
        public string Categoria { get; set; }
        public decimal ValorAplicado { get; set; }
        public DateTime DataAplicacao { get; set; }

    }
}
