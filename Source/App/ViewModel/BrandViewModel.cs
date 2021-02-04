using Project.Model;

namespace Project.ViewModel
{
    public class BrandViewModel : BaseViewModel<Brand>
    {
        public BrandViewModel(Brand model) : base(model)
        {
            Name = model.Name;
            Representive = model.Representive;
            RepContact = model.RepContact;
            Note = model.Note;
        }

        public string Name { get; set; }

        public string Representive { get; set; }

        public string RepContact { get; set; }

        public string Note { get; set; }

    }
}