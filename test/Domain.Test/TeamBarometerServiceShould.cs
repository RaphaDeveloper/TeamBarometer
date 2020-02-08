using NUnit.Framework;

namespace Domain.Test
{
	public class TeamBarometerServiceShould
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void GenerateUniqueSessionID()
		{
			TeamBarometerService service = new TeamBarometerService();


			string firstSessionID = service.GenerateSessionID();
			string secondSessionID = service.GenerateSessionID();
			string thirdSessionID = service.GenerateSessionID();


			Assert.That(firstSessionID, Is.Not.Null.And.Not.Empty);
			Assert.That(secondSessionID, Is.Not.Null.And.Not.Empty);
			Assert.That(thirdSessionID, Is.Not.Null.And.Not.Empty);

			Assert.That(firstSessionID, Is.Not.EqualTo(secondSessionID), "First session id is equal to second");
			Assert.That(secondSessionID, Is.Not.EqualTo(thirdSessionID), "Second session id is equal to third");
		}
	}
}