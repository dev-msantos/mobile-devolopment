using System;

namespace Biblioteca.Sql
{
    public class LivroSql
    {
        public static string INSERT_LIVRO()
        {
            return @"insert into Books (Autor, Titulo, Ano) values (@Autor, @Titulo, @Ano);";
        }

        public static string GET_LAST()
        {
            return @"select Id, Autor, Titulo, Ano from Books where Id = LAST_INSERT_ID();";
        }

        public static string GET_BY_ID()
        {
            return @"select Id, Autor, Titulo, Ano from Books where Id = @Id;";
        }

        public static string GET_ALL()
        {
            return @"select Id, Autor, Titulo, Ano from Books;";
        }

        public static string DELETE_BY_ID()
        {
            return @"delete from Books where Id = @Id;";
        }

        public static string UPDATE_BOOK()
        {
            return @"
                    update Books 
                       set Autor = @Autor
                          ,Titulo = @Titulo
                          ,Ano = @Ano
                     where Id = @Id;
                    ";
        }
    }
}