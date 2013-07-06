using System;
using TechTalk.SpecFlow;
using Xunit;

namespace Cb.Payments.Specs.OrderServiceSpecs
{
    [Binding]
    public class GetAnErrorWhenRetrievingOrdersWithInvalidCredentialsSteps
    {
        private OrderService _service;
        private string _invalideDevKey;
        private string _invalidClerkKey;

        [Given(@"I try to access the ClickBank Order API without credentials")]
        public void GivenITryToAccessTheClickBankOrderAPIWithoutCredentials()
        {
            _service = new OrderService();
        }

        [Given(@"I try to access the ClickBank Order API with invalid credentials")]
        public void GivenITryToAccessTheClickBankOrderAPIWithInvalidCredentials()
        {
            _service = new OrderService(_invalideDevKey, _invalidClerkKey);
        }

        [When(@"I call the list portion of the orders service")]
        public void WhenICallTheListPortionOfTheOrdersService()
        {
            try
            {
                var orders = _service.List;
            }
            catch (ArgumentException ex)
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

        [Then(@"I should receive an Exception message '(.*)'")]
        public void ThenIShouldReceiveAnExceptionMessage(string p0)
        {
            var ex = (Exception)ScenarioContext.Current["Exception"];
            Assert.Equal("Access Denied", ex.Message);
        }
    }
}
