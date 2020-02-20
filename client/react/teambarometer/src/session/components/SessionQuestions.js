import React, { Component } from 'react';

import './SessionQuestions.css';
import playImage from '../assets/play-button.png';

export default class SessionQuestions extends Component {
    renderQuestions() {
        return (
            this.props.questions.map(question =>
                <li key={question.description} className={question.isCurrent ? 'current-question' : ''}>
                    <div className="question d-flex">
                        <div className="mr-auto">{question.description}</div>
                        <div className="count-red">{question.amountOfAnswerRed}</div>
                        <div className="count-yellow">{question.amountOfAnswerYellow}</div>
                        <div className="count-green">{question.amountOfAnswerGreen}</div>
                        {question.isCurrent &&
                            <div className="play">
                                <img src={playImage} alt=""></img>
                            </div>
                        }
                    </div>
                </li>
            )
        );
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
}