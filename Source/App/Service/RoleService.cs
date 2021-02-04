using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Project.Model;
using Project.Repository;
using Project.RequestModel;
using Project.ViewModel;

namespace Project.Service
{

    public interface IRoleService : IBaseService<Role, RoleViewModel>
    {
        GridResponseModel<RoleViewModel> RoleGridView(GridRequestModel request);

        bool getPermissionsById(string roleId, string permissionId);


    }

    public class RoleService : BaseService<Role, RoleViewModel>, IRoleService
    {
        private readonly IRoleRepository _repository;
        private readonly IPermissionMapRepository _permissionMapRepository;


        public RoleService(IRoleRepository repository, IPermissionMapRepository permissionMapRepository) : base(repository)
        {
            _repository = repository;
            _permissionMapRepository = permissionMapRepository;
        }
        public GridResponseModel<RoleViewModel> RoleGridView(GridRequestModel request)
        {
            GridResponseModel<RoleViewModel> gridData = new GridResponseModel<RoleViewModel>();

            gridData.Count = _repository.GetAllActive().Count();

            var query = _repository.GetAllActive().Include(x => x.PermissionMaps.Select(y => y.Permission));

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.Name.Contains(request.Keyword) || x.Name.Contains(request.Keyword));
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

            List<Role> roles = query.ToList();

            foreach (var role in roles)
            {
                gridData.Data.Add(new RoleViewModel(role));
            }

            return gridData;
        }

        public bool getPermissionsById(string roleId, string permissionId)
        {
           var  exist = _permissionMapRepository.GetAllActive().FirstOrDefault(x=>x.PermissionId == permissionId && x.RoleId == roleId);

           if (exist == null)
           {
               return false;
           }
           return true;
        }
    }
}