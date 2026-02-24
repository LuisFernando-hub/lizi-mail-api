namespace lizi_mail_api.Response
{
    public class Result<T>
    {
        public bool status { get; set; }
        public string message { get; set; }
        public T data { get; set; }

        private Result(bool status, string message, T data)
        {
            this.status = status;
            this.message = message;
            this.data = data;
        }

        private Result(bool status, T data, string message)
        {
            this.status = status;
            this.message = message;
            this.data = data;
        }

        public static Result<T> success(T data, string? message = null) => new Result<T>(true, message, data);

        public static Result<T> error(bool status, string message) => new Result<T>(false, message, default);
    }
}
