namespace Domain.TeamBarometer.Entities
{
    public class AnswerWithAnnotation
    {
        public AnswerWithAnnotation(Answer answer, string annotation)
        {
            Answer = answer;
            Annotation = annotation;
        }

        public Answer Answer { get; }
        public string Annotation { get; }
    }
}
