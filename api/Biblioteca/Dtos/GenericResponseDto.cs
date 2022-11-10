using System;
using System.Collections.Generic;

namespace Biblioteca.Dtos
{
    public class GenericResponseDto
    {
        public string ResponseMessage { get; }
        public string DateAndTime { get; } = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        public List<string> ErrorList { get; } = new List<string>();
        public object Object { get; }
        public GenericResponseDto(string responseMessage, List<string> errorList, object obj)
        {
            ResponseMessage = responseMessage;
            ErrorList = errorList;
            Object = obj;
        }

    }
}