using Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository
{
    public interface IJournalRepository : IBaseRepository<Journal>
    {

    }

    public class JournalRepository : BaseRepository<Journal>, IJournalRepository
    {
        public JournalRepository(BusinessDbContext db) : base(db)
        {

        }
    }
}
