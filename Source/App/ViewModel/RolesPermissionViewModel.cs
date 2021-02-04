using System.Runtime.InteropServices;
using System.Xml;

namespace Project.ViewModel
{
    public class RolesPermissionViewModel
    {
        public RolesPermissionViewModel(string Id, string Name, bool HasPermission)
        {
            this.Id = Id;
            this.Name = Name;
            this.HasPermission = HasPermission;
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public bool HasPermission { get; set; }
    }
}