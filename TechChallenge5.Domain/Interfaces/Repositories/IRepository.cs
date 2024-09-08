using TechChallenge5.Domain.Entities;

namespace TechChallenge5.Domain.Interfaces.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> Add(T entity);

        Task<T> GetById(int id);

        Task<IList<T>> GetAll();

        Task<T> Update(T entity);

        Task Delete(T entity);
    }
}
