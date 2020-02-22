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
            this.props.questions.map(question =>
                <li onClick={() => this.selectQuestion(question) } key={question.description} className={this.getClassNameOfTheQuestion(question)}>
                    <div className="question d-flex">
                        <div className="question-description mr-auto">{question.description}</div>
                        {question.isCurrent &&
                            <input className="play" type="image" src={playImage} alt="Play"/>
                        }
                        <div className="count-red">{question.amountOfAnswerRed}</div>
                        <div className="count-yellow">{question.amountOfAnswerYellow}</div>
                        <div className="count-green">{question.amountOfAnswerGreen}</div>
                    </div>
                </li>
            )
        );
    }

    selectQuestion(question) {
        this.props.onSelectQuestion(question);
    }

    getClassNameOfTheQuestion(question) {
        let className = '';

        if (this.props.selectedQuestion.isEqualTo(question)) {
            className = 'selected';
        }

        if (question.isCurrent) {
            className = 'current-question';
        }

        return className;
    }
}