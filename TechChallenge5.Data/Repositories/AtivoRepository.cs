using TechChallenge5.Data.DataContext;
using TechChallenge5.Domain.Entities;
using TechChallenge5.Domain.Interfaces.Repositories;

namespace TechChallenge5.Data.Repositories
{
    public class AtivoRepository : EFRepository<AtivoEntity>, IAtivoRepository
    {
        public AtivoRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
