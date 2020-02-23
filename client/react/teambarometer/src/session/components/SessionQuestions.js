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
                        <div className="count-red">{question.amountOfAnswerRed}</div>
                        <div className="count-yellow">{question.amountOfAnswerYellow}</div>
                        <div className="count-green">{question.amountOfAnswerGreen}</div>
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

        if (question.isCurrent) {
            className = 'current-question';
        }

        return className;
    }

    renderPlayButton(question) {
        return (
            question.isCurrent &&
            this.props.session.memberIsTheFacilitator &&
            <input className="play" type="image" src={playImage} alt="Play" />
        );
    }
}