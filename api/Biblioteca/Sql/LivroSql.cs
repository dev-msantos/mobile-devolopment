namespace Biblioteca.Sql
{
    public struct LivroSql
    {
        public const string INSERT_LIVRO = @"
            insert into Books (Id, Autor, Titulo, Ano) values (@Id, @Autor, @Titulo, @Ano);";
        public const string GET_BY_ID = @"
            select Id, Autor, Titulo, Ano from Books where Id = @Id;";
        public const string GET_ALL =
            @"select Id, Autor, Titulo, Ano from Books;";
        public const string DELETE_BY_ID =
            @"delete from Books where Id = @Id;";
        public const string UPDATE_BOOK =
            @"update Books set Autor = @Autor, Titulo = @Titulo, Ano = @Ano where Id = @Id;";
    }
}