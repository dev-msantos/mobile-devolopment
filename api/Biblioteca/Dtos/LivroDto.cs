using Biblioteca.Models;

namespace Biblioteca.Dtos
{
    public class LivroDto
    {
        public Livro Livro { get; }
        public LivroDto(Livro livro) => Livro = livro;        
    }
}