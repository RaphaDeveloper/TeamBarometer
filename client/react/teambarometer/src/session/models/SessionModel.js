import Question from "./Question";

export default class SessionModel {
    constructor({id, questions, userIsTheFacilitator}) {
        this.id = id;
        this.questions = (questions || []).map(question => new Question(question));
        this.userIsTheFacilitator = userIsTheFacilitator;
    }

    getCurrentQuestion() {
        return this.questions.find(q => q.isTheCurrent);
    }
}