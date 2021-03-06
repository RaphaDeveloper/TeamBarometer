﻿using Domain.TeamBarometer.Entities;
using System;

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
        }

        public Guid Id { get; set; }
        public string Description { get; private set; }
        public string RedAnswer { get; private set; }
        public string GreenAnswer { get; private set; }
        public bool IsTheCurrent { get; private set; }
        public int AmountOfRedAnswers { get; private set; }
        public int AmountOfYellowAnswers { get; private set; }
        public int AmountOfGreenAnswers { get; private set; }
        public bool IsUpForAnswer { get; set; }

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
