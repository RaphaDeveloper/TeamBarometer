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
                        <div className="cont-red">{question.amountOfAnswerRed}</div>
                        <div className="cont-yellow">{question.amountOfAnswerYellow}</div>
                        <div className="cont-green">{question.amountOfAnswerGreen}</div>
                        {question.isCurrent && this.props.userIsTheFacilitator() &&
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