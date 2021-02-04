using Project.Model;

namespace Project.Repository
{
    public interface IRoleRepository : IBaseRepository<Role>
    {
        
    }

    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(BusinessDbContext db) : base(db)
        {

        }
    }
}