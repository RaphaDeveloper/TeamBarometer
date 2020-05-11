export default class QuestionModel {
    constructor({id, description, redAnswer, greenAnswer, isTheCurrent, isUpForAnswer, amountOfRedAnswers, amountOfYellowAnswers, amountOfGreenAnswers}) {
        this.id = id;
        this.description = description;
        this.redAnswer = redAnswer;
        this.greenAnswer = greenAnswer;
        this.isTheCurrent = isTheCurrent;
        this.amountOfRedAnswers = amountOfRedAnswers;
        this.amountOfYellowAnswers = amountOfYellowAnswers;
        this.amountOfGreenAnswers = amountOfGreenAnswers;
        this.isUpForAnswer = isUpForAnswer;
    }

    isEqualTo(question) {
        return this.description === question.description;
    }
}