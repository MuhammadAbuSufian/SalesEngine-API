using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using Newtonsoft.Json;
using Project.Model;
using Project.Repository;
using Project.RequestModel;
using Project.ViewModel;

namespace Project.Service
{
    public interface IUserService : IBaseService<User, UserViewModel>
    {
        UserViewModel Authorize(LoginRequestModel requestModel);


        Token CreateToken(User user);

        string CreatePasswordHash(string pwd);

        bool IsUniqueEmail(string email);

        List<DropdownViewModel> GetRolesDropdownData();

        GridResponseModel<UserViewModel> GridView(GridRequestModel request);

        string OldPassword(string id);

        bool ChangePassword(string oldPassword, string newPassowrd);

        ICollection<PermissionViewModel> Permissions();

    }

    public class UserService : BaseService<User, UserViewModel>, IUserService
    {
        private readonly IUserRepository _repository;
        private readonly ITokenRepository _tokenRepository;
        private readonly IRoleRepository _roleRepository;

        private IUserService _userServiceImplementation;

        public UserService(IUserRepository repository, ITokenRepository tokenRepository, IRoleRepository roleRepository) : base(repository)
        {
            _repository = repository;
            _tokenRepository = tokenRepository;
            _roleRepository = roleRepository;
        }

        public UserViewModel Authorize(LoginRequestModel requestModel)
        {
            string requestPassword = CreatePasswordHash(requestModel.Password);
            User user = _repository.GetAllActive().Include(x => x.Company).Include(x => x.Role.PermissionMaps.Select(y => y.Permission)).FirstOrDefault(x => x.Email.Contains(requestModel.Email) && x.Password == requestPassword);

            if (user == null)
            {
                return null;
            }

            Token token = CreateToken(user);

            user.Tokens = new List<Token>();
            user.Tokens.Add(token);

            return new UserViewModel(user);

        }



        public Token CreateToken(User user)
        {
            Token newToken = new Token();
            newToken.Id = Guid.NewGuid().ToString();
            newToken.ExpireAt = DateTime.Now.AddDays(3);
            newToken.UserId = user.Id;
            newToken.Active = true;
            newToken.CreatedCompany = user.CompanyId;
            newToken.Ticket = GetToken();
            _tokenRepository.Add(newToken);
            _tokenRepository.Commit();
            return newToken;
        }



        public string CreatePasswordHash(string pwd)
        {
            string pwdAndSalt = String.Concat(pwd, "9F195724C95A7E56CBB5");
            string hashedPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(pwdAndSalt, "sha1");

            return hashedPwd;
        }

        public bool IsUniqueEmail(string email)
        {
            User user = _repository.GetAll().FirstOrDefault(x => x.Email == email);
            if (user == null)
            {
                return true;
            }

            return false;
        }

        public List<DropdownViewModel> GetRolesDropdownData()
        {
            List<DropdownViewModel> roles = _roleRepository.GetAllActive().Select(
                x => new DropdownViewModel()
                {
                    Id = x.Id,
                    Name = x.Name
                }
            ).ToList();

            return roles;

        }

        public GridResponseModel<UserViewModel> GridView(GridRequestModel request)
        {
            GridResponseModel<UserViewModel> gridData = new GridResponseModel<UserViewModel>();

            var user = GetUserFromToken();

            gridData.Count = _repository.GetAllActive().Count();

            IQueryable<User> query = null;



            if (user.Role.Name == "SuperAdmin")
            {
                gridData.Count = _repository.GetAllActive().Count();
                query = _repository.GetAllActive().Include(x => x.Role).Include(x => x.Company);
            }
            else
            {
                gridData.Count = _repository.GetAllActive(user.CompanyId).Count();
                query = _repository.GetAllActive(user.CompanyId).Include(x => x.Role).Include(x => x.Company);

            }

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

            List<User> users = query.ToList();

            foreach (var usr in users)
            {
                gridData.Data.Add(new UserViewModel(usr));
            }

            return gridData;
        }

        public bool ChangePassword(string oldPassword, string newPassowrd)
        {
            var oldPass = CreatePasswordHash(oldPassword);
            var newPass = CreatePasswordHash(newPassowrd);

            var userFromToken = this.GetUserFromToken();

            User user = _repository.GetById(userFromToken.Id);

            if (user.Password != oldPass)
            {
                return false;
            }

            user.Password = newPass;

            user = this.CreateFootPrintForEdit(user, userFromToken);

            return _repository.Commit();

        }

        public string OldPassword(string id)
        {
            return _repository.GetById(id).Password;
        }

        public string GetToken()
        {
            byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            byte[] key = Guid.NewGuid().ToByteArray();
            string token = Convert.ToBase64String(time.Concat(key).ToArray());
            return token;
        }

        public ICollection<PermissionViewModel> Permissions()
        {
            var user = this.GetUserFromToken();
            var role = this._roleRepository.
                GetAllActive().Include(x=>x.PermissionMaps.Select(y=>y.Permission))
                .FirstOrDefault(x => x.Id == user.Role.Id);

            List<PermissionViewModel> permissions = new List<PermissionViewModel>();

            foreach(var permissionMap in role.PermissionMaps)
            {
                permissions.Add(new PermissionViewModel(permissionMap.Permission));
            }

            return permissions;
        } 

    }

}

