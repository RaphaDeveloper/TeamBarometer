using Application.Sessions;
using Application.Sessions.UseCases;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class SessionsController : ControllerBase
	{
		private readonly SessionAppService sessionAppService;

		public SessionsController(SessionAppService sessionAppService)
		{
			this.sessionAppService = sessionAppService;
		}

		[HttpPost("user/{userId}")]
		public IActionResult CreateSession(Guid userId)
		{
			SessionModel session = sessionAppService.CreateSession(userId);

			return Created("", session);
		}

		[HttpPut("{sessionId}/user/{userId}/")]
		public IActionResult JoinTheSession(Guid sessionId, Guid userId)
		{
			SessionModel session = sessionAppService.JoinTheSession(sessionId, userId);

			return Ok(session);
		}

		[HttpPut("{sessionId}/user/{userId}/")]
		public IActionResult EnableAnswersOfTheCurrentQuestion(Guid sessionId, Guid userId)
		{
			sessionAppService.EnableAnswersOfTheCurrentQuestion(sessionId, userId);

			return Ok();
		}
	}
}