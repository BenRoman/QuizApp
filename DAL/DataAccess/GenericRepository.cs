using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MoreLinq;

namespace DAL.DataAccess
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly TestingContext _dataContext;
        private readonly IDbSet<TEntity> _dbset;

        public GenericRepository(DataContextProvider dcProvider)
        {
            _dataContext = dcProvider.Get();
            _dbset = _dataContext.Set<TEntity>();
        }

        public virtual void Add(TEntity entity)
        {
            _dbset.Add(entity);
            _dataContext.SaveChanges();
        }

        public virtual void Update(TEntity entity)
        {
            _dataContext.Entry(entity).State = EntityState.Modified;
            _dataContext.SaveChanges();
        }

        public virtual void Delete(TEntity entity)
        {
            _dbset.Remove(entity);
            _dataContext.SaveChanges();
        }

        public virtual void Delete(Expression<Func<TEntity, bool>> where)
        {
             _dbset.Where<TEntity>(where)
                //.AsEnumerable()
                .ForEach(entity => _dbset.Remove(entity));
            _dataContext.SaveChanges();
        }

        //public void AttachToContext(TEntity entity)
        //{
        //    ((IObjectContextAdapter)_dataContext).ObjectContext.Attach(entity);
        //}

        public void DettachFromContext(TEntity entity)
        {
            ((IObjectContextAdapter)_dataContext).ObjectContext.Detach(entity);
        }

        public virtual TEntity Get(long id)
        {
            return _dbset.Find(id);
        }

        public virtual TEntity Get(Expression<Func<TEntity, bool>> where)
        {
            return _dbset.Where(where).FirstOrDefault<TEntity>();
        }

        public virtual IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where)
        {
            return _dbset.Where(where).ToList();
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _dbset.ToList();
        }


    }
}
