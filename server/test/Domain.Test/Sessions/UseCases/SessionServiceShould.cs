using Domain.Questions;
using Domain.Sessions;
using Domain.Sessions.Repositories;
using Domain.Sessions.UseCases;
using NUnit.Framework;
using System;
using System.Linq;

namespace Domain.Test.Sessions.UseCases
{
	public class SessionServiceShould
	{
		private Guid facilitatorId = Guid.NewGuid();


		#region Create Session

		[Test]
		public void CreateSessionWithFacilitatorAndQuestions()
		{
			SessionService service = CreateService();

			Session session = service.CreateSession(facilitatorId);

			Assert.That(session, Is.Not.Null);
			Assert.That(session.FacilitatorId, Is.EqualTo(facilitatorId));
			Assert.That(session.Questions, Is.Not.Null.And.Not.Empty);
		}

		[Test]
		public void PersistTheCreatedSession()
		{
			InMemorySessionRepository sessionRepository = new InMemorySessionRepository();
			SessionService service = CreateService(sessionRepository);

			Session session = service.CreateSession(facilitatorId);

			Assert.That(session, Is.EqualTo(sessionRepository.GetById(session.Id)));
		}

		[Test]
		public void CreateTheSessionWithTheFirstQuestionBeingTheCurrent()
		{
			SessionService service = CreateService();

			Session session = service.CreateSession(facilitatorId);

			Assert.That(session.Questions.First(), Is.EqualTo(session.CurrentQuestion));
			Assert.True(session.CurrentQuestion.IsTheCurrent);
		}

		#endregion


		#region Answer The Current Question

		[Test]
		public void AnswerTheSessionQuestion()
		{
			Guid teamMemberId = Guid.NewGuid();
			SessionService service = CreateService();
			Session session = service.CreateSession(facilitatorId);
			Question currentQuestion = session.CurrentQuestion;
			service.AddTeamMemberInTheSession(teamMemberId, session.Id);
			service.EnableAnswersOfTheSessionCurrentQuestion(session.Id);


			service.AnswerTheSessionCurrentQuestion(teamMemberId, Answer.Red, session.Id);


			Assert.That(currentQuestion.GetCountOfTheAnswer(Answer.Red), Is.EqualTo(1));
		}

		[Test]
		public void ChangeTheCurrentQuestionWhenAllTeamMemberAnswerTheQuestion()
		{
			Guid firstTeamMemberId = Guid.NewGuid();
			Guid secondTeamMemberId = Guid.NewGuid();
			SessionService service = CreateService();
			Session session = service.CreateSession(facilitatorId);
			Question priorCurrentQuestion = session.CurrentQuestion;
			service.AddTeamMemberInTheSession(firstTeamMemberId, session.Id);
			service.AddTeamMemberInTheSession(secondTeamMemberId, session.Id);
			service.EnableAnswersOfTheSessionCurrentQuestion(session.Id);


			service.AnswerTheSessionCurrentQuestion(firstTeamMemberId, Answer.Green, session.Id);
			service.AnswerTheSessionCurrentQuestion(secondTeamMemberId, Answer.Green, session.Id);


			Assert.That(priorCurrentQuestion.NextQuestion, Is.EqualTo(session.CurrentQuestion));
			Assert.True(session.CurrentQuestion.IsTheCurrent);
			Assert.False(priorCurrentQuestion.IsTheCurrent);
		}

		[Test]
		public void DisableAnswersOfTheQuestionWhenAllTeamMemberAnswerIt()
		{
			Guid firstTeamMemberId = Guid.NewGuid();
			Guid secondTeamMemberId = Guid.NewGuid();
			SessionService service = CreateService();
			Session session = service.CreateSession(facilitatorId);
			Question currentQuestion = session.CurrentQuestion;
			service.AddTeamMemberInTheSession(firstTeamMemberId, session.Id);
			service.AddTeamMemberInTheSession(secondTeamMemberId, session.Id);
			service.EnableAnswersOfTheSessionCurrentQuestion(session.Id);


			service.AnswerTheSessionCurrentQuestion(firstTeamMemberId, Answer.Green, session.Id);
			service.AnswerTheSessionCurrentQuestion(secondTeamMemberId, Answer.Green, session.Id);


			Assert.False(currentQuestion.IsUpForAnswer);
		}

		[Test]
		public void NotAnswerTheSessionQuestionMoreThanOnceForTheSameTeamMember()
		{
			Guid teamMemberId = Guid.NewGuid();
			SessionService service = CreateService();
			Session session = service.CreateSession(facilitatorId);
			Question currentQuestion = session.CurrentQuestion;
			service.AddTeamMemberInTheSession(teamMemberId, session.Id);
			service.EnableAnswersOfTheSessionCurrentQuestion(session.Id);


			service.AnswerTheSessionCurrentQuestion(teamMemberId, Answer.Green, session.Id);
			service.AnswerTheSessionCurrentQuestion(teamMemberId, Answer.Green, session.Id);


			Assert.That(currentQuestion.GetCountOfTheAnswer(Answer.Green), Is.EqualTo(1));
		}

		[Test]
		public void NotAnswerTheSessionQuestionWhenTheTeamMemberIsNotInTheSession()
		{
			Guid teamMemberId = Guid.NewGuid();
			SessionService service = CreateService();
			Session session = service.CreateSession(facilitatorId);
			Question currentQuestion = session.CurrentQuestion;
			service.EnableAnswersOfTheSessionCurrentQuestion(session.Id);


			service.AnswerTheSessionCurrentQuestion(teamMemberId, Answer.Red, session.Id);


			Assert.False(currentQuestion.HasAnyAnswer());
		}

		[Test]
		public void NotAnswerTheSessionQuestionWhenTheQuestionsIsNotUpForAnswer()
		{
			Guid firstTeamMemberId = Guid.NewGuid();
			SessionService service = CreateService();
			Session session = service.CreateSession(facilitatorId);
			Question currentQuestion = session.CurrentQuestion;
			service.AddTeamMemberInTheSession(firstTeamMemberId, session.Id);


			service.AnswerTheSessionCurrentQuestion(firstTeamMemberId, Answer.Green, session.Id);


			Assert.False(currentQuestion.HasAnyAnswer());
		}

		#endregion


		#region EnterInTheSession

		[Test]
		public void EnterInTheSession()
		{
			SessionService sessionService = CreateService();
			Session session = sessionService.CreateSession(facilitatorId);
			Guid teamMemberId = Guid.NewGuid();

			sessionService.AddTeamMemberInTheSession(teamMemberId, session.Id);

			Assert.True(session.TeamMemberIsParticipating(teamMemberId));
		}

		[Test]
		public void DoesNotEnterInTheSessionIfTheTeamMemberIsAlreadyInTheSession()
		{
			SessionService sessionService = CreateService();
			Session session = sessionService.CreateSession(facilitatorId);
			Guid teamMemberId = Guid.NewGuid();

			sessionService.AddTeamMemberInTheSession(teamMemberId, session.Id);

			Assert.That(() => sessionService.AddTeamMemberInTheSession(teamMemberId, session.Id),
				Throws.Exception.With.Message.EqualTo("Team member is already participating of this session."));
		}

		#endregion


		#region EnableAnswers

		[Test]
		public void EnableAnswersOfTheCurrentQuestionOfTheSession()
		{
			SessionService service = CreateService();
			Session session = service.CreateSession(facilitatorId);


			service.EnableAnswersOfTheSessionCurrentQuestion(session.Id);


			Assert.True(session.CurrentQuestion.IsUpForAnswer);
		}

		#endregion


		private SessionService CreateService(InMemorySessionRepository sessionRepository = null)
		{
			sessionRepository ??= new InMemorySessionRepository();

			InMemoryQuestionTemplateRepository questionRepository = new InMemoryQuestionTemplateRepository();

			return new SessionService(sessionRepository, questionRepository);
		}
	}
}
