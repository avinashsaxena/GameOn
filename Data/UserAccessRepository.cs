using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface IUserAccessRepository
    {
        Task<IEnumerable<UserAccess>> GetAllUserAccess();
        Task<UserAccess> GetUserAccessById(int id);
        Task AddUserAccess(UserAccess userAccess);
        Task UpdateUserAccess(UserAccess userAccess);
        Task DeleteUserAccess(int id);
    }

    public class UserAccessRepository : IUserAccessRepository
    {
        private readonly string _connectionString;
        public UserAccessRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private IDbConnection Connection => new SqlConnection(_connectionString);

        public async Task<IEnumerable<UserAccess>> GetAllUserAccess() => await Connection.QueryAsync<UserAccess>("SELECT * FROM user_access");
        public async Task<UserAccess> GetUserAccessById(int id) => await Connection.QueryFirstOrDefaultAsync<UserAccess>("SELECT * FROM user_access WHERE uaid = @Id", new { Id = id });
        public async Task AddUserAccess(UserAccess userAccess) => await Connection.ExecuteAsync("INSERT INTO user_access (user_id, access_name, allow, created_by, act_inact_ind, created_date) VALUES (@UserId, @AccessName, @Allow, @CreatedBy, @ActInactInd, @CreatedDate)", userAccess);
        public async Task UpdateUserAccess(UserAccess userAccess) => await Connection.ExecuteAsync("UPDATE user_access SET user_id = @UserId, access_name = @AccessName, allow = @Allow, act_inact_ind = @ActInactInd, modified_date = @ModifiedDate WHERE uaid = @Uaid", userAccess);
        public async Task DeleteUserAccess(int id) => await Connection.ExecuteAsync("DELETE FROM user_access WHERE uaid = @Id", new { Id = id });
    }
}
