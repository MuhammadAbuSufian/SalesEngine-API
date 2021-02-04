using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model;

namespace Project.ViewModel
{
    public sealed class UserViewModel : BaseViewModel<User>
    {
        public UserViewModel(User model) : base(model)
        {
            UserId = model.Id;
            Name = model.Name;
            Email = model.Email;
            Address = model.Address;
            Phone = model.Phone;
            RoleId = model.RoleId;
            CompanyId = model.CompanyId;

            if (model.Role != null)
            {
                Role = new RoleViewModel(model.Role);
            }
            if (model.Company != null)
            {
                Company = new CompanyViewModel(model.Company);
            }

            Tokens = new List<TokenViewModel>();

            if (model.Tokens != null)
            {
                foreach (var t in model.Tokens)
                {
                    Tokens.Add(new TokenViewModel(t));
                }
            }

        }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string RoleId { get; set; }

        public RoleViewModel Role { get; set; }

        public string CompanyId { get; set; }

        public CompanyViewModel Company { get; set; }

        public ICollection<TokenViewModel> Tokens { get; set; }
    }

}