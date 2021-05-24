namespace DataIngestion.Src.Responses
{
    public class ApiResponse<TData> 
    {
        public string StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public TData Data { get; set; }
    }
}