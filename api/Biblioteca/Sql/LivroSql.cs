using System;

namespace Biblioteca.Sql
{
    public class LivroSql
    {
        public static string INSERT_LIVRO = @"
            insert into Books (Id, Autor, Titulo, Ano) 
            values (@Id, @Autor, @Titulo, @Ano);";  

        public static string GET_BY_ID = @"
            select Id, Autor, Titulo, Ano 
              from Books where Id = @Id;";

        public static string GET_ALL =
            @"select Id, Autor, Titulo, Ano from Books;";

        public static string DELETE_BY_ID =
            @"delete from Books where Id = @Id;";
        
        public static string UPDATE_BOOK =
            @"update Books 
                 set Autor = @Autor
                    ,Titulo = @Titulo
                    ,Ano = @Ano
               where Id = @Id;";
        
    }
}