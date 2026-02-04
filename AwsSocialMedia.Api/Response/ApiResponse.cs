namespace AwsSocialMedia.Api.Response
{
    public class ApiResponse<T>(T data)
    {
        public T Data { get; set; } = data;
    }
}
