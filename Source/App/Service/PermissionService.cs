using System;
using System.Collections.Generic;
using System.Linq;
using Project.Model;
using Project.Repository;
using Project.RequestModel;
using Project.ViewModel;

namespace Project.Service
{
    public interface IPermissionService : IBaseService<Permission, PermissionViewModel>
    {
        GridResponseModel<PermissionViewModel> PermissionGridView(GridRequestModel request);

        bool MapPermission(List<RolesPermissionViewModel> request, string roleId);
    }

    public class PermissionService : BaseService<Permission, PermissionViewModel>, IPermissionService
    {
        private readonly IPermissionRepository _repository;
        private readonly IPermissionMapRepository _permissionMapRepository;


        public PermissionService(IPermissionRepository repository, IPermissionMapRepository permissionMapRepository) : base(repository)
        {
            _repository = repository;
            _permissionMapRepository = permissionMapRepository;
        }
        public GridResponseModel<PermissionViewModel> PermissionGridView(GridRequestModel request)
        {
            GridResponseModel<PermissionViewModel> gridData = new GridResponseModel<PermissionViewModel>();

            gridData.Count = _repository.GetAllActive().Count();

            var query = _repository.GetAllActive();

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.Resource.Contains(request.Keyword) || x.Resource.Contains(request.Keyword));
            }

            if (request.IsAscending)
            {
                switch (request.OrderBy)
                {
                    case "Name": query = query.OrderBy(l => l.Resource); break;
                    default: query = query.OrderBy(l => l.Created); break;
                }
            }
            else
            {
                switch (request.OrderBy)
                {
                    case "Name": query = query.OrderByDescending(l => l.Resource); break;
                    default: query = query.OrderByDescending(l => l.Created); break;
                }
            }

            query = query.Skip(((request.Page - 1) * request.PerPageCount)).Take(request.PerPageCount);

            List<Permission> permissions = query.ToList();

            foreach (var permission in permissions)
            {
                gridData.Data.Add(new PermissionViewModel(permission));
            }

            return gridData;
        }

        public bool MapPermission(List<RolesPermissionViewModel> request, string roleId)
        {
            var oldPermissionMaps = _permissionMapRepository.GetAllActive().Where(x => x.RoleId == roleId).ToList();

            _permissionMapRepository.RemoveAll(oldPermissionMaps);
//            _permissionMapRepository.Commit();
            var newPermissionMaps = new List<PermissionMap>();

            foreach (var mapItem in request)
            {
                if (mapItem.HasPermission == true)
                {
                    PermissionMap newPermissionMap = new PermissionMap();
                    newPermissionMap.RoleId = roleId;
                    newPermissionMap.PermissionId = mapItem.Id;

                    var user = GetUserFromToken();
                    newPermissionMap.Id = Guid.NewGuid().ToString();
                    newPermissionMap.Created = DateTime.Now;
                    newPermissionMap.Modified = DateTime.Now;
                    newPermissionMap.CreatedBy = user.Id;
                    newPermissionMap.ModifiedBy = user.Id;
                    newPermissionMap.CreatedCompany = user.Company.Id;
                    newPermissionMap.Active = true;
                    newPermissionMap.DeletedBy = null;
                    newPermissionMap.DeletionTime = null;

                    newPermissionMaps.Add(newPermissionMap);
                }
                
            }

            _permissionMapRepository.Add(newPermissionMaps);

            return _permissionMapRepository.Commit();
        }
    }
}