export default class SessionModel {
    constructor(id, questions, memberIsTheFacilitator) {        
        this.id = id;
        this.questions = questions || [];
        this.memberIsTheFacilitator = memberIsTheFacilitator;
    }

    getCurrentQuestion() {
        return this.questions.find(q => q.isCurrent);
    }
}