using System;
using System.Threading.Tasks;
using Biblioteca.Forms;
using Biblioteca.Models;
using Biblioteca.Sql;
using Dapper;
using MySqlConnector;

namespace Biblioteca.DAOs
{
    public class AuthDAO
    {
        private string ConnectionString;
        public AuthDAO(string connectionString) => ConnectionString = connectionString;
        public async Task<Account> CreateAccount(CreateAccountForm form)
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();

                var parameters = new
                {
                    Id = Guid.NewGuid().ToString(),
                    Username = form.Username,
                    Name = form.Name,
                    Password = form.Password
                };

                var sql = AccountSql.INSERT_ACCOUNT + AccountSql.FIND_ACCOUNT;
                return await conn.QueryFirstAsync<Account>(sql, parameters);
            }
        }
        public async Task<Account> FindAccount(string username)
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();

                return await conn.QueryFirstOrDefaultAsync<Account>(AccountSql.FIND_ACCOUNT, new { Username = username });
            }
        }
        public async Task<bool> ExistsAccount(string username)
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();

                int result = await conn.QueryFirstOrDefaultAsync<int>(AccountSql.EXISTS_USERNAME, new { Username = username });
                return result.Equals(1) ? true : false;
            }
        }
    }
}