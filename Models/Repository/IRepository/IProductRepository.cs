// IProductRepository.cs
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Repository.IRepository
{
    public interface IProductRepository
    {
        void Update(Product product);
        IEnumerable<SelectListItem> GetAllDropDownList(string obj);
        Task<Product> FirstOrDefaultAsync(System.Linq.Expressions.Expression<Func<Product, bool>> predicate, string includeProperties = null);
        Task<IEnumerable<Product>> GetAllAsync(string includeProperties = null);
        Task<Product> GetAsync(int id);
        Task<IEnumerable<Product>> SearchAsync(string name);
    }
}