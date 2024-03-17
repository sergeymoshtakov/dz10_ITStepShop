using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Models.Repository.IRepository
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync(Expression<Func<Order, bool>> filter = null);
        Task<Order> FirstOrDefaultAsync(Expression<Func<Order, bool>> filter = null);
    }
}
