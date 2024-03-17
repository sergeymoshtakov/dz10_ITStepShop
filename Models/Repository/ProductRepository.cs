// ProductRepository.cs
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models.Data;
using Models.Repository.IRepository;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetAllDropDownList(string obj)
        {
            if (obj == "Category")
            {
                return _db.Category.Select(x => new SelectListItem { Text = x.CategoryName, Value = x.Id.ToString() });
            }
            return null;
        }

        public async Task<IEnumerable<Product>> GetAllAsync(string includeProperties = null)
        {
            IQueryable<Product> query = _db.Product;

            if (includeProperties != null)
            {
                query = query.Include(includeProperties);
            }

            return await query.ToListAsync();
        }

        public async Task<Product> GetAsync(int id)
        {
            return await _db.Product.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> SearchAsync(string name)
        {
            return await _db.Product.Where(p => p.Name.Contains(name)).ToListAsync();
        }

        public void Update(Product product)
        {
            _db.Product.Update(product);
        }

        public async Task<Product> FirstOrDefaultAsync(System.Linq.Expressions.Expression<Func<Product, bool>> predicate, string includeProperties = null)
        {
            IQueryable<Product> query = _db.Set<Product>();

            if (includeProperties != null)
            {
                query = query.Include(includeProperties);
            }

            return await query.FirstOrDefaultAsync(predicate);
        }
    }
}
