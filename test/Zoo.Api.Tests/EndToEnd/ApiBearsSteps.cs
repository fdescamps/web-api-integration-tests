namespace Zoo.Api.Tests.EndToEnd
{
    using System.Collections;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    using Administration.AnimalsRegistrationAggregate.Models;

    using Core;

    using FluentAssertions;

    using Infrastructure.Entities;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    using Park.BearsAggregate.Models;

    using ROI.WebApp.Test.EndToEnd;

    using TechTalk.SpecFlow;
    using TechTalk.SpecFlow.Assist;

    [Binding]
    public class ApiBearsSteps : Steps
    {
        private readonly ScenarioContext scenarioContext;

        public ApiBearsSteps(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }

        [Given(@"the referential have any animals")]
        public void GivenTheReferentialHaveAnyAnimals(IEnumerable animals)
        {
            this.scenarioContext.Configure<EndToEndDbContext>(
                context => { context.Animals.AddRange(animals.Cast<Animal>()); });
        }

        [Given(@"the referential have any classification")]
        public void GivenTheReferentialHaveAnyClassification(IEnumerable classification)
        {
            this.scenarioContext.Configure<EndToEndDbContext>(
                context => { context.Classifications.AddRange(classification.Cast<Classification>()); });
        }

        [Given(@"the referential have any families")]
        public void GivenTheReferentialHaveAnyFamilies(IEnumerable families)
        {
            this.scenarioContext.Configure<EndToEndDbContext>(
                context => { context.Families.AddRange(families.Cast<Family>()); });
        }

        [Then(@"the content have restrained bears")]
        public async Task ThenTheContentHaveRestrainedAnimals(IEnumerable expectedRetrainedAnimals)
        {
            var response = this.scenarioContext.Get<HttpResponseMessage>();
            var actualValue = await response.Content.ReadContentAsync(
                                  value => JsonConvert.DeserializeObject(
                                      value,
                                      expectedRetrainedAnimals.GetType()));

            actualValue.Should().BeEquivalentTo(expectedRetrainedAnimals);
        }

        [Then(@"the http status code of response is (.*)")]
        public void ThenTheHttpStatusCodeOfResponseIs(HttpStatusCode httpStatusCode)
        {
            var response = this.scenarioContext.Get<HttpResponseMessage>();
            response.StatusCode.Should().Be(httpStatusCode);
        }

        [When(@"i call the http resource '(.*)' with (.*) http method")]
        public async Task WhenICallTheHttpResourceWithHttpMethod(string resource, HttpMethod httpMethod)
        {
            this.scenarioContext.TryGetValue("Post", out var postingValue);
            HttpContent httpContent = null;
            if (postingValue != null)
            {
                httpContent = new StringContent(
                    JObject.FromObject(postingValue).ToString(),
                    Encoding.UTF8,
                    "application/json");
            }

            var webFactory = this.scenarioContext.Get<WebFactory<Startup, EndToEndDbContext>>();
            using var client = webFactory.CreateClient();
            var response = await client.SendAsync(
                               new HttpRequestMessage(httpMethod, $"{resource}")
                                   {
                                       Content = httpContent
                                   });
            this.scenarioContext.Set(response);
        }

        [Given(@"the referential have any foods")]
        public void GivenTheReferentialHaveAnyFoods(IEnumerable foods)
        {
            this.scenarioContext.Configure<EndToEndDbContext>(
                context => { context.Foods.AddRange(foods.Cast<Food>()); });
        }
        
        [Given(@"the animal can eats")]
        public void GivenTheAnimalCanEats(IEnumerable animalCanEats)
        {
            this.scenarioContext.Configure<EndToEndDbContext>(
                context => { context.AnimalCanEats.AddRange(animalCanEats.Cast<AnimalCanEat>()); });
        }
        
        [When(@"i would like register bear")]
        public void WhenIWouldLikeRegisterBear(Table table)
        {
            var bearCreating = table.CreateInstance<BearCreating>();
            this.scenarioContext.Set(bearCreating, "Post");
        }
        
        [Then(@"the content is")]
        public async Task ThenTheContentIs(Table table)
        {
            var response = this.ScenarioContext.Get<HttpResponseMessage>();
            Task<T> ReadContent<T>() => response.Content.ReadContentAsync<T>();
            var expectedLocation = table.Rows[0].GetString("location");
            var expectedSuccessValue = table.CreateInstance<BearDetails>();
            var successValue = await ReadContent<BearDetails>();
            var locationValue = response.Headers.Location;
            successValue.Should().BeEquivalentTo(expectedSuccessValue);
            locationValue.Should().Be(expectedLocation);
        }
    }
}