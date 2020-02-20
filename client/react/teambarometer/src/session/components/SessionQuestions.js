import React, { Component } from 'react';

import './SessionQuestions.css';
import playImage from '../assets/play-button.png';

export default class SessionQuestions extends Component {
    constructor(props) {
        super(props);
        this.state = {
            selectedQuestion: null
        }
    }

    renderQuestions() {
        return (
            this.props.questions.map(question =>
                <li onClick={() => { this.setState({ selectedQuestion: question }); }} key={question.description} className={this.getClassNameOfTheQuestion(question)}>
                    <div className="question d-flex">
                        <div className="mr-auto">{question.description}</div>
                        {question.isCurrent &&
                            <div className="play">
                                <img src={playImage} alt=""></img>
                            </div>
                        }
                        <div className="count-red">{question.amountOfAnswerRed}</div>
                        <div className="count-yellow">{question.amountOfAnswerYellow}</div>
                        <div className="count-green">{question.amountOfAnswerGreen}</div>
                    </div>
                </li>
            )
        );
    }

    getClassNameOfTheQuestion(question) {
        let className = question.isCurrent ? 'current-question' : '';

        if (this.state.selectedQuestion && this.state.selectedQuestion.description === question.description) {
            className += ' selected';
        }

        return className;
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