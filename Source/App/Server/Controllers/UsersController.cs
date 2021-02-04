using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Web.Http;

using Project.Model;
using Project.RequestModel;
using Project.Server.Filters;
using Project.Service;
using Project.ViewModel;


namespace Project.Server.Controllers
{
    [CustomAuthorization(Disable = true)]
    public class UsersController : BaseController<User, UserViewModel, UserRequestModel>
    {
        private readonly IUserService _service;
        private readonly IRoleService _roleService;
        private readonly IPermissionService _permissionService;
        public UsersController(IUserService service, IRoleService roleService, IPermissionService permissionService) : base(service)
        {
            //            var token = Request.Headers.Authorization.ToString();

            _service = service;
            _roleService = roleService;
            _permissionService = permissionService;
        }

        public IHttpActionResult Get()
        {

            return Ok("success");
        }

        [CustomAuthorization(Disable = true)]
        [HttpPost]
        [Route("api/User/Login")]
        public IHttpActionResult Login(LoginRequestModel request)
        {
            var user = _service.Authorize(request);
            return Ok(user);
        }


        [HttpPost]
        [Route("api/user/Add")]
        public bool AddUser(User request)
        {
            if (request.Id == null)
            {
                var isUniqueEmail = _service.IsUniqueEmail(request.Email);

                if (isUniqueEmail)
                {
                    var user = new User();
                    user.Name = request.Name;
                    user.Email = request.Email;
                    user.Address = request.Address;
                    user.RoleId = request.RoleId;
                    user.Password = _service.CreatePasswordHash(request.Password);
                    user.CompanyId = request.CompanyId;
                    return _service.Add(user, request.CompanyId);
                }
            }
            else
            {
                if (String.IsNullOrEmpty(request.Password))
                {
                    request.Password = _service.OldPassword(request.Id);
                }
                else
                {
                    request.Password = _service.CreatePasswordHash(request.Password);
                }

                return _service.Edit(request);

            }


            return false;

        }

        [HttpGet]
        [Route("api/role/Dropdown")]
        public IHttpActionResult UserDropdown()
        {
            return Ok(_service.GetRolesDropdownData());
        }

        [HttpPost]
        [Route("api/user/Grid")]
        public IHttpActionResult GetUsers(GridRequestModel request)
        {
            return Ok(_service.GridView(request).Data);
        }

        [HttpPost]
        [Route("api/user/role/Add")]
        public bool AddRole(RoleRequestModel request)
        {
            RoleViewModel existRole = _roleService.GetAll().FirstOrDefault(x => x.Name == request.Name);

            if (existRole != null) return false;

            Role newRole = new Role();
            newRole.Name = request.Name;
            return _roleService.Add(newRole);

        }

        [HttpPost]
        [Route("api/user/role/grid")]
        public IHttpActionResult GetRole(GridRequestModel request)
        {
            return Ok(_roleService.RoleGridView(request).Data);
        }

        [HttpPost]
        [Route("api/user/permission/Add")]
        public bool AddPermission(PermissionRequestModel request)
        {
            PermissionViewModel existPermission = _permissionService.GetAll().FirstOrDefault(x => x.Name == request.Name);

            if (existPermission != null) return false;

            Permission newPermission = new Permission();
            newPermission.Resource = request.Name;
            return _permissionService.Add(newPermission);

        }

        [HttpPost]
        [Route("api/user/permission/grid")]
        public IHttpActionResult GetPermission(GridRequestModel request)
        {
            return Ok(_permissionService.PermissionGridView(request).Data);
        }

        [HttpGet]
        [Route("api/user/role/permission")]
        public IHttpActionResult GetRolePermission(string roleId)
        {
            var permissions = _permissionService.GetAllActive();

            List<RolesPermissionViewModel> returnable = new List<RolesPermissionViewModel>();

            foreach (var permission in permissions)
            {
                returnable.Add(new RolesPermissionViewModel(permission.Id, permission.Name, _roleService.getPermissionsById(roleId, permission.Id)));
            }
            return Ok(returnable);
        }

        [HttpGet]
        [Route("api/user/changePassword")]
        public IHttpActionResult ChangePassword(string oldPassword, string newPassword)
        {
            
            return Ok(_service.ChangePassword(oldPassword, newPassword));
        }

        [HttpGet]
        [Route("api/user/permissions")]
        public IHttpActionResult GetPermissions()
        {
            
            return Ok(_service.Permissions());
        }


        [HttpPost]
        [Route("api/user/permission/map")]
        public IHttpActionResult GetPermission(List<RolesPermissionViewModel> request, string roleId)
        {
            return Ok(_permissionService.MapPermission(request, roleId));
        }


    }
}
