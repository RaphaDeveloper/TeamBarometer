export default class SessionModel {
    constructor(questions) {
        this.questions = questions || [];
    }

    getCurrentQuestion() {
        return this.questions.find(q => q.isCurrent);
    }
}