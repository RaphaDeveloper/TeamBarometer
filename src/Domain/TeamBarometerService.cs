using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Domain
{
	public class TeamBarometerService
	{
		public InMemoryQuestionRepository QuestionRepository { get; }

		public TeamBarometerService(InMemoryQuestionRepository questionRepository)
		{
			QuestionRepository = questionRepository;
		}

		public string GenerateSessionID()
		{
			return Guid.NewGuid().ToString();
		}

		public Session CreateSession()
		{
			IEnumerable<Question> questions = QuestionRepository.GetAll();

			return new Session(questions);
		}
	}

	public class Session
	{
		public Session(IEnumerable<Question> questions)
		{
			Id = Guid.NewGuid().ToString();
			Questions = questions;
		}

		public string Id { get; }
		public IEnumerable<Question> Questions { get; set; }
	}

	public class Question
	{

	}

	public class InMemoryQuestionRepository
	{
		public IEnumerable<Question> GetAll()
		{
			Question confidence = new Question();

			yield return confidence;
		}
	}
}
