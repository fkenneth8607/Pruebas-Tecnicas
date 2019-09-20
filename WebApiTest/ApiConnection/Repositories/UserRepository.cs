using Dapper;
using ApiConnection.Entities;
using ApiConnection.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
 
namespace ApiConnection.Repositories
{
    public class UserRepository : IUserRepository
    {

         private readonly IDbConnectionFactory _db;

        public UserRepository(IDbConnectionFactory connectionFactory)
        {
            this._db = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        public  async Task<int> AddAsync(User entity)
        {
            int result = 0;
            using (var connection = _db.Create())
            {

                int entityInserted = await connection.ExecuteScalarAsync<int>(@"INSERT INTO [dbo].[Users]
                           ([Name]
                           ,[LastName]
                           ,[Address]
                           ,[CreatedDate])
                           VALUES
                           (@Name
                           , @LastName
                           , @Address
                           , getdate()) 
             SELECT CAST(SCOPE_IDENTITY() as int)", new
                {
                    Name = entity.Name,
                    LastName = entity.LastName,
                    Address = entity.Address

                }, null);

                result = entityInserted;
            }
            return result;
        }

        public async Task<int> DeleteAsync(User entity)
        {
            int result = 0;
            using (var connection = _db.Create())
            {

                int entityUpdated = await connection.ExecuteScalarAsync<int>(@"DELETE FROM [dbo].[Users]
                     WHERE id =@Id
                     select  @@ROWCOUNT", new
                {
                    Id = entity.Id,
                }, null);

                result = entityUpdated;
            }
            return result;
        }

        public async Task<int> DeleteAsync(int id)
        {
            int result = 0;
            using (var connection = _db.Create())
            {

                int entityDeleted = await connection.ExecuteScalarAsync<int>(@"DELETE FROM [dbo].[Users]
                     WHERE id =@Id
                select  @@ROWCOUNT", new
                {
                    Id = id,
                }, null);

                result = entityDeleted;
            }
            return result;
        }
        public async Task<User> GetByIdAsync(int id)
        {
            User user = new User();
            using (var connection = _db.Create())
            {
                var userOne = await connection.QueryFirstAsync<User>(@"
                       SELECT [Id]
                      ,[Name]
                      ,[LastName]
                      ,[Address]
                      ,[CreatedDate]
                      ,[UpdatedDate]
                  FROM [dbo].[Users] where Id=@Id ", new { Id = id });
                user = userOne;
            }
            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            IEnumerable<User> users;
            using (var connection = _db.Create())
            {
                var allUsers = await connection.QueryAsync<User>(@"
                       SELECT [Id]
                      ,[Name]
                      ,[LastName]
                      ,[Address]
                      ,[CreatedDate]
                      ,[UpdatedDate]
                  FROM [dbo].[Users]", null);
                users = allUsers;
            }
            return users;
        }
        public   IEnumerable<User> GetUsersNoAsync()
        {
            IEnumerable<User> users;
            using (var connection = _db.Create())
            {
                var allUsers =   connection.Query<User>(@"
                       SELECT [Id]
                      ,[Name]
                      ,[LastName]
                      ,[Address]
                      ,[CreatedDate]
                      ,[UpdatedDate]
                  FROM [dbo].[Users]", null);
                users = allUsers;
            }
            return users;
        }
        public async Task<int> UpdateAsync(User entity)
        {
            int result = 0;
            using (var connection = _db.Create())
            {

                int entityUpdated = await connection.ExecuteScalarAsync<int>(@"UPDATE [dbo].[Users]
                       SET [Name] = @Name 
                          ,[LastName] = @LastName 
                          ,[Address] = @Address
                          ,[UpdatedDate] = getdate()
                     WHERE id =@Id
                     select  @@ROWCOUNT", new
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    LastName = entity.LastName,
                    Address = entity.Address

                }, null);

                result = entityUpdated;
            }
            return result;
        }
    }
}
