namespace Data
{
    using System.Data;
    using Microsoft.Data.SqlClient;

    using Dapper;
    using Microsoft.Extensions.Configuration;
    using Model;


    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(int id);
        Task AddUser(User user);
        Task UpdateUser(User user);
        Task DeleteUser(int id);
    }

    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;
        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private IDbConnection Connection => new SqlConnection(_connectionString);

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            using var db = Connection;
            return await db.QueryAsync<User>("SELECT * FROM user_master");
        }

        public async Task<User> GetUserById(int id)
        {
            using var db = Connection;
            return await db.QueryFirstOrDefaultAsync<User>("SELECT * FROM user_master WHERE uid = @Id", new { Id = id });
        }

        public async Task AddUser(User user)
        {
            using var db = Connection;
            var sql = "INSERT INTO user_master (first_name, last_name, address_line1, address_line2, city, state, country, zip_code, phone_no, whatsapp_no, is_super_user, user_name, user_code, act_inact_ind, created_by, created_date) VALUES (@FirstName, @LastName, @AddressLine1, @AddressLine2, @City, @State, @Country, @ZipCode, @PhoneNo, @WhatsappNo, @IsSuperUser, @UserName, @UserCode, @ActInactInd, @CreatedBy, @CreatedDate)";
            await db.ExecuteAsync(sql, user);
        }

        public async Task UpdateUser(User user)
        {
            using var db = Connection;
            var sql = "UPDATE user_master SET first_name = @FirstName, last_name = @LastName, address_line1 = @AddressLine1, address_line2 = @AddressLine2, city = @City, state = @State, country = @Country, zip_code = @ZipCode, phone_no = @PhoneNo, whatsapp_no = @WhatsappNo, is_super_user = @IsSuperUser, user_name = @UserName, user_code = @UserCode, act_inact_ind = @ActInactInd, modified_date = @ModifiedDate WHERE uid = @Uid";
            await db.ExecuteAsync(sql, user);
        }

        public async Task DeleteUser(int id)
        {
            using var db = Connection;
            await db.ExecuteAsync("DELETE FROM user_master WHERE uid = @Id", new { Id = id });
        }
    }
}
