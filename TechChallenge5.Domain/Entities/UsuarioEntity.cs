using System.Security.Cryptography;
using System.Text;

namespace TechChallenge5.Domain.Entities
{
    public class UsuarioEntity : BaseEntity
    {
        public string Nome { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Senha { get; set; } = null!;

        public UsuarioEntity(string nome, string email, string senha)
        {

            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new ArgumentNullException("Nome nao pode ser vazio", nome);
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email nao pode ser vazio", email);
            }

            if (string.IsNullOrWhiteSpace(senha))
            {
                throw new ArgumentException("Senha nao pode ser vazio", senha);
            }

            Nome = nome;
            Email = email;
            Senha = senha;
        }

        public void GerarHashSenha()
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(Senha));

                Senha = BitConverter.ToString(bytes).Replace("-", "");
            }
        }
    }
}
