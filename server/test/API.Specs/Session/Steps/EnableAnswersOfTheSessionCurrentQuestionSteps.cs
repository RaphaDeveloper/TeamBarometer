using API.Handlers;
using API.Specs.Session.Support;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace API.Specs.Session.Steps
{
	[Binding]
    public class EnableAnswersOfTheSessionCurrentQuestionSteps
    {
        private readonly Context context;
        private readonly HttpClient httpClient;
        private SessionModel session;

        public EnableAnswersOfTheSessionCurrentQuestionSteps(Context context)
        {
            this.context = context;
            this.httpClient = new HttpClient();
        }

        [When(@"Enable the answers of the session current question")]
        public async Task WhenEnableCurrentQuestionAnswers()
        {
            Guid userId = Guid.NewGuid();

            string content = await context.HttpResponse.Content.ReadAsStringAsync();

            session = JsonConvert.DeserializeObject<SessionModel>(content);

            string endpoint = $"http://localhost:58824/api/sessions/EnableAnswersOfTheCurrentQuestion/{session.id}/user/{userId}";

            context.HttpResponse = await httpClient.PutAsync(endpoint, null);
        }

        [Then(@"The users should be notified")]
        public void ThenTheUsersShouldBeNotified()
        {
            Assert.True(FakeHandler.SessionWasNotified(session.id));
        }
    }
}
