

using TheListener.Models;
using TheListener.Repository.IRepository;

namespace TheListener.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {

        Task<Product> UpdateAsync(Product entity);

    }
}
