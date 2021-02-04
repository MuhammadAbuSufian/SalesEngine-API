using System;
using Project.Model;

namespace Project.ViewModel
{
    public class CompanyViewModel : BaseViewModel<Company>
    {
        public CompanyViewModel(Company model) : base(model)
        {
            Name = model.Name;
            Email = model.Email;
            Address = model.Address;
            ValidTill = model.ValidTill;
            BusinessType = model.BusinessType;
            Balence = model.Balence;
            Note = model.Note;
        }
        public string Name { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public DateTime ValidTill { get; set; }

        public int? BusinessType { get; set; }

        public decimal? Balence { get; set; }

        public string Note { get; set; }

    }
}