using System;

namespace Domain
{
	public class TeamBarometerService
	{
		public string GenerateSessionID()
		{
			return Guid.NewGuid().ToString();
		}
	}
}
