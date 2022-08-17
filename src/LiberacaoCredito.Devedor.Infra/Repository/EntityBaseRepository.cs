using Microsoft.EntityFrameworkCore;
using LiberacaoCredito.Devedor.Domain.Interfaces.Repository;
using LiberacaoCredito.Devedor.Infra.Context;
using System;
using System.Collections.Generic;

namespace LiberacaoCredito.Devedor.Infra.Repository
{
    public class EntityBaseRepository<TEntity> : IEntityBaseRepository<TEntity> where TEntity : class
    {
        protected readonly EntityContext Db;
        protected readonly DbSet<TEntity> DbSet;

        public EntityBaseRepository(EntityContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }

        public virtual void Add(TEntity obj)
        {
            DbSet.Add(obj);
        }

        public virtual void AddRange(List<TEntity> obj)
        {
            DbSet.AddRange(obj);
        }

        public virtual void Update(TEntity obj)
        {
            DbSet.Update(obj);
        }

        public virtual void UpdateRange(List<TEntity> obj)
        {
            DbSet.UpdateRange(obj);
        }

        public virtual void Remove(TEntity obj)
        {
            DbSet.Remove(obj);
        }

        public virtual void RemoveRange(List<TEntity> obj)
        {
            DbSet.RemoveRange(obj);
        }

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}