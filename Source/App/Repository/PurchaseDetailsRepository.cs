using Project.Model;

namespace Project.Repository
{
    public interface IPurchaseDetailsRepository : IBaseRepository<PurchaseDetail>
    {

    }

    public class PurchaseDetailsRepository : BaseRepository<PurchaseDetail>, IPurchaseDetailsRepository
    {
        public PurchaseDetailsRepository(BusinessDbContext db) : base(db)
        {

        }
    }
}