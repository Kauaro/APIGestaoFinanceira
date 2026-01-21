namespace GestaoFinanceiraApi.DTOs.ResumoSaldo
{
    public class ResumoFinanceiroTotalDTO
    {
        public decimal TotalGanhos { get; set; }
        public decimal TotalGastos { get; set; }
        public decimal Saldo => TotalGanhos - TotalGastos;
    }

}
