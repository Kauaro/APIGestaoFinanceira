namespace GestaoFinanceiraApi.DTOs.ResumoSaldo
{
    public class ResumoFinanceiroTotalDTO
    {
        public decimal TotalGanhos { get; set; }
        public decimal TotalGastos { get; set; }
        public decimal TotalInvestimentos { get; set; }
        public decimal SaldoDisponivel => TotalGanhos - TotalGastos;
    }

}
