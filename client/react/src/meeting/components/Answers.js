import './styles/Answers.css';
import React, { Component } from "react";

export default class Answers extends Component {
    constructor(props) {
        super(props);
        this.state = {
            answerByQuestion: null
        };
    }

    render() {        
        return (
            <div className="answers col-sm">
                <button onClick={() => this.onSelectAnswer('Red')} className={this.getAnswerClasses("btn-block red", "Red")} disabled={this.disableAnswer()}>
                    {this.props.question && this.props.question.redAnswer}
                </button>
                <button onClick={() => this.onSelectAnswer('Yellow')} className={this.getAnswerClasses("btn-block yellow", "Yellow")} disabled={this.disableAnswer()}></button>
                <button onClick={() => this.onSelectAnswer('Green')} className={this.getAnswerClasses("btn-block green", "Green")} disabled={this.disableAnswer()}>
                    {this.props.question && this.props.question.greenAnswer}
                </button>
            </div>
        );
    }

    getAnswerClasses(classes, answer) {
        if (this.state.answerByQuestion) {
            if (this.state.answerByQuestion[this.props.question.id.toString()] === answer) {
                classes += " selected-answer";
            }
        }

        return classes;
    }

    disableAnswer = () => {
        return !this.props.question.isUpForAnswer || this.props.userIsTheFacilitator;
    }

    onSelectAnswer = (answer) => {   
        let answerByQuestion = this.state.answerByQuestion || {};
        
        answerByQuestion[this.props.question.id.toString()] = answer;

        this.setState({ answerByQuestion });
        
        this.props.onSelectAnswer(answer);
    }
}