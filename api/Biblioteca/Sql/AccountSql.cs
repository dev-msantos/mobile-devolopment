using System;

namespace Biblioteca.Sql
{
    public class AccountSql
    {
        public static string INSERT_ACCOUNT =
            @"insert into Accounts (Id, Username, Name, Password) 
              values (@Id, @Username, @Name, @Password);";
        
        public static string FIND_ACCOUNT =
             @"select Id, Username, Name, Password 
                 from Accounts where Username = @Username;";
        
        public static string EXISTS_USERNAME =
            @"select 1 from Accounts where Username = @Username;";
        
    }
}