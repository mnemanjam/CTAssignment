using CT.Core.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CT.Core.DAL.Repository.RepositoryImpl
{
    public abstract class SqlRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        #region "Fields"

        protected CTEntities context;
        protected DbSet<TEntity> dbSet;

        #endregion

        #region "Constructor"
        public SqlRepository(CTEntities context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }
        #endregion

        #region "GetById"
        public abstract TEntity GetById(object id);
        #endregion

        #region "GetAll()"
        public IQueryable<TEntity> GetAll()
        {
            return dbSet;
        }
        #endregion

        #region "FindWhere"
        public IQueryable<TEntity> FindWhere(Expression<Func<TEntity, bool>> predicate)
        {
            return dbSet.Where(predicate);
        }
        #endregion

        #region "Add"
        public void Add(TEntity entity)
        {
            dbSet.Add(entity);
        }
        #endregion

        #region "Remove"
        public void Remove(TEntity entity)
        {
            dbSet.Attach(entity); 
            dbSet.Remove(entity); 
        }
        #endregion

        #region "FindAll"
        public IQueryable<TEntity> FindAll()
        {
            return dbSet;
        }
        #endregion

        #region "Update(TEntity entity)"
        public void Update(TEntity entity)
        {
            dbSet.Attach(entity); 
            this.context.Entry(entity).State = EntityState.Modified; 
        }
        #endregion

        #region "ExecuteStoreCommand"
        public int ExecuteSqlCommand(String commandText, params object[] parameters)
        {
            if (parameters == null)
                return context.Database.ExecuteSqlCommand(commandText, new Object[] { });
            else
                return context.Database.ExecuteSqlCommand(commandText, parameters);
        }
        #endregion

        #region "ExecuteStoreQuery"
        public List<T> ExecuteSqlQuery<T>(String commandText, params object[] parameters)
        {
            if (parameters == null)
                return context.Database.SqlQuery<T>(commandText, new Object[] { }).ToList<T>();
            else
                return context.Database.SqlQuery<T>(commandText, parameters).ToList<T>();
        }
        #endregion

        #region "Update(TEntity entity, String[] modifiedProperties)"
        public void Update(TEntity entity, String[] modifiedProperties, bool validate)
        {
            this.context.Configuration.ValidateOnSaveEnabled = validate;

            dbSet.Attach(entity);
            DbEntityEntry dbEntityEntry = this.context.Entry(entity);
            foreach (String modifiedProperty in modifiedProperties)
            {
                dbEntityEntry.Property(modifiedProperty).IsModified = true; 
            }
        }
        #endregion        

        #region "SetDefaultIsolationLevel"
        public void SetDefaultIsolationLevel()
        {
            this.ExecuteSqlCommand("set transaction isolation level read committed;");
        }
        #endregion

        #region "SetIsolationLevelForReporting"
        public void SetIsolationLevelForReporting()
        {
            this.ExecuteSqlCommand("set transaction isolation level read uncommitted;");
        }
        #endregion

    }
}
