using Project.Model;

namespace Project.Repository
{
    public interface IProductRepository : IBaseRepository<Product>
    {

    }

    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(BusinessDbContext db) : base(db)
        {

        }
    }
}