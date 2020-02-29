﻿using Domain.Sessions;
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
            IsTheCurrent = question.IsTheCurrent;
        }

        public Guid Id { get; set; }
        public string Description { get; private set; }
        public string RedAnswer { get; private set; }
        public string GreenAnswer { get; private set; }
        public bool IsTheCurrent { get; private set; }
        public int AmountOfRedAnswers { get; private set; }
        public int AmountOfYellowAnswers { get; private set; }
        public int AmountOfGreenAnswers { get; private set; }

        public override bool Equals(object obj)
        {
            QuestionModel question = obj as QuestionModel;

            return this.Id == question?.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
