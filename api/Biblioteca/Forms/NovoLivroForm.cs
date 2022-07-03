using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Forms
{
    public class NovoLivroForm
    {
        [Required]
        public string Autor { get; set; }
        [Required]
        public string Titulo { get; set; }
        [Required]
        public int Ano { get; set; }
    }
}