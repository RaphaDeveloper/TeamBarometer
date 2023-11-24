using Application.TeamBarometer.Models;
using Application.TeamBarometer.UseCases;
using Domain.TeamBarometer.Entities;
using Domain.TeamBarometer.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MeetingsController : ControllerBase
	{
		private readonly MeetingAppService meetingAppService;

		public MeetingsController(MeetingAppService meetingAppService)
		{
			this.meetingAppService = meetingAppService;
		}

		[HttpPost("CreateMeeting/user/{userId}")]
		public IActionResult CreateMeeting(Guid userId)
		{
			MeetingModel meeting = meetingAppService.CreateMeeting(userId);

			return Created("", meeting);
		}

		[HttpPut("JoinTheMeeting/{meetingId}/user/{userId}/")]
		public IActionResult JoinTheMeeting(Guid meetingId, Guid userId)
		{
			try
			{
				MeetingModel meeting = meetingAppService.JoinTheMeeting(meetingId, userId);

				return Ok(meeting);
			}
			catch (NonExistentMeetingException)
			{
				return BadRequest();
			}
		}

		[HttpPut("EnableAnswersOfTheCurrentQuestion/{meetingId}/user/{userId}/")]
		public IActionResult EnableAnswersOfTheCurrentQuestion(Guid meetingId, Guid userId)
		{
			meetingAppService.EnableAnswersOfTheCurrentQuestion(meetingId, userId);

			return Ok();
		}

		[HttpPut("AnswerTheCurrentQuestion/{meetingId}/user/{userId}/answer/{answer}/annotation/{annotation}")]
		public IActionResult AnswerTheCurrentQuestion(Guid userId, Answer answer, Guid meetingId, string annotation)
		{
			meetingAppService.AnswerTheCurrentQuestion(meetingId, userId, answer, annotation);

			return Ok();
		}

		[HttpGet("{meetingId}/user/{userId}")]
		public IActionResult Get(Guid meetingId, Guid userId)
		{
			try
			{
				MeetingModel meeting = meetingAppService.GetMeeting(meetingId, userId);
				
				return Ok(meeting);
			}
			catch (NonExistentMeetingException)
			{
				return BadRequest();
			}
		}
	}
}