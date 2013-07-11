using Cb.Payments.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Cb.Payments
{
    public class OrderService
    {
        private Authorization _auth;
        private HttpClient _httpClient;
        private string _apiBase = "https://api.clickbank.com/rest/1.3/orders/";

        /// <summary>
        /// Base of the Orders API.
        /// Allow it to be overridden as the API URL
        /// can possibly change in the future.
        /// </summary>
        public string ApiBase
        {
            get
            {
                return _apiBase;
            }
            set
            {
                _apiBase = value;
            }
        }

        public OrderService(Authorization auth, HttpClient httpClient)
        {
            _auth = auth;
            _httpClient = httpClient;
        }

        /// <summary>
        /// Get a list from Clickbank starting at the beginning.
        /// </summary>
        /// <returns></returns>
        public OrderResponse GetList(int page = 1)
        {
            // Orders are available up to year in the past
            // with the ClickBank API based on plenty of past
            // experience using the API. So to get all orders, 
            // set the start date to one year ago. Subtract
            // 7 hours for ClickBank time.
            DateTime startDate =
                DateTime.UtcNow.AddDays(-365).AddHours(-7);

            var queryParams = new Dictionary<string, string>();
            queryParams.Add("startDate",
                startDate.ToString("yyyy-M-d"));

            return GetList(queryParams, page);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryParams">Dictionary<string,string>
        /// Paramters that we pass to the ClickBank API to
        /// limit the results returned to us.</param>
        /// <returns></returns>
        public OrderResponse GetList(Dictionary<string, string> queryParams, int page = 1)
        {
            SetDefaultHeaders();

            _httpClient.DefaultRequestHeaders.Add("Page", page.ToString());

            var orderResponse = new OrderResponse();

            // Get the data from ClickBank.
            var response = _httpClient.GetAsync(ApiBase +
                "list" + GetQueryString(queryParams));
            response.Wait();

            var responseMessage = response.Result;

            // Handle the status code.
            orderResponse.Status = responseMessage.StatusCode;

            // If the server tells us that we aren't authorized,
            // then throw an error as such. Since this is a portable
            // class library, we are limited and the closest
            // Exception type to no authorization is thrown.
            if (orderResponse.Status == HttpStatusCode.Forbidden)
                throw new UnauthorizedAccessException("Access Denied");

            // Read in the data
            var responseContent = responseMessage
                .Content.ReadAsStringAsync();

            responseContent.Wait();

            // Add to the response DTO.
            orderResponse.Data = responseContent.Result;

            return orderResponse;
        }

        /// <summary>
        /// Generate a URL query string based on the parameters
        /// </summary>
        /// <param name="queryParams">Dictionary<string, string> 
        /// of parameters to create a query string out of.</param>
        /// <returns>String - Query string used in a URL</returns>
        private string GetQueryString(Dictionary<string, string> queryParams)
        {
            if (queryParams.Count == 0)
                return String.Empty;

            return "?" + queryParams
                    .Select(p => p.Key + "=" + p.Value)
                    .Aggregate((a, b) => a + "&" + b);
        }

        /// <summary>
        /// Set the default headers that we are going to use for
        /// all request sent to ClickBank.
        /// </summary>
        private void SetDefaultHeaders()
        {
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(_auth.DevKey, _auth.ClerkPart);
        }
    }
}