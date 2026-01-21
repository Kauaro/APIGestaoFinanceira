namespace GestaoFinanceiraApi.DTOs.Gasto
{
    public class CriarGastoDTO
    {
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public string Categoria { get; set; } = string.Empty;
        public string Pagamento { get; set; } = string.Empty;
        public DateTime DataGasto { get; set; }
    }
}
