using API.Hubs;
using Application.Sessions;
using Application.Sessions.UseCases;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SessionsController : ControllerBase
	{
		private readonly SessionAppService sessionAppService;
		private readonly SessionHub sessionHub;

		public SessionsController(SessionAppService sessionAppService, SessionHub sessionHub)
		{
			this.sessionAppService = sessionAppService;
			this.sessionHub = sessionHub;
		}

		[HttpPost("CreateSession/user/{userId}")]
		public IActionResult CreateSession(Guid userId)
		{
			SessionModel session = sessionAppService.CreateSession(userId);

			return Created("", session);
		}

		[HttpPut("JoinTheSession/{sessionId}/user/{userId}/")]
		public IActionResult JoinTheSession(Guid sessionId, Guid userId)
		{
			SessionModel session = sessionAppService.JoinTheSession(sessionId, userId);

			return Ok(session);
		}

		[HttpPut("EnableAnswersOfTheCurrentQuestion/{sessionId}/user/{userId}/")]
		public IActionResult EnableAnswersOfTheCurrentQuestion(Guid sessionId, Guid userId)
		{
			sessionAppService.EnableAnswersOfTheCurrentQuestion(sessionId, userId);

			sessionHub.NotifySession(sessionId);

			return Ok();
		}

		[HttpGet("{sessionId}/user/{userId}")]
		public IActionResult Get(Guid sessionId, Guid userId)
		{
			SessionModel session = sessionAppService.GetSession(sessionId, userId);

			return Ok(session);
		}
	}
}