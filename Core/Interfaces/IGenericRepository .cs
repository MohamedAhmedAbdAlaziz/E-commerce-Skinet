using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entites;
using Core.Specifications;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
         Task<T> GetbyIdAsync(int id);

         Task<IReadOnlyList<T>> ListAllAsync();

         Task<T> GetEntiyWithSpec(ISpecification<T> spec);
         Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
         Task<int> CountAsync(ISpecification<T> spec);
    }
}