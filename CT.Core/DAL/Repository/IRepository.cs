using CT.Core.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CT.Core.DAL.Repository
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        TEntity GetById(object id);
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> FindWhere(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> FindAll();
        void Add(TEntity entity);
        void Remove(TEntity entity);
        void Update(TEntity entity);
        int ExecuteSqlCommand(String commandText, params object[] parameters);
        List<T> ExecuteSqlQuery<T>(String commandText, params object[] parameters);
        void Update(TEntity entity, String[] modifiedProperties, bool onemoguciValidaciju);
        void SetDefaultIsolationLevel();
        void SetIsolationLevelForReporting();
    }
}
