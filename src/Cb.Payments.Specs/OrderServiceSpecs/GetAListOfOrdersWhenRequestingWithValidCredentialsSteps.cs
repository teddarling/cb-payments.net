using Cb.Payments.Model;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using TechTalk.SpecFlow;
using Xunit;

namespace Cb.Payments.Specs
{
    [Binding]
    public class GetAListOfOrdersWhenRequestingWithValidCredentialsSteps
    {
        private OrderService _service;
        private HttpClient _httpClient;
        private Cb.Payments.Model.Authorization _auth;
        private string _response;

        private Dictionary<string, string> _queryParams = new Dictionary<string,string>();

        private OrderResponse _orderResponse;

        private int _page;


        [Given(@"I have a valid developer key '(.*)' and a valid clerk Key '(.*)'")]
        public void GivenIHaveAValidDeveloperKeyAndAValidClerkKey(string validDevKey, string validClerkKey)
        {
            _auth = new Cb.Payments.Model.Authorization
            {
                DevKey = validDevKey,
                ClerkKey = validClerkKey
            };
        }

        [Given(@"the following API return value '(.*)'")]
        public void GivenTheFollowingAPIReturnValue(string response)
        {
            _response = response;
        }
        
        [Given(@"an API HttpStatusCode of PartialContent")]
        public void GivenAnAPIHttpStatusCodeOfPartialContent()
        {

            // Setup the HttpClient with fake data.
            var responseMessage = new HttpResponseMessage(HttpStatusCode.PartialContent);
            responseMessage.Content = new FakeContent(_response);
            var messageHandler = new FakeMessageHandler(responseMessage);
            _httpClient = new HttpClient(messageHandler);

            _service = new OrderService(_auth, _httpClient);
        }
        
        [Given(@"an API HttpStatusCode of OK")]
        public void GivenAnAPIHttpStatusCodeOfOK()
        {

            // Setup the HttpClient with fake data.
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK);
            responseMessage.Content = new FakeContent(_response);
            var messageHandler = new FakeMessageHandler(responseMessage);
            _httpClient = new HttpClient(messageHandler);

            _service = new OrderService(_auth, _httpClient);
        }
        
        [When(@"I request a list with no paramaters")]
        public void WhenIRequestAListWithNoParamaters()
        {
            _orderResponse = _service.GetList();
        }

        [When(@"I set the paramater '(.*)' of '(.*)'")]
        public void WhenISetTheParamaterOf(string name, string value)
        {
            _queryParams.Add(name, value);
        }
        
        [When(@"'(.*)' of '(.*)'")]
        public void WhenOf(string name, string value)
        {
            _queryParams.Add(name, value);
        }

        [When(@"call a list with those parameters")]
        public void WhenCallAListWithThoseParameters()
        {
            _orderResponse = _service.GetList(_queryParams);
        }

        
        [Then(@"I should receive the following order info '(.*)'")]
        public void ThenIShouldReceiveTheFollowingOrderInfo(string p0)
        {
            Assert.Equal(_response, _orderResponse.Data);
        }
        
        [Then(@"an HttpStatusCode of PartialContent")]
        public void ThenAnHttpStatusCodeOfPartialContent()
        {
            Assert.Equal(HttpStatusCode.PartialContent, _orderResponse.Status);
        }
        
        [Then(@"an HttpStatusCode of OK")]
        public void ThenAnHttpStatusCodeOfOK()
        {
            Assert.Equal(HttpStatusCode.OK, _orderResponse.Status);
        }

        [Given(@"the following API Page (.*) return value '(.*)'")]
        public void GivenTheFollowingAPIPageReturnValue(int page, string response)
        {
            _page = page;
            _response = response;
        }

        [When(@"I Request Page (.*)")]
        public void WhenIRequestPage(int page)
        {
            _orderResponse = _service.GetList(page: page);
        }

    }
}
