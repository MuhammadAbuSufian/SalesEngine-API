using System;
using System.Collections.Generic;
using System.Web.Helpers;
using System.Web.Http;
using Newtonsoft.Json;
using Project.Model;
using Project.Model.Enums;
using Project.RequestModel;
using Project.Server.Filters;
using Project.Service;
using Project.ViewModel;

namespace Project.Server.Controllers
{
    public interface ISaveEntriesController<T> where T : EntityBase
    {
        [Route("SaveEntries")]
        IHttpActionResult SaveEntries(List<T> entities);
    }

    public interface IPagingController
    {
        IHttpActionResult Get(PagingDataType status, string request);
        IHttpActionResult Get(SearchType type, PagingDataType status, string request);
    }

    public interface IBaseController<in T> : IPagingController where T : EntityBase
    {
        IHttpActionResult Get(DataType type);
        IHttpActionResult Get(string id);
        IHttpActionResult Post(T entity);
        IHttpActionResult Put(T entity);
        IHttpActionResult Delete(DeleteActionType type, string id);
    }
      

    [CustomAuthorization]
    public abstract class BaseController<TEntity, TVm, TRm> : ApiController, IBaseController<TEntity>
        where TEntity : EntityBase
        where TVm : BaseViewModel<TEntity>
        where TRm : BaseRequestModel<TEntity>
    {
        protected IBaseService<TEntity, TVm> Service;

        protected BaseController(IBaseService<TEntity, TVm> service)
        {
            Service = service;
        }

        public BaseController()
        {
        }

        //**************PAGING START**********************************************
        public virtual IHttpActionResult Get(PagingDataType status, string request)
        {
            var requestModel = JsonConvert.DeserializeObject<TRm>(request);

            switch (status)
            {
                case PagingDataType.All:
                    return Ok(Service.GetAll(requestModel));
                case PagingDataType.Active:
                    return Ok(Service.GetAllActive(requestModel));
                case PagingDataType.Inactive:
                    return Ok(Service.GetAllInactive(requestModel));
                default:
                    throw new ArgumentOutOfRangeException(nameof(status), status, null);
            }
        }

        public virtual IHttpActionResult Get(SearchType type, PagingDataType status, string request)
        {
            switch (type)
            {
                case SearchType.General:
                    var entities = Service.GeneralSearch(status, JsonConvert.DeserializeObject<TRm>(request));
                    return Ok(entities);

                case SearchType.Precise:
                    return Ok();

                case SearchType.Filter:
                    return Ok();

                case SearchType.Complex:
                    return Ok();

                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
        //**************PAGING END**********************************************


        public virtual IHttpActionResult Get(DataType type)
        {
            switch (type)
            {
                case DataType.All:
                    return Ok(Service.GetAll());
                case DataType.Active:
                    return Ok(Service.GetAllActive());
                case DataType.Inactive:
                    return Ok(Service.GetAllInactive());
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }


        public virtual IHttpActionResult Get(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return BadRequest("Id can not be null or empty or white space");
            var entity = Service.GetById(id);

            return Ok(entity);
        }


        public virtual IHttpActionResult Post(TEntity entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (string.IsNullOrWhiteSpace(entity.Id)) entity.Id = Guid.NewGuid().ToString();
            entity.Active = true;
            var add = Service.Add(entity);

            return Ok(add);
        }


        //[HttpPost]
        //[ActionName("SaveEntries")]
        //public IHttpActionResult SaveEntries(List<TEntity> entities)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    foreach (TEntity entity in entities)
        //    {
        //        entity.Id = Guid.NewGuid().ToString();
        //        entity.Active = true;
        //    }

        //    bool addRanges = EpbService.Add(entities);

        //    return Ok(addRanges);
        //}


        public virtual IHttpActionResult Put(TEntity model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var edit = Service.Edit(model);

            return Ok(edit);
        }


        public virtual IHttpActionResult Delete(DeleteActionType type, string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return BadRequest("Id can not be null or empty or white space");

            switch (type)
            {
                case DeleteActionType.Delete:
                    var delete = Service.Delete(id);
                    return Ok(delete);
                case DeleteActionType.Trash:
                    var trash = Service.Trash(id);
                    return Ok(trash);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}
