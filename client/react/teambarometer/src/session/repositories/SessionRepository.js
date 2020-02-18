export default class SessionRepository {
    getSession() {
        const questions = [
            new Question('Confiança', true),
            new Question('Feedback'),
        ];

        return new SessionModel([questions]);
    }
}