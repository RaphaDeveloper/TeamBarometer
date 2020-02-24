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
		#region Create Session

		[Test]
		public void CreateSession()
		{
			SessionService service = CreateService();

			Session session = service.CreateSession();

			Assert.That(session, Is.Not.Null);
		}

		[Test]
		public void CreateSessionWithQuestions()
		{
			SessionService service = CreateService();

			Session session = service.CreateSession();

			Assert.That(session.QuestionsById, Is.Not.Null.And.Not.Empty);
		}

		[Test]
		public void PersistTheCreatedSession()
		{
			InMemorySessionRepository sessionRepository = new InMemorySessionRepository();
			SessionService service = CreateService(sessionRepository);

			Session session = service.CreateSession();

			Assert.That(session, Is.EqualTo(sessionRepository.GetById(session.Id)));
		}

		[Test]
		public void CreateTheSessionWithTheFirstQuestionBeingTheCurrent()
		{
			SessionService service = CreateService();


			Session session = service.CreateSession();


			Assert.That(session.QuestionsById.Values.First(), Is.EqualTo(session.CurrentQuestion));
		}

		#endregion


		#region Answer The Current Question

		[Test]
		public void AnswerTheSessionQuestion()
		{
			SessionService service = CreateService();
			Session session = service.CreateSession();
			Question firstQuestion = session.QuestionsById.Values.ElementAt(0);
			Question secondQuestion = session.QuestionsById.Values.ElementAt(1);
			Guid teamMemberId = Guid.NewGuid();
			service.AddTeamMemberInTheSession(teamMemberId, session.Id);


			service.EnableAnswersOfTheSessionCurrentQuestion(session.Id);
			service.AnswerTheSessionCurrentQuestion(teamMemberId, Answer.Red, session.Id);

			service.EnableAnswersOfTheSessionCurrentQuestion(session.Id);
			service.AnswerTheSessionCurrentQuestion(teamMemberId, Answer.Green, session.Id);


			Assert.That(firstQuestion.GetCountOfTheAnswer(Answer.Red), Is.EqualTo(1));
			Assert.That(secondQuestion.GetCountOfTheAnswer(Answer.Green), Is.EqualTo(1));
		}

		[Test]
		public void ChangeTheCurrentQuestionWhenAllTeamMemberAnswerTheQuestion()
		{
			SessionService service = CreateService();
			Session session = service.CreateSession();
			Question secondQuestion = session.QuestionsById.Values.ElementAt(1);
			Guid firstTeamMemberId = Guid.NewGuid();
			Guid secondTeamMemberId = Guid.NewGuid();
			service.AddTeamMemberInTheSession(firstTeamMemberId, session.Id);
			service.AddTeamMemberInTheSession(secondTeamMemberId, session.Id);


			service.EnableAnswersOfTheSessionCurrentQuestion(session.Id);
			service.AnswerTheSessionCurrentQuestion(firstTeamMemberId, Answer.Green, session.Id);
			service.AnswerTheSessionCurrentQuestion(secondTeamMemberId, Answer.Green, session.Id);


			Assert.That(secondQuestion, Is.EqualTo(session.CurrentQuestion));
		}

		[Test]
		public void DisableAnswersOfTheQuestionWhenAllTeamMemberAnswerIt()
		{
			SessionService service = CreateService();
			Session session = service.CreateSession();
			Question firstQuestion = session.QuestionsById.Values.ElementAt(0);
			Guid firstTeamMemberId = Guid.NewGuid();
			Guid secondTeamMemberId = Guid.NewGuid();
			service.AddTeamMemberInTheSession(firstTeamMemberId, session.Id);
			service.AddTeamMemberInTheSession(secondTeamMemberId, session.Id);


			service.EnableAnswersOfTheSessionCurrentQuestion(session.Id);
			service.AnswerTheSessionCurrentQuestion(firstTeamMemberId, Answer.Green, session.Id);
			service.AnswerTheSessionCurrentQuestion(secondTeamMemberId, Answer.Green, session.Id);


			Assert.False(firstQuestion.IsUpForAnswer);
		}

		[Test]
		public void NotAnswerTheSessionQuestionMoreThanOnceForTheSameTeamMember()
		{
			SessionService service = CreateService();
			Session session = service.CreateSession();
			Question firstQuestion = session.QuestionsById.Values.First();
			Guid firstTeamMemberId = Guid.NewGuid();
			Guid secondTeamMemberId = Guid.NewGuid();
			service.AddTeamMemberInTheSession(firstTeamMemberId, session.Id);
			service.AddTeamMemberInTheSession(secondTeamMemberId, session.Id);


			service.EnableAnswersOfTheSessionCurrentQuestion(session.Id);
			service.AnswerTheSessionCurrentQuestion(firstTeamMemberId, Answer.Green, session.Id);
			service.AnswerTheSessionCurrentQuestion(secondTeamMemberId, Answer.Green, session.Id);
			service.AnswerTheSessionCurrentQuestion(secondTeamMemberId, Answer.Green, session.Id);


			Assert.That(firstQuestion.GetCountOfTheAnswer(Answer.Green), Is.EqualTo(2));
		}

		[Test]
		public void NotAnswerTheSessionQuestionWhenTheTeamMemberIsNotInTheSession()
		{
			SessionService service = CreateService();
			Session session = service.CreateSession();
			Question firstQuestion = session.QuestionsById.Values.First();
			Guid teamMemberId = Guid.NewGuid();


			service.EnableAnswersOfTheSessionCurrentQuestion(session.Id);
			service.AnswerTheSessionCurrentQuestion(teamMemberId, Answer.Red, session.Id);


			Assert.False(firstQuestion.HasAnyAnswer());
		}

		[Test]
		public void NotAnswerTheSessionQuestionWhenTheQuestionsIsNotUpForAnswer()
		{
			SessionService service = CreateService();
			Session session = service.CreateSession();
			Question firstQuestion = session.QuestionsById.Values.ElementAt(0);
			Guid firstTeamMemberId = Guid.NewGuid();
			service.AddTeamMemberInTheSession(firstTeamMemberId, session.Id);


			service.AnswerTheSessionCurrentQuestion(firstTeamMemberId, Answer.Green, session.Id);


			Assert.False(firstQuestion.HasAnyAnswer());
		}

		#endregion


		#region EnterInTheSession

		[Test]
		public void EnterInTheSession()
		{
			SessionService sessionService = CreateService();
			Session session = sessionService.CreateSession();
			Guid teamMemberId = Guid.NewGuid();

			sessionService.AddTeamMemberInTheSession(teamMemberId, session.Id);

			Assert.True(session.TeamMemberIsParticipating(teamMemberId));
		}

		[Test]
		public void DoesNotEnterInTheSessionIfTheTeamMemberIsAlreadyInTheSession()
		{
			SessionService sessionService = CreateService();
			Session session = sessionService.CreateSession();
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
			Session session = service.CreateSession();


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
