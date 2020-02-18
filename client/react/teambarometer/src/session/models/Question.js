export default class Question {
    constructor(description, isCurrent, redCount, yellowCount, greenCount) {
        this.description = description;
        this.isCurrent = isCurrent;
        this.redCount = redCount;
        this.yellowCount = yellowCount;
        this.greenCount = greenCount;
    }
}