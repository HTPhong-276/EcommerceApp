using Infrastructure;
using Repository.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implements
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext context;
        private Hashtable repositories;
        public UnitOfWork(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<int> Complete()
        {
            return await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            if (repositories == null) repositories = new Hashtable();

            var type = typeof(TEntity).Name;

            if (!repositories.ContainsKey(type))
            {
                var repoType = typeof(GenericRepository<>);
                var repoInstance = Activator.CreateInstance(repoType.MakeGenericType(typeof(TEntity)), context);

                repositories.Add(type, repoInstance);
            }

            return (IGenericRepository<TEntity>)repositories[type];
        }
    }
}
