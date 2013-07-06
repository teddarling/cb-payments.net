using System;
using TechTalk.SpecFlow;

namespace Cb.Payments.Specs
{
    [Binding]
    public class GetAListOfOrdersWhenRequestingWithValidCredentialsSteps
    {
        [Given(@"I have valid credentials")]
        public void GivenIHaveValidCredentials()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"I should receive a list of orders")]
        public void ThenIShouldReceiveAListOfOrders()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
