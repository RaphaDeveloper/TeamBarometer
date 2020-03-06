using API.Specs.Session.Support;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace API.Specs.Session.Steps
{
    [Binding]
    public class JoinTheSessionSteps
    {
        private readonly Context context;
        private readonly HttpClient httpClient;

        public JoinTheSessionSteps(Context context)
        {
            this.context = context;
            this.httpClient = new HttpClient();
        }

        [Given(@"The session was created")]
        public async Task GivenTheSessionWasCreated()
        {
            Guid facilitatorId = Guid.NewGuid();

            string endpoint = $"http://localhost:58824/api/sessions/createsession/user/{facilitatorId}";

            context.HttpResponse = await httpClient.PostAsync(endpoint, null);
        }
        
        [When(@"I request to join the session")]
        public async Task WhenIRequestToJoinTheSession()
        {
            Guid userId = Guid.NewGuid();

            string content = await context.HttpResponse.Content.ReadAsStringAsync();

            SessionModel session = JsonConvert.DeserializeObject<SessionModel>(content);

            string endpoint = $"http://localhost:58824/api/sessions/jointhesession/{session.id}/user/{userId}";

            context.HttpResponse = await httpClient.PutAsync(endpoint, null);
        }
        
        [Then(@"I should join the session successfully")]
        public void ThenIShouldJoinTheSessionSuccessfully()
        {
            Assert.That(context.HttpResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Then(@"I should not be the facilitator")]
        public async Task ThenIShouldNotBeTheFacilitator()
        {
            string content = await context.HttpResponse.Content.ReadAsStringAsync();

            SessionModel session = JsonConvert.DeserializeObject<SessionModel>(content);

            Assert.False(session.userIsTheFacilitator);
        }

    }
}
