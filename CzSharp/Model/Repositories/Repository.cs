using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CzSharp.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace CzSharp.Model.Repositories
{
    public abstract class Repository<T>: IRepository<T> where T: class, IIdentifiable
    {
        protected AppDbContext DbContext;
        
        protected Repository(AppDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public IQueryable<T> FindAll()
        {
            return DbContext.Set<T>();
        }

        public async Task<T> FindByIdAsync(int id)
        {
            return await FindAll().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task CreateAsync(T item)
        {
            await DbContext.Set<T>().AddAsync(item);
            await DbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T item)
        {
            DbContext.Set<T>().Update(item);
            await DbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await FindByIdAsync(id);
            await DeleteAsync(item);
        }

        public async Task DeleteAsync(T item)
        {
            DbContext.Set<T>().Remove(item);
            await DbContext.SaveChangesAsync();
        }
    }
}