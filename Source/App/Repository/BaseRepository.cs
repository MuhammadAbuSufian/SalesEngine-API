using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using Project.Model;

namespace Project.Repository
{
    public interface IBaseRepository<T> where T : EntityBase
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(string createdCompany);
        IQueryable<T> GetAllActive();
        IQueryable<T> GetAllActive(string createdCompany);
        IQueryable<T> GetAllInactive();
        IQueryable<T> GetAllInactive(string createdCompany);
        T GetById(string id);
        T GetById(string id, string createdCompany);
        T Add(T entity);
        IEnumerable<T> Add(List<T> entities);
        EntityState Edit(T entity);
        EntityState Trash(T entity);
        EntityState TrashAll(List<T> entries);
        T Delete(T entity);
        IEnumerable<T> RemoveAll(List<T> entries);

        bool Commit();
        void Dispose();
    }



    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : EntityBase
    {
        protected DbContext DbContext;

        protected BaseRepository(DbContext db)
        {
            DbContext = db;
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return DbContext.Set<TEntity>().AsQueryable();
        }

        public virtual IQueryable<TEntity> GetAll(string createdCompany)
        {
            return DbContext.Set<TEntity>().Where(x => x.CreatedCompany == createdCompany).AsQueryable();
        }

        public virtual IQueryable<TEntity> GetAllActive()
        {
            return DbContext.Set<TEntity>().Where(x => x.Active).AsQueryable();
        }

        public virtual IQueryable<TEntity> GetAllActive(string createdCompany)
        {
            return DbContext.Set<TEntity>().Where(x => x.CreatedCompany == createdCompany).Where(x => x.Active).AsQueryable();
        }

        public virtual IQueryable<TEntity> GetAllInactive()
        {
            return DbContext.Set<TEntity>().Where(x => !x.Active).AsQueryable();
        }

        public virtual IQueryable<TEntity> GetAllInactive(string createdCompany)
        {
            return DbContext.Set<TEntity>().Where(x => x.CreatedCompany == createdCompany).Where(x => !x.Active).AsQueryable();
        }

        public virtual TEntity GetById(string id)
        {
            return DbContext.Set<TEntity>().Find(id);
        }

        public virtual TEntity GetById(string id, string createdCompany)
        {
            return DbContext.Set<TEntity>().FirstOrDefault(x => x.Id == id && x.CreatedCompany == createdCompany);
        }

        public virtual TEntity Add(TEntity entity)
        {
            return DbContext.Set<TEntity>().Add(entity);
        }

        public virtual IEnumerable<TEntity> Add(List<TEntity> entities)
        {
            return DbContext.Set<TEntity>().AddRange(entities);
        }

        public virtual EntityState Edit(TEntity entity)
        {
            return DbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual EntityState Trash(TEntity entity)
        {
            entity.Active = false;
            return DbContext.Entry(entity).State = EntityState.Modified;
        }

        public EntityState TrashAll(List<TEntity> entries)
        {
            foreach (TEntity entity in entries)
            {
                entity.Active = false;
                DbContext.Entry(entity).State = EntityState.Modified;
            }

            return EntityState.Modified;
        }

        public virtual TEntity Delete(TEntity entity)
        {
            return DbContext.Set<TEntity>().Remove(entity);
        }

        public virtual IEnumerable<TEntity> RemoveAll(List<TEntity> entries)
        {
            return DbContext.Set<TEntity>().RemoveRange(entries);
        }

        public bool Commit()
        {
            int saveChanges;
            try
            {
                saveChanges = DbContext.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }

            return saveChanges > 0;
        }


        public void Dispose()
        {
            DbContext.Dispose();
        }
    }


}
