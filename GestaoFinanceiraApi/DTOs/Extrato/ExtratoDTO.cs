namespace GestaoFinanceiraApi.DTOs.Extrato
{
    public class ExtratoDTO
    {
        public string Tipo { get; set; } 
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public string Categoria { get; set; }
        public DateTime Data { get; set; }
    }
}
