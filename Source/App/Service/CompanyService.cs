using System;
using System.Collections.Generic;
using System.Linq;
using Project.Model;
using Project.Repository;
using Project.ViewModel;

namespace Project.Service
{
    

    public interface ICompanyService : IBaseService<Company, CompanyViewModel>
    {
        List<DropdownViewModel> GetCompanyDropdownData();
        bool AddBalence(decimal amount);
        bool DeductBalence(decimal amount);

    }

    public class CompanyService : BaseService<Company, CompanyViewModel>, ICompanyService
    {
        private readonly ICompanyRepository _repository;

        public CompanyService(ICompanyRepository repository) : base(repository)
        {
            _repository = repository;
            
        }

        public List<DropdownViewModel> GetCompanyDropdownData()
        {
            List<DropdownViewModel> companies = GetAllActive().Select(
                x => new DropdownViewModel()
                {
                    Id = x.Id,
                    Name = x.Name
                }
            ).ToList();

            return companies;
        }

        public bool AddBalence(decimal amount)
        {
            var user = this.GetUserFromToken();
            var updateEntity = _repository.GetById(user.CompanyId);
            updateEntity.Balence = updateEntity.Balence + amount ;
            updateEntity.Modified = DateTime.Now;
            updateEntity.ModifiedBy = user.Id;
            return _repository.Commit();
        }

        public bool DeductBalence(decimal amount)
        {
            var user = this.GetUserFromToken();
            var updateEntity = _repository.GetById(user.CompanyId);
            updateEntity.Balence = updateEntity.Balence - amount;
            updateEntity.Modified = DateTime.Now;
            updateEntity.ModifiedBy = user.Id;
            return _repository.Commit();
        }
    }
}