import React, { Component } from 'react';

export default class Question extends Component {
    constructor(props) {
        super(props);
        this.question = React.createRef();
    }

    componentDidUpdate() {
        if (this.props.question.isTheCurrent && !this.alreadyUpdated) {
            this.props.onUpdateCurrentQuestion(this.question.current);
            this.alreadyUpdated = true;
        }
    }

    render() {
        return (
            <li ref={this.question} onClick={() => this.props.onSelectQuestion(this.props.question)} className={this.getQuestionClassName(this.props.question)}>
                <div className="question d-flex">
                    <div className="question-description">{this.props.question.description}</div>
                    <div className="play mr-auto">
                        {this.renderPlayButton(this.props.question)}
                        {this.renderLoader(this.props.question)}
                    </div>
                    <div className="count-red">{this.props.question.amountOfRedAnswers}</div>
                    <div className="count-yellow">{this.props.question.amountOfYellowAnswers}</div>
                    <div className="count-green">{this.props.question.amountOfGreenAnswers}</div>
                </div>
            </li>
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
            !question.isUpForAnswer &&
            question.isTheCurrent &&
            this.props.meeting.userIsTheFacilitator &&
            <a className="button button-play" onClick={this.props.onPlayQuestion} href="javascript:void(0)"></a>
        );
    }

    renderLoader(question) {
        return (
            question.isUpForAnswer && <div className="loader"></div>
        );
    }
}