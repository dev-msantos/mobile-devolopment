namespace Biblioteca.Sql
{
    public struct AccountSql
    {
        public const string INSERT_ACCOUNT =
            @"insert into Accounts (Id, Username, Name, Password) values (@Id, @Username, @Name, @Password);";
        public const string FIND_ACCOUNT =
             @"select Id, Username, Name, Password from Accounts where Username = @Username;";
        public const string EXISTS_USERNAME = 
            @"select 1 from Accounts where Username = @Username;";
    }
}