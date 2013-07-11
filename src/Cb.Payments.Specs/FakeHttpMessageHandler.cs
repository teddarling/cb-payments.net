using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cb.Payments.Specs
{
    public class FakeMessageHandler : HttpMessageHandler
    {
        private HttpResponseMessage _response;

        public FakeMessageHandler(HttpResponseMessage response)
        {
            _response = response;
        }
        
        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var responseMessage = 
                new TaskCompletionSource<HttpResponseMessage>();

            responseMessage.SetResult(_response);

            return responseMessage.Task;
        }
    }

    public class FakeContent : HttpContent
    {
        private string _content;

        public FakeContent(string content)
        {
            _content = content;
        }

        protected async override Task SerializeToStreamAsync(
            Stream stream, TransportContext context)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(_content);
            await stream.WriteAsync(bytes, 0, bytes.Length);
        }

        protected override bool TryComputeLength(out long length)
        {
            length = _content.Length;
            return true;
        }
    }
}
