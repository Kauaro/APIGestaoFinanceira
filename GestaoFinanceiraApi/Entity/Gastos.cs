namespace GestaoFinanceiraApi.Entity
{
    public class Gastos : Identificador
    {


        public string Descricao { get; private set; } = string.Empty;
        public decimal Valor { get; private set; } = 0;
        public string Categoria { get; private set; } = string.Empty;
        public string Pagamento { get; private set; } = string.Empty;
        public DateTime DataGasto { get; private set; }
        public long UsuarioId { get; private set; }


        protected Gastos() { }



        public Gastos(
            string descricao,
            decimal valor,
            string categoria,
            string pagamento,
            DateTime dataGasto,
            long usuarioId)

            {
                AlterarDescricao(descricao);
                AlterarValor(valor);
                AlterarCategoria(categoria);
                AlterarPagamento(pagamento);
                AlterarDataGasto(dataGasto);

                UsuarioId = usuarioId;
            }


        public void AlterarDescricao(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao))
                throw new ArgumentException("Descrição inválida");

            Descricao = descricao;
        }

        public void AlterarValor(decimal valor)
        {
            if (valor <= 0)
                throw new ArgumentException("Valor inválido");

            Valor = valor;
        }

        public void AlterarCategoria(string categoria)
        {
            if (string.IsNullOrWhiteSpace(categoria))
                throw new ArgumentException("Categoria inválida");

            Categoria = categoria;
        }

        public void AlterarPagamento(string pagamento)
        {
            if (string.IsNullOrWhiteSpace(pagamento))
                throw new ArgumentException("Tipo de pagamento inválido");

            Pagamento = pagamento;
        }

        public void AlterarDataGasto(DateTime dataGasto)
        {
            if (dataGasto > DateTime.Now)
                throw new ArgumentException("Data do gasto não pode ser futura");

            DataGasto = dataGasto;
        }
    }
}
