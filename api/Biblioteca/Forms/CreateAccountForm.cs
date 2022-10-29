using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Forms
{
    public class CreateAccountForm
    {
        [Required]
        [StringLength(30)]
        public string Username { get; set; }
        
        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(7)]
        [MaxLength(50)]
        public string Password { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [MinLength(7)]
        [MaxLength(50)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}