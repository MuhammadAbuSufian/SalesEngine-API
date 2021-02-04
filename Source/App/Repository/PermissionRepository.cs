using Project.Model;

namespace Project.Repository
{
    public interface IPermissionRepository : IBaseRepository<Permission>
    {

    }

    public class PermissionRepository : BaseRepository<Permission>, IPermissionRepository
    {
        public PermissionRepository(BusinessDbContext db) : base(db)
        {

        }
    }
}