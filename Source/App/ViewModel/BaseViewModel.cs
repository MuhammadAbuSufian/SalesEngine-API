using System;
using Project.Model;

namespace Project.ViewModel
{
    public abstract class BaseViewModel<T> where T : EntityBase
    {
        protected BaseViewModel()
        {
        }

        protected BaseViewModel(T model)
        {
            Id = model.Id;
            Created = model.Created;
            CreatedBy = model.CreatedBy;
            Modified = model.Modified;
            ModifiedBy = model.ModifiedBy;
            Active = model.Active;
            CreatedCompany = model.CreatedCompany;
        }

        public string Id { get; set; }
        public DateTime? Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? Modified { get; set; }
        public string ModifiedBy { get; set; }
        public bool Active { get; set; }
        public string CreatedCompany { get; set; }
    }
}