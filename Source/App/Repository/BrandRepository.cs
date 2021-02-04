using Project.Model;

namespace Project.Repository
{
    public interface IBrandRepository : IBaseRepository<Brand>
    {

    }

    public class BrandRepository : BaseRepository<Brand>, IBrandRepository
    {
        public BrandRepository(BusinessDbContext db) : base(db)
        {

        }
    }
}