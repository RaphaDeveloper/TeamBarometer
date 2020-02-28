using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
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
			string endpoint = "http://localhost:58824/api/sessions";

			string data = JsonConvert.SerializeObject(context.UserId);

			StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

			context.HttpResponse = await httpClient.PostAsync(endpoint, content);
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

			Session session = JsonConvert.DeserializeObject<Session>(content);

			Assert.True(session.teamMemberIsTheFacilitator);
		}

		[Then(@"The created session should has an Id")]
		public async Task ThenTheCreatedSessionShouldHasAnId()
		{
			string content = await context.HttpResponse.Content.ReadAsStringAsync();

			Session session = JsonConvert.DeserializeObject<Session>(content);

			Assert.That(session.id, Is.Not.EqualTo(Guid.Empty));
		}


	}

	public class Context
	{
		public string UserId { get; set; }
		public HttpResponseMessage HttpResponse { get; set; }
	}

	public class Session
	{
		public Guid id;
		public bool teamMemberIsTheFacilitator;
	}
}
