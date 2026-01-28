using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoFinanceiraApi.Entity
{


    public class Investimento : Identificador
    {
        public string Descricao { get; private set; } = string.Empty;
        public string Categoria { get; private set; } = string.Empty;
        public decimal ValorAplicado { get; private set; } = 0;
        public DateTime DataAplicacao { get; private set; }
        public long UsuarioId { get; private set; }


        protected Investimento() { }

        public Investimento(
            string descricao,
            string categoria,
            decimal valorAplicado,
            DateTime dataAplicacao,
            long usuarioId)
        {
            AlterarDescricao(descricao);
            AlterarCategoria(categoria);
            AlterarValorAplicado(valorAplicado);
            AlterarDataAplicacao(dataAplicacao);

            UsuarioId = usuarioId;
            
        }



        public void Resgatar(decimal valor)
        {
            if (valor <= 0)
                throw new InvalidOperationException("Valor inválido");

            if (valor > ValorAplicado)
                throw new InvalidOperationException("Saldo insuficiente no investimento");

            ValorAplicado -= valor;
        }


        public void AlterarDescricao(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao))
                throw new ArgumentException("Descrição inválida");

            Descricao = descricao;
        }

        public void AlterarCategoria(string categoria)
        {
            if (string.IsNullOrWhiteSpace(categoria))
                throw new ArgumentException("Categoria inválida");

            Categoria = categoria;
        }

        public void AlterarValorAplicado(decimal valor)
        {
            if (Descricao != "Resgate")
            {
                if (valor <= 0)
                    throw new ArgumentException("Valor aplicado inválido");
            }

            ValorAplicado = valor;
        }

        public void AlterarDataAplicacao(DateTime dataAplicacao)
        {
            if (Categoria != "Poupança" && Categoria != "Investimento")
            {
                if (dataAplicacao > DateTime.Now)
                    throw new ArgumentException("Data da aplicação não pode ser futura");
            }

            DataAplicacao = dataAplicacao;
        }
    }
}
