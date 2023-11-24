using API.Specs.Meeting.Support;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace API.Specs.Meeting.Steps
{
    [Binding]
    public class JoinTheMeetingSteps
    {
        private readonly Context context;
        private readonly HttpClient httpClient;

        public JoinTheMeetingSteps(Context context)
        {
            this.context = context;
            httpClient = new HttpClient();
        }

        [Given(@"The meeting was created")]
        public async Task GivenTheMeetingWasCreated()
        {
            Guid facilitatorId = Guid.NewGuid();

            string endpoint = $"http://localhost:58824/api/Meetings/CreateMeeting/User/{facilitatorId}";

            context.HttpResponse = await httpClient.PostAsync(endpoint, null);
        }

        [When(@"I request to join the meeting")]
        public async Task WhenIRequestToJoinTheMeeting()
        {
            Guid userId = Guid.NewGuid();

            string content = await context.HttpResponse.Content.ReadAsStringAsync();

            MeetingModel meeting = JsonConvert.DeserializeObject<MeetingModel>(content);

            string endpoint = $"http://localhost:58824/api/Meetings/JoinTheMeeting/{meeting.id}/User/{userId}";

            context.HttpResponse = await httpClient.PutAsync(endpoint, null);
        }

        [Then(@"I should join the meeting successfully")]
        public void ThenIShouldJoinTheMeetingSuccessfully()
        {
            Assert.That(context.HttpResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Then(@"I should not be the facilitator")]
        public async Task ThenIShouldNotBeTheFacilitator()
        {
            string content = await context.HttpResponse.Content.ReadAsStringAsync();

            MeetingModel meeting = JsonConvert.DeserializeObject<MeetingModel>(content);

            Assert.False(meeting.userIsTheFacilitator);
        }

    }
}
