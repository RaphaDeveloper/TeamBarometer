using Application.Sessions;
using Application.Sessions.UseCases;
using Domain.Sessions;
using Domain.Sessions.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SessionsController : ControllerBase
	{
		private readonly SessionAppService sessionAppService;

		public SessionsController(SessionAppService sessionAppService)
		{
			this.sessionAppService = sessionAppService;
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
			try
			{
				SessionModel session = sessionAppService.JoinTheSession(sessionId, userId);

				return Ok(session);
			}
			catch (NonExistentSessionException)
			{
				return BadRequest();
			}
		}

		[HttpPut("EnableAnswersOfTheCurrentQuestion/{sessionId}/user/{userId}/")]
		public IActionResult EnableAnswersOfTheCurrentQuestion(Guid sessionId, Guid userId)
		{
			sessionAppService.EnableAnswersOfTheCurrentQuestion(sessionId, userId);

			return Ok();
		}

		[HttpPut("AnswerTheCurrentQuestion/{sessionId}/user/{userId}/answer/{answer}")]
		public IActionResult AnswerTheCurrentQuestion(Guid userId, Answer answer, Guid sessionId)
		{
			sessionAppService.AnswerTheCurrentQuestion(userId, answer, sessionId);

			return Ok();
		}

		[HttpGet("{sessionId}/user/{userId}")]
		public IActionResult Get(Guid sessionId, Guid userId)
		{
			try
			{
				SessionModel session = sessionAppService.GetSession(sessionId, userId);
				
				return Ok(session);
			}
			catch (NonExistentSessionException)
			{
				return BadRequest();
			}
		}
	}
}