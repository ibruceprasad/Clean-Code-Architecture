using libraries.domain;
using library.services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace library.services.Contracts
{
    public interface IDbRepository<T> where T : EntityBase
    {
        public Task<List<T>> GetAllAsync();
        public Task<T?> GetByIdAsync(int id);
        public Task<T> AddAsync(T  entity);
        public Task<T> UpdateAsync(int id, T entity);
        public Task<bool> DeleteAsync(int id);
    }
}