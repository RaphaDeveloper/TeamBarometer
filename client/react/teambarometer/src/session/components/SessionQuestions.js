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

    selectQuestion(question) {
        this.setState({ selectedQuestion: question });
    }

    getClassNameOfTheQuestion(question) {
        let className = '';

        if (this.questionIsSelected(question)) {
            className = 'selected';
        }

        if (question.isCurrent) {
            className = 'current-question';
        }

        return className;
    }

    questionIsSelected(question) {
        return this.state.selectedQuestion && this.state.selectedQuestion.description === question.description
    }
}