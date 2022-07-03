using System.Collections.Generic;
using Biblioteca.Models;

namespace Biblioteca.Dtos
{
    public class LivrosDto
    {
        public List<Livro> Livros { get; }

        public LivrosDto(List<Livro> livros)
        {
            Livros = livros;
        }
    }
}