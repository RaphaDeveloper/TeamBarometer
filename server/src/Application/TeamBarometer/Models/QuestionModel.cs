using Domain.TeamBarometer.Entities;
using System;
using System.Collections.Generic;

namespace Application.TeamBarometer.Models
{
    public class QuestionModel
    {
        public QuestionModel(Question question)
        {
            Id = question.Id;
            Description = question.Description;
            RedAnswer = question.GetAnswerDescription(Answer.Red);
            GreenAnswer = question.GetAnswerDescription(Answer.Green);
            IsTheCurrent = question.IsTheCurrent;
            IsUpForAnswer = question.IsUpForAnswer;
            AmountOfGreenAnswers = question.GetCountOfTheAnswer(Answer.Green);
            AmountOfYellowAnswers = question.GetCountOfTheAnswer(Answer.Yellow);
            AmountOfRedAnswers = question.GetCountOfTheAnswer(Answer.Red);
            AnswersWithAnnotation = question.GetAnswersWithAnnotation();
        }

        public Guid Id { get; }
        public string Description { get; }
        public string RedAnswer { get; }
        public string GreenAnswer { get; }
        public bool IsTheCurrent { get; }
        public int AmountOfRedAnswers { get; }
        public int AmountOfYellowAnswers { get; }
        public int AmountOfGreenAnswers { get; }
        public bool IsUpForAnswer { get; }
        public IEnumerable<AnswerWithAnnotation> AnswersWithAnnotation { get; }

        public override bool Equals(object obj)
        {
            QuestionModel question = obj as QuestionModel;

            return Id == question?.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
