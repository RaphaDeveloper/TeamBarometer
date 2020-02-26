using NUnit.Framework;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace API.Specs.Session.Steps
{
    [Binding]
    public class CreateSessionSteps
    {
        private readonly Context context;
        private readonly HttpClient httpClient;

        public CreateSessionSteps(Context context)
        {
            this.context = context;
            this.httpClient = new HttpClient();
        }

        [Given(@"I am creating a session")]
        public void GivenIAmCreatingASession()
        {
            context.Endpoint = "/sessions";
        }

        [When(@"I request the creation")]
        public async Task WhenIRequestTheCreation()
        {
            string uri = "http://localhost:58824/api";
            
            string endpoint = uri + context.Endpoint;

            context.HttpResponse = await httpClient.PostAsync(endpoint, null);
        }

        [Then(@"Session should be created successfully")]
        public void ThenSessionShouldBeCreatedSuccessfully()
        {
            Assert.That(context.HttpResponse.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }
    }

    public class Context
    {
        public string Endpoint { get; set; }
        public HttpResponseMessage HttpResponse { get; set; }
    }
}
