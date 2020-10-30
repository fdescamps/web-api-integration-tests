namespace ROI.WebApp.Test.EndToEnd
{
    using TechTalk.SpecFlow;

    using Zoo.Api.Tests.EndToEnd;

    internal static class ScenarioContextExtensions
    {
        public static void AddAssertion(this ScenarioContext scenarioContext, Assertion assertion)
        {
            if (scenarioContext.TryGetValue(out Assertion currentAssertion))
            {
                currentAssertion += assertion;
                scenarioContext.Set(currentAssertion);
                return;
            }

            scenarioContext.Set(assertion);
        }

        public static void Configure<T>(this ScenarioContext scenarioContext, Configure<T> configure)
        {
            if (scenarioContext.TryGetValue(out Configure<T> currentConfigure))
            {
                currentConfigure += configure;
                scenarioContext.Set(currentConfigure);
                return;
            }

            scenarioContext.Set(configure);
        }
    }
}