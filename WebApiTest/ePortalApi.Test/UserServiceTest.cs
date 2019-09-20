using ApiConnection.ConnectionFactory;
using ApiConnection.Entities;
using ApiConnection.Interfaces;
using ApiConnection.Repositories;
using ePortalApi.Interfaces;
using ePortalApi.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ePortalApi.Test
{
    public class UserServiceTest : IUserService
    {
        private readonly IUserService _service;
        private readonly IDbConnectionFactory _db;
        private readonly IUserRepository _userRepository;

        public UserServiceTest()
        {

            _db = new DbConnectionFactory("Data Source=.\\SQLEXPRESS;Initial Catalog=TEST_ESTAGIRANET;User ID=sa;Password=sa!123; Application Name=ePortal;");
            _userRepository = new UserRepository(_db);
            _service = new UserService(_userRepository);
        }

        public  Task<int> SaveUser(User user)
        {
            var result =  _userRepository.AddAsync(user);
            return result;
        }

        public  Task<int> UpdateUser(User user)
        {
            var result =  _userRepository.UpdateAsync(user);
            return result;
        }

        public  Task<int> DeleteUser(int Id)
        {
            var result =  _userRepository.DeleteAsync(Id);
            return result;
        }

        public  Task<User> GetOneUser(int Id)
        {
            var result =  _userRepository.GetByIdAsync(Id);
            return result;
        }

        public  Task<IEnumerable<User>> GetUsers()
        {
            var result =  _userRepository.GetUsers();
            return result;
        }

        public  Task<int> DeleteUser(User entity)
        {
            var result =  _userRepository.DeleteAsync(entity);
            return result;
        }
    }
}
