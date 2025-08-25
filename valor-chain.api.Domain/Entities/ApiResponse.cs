namespace valor_chain.api.Domain.Entities
{
    public class ApiResponse<T>
    {
        public ApiResponseType Category { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public List<string> Errors { get; set; }

        public ApiResponse()
        {
            Errors = new List<string>();
        }

        public static ApiResponse<T> Ok(T data, string message = "Succès")
            => new ApiResponse<T> { Category = ApiResponseType.Success, Message = message, Data = data };

        public static ApiResponse<T> Fail(ApiResponseType category, List<string> errors, string message = "Échec")
            => new ApiResponse<T> { Category = category, Message = message, Errors = errors };
    }

    public enum ApiResponseType
    {
        Success,
        BadRequest,
        NotFound,
        Unauthorized,
        InvalidParameters
    }

}
