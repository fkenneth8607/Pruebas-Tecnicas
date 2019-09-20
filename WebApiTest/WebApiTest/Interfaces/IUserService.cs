using ApiConnection.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ePortalApi.Interfaces
{
    public interface IUserService
    {

        Task<User> GetOneUser(int id);
        Task<IEnumerable<User>> GetUsers();
        Task<int> SaveUser(User entity);
        Task<int> UpdateUser(User entity);
        Task<int> DeleteUser(User entity);
        Task<int> DeleteUser(int id);
         
    }
}
