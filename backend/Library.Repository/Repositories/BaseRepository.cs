using libraries.domain;
using library.repository;
using library.services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Library.Repository.Repositories
{
    public class BaseRepository<T> : IDbRepository<T> where T : EntityBase
    {
        // note: concerate class
        private readonly LibraryDbContext _libraryDbContext;

        public BaseRepository(LibraryDbContext libraryDbContext)
        {
            _libraryDbContext = libraryDbContext;
        }

        public async Task<List<T>> GetAllAsync() => await _libraryDbContext.Set<T>().AsQueryable().ToListAsync();
        public async Task<T?> GetByIdAsync(int id) => await _libraryDbContext.Set<T>().AsNoTracking().SingleOrDefaultAsync(item => item.Id == id); 
        
        public async Task<T> AddAsync(T entity)  
        {
 
            await _libraryDbContext.Set<T>().AddAsync(entity);
            var count = await _libraryDbContext.SaveChangesAsync();
            if (count == 1)
                return entity;
            else
                throw new ApplicationException($"Database Add error - Failed to add book entity {entity.Id}"); 
        }

        public async  Task<bool> DeleteAsync(int id)
        {
            T? item = await GetByIdAsync(id);
            if (item == null) return false;
            _libraryDbContext.Set<T>().Remove(item);
            await _libraryDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<T> UpdateAsync(int id, T entity)
        {
            
            var item = await GetByIdAsync(id);
            if (item is null) throw new ApplicationException($"Database Update error - Failed to get book entity {entity.Id}");
            if (id != entity.Id) throw new ApplicationException($"Database Update error - Id:{id} is not maches with enity id:{entity.Id}");
            _libraryDbContext.Set<T>().Entry(entity).State = EntityState.Modified;
            //_libraryDbContext.Update(entity);          
            var count = await _libraryDbContext.SaveChangesAsync();
            if (count == 1)
                return entity;
            else
                throw new ApplicationException($"Database Add error - Failed to add book entity {entity.Id}");
        }

    }

 
}
