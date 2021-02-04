using Project.Model;

namespace Project.Repository
{
    public interface ISaleRepository : IBaseRepository<Sale>
    {

    }

    public class SaleRepository : BaseRepository<Sale>, ISaleRepository
    {
        public SaleRepository(BusinessDbContext db) : base(db)
        {

        }
    }
}