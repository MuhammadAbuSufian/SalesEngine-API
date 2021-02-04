using Project.Model;

namespace Project.Repository
{
    
    public interface ITokenRepository : IBaseRepository<Token>
    {

    }

    public class TokenRepository : BaseRepository<Token>, ITokenRepository
    {
        public TokenRepository(BusinessDbContext db) : base(db)
        {

        }
    }
}