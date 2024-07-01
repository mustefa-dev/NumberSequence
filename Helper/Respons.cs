using System.Runtime.InteropServices;

namespace ScopeSky.Models
{
    public class CustomResponse
    {
        public CustomResponse(int status, object data,[Optional]string message,[Optional]int Count)
        {
            this.Status = status;
            this.Message = message;
            this.Data = data;
            this.Count = Count;
        }
        public int Count { get; set; }
        public int Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}