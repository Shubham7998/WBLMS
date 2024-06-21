using Microsoft.EntityFrameworkCore;
using WBLMS.Data;
using WBLMS.IRepositories;

namespace WBLMS.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly WBLMSDbContext _dbContext;
        
        public Repository(WBLMSDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> CreateAsync(T entity)
        {
            var result = await _dbContext.Set<T>().AddAsync(entity);
            var result2 =  await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            var row = await _dbContext.SaveChangesAsync();
            return row > 0;
        }



        public async Task<IEnumerable<T>> GetAsyncAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetAsyncById(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
    }

}
