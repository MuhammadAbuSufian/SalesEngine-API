using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Model
{
    public class Permission: EntityBase
    {
        public string Resource { get; set; }

    }
}