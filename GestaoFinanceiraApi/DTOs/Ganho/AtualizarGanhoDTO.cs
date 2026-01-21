namespace GestaoFinanceiraApi.DTOs.Ganho
{
    public class AtualizarGanhoDTO
    {
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public string Categoria { get; set; } = string.Empty;
        public string Pagamento { get; set; } = string.Empty;
        public DateTime DataGanho { get; set; }
    }
}
