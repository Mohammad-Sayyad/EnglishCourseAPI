
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using TheListener.Data;
using TheListener.Models;
using TheListener.Repository;
using TheListener.Repository.IRepository;

namespace TheListener.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public async Task<Category> UpdateAsync(Category entity)
        {
            _db.Categories.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
