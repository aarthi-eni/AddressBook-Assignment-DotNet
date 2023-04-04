using System;
using Entities.Dtos;

namespace CustomExceptionHandling
{
    public class ExceptionModel : Exception
    {
        public ErrorDto error { get; set; } = new ErrorDto();

        public ExceptionModel(string message, string description, int statusCode)
        {
            this.error.StatusCode= statusCode;
            this.error.Description = description;
            this.error.Message = message;
        }
    }
}