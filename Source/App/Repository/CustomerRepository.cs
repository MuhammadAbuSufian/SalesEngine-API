using Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository
{

    public interface ICustomerRepository : IBaseRepository<Customer>
    {

    }

    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(BusinessDbContext db) : base(db)
        {

        }
    }
}
