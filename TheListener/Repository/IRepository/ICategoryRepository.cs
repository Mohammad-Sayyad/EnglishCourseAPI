
using TheListener.Models;
using TheListener.Repository.IRepository;

namespace TheListener.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {

        Task<Category> UpdateAsync(Category entity);

    }
}
