export default class Question {
    constructor(description, redAnswer, greenAnswer, isTheCurrent, amountOfRedAnswers, amountOfYellowAnswers, amountOfGreenAnswers) {
        this.description = description;
        this.redAnswer = redAnswer;
        this.greenAnswer = greenAnswer;
        this.isTheCurrent = isTheCurrent;
        this.amountOfRedAnswers = amountOfRedAnswers;
        this.amountOfYellowAnswers = amountOfYellowAnswers;
        this.amountOfGreenAnswers = amountOfGreenAnswers;
    }

    isEqualTo(question) {
        return this.description === question.description;
    }
}