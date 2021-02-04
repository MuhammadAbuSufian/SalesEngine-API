using System.Collections.Generic;

namespace Project.Model
{
    public class Role: EntityBase
    {
        public string Name { get; set; }

        public virtual ICollection<PermissionMap> PermissionMaps { get; set; }
    }
}