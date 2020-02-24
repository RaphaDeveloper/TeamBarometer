using Domain.Sessions;
using System;

namespace Application.Sessions
{
    public class QuestionModel
    {
        public QuestionModel(Question question)
        {
            Id = question.Id;
            Description = question.Description;
            RedAnswer = question.GetDescriptionOfTheAnswer(Answer.Red);
            GreenAnswer = question.GetDescriptionOfTheAnswer(Answer.Green);
        }

        public Guid Id { get; private set; }
        public string Description { get; private set; }
        public string RedAnswer { get; private set; }
        public string GreenAnswer { get; private set; }
    }
}
