using System;

namespace Biblioteca.Sql
{
    public class AccountSql
    {
        public static string INSERT_ACCOUNT()
        {
            return @"insert into Accounts (Username, Name, Password) values (@Username, @Name, @Password);";
        }
        public static string GET_LAST()
        {
            return @"select Id, Username, Name, Password from Accounts where Id = LAST_INSERT_ID();";
        }
        public static string FIND_ACCOUNT()
        {
            return @"select Id, Username, Name, Password from Accounts where Username = @Username;";
        }
        public static string EXISTS_USERNAME()
        {
            return "select 1 from Accounts where Username = @Username;";
        }
    }
}