using Microsoft.AspNetCore.Http;

namespace ERPFrigorifico.Application.Exceptions
{
    public abstract class ApiException : Exception
    {
        public int StatusCode { get; }
        public string Title { get; }
        public string? Detail { get; }

        protected ApiException(int statusCode, string title, string? detail = null)
            : base(detail ?? title)
        {
            StatusCode = statusCode;
            Title = title;
            Detail = detail;
        }
    }

    public sealed class BadRequestException : ApiException
    {
        public BadRequestException(string title, string? detail = null)
            : base(StatusCodes.Status400BadRequest, title, detail) { }
    }

    public sealed class NotFoundException : ApiException
    {
        public NotFoundException(string title, string? detail = null)
            : base(StatusCodes.Status404NotFound, title, detail) { }
    }

    public sealed class ConflictException : ApiException
    {
        public ConflictException(string title, string? detail = null)
            : base(StatusCodes.Status409Conflict, title, detail) { }
    }

    public sealed class UnprocessableEntityException : ApiException
    {
        public UnprocessableEntityException(string title, string? detail = null)
            : base(StatusCodes.Status422UnprocessableEntity, title, detail) { }
    }
}
