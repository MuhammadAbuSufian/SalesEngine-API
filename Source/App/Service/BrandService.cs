using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model;
using Project.Repository;
using Project.RequestModel;
using Project.ViewModel;

namespace Project.Service
{
   

    public interface IBrandService : IBaseService<Brand, BrandViewModel>
    {
        List<DropdownViewModel> GetCompanyDropdownData();

        GridResponseModel<BrandViewModel> GetGridData(GridRequestModel request);

        List<DropdownViewModel> GetDropdownData(string key);

        bool UpdateBrand(Brand brand);

        string RestoreBrand(string brandName);

    }

    public class BrandService : BaseService<Brand, BrandViewModel>, IBrandService
    {
        private readonly IBrandRepository _repository;


        public BrandService(IBrandRepository repository) : base(repository)
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

        public GridResponseModel<BrandViewModel> GetGridData(GridRequestModel request)
        {
            GridResponseModel<BrandViewModel> gridData = new GridResponseModel<BrandViewModel>();

            gridData.Count = _repository.GetAllActive(getCreatedCompanyId()).Count();

            var query = _repository.GetAllActive(getCreatedCompanyId());

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.Name.Contains(request.Keyword) );
            }

            if (request.IsAscending)
            {
                switch (request.OrderBy)
                {
                    case "Name": query = query.OrderBy(l => l.Name); break;
                    default: query = query.OrderBy(l => l.Created); break;
                }
            }
            else
            {
                switch (request.OrderBy)
                {
                    case "Name": query = query.OrderByDescending(l => l.Name); break;
                    default: query = query.OrderByDescending(l => l.Created); break;
                }
            }


            query = query.Skip(((request.Page - 1) * request.PerPageCount)).Take(request.PerPageCount);

            List<Brand> brands = query.ToList();

            foreach (var brand in brands)
            {
                gridData.Data.Add(new BrandViewModel(brand));
            }

            return gridData;
        }

        public List<DropdownViewModel> GetDropdownData(string key)
        {
            return _repository.GetAllActive(getCreatedCompanyId()).Where(x => x.Name.Contains(key)).Select(
                x =>
                    new DropdownViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name
                    }
            ).Take(10).ToList();
        }

        public bool UpdateBrand(Brand brand)
        {
            var updateBrand = _repository.GetById(brand.Id);
            updateBrand.Name = brand.Name;
            updateBrand.Representive = brand.Representive;
            updateBrand.RepContact = brand.RepContact;
            updateBrand.Note = brand.Note;

            var user = this.GetUserFromToken();

            updateBrand.Modified = DateTime.Now;
            updateBrand.ModifiedBy = user.Id;

            return _repository.Commit();

        }

        public string RestoreBrand(string brandName)
        {
            var brand = _repository.GetAllActive().FirstOrDefault(x => x.Name == brandName);

            if (brand != null)
            {
                return brand.Id;
            }
            else
            {
                Brand newBrand = new Brand();
                newBrand.Name = brandName;
                return AddwithReturnId(newBrand).Id;
            }
        }
    }
}
