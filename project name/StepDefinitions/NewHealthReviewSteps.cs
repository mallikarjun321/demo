using System;
using TechTalk.SpecFlow;

namespace NSS_ChildHealth.StepDefinitionsScheduledImms
{
    [Binding]
    public class NewHealthReviewSteps
    {
        [When(@"I click on the New Health Review link on the bottom of the page")]
        public void WhenIClickOnTheNewHealthReviewLinkOnTheBottomOfThePage()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"enter (.*) Code")]
        public void ThenEnterCode(int p0)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"enter Code description auto test")]
        public void ThenEnterCodeDescriptionAutoTest()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"enter a Minimum Age as (.*)")]
        public void ThenEnterAMinimumAgeAs(int p0)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"select Years")]
        public void ThenSelectYears()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"enter a Maximum Age as (.*)")]
        public void ThenEnterAMaximumAgeAs(int p0)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"Select preferred sex as both/any")]
        public void ThenSelectPreferredSexAsBothAny()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
