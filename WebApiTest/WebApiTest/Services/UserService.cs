using ApiConnection.Entities;
using ApiConnection.Interfaces;
using ePortalApi.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 

namespace ePortalApi.Services
{
    public class UserService : IUserService
    {
       
        private readonly IUserRepository _userRepository;
      
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository)); 
        }
 
        public async Task<int> SaveUser(User user)
        {
            var result = await _userRepository.AddAsync(user);
            return result;
        }

        public async Task<int> UpdateUser(User user)
        {
            var result = await _userRepository.UpdateAsync(user);
            return result;
        }

        public async Task<int> DeleteUser(int Id)
        {
            var result = await _userRepository.DeleteAsync(Id);
            return result;
        }

        public async Task<User> GetOneUser(int Id)
        {
            var result = await _userRepository.GetByIdAsync(Id);
            return result;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var result = await _userRepository.GetUsers();
            return result;
        }
        
        public async Task<int> DeleteUser(User entity)
        {
            var result = await _userRepository.DeleteAsync(entity);
            return result;
        }
    }

}
