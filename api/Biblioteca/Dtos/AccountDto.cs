using Biblioteca.Models;

namespace Biblioteca.Dtos
{
    public class AccountDto
    {
        public string Username { get; }
        public string Name { get; }
        public AccountDto(Account account)
        {
            Username = account.Username;
            Name = account.Name;
        }
    }
}