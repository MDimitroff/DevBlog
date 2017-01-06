using System.Collections.Generic;
using DevBlog.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevBlog.Repositories.Abstract
{
    public abstract class BaseRepository<T> where T : class
    {
        private DbSet<T> _dbSet;
        private DbSet<T> DbSet => _dbSet ?? (_dbSet = Context.Set<T>());

        protected readonly BlogContext Context;

        public BaseRepository(BlogContext context)
        {
            Context = context;
        }

        public virtual void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public virtual void Add(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                DbSet.Add(entity);
            }
        }

        public virtual long Create(T entity)
        {
            DbSet.Add(entity);

            return SaveChanges();
        }

        public virtual int Delete(T entity)
        {
            DeleteWithoutSave(entity);

            return SaveChanges();
        }

        public virtual void DeleteWithoutSave(T entity)
        {
            if (entity != null)
            {
                DbSet.Remove(entity);
            }
        }

        public virtual int SaveChanges()
        {
            return Context.SaveChanges();
        }
    }
}
