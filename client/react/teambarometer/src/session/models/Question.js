export default class Question {
    constructor(description, isCurrent, amountOfAnswerRed, amountOfAnswerYellow, amountOfAnswerGreen) {
        this.description = description;
        this.isCurrent = isCurrent;
        this.amountOfAnswerRed = amountOfAnswerRed;
        this.amountOfAnswerYellow = amountOfAnswerYellow;
        this.amountOfAnswerGreen = amountOfAnswerGreen;
    }
}