export default class InMemorySessionRepository {
    getSession() {
        const questions = [
            new Question('Confiança', true),
            new Question('Feedback'),
            new Question('Autonomia'),
        ];

        return new Session(questions);
    }
}

class Session {
    constructor(questions) {
        this.questions = questions;
    }
}

class Question {
    constructor(description, isCurrent) {
        this.description = description;
        this.isCurrent = isCurrent;
    }
}