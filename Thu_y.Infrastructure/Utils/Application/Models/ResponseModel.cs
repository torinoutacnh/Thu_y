namespace Thu_y.Utils.Infrastructure.Application.Models
{
    public class ResponseModel<T>
    {
        public T? data { get; set; }
        public object? additionalData { get; set; }
        public string? message { get; set; }

        public ResponseModel(T? data, object? additionalData = null, string? message = null)
        {
            this.data = data;
            this.message = message;
            this.additionalData = additionalData;
        }

        public ResponseModel(string? message)
        {
            this.message = message;
        }
    }
}
