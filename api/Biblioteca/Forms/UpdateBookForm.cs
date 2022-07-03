using System.ComponentModel.DataAnnotations;
using Biblioteca.Models;

namespace Biblioteca.Forms
{
    public class UpdateBookForm
    {
        [Required]
        public Livro Livro { get; set; }
    }
}