using Cb.Payments.Model;
using System;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using TechTalk.SpecFlow;
using Xunit;

namespace Cb.Payments.Specs.OrderServiceSpecs
{
    [Binding]
    public class GetAnErrorWhenRetrievingOrdersWithInvalidCredentialsSteps
    {
        private OrderService _service;
        private HttpClient _httpClient;

        [Before]
        public void BeforeScenario()
        {
            var responseMessage = new HttpResponseMessage(HttpStatusCode.Forbidden);
            responseMessage.Content = new FakeContent("");
            var messageHandler = new FakeMessageHandler(responseMessage);
            _httpClient = new HttpClient(messageHandler);
        }

        [Given(@"I try to access the ClickBank Order API without credentials")]
        public void GivenITryToAccessTheClickBankOrderAPIWithoutCredentials()
        {
            var auth = new Cb.Payments.Model.Authorization();

            _service = new OrderService(auth, _httpClient);
        }

        [Given(@"I try to access the ClickBank Order API with invalid credentials")]
        public void GivenITryToAccessTheClickBankOrderAPIWithInvalidCredentials()
        {
            var auth = new Cb.Payments.Model.Authorization
            {
                DevKey = "INVALIDDEVKEY",
                ClerkKey = "INVALIDCLERKKEY"
            };

            _service = new OrderService(auth, _httpClient);
        }

        [When(@"I call the list portion of the orders service")]
        public void WhenICallTheListPortionOfTheOrdersService()
        {
            try
            {
                var orders = _service.GetList();
            }
            catch (Exception ex)
            {
                ScenarioContext.Current.Add("Exception", ex);
            }
        }

        [Then(@"an ArgumentException should occur")]
        public void ThenAnArgumentExceptionShouldOccur()
        {
            var ex = ScenarioContext.Current["Exception"];
            Assert.Equal(typeof(ArgumentException), ex.GetType());
        }


        [Then(@"an UnauthorizedAccessException should occur")]
        public void ThenAnUnauthorizedAccessExceptionShouldOccur()
        {
            var ex = ScenarioContext.Current["Exception"];
            Assert.Equal(typeof(UnauthorizedAccessException), ex.GetType());
        }

        [Then(@"I should receive an Exception message '(.*)'")]
        public void ThenIShouldReceiveAnExceptionMessage(string errorMessage)
        {
            var ex = (Exception)ScenarioContext.Current["Exception"];
            Assert.Contains(errorMessage, ex.Message);
        }

        [After]
        public void AfterScenario()
        {
            _httpClient.Dispose();
        }
    }
}
