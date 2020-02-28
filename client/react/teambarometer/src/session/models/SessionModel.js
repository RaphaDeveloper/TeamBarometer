import Question from "./Question";

export default class SessionModel {
    constructor({id, questions, teamMemberIsTheFacilitator}) {
        this.id = id;
        this.questions = (questions || []).map(question => new Question(question));
        this.teamMemberIsTheFacilitator = teamMemberIsTheFacilitator;
    }

    getCurrentQuestion() {
        return this.questions.find(q => q.isTheCurrent);
    }
}