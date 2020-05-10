import Question from "./Question";

export default class SessionModel {
    constructor({id, questions, userIsTheFacilitator}) {
        this.id = id;
        this.questions = (questions || []).map(question => new Question(question));
        this.userIsTheFacilitator = userIsTheFacilitator;
    }

    getCurrentQuestion() {
        let currentQuestion = this.questions.find(q => q.isTheCurrent);

        if (!currentQuestion) {
            currentQuestion = this.getLastQuestion();
        }

        return currentQuestion;
    }

    getLastQuestion() {
        return this.questions[this.questions.length - 1];
    }
}