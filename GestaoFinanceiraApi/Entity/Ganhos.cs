    namespace GestaoFinanceiraApi.Entity
    {
        public class Ganhos : Identificador
        {


            public string Descricao { get; private set; } = string.Empty;
            public decimal Valor { get; private set; }
            public string Categoria { get; private set; } = string.Empty;
            public string Pagamento { get; private set; } = string.Empty;
            public DateTime DataGanho { get; private set; }
            public long UsuarioId { get; private set; }


            protected Ganhos() { }



            public Ganhos(
                string descricao,
                decimal valor,
                string categoria,
                string pagamento,
                DateTime dataGanho,
                long usuarioId)

            {
                AlterarDescricao(descricao);
                AlterarValor(valor);
                AlterarCategoria(categoria);
                AlterarPagamento(pagamento);
                AlterarDataGanho(dataGanho);

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

            public void AlterarDataGanho(DateTime dataGanho)
            {

            if (Categoria != "Poupança" && Categoria != "Investimento")
            {
                if (dataGanho >= DateTime.Now)
                    throw new ArgumentException("Data do ganho não pode ser futura");
            }

                DataGanho = dataGanho;
            }
        }
    }
