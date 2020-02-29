﻿using Application.Sessions;
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

		public SessionsController(SessionAppService sessionAppService)
		{
			this.sessionAppService = sessionAppService;
		}

		[HttpPost("user/{userId}")]
		public IActionResult Post(Guid userId)
		{
			SessionModel session = sessionAppService.CreateSession(userId);

			return Created("", session);
		}
	}
}