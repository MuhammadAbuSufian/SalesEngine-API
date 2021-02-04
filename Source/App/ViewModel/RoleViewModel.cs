using System.Collections.Generic;
using Project.Model;

namespace Project.ViewModel
{
    public class RoleViewModel : BaseViewModel<Role>
    {
        public RoleViewModel(Role model) : base(model)
        {
            Name = model.Name;

            Permissions = new List<PermissionViewModel>();

            if (model.PermissionMaps != null)
            {
                foreach (var pMap in model.PermissionMaps)
                {
                    Permissions.Add(new PermissionViewModel(pMap.Permission));
                }
            }
        }

        public string Name { get; set; }

        public virtual ICollection<PermissionViewModel> Permissions { get; set; }

    }
}