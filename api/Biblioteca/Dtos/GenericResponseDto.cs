using System;
using System.Collections.Generic;

namespace Biblioteca.Dtos
{
    public class GenericResponseDto
    {
        public string Message { get; }
        public string DateAndTime { get; } = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        public List<string> Errors { get; } = new List<string>();
        public object Obj { get; }
        public GenericResponseDto(string message, List<string> errors, object obj)
        {
            Message = message;
            Errors = errors;
            Obj = obj;
        }

    }
}