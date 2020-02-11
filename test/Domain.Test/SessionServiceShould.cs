using NUnit.Framework;
using System;
using System.Linq;

namespace Domain.Test
{
	public class SessionServiceShould
	{
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
		public void AnswerTheSessionQuestion()
		{
			SessionService service = CreateService();
			Session session = service.CreateSession();
			QuestionOfTheSession firstQuestion = session.QuestionsById.Values.ElementAt(0);
			QuestionOfTheSession secondQuestion = session.QuestionsById.Values.ElementAt(1);
			Guid teamMemberId = Guid.NewGuid();
			service.AddTeamMemberToTheSession(teamMemberId, session.Id);


			service.AnswerTheSessionQuestion(teamMemberId, firstQuestion.Id, Answer.Red, session.Id);
			service.AnswerTheSessionQuestion(teamMemberId, secondQuestion.Id, Answer.Green, session.Id);
			
			
			Assert.That(firstQuestion.GetCountOfTheAnswer(Answer.Red), Is.EqualTo(1));
			Assert.That(secondQuestion.GetCountOfTheAnswer(Answer.Green), Is.EqualTo(1));
		}

		[Test]
		public void NotAnswerTheSessionQuestionMoreThanOnceForTheSameTeamMember()
		{
			SessionService service = CreateService();
			Session session = service.CreateSession();
			QuestionOfTheSession firstQuestion = session.QuestionsById.Values.First();
			Guid firstTeamMemberId = Guid.NewGuid();
			Guid secondTeamMemberId = Guid.NewGuid();
			service.AddTeamMemberToTheSession(firstTeamMemberId, session.Id);
			service.AddTeamMemberToTheSession(secondTeamMemberId, session.Id);


			service.AnswerTheSessionQuestion(firstTeamMemberId, firstQuestion.Id, Answer.Green, session.Id);
			service.AnswerTheSessionQuestion(secondTeamMemberId, firstQuestion.Id, Answer.Green, session.Id);
			service.AnswerTheSessionQuestion(secondTeamMemberId, firstQuestion.Id, Answer.Green, session.Id);


			Assert.That(firstQuestion.GetCountOfTheAnswer(Answer.Green), Is.EqualTo(2));
		}

		[Test]
		public void NotAnswerTheSessionQuestionWhenTheTeamMemberIsNotInTheSession()
		{
			SessionService service = CreateService();
			Session session = service.CreateSession();
			QuestionOfTheSession firstQuestion = session.QuestionsById.Values.First();
			Guid teamMemberId = Guid.NewGuid();


			service.AnswerTheSessionQuestion(teamMemberId, firstQuestion.Id, Answer.Red, session.Id);


			Assert.False(firstQuestion.HasAnyAnswer());
		}

		[Test]
		public void AddTeamMemberToTheSession()
		{
			SessionService sessionService = CreateService();
			Session session = sessionService.CreateSession();
			Guid teamMemberId = Guid.NewGuid();

			sessionService.AddTeamMemberToTheSession(teamMemberId, session.Id);

			Assert.True(session.TeamMemberIsParticipating(teamMemberId));
		}

		[Test]
		public void DoesNotAddTheSameTeamMemberToTheSession()
		{
			SessionService sessionService = CreateService();
			Session session = sessionService.CreateSession();
			Guid teamMemberId = Guid.NewGuid();

			sessionService.AddTeamMemberToTheSession(teamMemberId, session.Id);

			Assert.That(() => sessionService.AddTeamMemberToTheSession(teamMemberId, session.Id), 
				Throws.Exception.With.Message.EqualTo("Team member is already participating of this session."));
		}

		private SessionService CreateService(InMemorySessionRepository sessionRepository = null)
		{
			sessionRepository ??= new InMemorySessionRepository();

			InMemoryQuestionRepository questionRepository = new InMemoryQuestionRepository();

			return new SessionService(sessionRepository, questionRepository);
		}
	}

	public class SessionShould
	{
		[Test]
		public void HasId()
		{
			Session session = CreateSession();

			Assert.That(session.Id, Is.Not.Null);
		}

		[Test]
		public void HasUniqueId()
		{
			Session firstSession = CreateSession();
			Session secondSession = CreateSession();
			Session thirdSession = CreateSession();

			Assert.That(firstSession.Id, Is.Not.EqualTo(secondSession.Id));
			Assert.That(secondSession.Id, Is.Not.EqualTo(thirdSession.Id));
		}

		[Test]
		public void HasReadOnlyId()
		{
			Session session = CreateSession();

			bool idIsReadOnly = !session.GetType().GetProperty(nameof(session.Id)).CanWrite;

			Assert.True(idIsReadOnly, "Session id is not read only");
		}

		private Session CreateSession()
		{
			return new Session(Enumerable.Empty<Question>());
		}
	}

	public class QuestionOfTheSessionShould
	{
		[Test]
		public void BeCreatedFromQuestion()
		{
			Question question = new Question();

			QuestionOfTheSession questionOfTheSession = new QuestionOfTheSession(question);

			Assert.That(questionOfTheSession.Id, Is.EqualTo(question.Id));
		}
	}

}