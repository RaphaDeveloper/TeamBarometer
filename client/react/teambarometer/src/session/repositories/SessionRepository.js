import Question from '../models/Question';
import SessionModel from '../models/SessionModel';

export default class SessionRepository {
    getSession() {
        const questions = [
            new Question('Confiança', false, 2, 2, 4),
            new Question('Feedback', true),
            new Question('Autonomia', false),
          ];
  
          return new SessionModel(questions);
    }
}