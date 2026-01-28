namespace GestaoFinanceiraApi.DTOs.Investimento
{
    public class CriarInvestimentoDTO
    {

            public string Descricao { get; set; } = string.Empty;
            public string Categoria { get; set; } = string.Empty;
            public decimal ValorAplicado { get; set; }
            public DateTime DataAplicacao { get; set; }
        
    }
}
