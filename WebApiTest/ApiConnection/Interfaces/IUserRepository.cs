
using ApiConnection.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 

namespace ApiConnection.Interfaces
{
    public interface IUserRepository : IAsyncRepository<User>
    {
        Task<IEnumerable<User>> GetUsers();
        IEnumerable<User> GetUsersNoAsync();
    }
}
