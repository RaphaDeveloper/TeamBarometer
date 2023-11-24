import QuestionModel from "./QuestionModel";

export default class MeetingModel {
    constructor({id, questions, userIsTheFacilitator}) {
        this.id = id;
        this.questions = (questions || []).map(question => new QuestionModel(question));
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