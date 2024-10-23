namespace FrontJunior.Domain.Entities.Views
{
    public class ResponseModel
    {
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public object Response { get; set; }
    }
}
