namespace TechChallenge5.Domain.Entities
{
    public class TransacaoEntity : BaseEntity
    {
        public int PortifolioId { get; set; }
        public PortifolioEntity Portifolio { get; set; } = null!;

        public int AtivoId { get; set; }
        public AtivoEntity Ativo { get; set; } = null!;
        public string TipoTransacao { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }

        public TransacaoEntity(
            int portifolioId, 
            int ativoId, 
            string tipoTransacao, 
            int quantidade, 
            decimal preco
        )
        {
            if (portifolioId < 0)
            {
                throw new ArgumentOutOfRangeException("Portifolio id deve ser maior que zero");
            }

            if (ativoId < 0)
            {
                throw new ArgumentOutOfRangeException("Ativo id deve ser maior que zero");
            }

            if (string.IsNullOrWhiteSpace(tipoTransacao))
            {
                throw new ArgumentNullException("Tipo transacao nao pode ser vazio");
            }

            if (quantidade < 0)
            {
                throw new ArgumentOutOfRangeException("Quantidade id deve ser maior que zero");
            }

            if (preco < 0)
            {
                throw new ArgumentOutOfRangeException("Preco id deve ser maior que zero");
            }

            PortifolioId = portifolioId;
            AtivoId = ativoId;
            TipoTransacao = tipoTransacao;
            Quantidade = quantidade;
            Preco = preco;
        }

        public TransacaoEntity()
        {
            
        }
    }
}
