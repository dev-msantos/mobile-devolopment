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

        public AuthDAO(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public async Task<Account> CreateAccount(CreateAccountForm form)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                var parameters = new
                {
                    Username = form.Username,
                    Name = form.Name,
                    Password = form.Password
                };

                await connection.ExecuteAsync(AccountSql.INSERT_ACCOUNT(), parameters);
                Account account = await connection.QueryFirstOrDefaultAsync<Account>(AccountSql.GET_LAST());
                return account;
            }
        }
        public async Task<Account> FindAccount(string username)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                var parameters = new
                {
                    Username = username
                };

                Account account = await connection.QueryFirstOrDefaultAsync<Account>(AccountSql.FIND_ACCOUNT(), parameters);
                return account;
            }
        }
        public async Task<bool> ExistsAccount(string username)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                var parameters = new
                {
                    Username = username
                };

                int result = await connection.QueryFirstOrDefaultAsync<int>(AccountSql.EXISTS_USERNAME(), parameters);
                return result.Equals(1) ? true : false;
            }
        }
    }
}