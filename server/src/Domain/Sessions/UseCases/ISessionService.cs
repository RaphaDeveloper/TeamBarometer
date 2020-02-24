using System;

namespace Domain.Sessions.UseCases
{
	public interface ISessionService
	{
		void AddTeamMemberInTheSession(Guid teamMemberId, Guid sessionId);
		void AnswerTheSessionCurrentQuestion(Guid teamMemberId, Answer answer, Guid sessionId);
		Session CreateSession(Guid facilitatorId);
		void EnableAnswersOfTheSessionCurrentQuestion(Guid sessionId);
	}
}