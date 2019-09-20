using ApiConnection.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiConnection.Interfaces
{
    public interface IAsyncRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<int> AddAsync(T entity);
        Task<int>  UpdateAsync(T entity);
        Task<int>  DeleteAsync(T entity);
        Task <int> DeleteAsync(int id);
    }
}
