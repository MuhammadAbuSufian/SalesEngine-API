using Project.Model;

namespace Project.Repository
{
    public interface ISaleDetailsRepository : IBaseRepository<SalesDetail>
    {

    }

    public class SaleDetailsRepository : BaseRepository<SalesDetail>, ISaleDetailsRepository
    {
        public SaleDetailsRepository(BusinessDbContext db) : base(db)
        {

        }
    }
}