using Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository
{
    public interface IJournalTypeRepository : IBaseRepository<JournalType>
    {

    }

    public class JournalTypeRepository : BaseRepository<JournalType>, IJournalTypeRepository
    {
        public JournalTypeRepository(BusinessDbContext db) : base(db)
        {

        }
    }
}
