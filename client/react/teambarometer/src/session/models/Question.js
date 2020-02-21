export default class Question {
    constructor(description, redAnswer, greenAnswer, isCurrent, amountOfAnswerRed, amountOfAnswerYellow, amountOfAnswerGreen) {
        this.description = description;
        this.isCurrent = isCurrent;
        this.amountOfAnswerRed = amountOfAnswerRed;
        this.amountOfAnswerYellow = amountOfAnswerYellow;
        this.amountOfAnswerGreen = amountOfAnswerGreen;
        this.redAnswer = redAnswer;
        this.greenAnswer = greenAnswer;
    }

    isEqualTo(question) {
        return this.description === question.description;
    }
}