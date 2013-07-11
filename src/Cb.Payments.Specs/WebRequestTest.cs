using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Cb.Payments.Specs
{
    public class WebRequestTest : WebRequest
    {
        private string _response;
        private bool _validCredentials;


        private MemoryStream _requestStream = new MemoryStream();
        private MemoryStream _responseStream;

        public override string Method { get; set; }
        public override string ContentType { get; set; }
        public override long ContentLength { get; set; }

        public WebRequestTest(string response, bool validCredentials)
        {
            _response = response;
            _validCredentials = validCredentials;
            _responseStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(response));
        }

        public override WebResponse GetResponse()
        {
            if (!_validCredentials)
                throw new System.Security.Authentication.AuthenticationException("Access Denied");

            return new WebResponseTest(_responseStream);
        }
    }

    public class WebRequestTestCreate : IWebRequestCreate
    {
        private string _response;
        private bool _validCredentials;

        public WebRequestTestCreate(string response, bool validCredentials)
        {
            _response = response;
            _validCredentials = validCredentials;
        }
        
        public WebRequest Create(Uri uri)
        {
            return new WebRequestTest(_response, _validCredentials);
        }
    }

    public class WebResponseTest : WebResponse
    {
        private Stream _responseStream;

        public WebResponseTest(Stream responseStream)
        {
            _responseStream = responseStream;
        }
        

        public override Stream GetResponseStream()
        {
            return _responseStream;
        }
    }
}
