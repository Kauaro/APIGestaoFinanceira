namespace GestaoFinanceiraApi.DTOs.ResumoSaldo
{
    public class ResumoFinanceiroMensalDTO
    {
            public int Ano { get; set; }
            public int Mes { get; set; }


            public decimal TotalGanhos { get; set; }
            public decimal TotalGastos { get; set; }
            public decimal TotalInvestimentos { get; set; }


            public decimal SaldoDisponivel => TotalGanhos - TotalGastos;

    }
}
