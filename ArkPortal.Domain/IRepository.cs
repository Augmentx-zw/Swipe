using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ArkPotal.Domain
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity GetByID(object id);
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        TEntity GetOne(
    Expression<Func<TEntity, bool>> filter = null,
    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
    string includeProperties = "");
        void Insert(TEntity entity);

        void Delete(TEntity entityToDelete);

        void Update(TEntity entityToUpdate);
    }
}
