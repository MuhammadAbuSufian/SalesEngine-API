using Project.Model;

namespace Project.Repository
{
    public interface IGroupRepository : IBaseRepository<Group>
    {

    }

    public class GroupRepository : BaseRepository<Group>, IGroupRepository
    {
        public GroupRepository(BusinessDbContext db) : base(db)
        {

        }
    }
}