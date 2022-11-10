using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Biblioteca.Forms;
using Biblioteca.Models;
using Biblioteca.Sql;
using Dapper;
using MySqlConnector;
using System;

namespace Biblioteca.DAOs
{
    public class LivroDAO
    {
        private string ConnectionString;
        public LivroDAO(string connectionString) => ConnectionString = connectionString;        
        public async Task<Livro> NovoLivro(NovoLivroForm form)
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();

                var parameters = new
                {
                    Id = Guid.NewGuid().ToString(),
                    Autor = form.Autor,
                    Titulo = form.Titulo,
                    Ano = form.Ano
                };

                var sql = LivroSql.INSERT_LIVRO + LivroSql.GET_BY_ID;
                return await conn.QueryFirstAsync<Livro>(sql, parameters);
            }
        }
        public async Task<Livro> GetById(string id)
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                return await conn.QueryFirstOrDefaultAsync<Livro>(LivroSql.GET_BY_ID, new { Id = id });
            }
        }
        public async Task<Livro> UpdateBook(UpdateBookForm form)
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();

                var parameters = new
                {
                    Id = form.Livro.Id,
                    Autor = form.Livro.Autor,
                    Titulo = form.Livro.Titulo,
                    Ano = form.Livro.Ano
                };

                await conn.ExecuteAsync(LivroSql.UPDATE_BOOK, parameters);
                return await conn.QueryFirstAsync<Livro>(LivroSql.GET_BY_ID, parameters);
            }
        }
        public async Task<bool> DeleteBook(string id)
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                var count = await conn.ExecuteAsync(LivroSql.DELETE_BY_ID, new { Id = id });

                return count > 0 ? true : false;
            }
        }
        public async Task<List<Livro>> GetAll()
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                var livros = await conn.QueryAsync<Livro>(LivroSql.GET_ALL);
                return livros.ToList();
            }
        }
    }
}