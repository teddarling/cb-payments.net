using System.Net;

namespace Cb.Payments.Model
{
    public class OrderResponse
    {
        public HttpStatusCode Status { get; set; }
        public string Data { get; set; }
    }
}
