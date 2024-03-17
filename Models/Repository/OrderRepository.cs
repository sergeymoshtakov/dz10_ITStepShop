using Microsoft.EntityFrameworkCore;
using Models.Data;
using Models.Repository.IRepository;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Models.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly ApplicationDbContext _db;

        public OrderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Order> FirstOrDefaultAsync(Expression<Func<Order, bool>> filter = null)
        {
            IQueryable<Order> query = _db.Order;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync(Expression<Func<Order, bool>> filter = null)
        {
            IQueryable<Order> query = _db.Order;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }
    }
}
