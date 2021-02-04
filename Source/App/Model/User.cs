using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class User: EntityBase
    {
     

        public string Name { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [StringLength(128)]
        public string Email { get; set; }

        public string Address { get; set; }
     
        public string Phone { get; set; }

        [Required]
        public string Password { get; set; }

        public string RoleId { get; set; }

        [ForeignKey("RoleId")]
        public Role Role { get; set; }

        public string CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public Company Company { get; set; }

        public virtual ICollection<Token> Tokens { get; set; }

    }
}
