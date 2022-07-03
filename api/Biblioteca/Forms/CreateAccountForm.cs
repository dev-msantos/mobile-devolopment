using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Forms
{
    public class CreateAccountForm
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Password2 { get; set; }
    }
}