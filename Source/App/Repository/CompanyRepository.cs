using Project.Model;

namespace Project.Repository
{

    public interface ICompanyRepository : IBaseRepository<Company>
    {

    }

    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(BusinessDbContext db) : base(db)
        {

        }
    }
}