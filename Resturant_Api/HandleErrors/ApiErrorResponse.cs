namespace Resturant_Api.HandleErrors
{
    public class ApiErrorResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }

        public ApiErrorResponse(int code , string? mssge = null)
        {
            this.StatusCode = code;
            this.Message = mssge ?? GetDefaultMessageForStatusCode(StatusCode);
            
        }

        private String? GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "A bad request, you have made",
                401 => "UnAuthorized",
                404 => "Resource found, it was not",
                500 => "Errors are the path to the dark side. Errors lead to anger. Anger leads to hate. Hate leads to career change",
                _ => null
            };
        }
    }
}
