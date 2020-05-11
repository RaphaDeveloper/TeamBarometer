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
	public class CreateMeetingSteps
	{
		private readonly Context context;
		private readonly HttpClient httpClient;

		public CreateMeetingSteps(Context context)
		{
			this.context = context;
			httpClient = new HttpClient();
		}

		[Given(@"I am a user")]
		public void GivenIAmCreatingAMeeting()
		{
			context.UserId = Guid.NewGuid().ToString();
		}

		[When(@"I request the creation")]
		public async Task WhenIRequestTheCreation()
		{
			string endpoint = $"http://localhost:58824/api/Meeting/CreateMeeting/User/{context.UserId}";

			context.HttpResponse = await httpClient.PostAsync(endpoint, null);
		}

		[Then(@"The meeting should be created successfully")]
		public void ThenTheMeetingShouldBeCreatedSuccessfully()
		{
			Assert.That(context.HttpResponse.StatusCode, Is.EqualTo(HttpStatusCode.Created));
		}

		[Then(@"I should be the facilitator")]
		public async Task ThenIShouldBeTheFacilitator()
		{
			string content = await context.HttpResponse.Content.ReadAsStringAsync();

			MeetingModel meeting = JsonConvert.DeserializeObject<MeetingModel>(content);

			Assert.True(meeting.userIsTheFacilitator);
		}

		[Then(@"The created meeting should has an Id")]
		public async Task ThenTheCreatedMeetingShouldHasAnId()
		{
			string content = await context.HttpResponse.Content.ReadAsStringAsync();

			MeetingModel meeting = JsonConvert.DeserializeObject<MeetingModel>(content);

			Assert.That(meeting.id, Is.Not.EqualTo(Guid.Empty));
		}
	}
}
