using Project.Model;

namespace Project.Repository
{
    public interface IPurchaseRepository : IBaseRepository<Purchase>
    {

    }

    public class PurchaseRepository : BaseRepository<Purchase>, IPurchaseRepository
    {
        public PurchaseRepository(BusinessDbContext db) : base(db)
        {

        }
    }
}