﻿
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using TheListener.Data;
using TheListener.Models;
using TheListener.Repository.IRepository;

namespace TheListener.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public async Task<Product> UpdateAsync(Product entity)
        {
            _db.Products.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

    }
}
