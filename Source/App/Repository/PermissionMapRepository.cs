using Project.Model;

namespace Project.Repository
{
    public interface IPermissionMapRepository : IBaseRepository<PermissionMap>
    {

    }

    public class PermissionMapRepository : BaseRepository<PermissionMap>, IPermissionMapRepository
    {
        public PermissionMapRepository(BusinessDbContext db) : base(db)
        {

        }
    }
}