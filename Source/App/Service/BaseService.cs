using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Project.Model;
using Project.Model.Enums;
using Project.Repository;
using Project.RequestModel;
using Project.ViewModel;
namespace Project.Service
{

    public interface IPagingService<T, TVm> where T : EntityBase where TVm : BaseViewModel<T>
    {
        ResponseModel<TVm> GetAll(BaseRequestModel<T> requestModel);
        ResponseModel<TVm> GetAllActive(BaseRequestModel<T> requestModel);
        ResponseModel<TVm> GetAllInactive(BaseRequestModel<T> requestModel);

        ResponseModel<TVm> GeneralSearch(PagingDataType status, BaseRequestModel<T> requestModel);
    }

    public interface IBaseService<T, TVm> : IPagingService<T, TVm>
        where T : EntityBase
        where TVm : BaseViewModel<T>
    {
        List<TVm> GetAll();
        List<TVm> GetAllActive();
        List<TVm> GetAllInactive();
        TVm GetById(string id);
        TVm GetByIdAsNoTracking(string id);
        bool Add(T entity);
        TVm AddwithReturnId(T entity);
        bool Add(T entity, string companyId);
        bool Add(T entity, UserViewModel user);
        bool Add(List<T> entities);
        bool Edit(T entity);
        bool Delete(string id);
        bool Trash(string id);
        bool Restore(string id);
        bool RemoveAll(List<T> entries);

        string getCreatedCompanyId();
//        T CreateSign(T);
    }


    public abstract class BaseService<TEntity, TVm> : IBaseService<TEntity, TVm>
        where TEntity : EntityBase
        where TVm : BaseViewModel<TEntity>
    {
        protected IBaseRepository<TEntity> Repository;

        static BusinessDbContext db = new BusinessDbContext();
//        private IUserRepository _userRepository = new UserRepository( db );
        private ITokenRepository _tokenRepository = new TokenRepository( db );
        
        private string _token;
//        private User _user;
        protected BaseService(IBaseRepository<TEntity> repository)
        {
            Repository = repository;
//            _userRepository = userRepository;
            string autHeader = System.Web.HttpContext.Current.Request.Headers["Authorization"];

            if (autHeader != null)
            {
                _token = autHeader.Split(' ')[1];
            }

        }
         


        /*
         ***********************PAGING BLOCK START*************************************
         */
        public virtual ResponseModel<TVm> GetAll(BaseRequestModel<TEntity> requestModel)
        {
            var queryable = GetQuery(Repository.GetAll(), requestModel);
            var entities = GetEntries(queryable);
            var response = new ResponseModel<TVm>(entities, Repository.GetAll().Count());
            ////Repository.Dispose();
            return response;
        }

        public virtual ResponseModel<TVm> GetAllActive(BaseRequestModel<TEntity> requestModel)
        {
            var queryable = GetQuery(Repository.GetAllActive(), requestModel);
            var entities = GetEntries(queryable);
            var response = new ResponseModel<TVm>(entities, Repository.GetAllActive().Count());
            //Repository.Dispose();
            return response;
        }

        public virtual ResponseModel<TVm> GetAllInactive(BaseRequestModel<TEntity> requestModel)
        {
            var queryable = GetQuery(Repository.GetAllInactive(), requestModel);
            var entities = GetEntries(queryable);
            var response = new ResponseModel<TVm>(entities, Repository.GetAllInactive().Count());
            //Repository.Dispose();
            return response;
        }


        //***********************SEARCH*************************************         
        public virtual ResponseModel<TVm> GeneralSearch(PagingDataType status, BaseRequestModel<TEntity> requestModel)
        {
            int count;
            ResponseModel<TVm> response;

            switch (status)
            {
                case PagingDataType.All:
                    count = Repository.GetAll().Where(requestModel.GetExpression()).Count();
                    response = GetAll(requestModel);
                    response.Count = count;
                    return response;

                case PagingDataType.Active:
                    count = Repository.GetAllActive().Where(requestModel.GetExpression()).Count();
                    response = GetAllActive(requestModel);
                    response.Count = count;
                    return response;

                case PagingDataType.Inactive:
                    count = Repository.GetAllInactive().Where(requestModel.GetExpression()).Count();
                    response = GetAllInactive(requestModel);
                    response.Count = count;
                    return response;

                default:
                    throw new ArgumentOutOfRangeException(nameof(status), status, null);
            }
        }
        /*
         ***********************PAGING BLOCK END*************************************
         */



        public virtual List<TVm> GetAll()
        {
            var user = GetUserFromToken();
            var list = Repository.GetAll().Where(x => x.CreatedCompany == user.CompanyId).ToList();
            var entities = list.ConvertAll(x => (TVm)Activator.CreateInstance(typeof(TVm), x));
            //Repository.Dispose();
            return entities;
        }

        public virtual List<TVm> GetAllActive()
        {
            var user = GetUserFromToken();
            var queryable = Repository.GetAllActive().Where(x => x.CreatedCompany == user.CompanyId);
            var entities = GetEntries(queryable);
            //Repository.Dispose();
            return entities;
        }

        public virtual List<TVm> GetAllInactive()
        {
            var user = GetUserFromToken();
            var queryable = Repository.GetAllInactive().Where(x => x.CreatedCompany == user.CompanyId);
            var entities = GetEntries(queryable);
            //Repository.Dispose();
            return entities;
        }


        public virtual TVm GetById(string id)
        {
            var user = GetUserFromToken();
            var entity = Repository.GetById(id);
            var entityViewModel = (TVm)Activator.CreateInstance(typeof(TVm), entity);
            //Repository.Dispose();
            return entityViewModel;
        }
        

        public virtual bool Add(TEntity entity, UserViewModel user)
        {
            entity = CreateFootPrint(entity, user);
            var add = Repository.Add(entity);
            var commit = Repository.Commit();
            //Repository.Dispose();
            return commit;
        }

        public virtual bool Add(TEntity entity)
        {
            var user = GetUserFromToken();
            entity = CreateFootPrint(entity, user);
            var add = Repository.Add(entity);
            var commit = Repository.Commit();
            //Repository.Dispose();

            return commit;
        }

        public virtual TVm AddwithReturnId(TEntity entity)
        {
            var user = GetUserFromToken();
            entity = CreateFootPrint(entity, user);
            var returnEntity = Repository.Add(entity);
            var commit = Repository.Commit();
            //Repository.Dispose();
            var entityViewModel = (TVm)Activator.CreateInstance(typeof(TVm), returnEntity);

            return entityViewModel;
        }
        

        public virtual bool Add(TEntity entity, string companyId)
        {
            var user = GetUserFromToken();
            entity = CreateFootPrintWithGivenCompany(entity, user, companyId);
            var add = Repository.Add(entity);
            var commit = Repository.Commit();
            //Repository.Dispose();
            return commit;
        }
        public virtual bool Add(List<TEntity> entities)
        {
            List < TEntity > modifiedEntities = new List<TEntity>();
            var user = GetUserFromToken();
             foreach (var entity in entities)
            {
                var newEntity = CreateFootPrint(entity, user);
                modifiedEntities.Add(newEntity);
            }
            var queryable = Repository.Add(modifiedEntities);
            var commit = Repository.Commit();
            //Repository.Dispose();
            return commit;
        }


        public virtual bool Edit(TEntity entity)
        {
            var user = GetUserFromToken();
            entity = CreateFootPrintForEdit(entity, user);
            var entityState = Repository.Edit(entity);
            var commit = Repository.Commit();
            //Repository.Dispose();
            return commit;
        }

        public virtual bool Delete(string id)
        {
            var delete = Repository.Delete(Repository.GetById(id));
            var commit = Repository.Commit();
            //Repository.Dispose();
            return commit;
        }

        public virtual bool Trash(string id)
        {
            var entity = Repository.GetById(id);
            entity.Active = false;
            var entityState = Repository.Trash(entity);
            var commit = Repository.Commit();
            //Repository.Dispose();
            return commit;
        }

        public virtual bool Restore(string id)
        {
            var entity = Repository.GetById(id);
            entity.Active = true;
            var entityState = Repository.Edit(entity);
            var commit = Repository.Commit();
            //Repository.Dispose();
            return commit;
        }

        public virtual bool RemoveAll(List<TEntity> entries)
        {
            IEnumerable<TEntity> removeAll = Repository.RemoveAll(entries);
            bool commit = Repository.Commit();
            //Repository.Dispose();
            return commit;
        }

        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> queryable, BaseRequestModel<TEntity> requestModel)
        {
            return requestModel.GetSkipAndTakeQuery(requestModel.GetOrderedDataQuery(queryable.Where(requestModel.GetExpression())));
        }

        public static List<TVm> GetEntries(IQueryable<TEntity> queryable)
        {
            return queryable.ToList().ConvertAll(x => (TVm)Activator.CreateInstance(typeof(TVm), x));
        }

        public TVm GetByIdAsNoTracking(string id)
        {
            var entity = Repository.GetAll().AsNoTracking().Where(x => x.Id == id).FirstOrDefault();
            var entityViewModel = (TVm)Activator.CreateInstance(typeof(TVm), entity);
            return entityViewModel;
        }

        public TEntity CreateFootPrint(TEntity entity, UserViewModel user)
        {
            entity.Id = Guid.NewGuid().ToString();
            entity.Created = DateTime.Now;
            entity.Modified = DateTime.Now;
            entity.CreatedBy = user.Id;
            entity.ModifiedBy = user.Id;
            entity.CreatedCompany = user.Company.Id;
            entity.Active = true;
            entity.DeletedBy = null;
            entity.DeletionTime = null; 
            return entity;
        }

        public TEntity CreateFootPrintForEdit(TEntity entity, UserViewModel user)
        {
//            entity.Id = Guid.NewGuid().ToString();
            entity.Modified = DateTime.Now;
            entity.ModifiedBy = user.Id;
            entity.Active = true;
            entity.DeletedBy = null;
            entity.DeletionTime = null; 
            return entity;
        }


        public TEntity CreateFootPrintWithGivenCompany(TEntity entity, UserViewModel user, string companyId)
        {
            entity.Id = Guid.NewGuid().ToString();
            entity.Created = DateTime.Now;
            entity.Modified = DateTime.Now;
            entity.CreatedBy = user.Id;
            entity.ModifiedBy = null;
            entity.CreatedCompany = companyId;
            entity.Active = true;
            entity.DeletedBy = null;
            entity.DeletionTime = null;
            return entity;
        }

        public UserViewModel GetUserFromToken()
        {
            var token = _tokenRepository.GetAllActive().Include(x => x.User.Company).Include(x => x.User.Role)
                .Where(x => x.Ticket == _token && x.ExpireAt >= DateTime.Now).OrderByDescending(x=>x.Created).FirstOrDefault();

            return new UserViewModel(token.User);
        }

        public string getCreatedCompanyId()
        {
            var token = _tokenRepository.GetAllActive().Include(x => x.User.Company)
                .FirstOrDefault(x => x.Ticket == _token && x.ExpireAt >= DateTime.Now);

            return token.User.Company.Id;
        }
    }




}
