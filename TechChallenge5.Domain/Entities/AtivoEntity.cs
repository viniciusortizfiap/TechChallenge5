namespace TechChallenge5.Domain.Entities
{
    public class AtivoEntity : BaseEntity
    {
        public string TipoAtivo { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;

        public AtivoEntity(string tipoAtivo, string nome, string codigo)
        {
            if (string.IsNullOrWhiteSpace(tipoAtivo))
            {
                throw new ArgumentNullException("Tipo Ativo nao pode ser vazio", tipoAtivo);
            }

            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new ArgumentNullException("Nome nao pode ser vazio", nome);
            }

            if (string.IsNullOrWhiteSpace(codigo))
            {
                throw new ArgumentNullException("Codigo nao pode ser vazio", codigo);
            }

            TipoAtivo = tipoAtivo;
            Nome = nome;
            Codigo = codigo;
        }
    }
}
