using Empower.NHibernate.Base;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Empower.NHibernate.Interfaces
{
    public interface IRepository<TEntity>
        where TEntity : Entity
    {
        TEntity Add(TEntity entity);
        void Delete(TEntity entity);
        TEntity Get(int id);
        TEntity Update(TEntity entity);

        // Filtering
        IQueryOver<TEntity, TEntity> GetQueryOver();
    }
}
