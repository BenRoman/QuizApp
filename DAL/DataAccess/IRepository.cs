using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DAL.DataAccess
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        //void AddOrCreate(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void Delete(Expression<Func<TEntity, bool>> where);
        //void AttachToContext(TEntity entity);
        void DettachFromContext(TEntity entity);
        TEntity Get(long id);
        TEntity Get(Expression<Func<TEntity, bool>> where);
        IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where);
        IEnumerable<TEntity> GetAll();
    }
}