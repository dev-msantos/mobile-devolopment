using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Biblioteca.Forms;
using Biblioteca.Models;
using Biblioteca.Sql;
using Dapper;
using Microsoft.Extensions.Options;
using MySqlConnector;

namespace Biblioteca.DAOs
{
    public class LivroDAO
    {
        private string ConnectionString;
        public LivroDAO(string connectionString)
        {
            ConnectionString = connectionString;
        }
        public async Task<Livro> NovoLivro(NovoLivroForm form)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                var parameters = new
                {
                    Autor = form.Autor,
                    Titulo = form.Titulo,
                    Ano = form.Ano
                };

                await connection.ExecuteAsync(LivroSql.INSERT_LIVRO(), parameters);
                Livro livro = await connection.QueryFirstOrDefaultAsync<Livro>(LivroSql.GET_LAST());
                return livro;
            }
        }
        public async Task<Livro> GetById(int id)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                var parameters = new
                {
                    Id = id
                };

                return await connection.QueryFirstOrDefaultAsync<Livro>(LivroSql.GET_BY_ID(), parameters);
            }
        }
        public async Task<Livro> UpdateBook(UpdateBookForm form)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                var parameters = new
                {
                    Id = form.Livro.Id,
                    Autor = form.Livro.Autor,
                    Titulo = form.Livro.Titulo,
                    Ano = form.Livro.Ano
                };

                await connection.ExecuteAsync(LivroSql.UPDATE_BOOK(), parameters);
                return await connection.QueryFirstAsync<Livro>(LivroSql.GET_BY_ID(), parameters);
            }
        }
        public async Task<bool> DeleteBook(int id)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                var parameters = new
                {
                    Id = id
                };

                int count = await connection.ExecuteAsync(LivroSql.DELETE_BY_ID(), parameters);
                return count > 0 ? true : false;
            }
        }
        public async Task<List<Livro>> GetAll()
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                IEnumerable<Livro> livros = await connection.QueryAsync<Livro>(LivroSql.GET_ALL());
                return livros.ToList();
            }
        }
    }
}