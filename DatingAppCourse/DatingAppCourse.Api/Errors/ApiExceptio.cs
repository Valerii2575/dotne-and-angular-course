namespace DatingAppCourse.Api.Errors
{
    public class ApiExceptio(int statusCode, string message, string? details)
    {
        public int StausCode { get; set; } = statusCode;
        public string Message { get; set; } = message;
        public string Details { get; set; } = details;
    }
}
