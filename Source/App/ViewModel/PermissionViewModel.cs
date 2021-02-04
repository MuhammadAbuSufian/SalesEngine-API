using Project.Model;

namespace Project.ViewModel
{
    public class PermissionViewModel : BaseViewModel<Permission>
    {
        public PermissionViewModel(Permission model) : base(model)
        {
            Name = model.Resource;
        } 

        public string Name { get; set; }
    }
}