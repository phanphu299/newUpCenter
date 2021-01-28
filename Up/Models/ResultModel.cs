namespace Up.Models
{
    public class ResultModel
    {
        public string Message { get; set; }
        public string Status { get; set; }
        public object Result { get; set; }
        public int StatusCode { get; set; }
    }
}
