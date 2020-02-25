export default class SessionModel {
    constructor(id, questions, teamMemberIsTheFacilitator) {        
        this.id = id;
        this.questions = questions || [];
        this.teamMemberIsTheFacilitator = teamMemberIsTheFacilitator;
    }

    getCurrentQuestion() {
        return this.questions.find(q => q.isTheCurrent);
    }
}