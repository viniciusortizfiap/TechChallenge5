namespace TechChallenge5.Domain.Entities
{
    public class PortifolioEntity : BaseEntity
    {
        public int UsuarioId { get; set; }
        public UsuarioEntity Usuario { get; set; } = null!;
        public string Nome { get; set; } = null!;
        public string Descricao { get; set; } = null!;

        public PortifolioEntity(int usuarioId, string nome, string descricao)
        {
            if (usuarioId < 0)
            {
                throw new ArgumentException("Usuario id deve ser maior que zero");
            }

            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new ArgumentException("Nome nao pode ser vazio");
            }

            if (string.IsNullOrWhiteSpace(descricao))
            {
                throw new ArgumentException("Descricao nao pode ser vazia");
            }

            UsuarioId = usuarioId;
            Nome = nome;
            Descricao = descricao;
        }
    }
}
