export default class SessionModel {
    constructor(id, questions) {        
        this.id = id;
        this.questions = questions || [];
    }

    getCurrentQuestion() {
        return this.questions.find(q => q.isCurrent);
    }
}