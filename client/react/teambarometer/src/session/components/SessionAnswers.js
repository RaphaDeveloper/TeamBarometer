import React, { Component } from "react";

import './SessionAnswers.css';

export default class SessionAnswers extends Component {
    constructor(props) {
        super(props);
        this.state = {
            selectedAnswer: null
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
        if (this.state.selectedAnswer === answer) {
            classes += " selected-answer";
        }

        return classes;
    }

    disableAnswer = () => {
        return !this.props.question.isUpForAnswer || this.props.userIsTheFacilitator;
    }

    onSelectAnswer = (selectedAnswer) => {
        this.props.onSelectAnswer(selectedAnswer);
        this.setState({ selectedAnswer });
    }
}