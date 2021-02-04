using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Model
{
    public class PermissionMap: EntityBase
    {
        public string RoleId { get; set; }

        [ForeignKey("RoleId")]
        public Role Role { get; set; }

        public string PermissionId { get; set; }

        [ForeignKey("PermissionId")]
        public Permission Permission { get; set; }
    }
}