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
	public class CreateSessionSteps
	{
		private readonly Context context;
		private readonly HttpClient httpClient;

		public CreateSessionSteps(Context context)
		{
			this.context = context;
			this.httpClient = new HttpClient();
		}

		[Given(@"I am a user")]
		public void GivenIAmCreatingASession()
		{
			context.UserId = Guid.NewGuid().ToString();
		}

		[When(@"I request the creation")]
		public async Task WhenIRequestTheCreation()
		{
			string endpoint = $"http://localhost:58824/api/sessions/createsession/user/{context.UserId}";

			context.HttpResponse = await httpClient.PostAsync(endpoint, null);
		}

		[Then(@"The session should be created successfully")]
		public void ThenTheSessionShouldBeCreatedSuccessfully()
		{
			Assert.That(context.HttpResponse.StatusCode, Is.EqualTo(HttpStatusCode.Created));
		}

		[Then(@"I should be the facilitator")]
		public async Task ThenIShouldBeTheFacilitator()
		{
			string content = await context.HttpResponse.Content.ReadAsStringAsync();

			SessionModel session = JsonConvert.DeserializeObject<SessionModel>(content);

			Assert.True(session.userIsTheFacilitator);
		}

		[Then(@"The created session should has an Id")]
		public async Task ThenTheCreatedSessionShouldHasAnId()
		{
			string content = await context.HttpResponse.Content.ReadAsStringAsync();

			SessionModel session = JsonConvert.DeserializeObject<SessionModel>(content);

			Assert.That(session.id, Is.Not.EqualTo(Guid.Empty));
		}
	}
}
