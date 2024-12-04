namespace Resturant_Api.HandleErrors
{
    public class ApiExceptionError :ApiErrorResponse
    {
        public string? Details { get; set; }
        public ApiExceptionError(int code, string? mssge =null, string? details = null) : base(code, mssge)
        {
            this.Details = details;
        }
    }
}
