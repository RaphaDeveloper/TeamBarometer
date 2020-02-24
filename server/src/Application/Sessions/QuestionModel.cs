using Domain.Sessions;
using System;

namespace Application.Sessions
{
    public class QuestionModel
    {
        public QuestionModel(Question question)
        {
            Id = question.Id;
        }

        public Guid Id { get; private set; }
    }
}
