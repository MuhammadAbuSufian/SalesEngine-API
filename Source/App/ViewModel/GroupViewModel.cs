using Project.Model;

namespace Project.ViewModel
{
    public class GroupViewModel : BaseViewModel<Group>
    {
        public GroupViewModel(Group model) : base(model)
        {
            Name = model.Name;
            Description = model.Description;
        }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}