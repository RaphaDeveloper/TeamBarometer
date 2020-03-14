import React, { Component } from 'react';

import './SessionQuestions.css';
import playImage from '../assets/play-button.png';

export default class SessionQuestions extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div className="questions col-sm-6">
                <ul>
                    {this.renderQuestions()}
                </ul>
            </div>
        );
    }

    renderQuestions() {
        return (
            this.props.session.questions.map(question =>
                <li onClick={() => this.props.onSelectQuestion(question)} key={question.description} className={this.getQuestionClassName(question)}>
                    <div className="question d-flex">
                        <div className="question-description mr-auto">{question.description}</div>
                        {this.renderPlayButton(question)}
                        <div className="count-red">{question.amountOfRedAnswers}</div>
                        <div className="count-yellow">{question.amountOfYellowAnswers}</div>
                        <div className="count-green">{question.amountOfGreenAnswers}</div>
                    </div>
                </li>
            )
        );
    }

    getQuestionClassName(question) {
        let className = '';

        if (this.props.selectedQuestion.isEqualTo(question)) {
            className = 'selected';
        }

        if (question.isTheCurrent) {
            className = 'current-question';
        }

        return className;
    }

    renderPlayButton(question) {
        return (
            question.isTheCurrent &&
            this.props.session.userIsTheFacilitator &&
            <input className="play" type="image" src={playImage} alt="Play" onClick={this.props.onPlayQuestion} />
        );
    }
}