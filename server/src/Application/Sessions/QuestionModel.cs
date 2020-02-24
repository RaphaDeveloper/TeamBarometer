using Domain.Sessions;
using System;

namespace Application.Sessions
{
    public class QuestionModel
    {
        public QuestionModel(Question question)
        {
            Description = question.Description;
            RedAnswer = question.GetDescriptionOfTheAnswer(Answer.Red);
            GreenAnswer = question.GetDescriptionOfTheAnswer(Answer.Green);
            IsTheCurrent = question.IsTheCurrent;
        }
        
        public string Description { get; private set; }
        public string RedAnswer { get; private set; }
        public string GreenAnswer { get; private set; }
        public bool IsTheCurrent { get; set; }
    }
}
