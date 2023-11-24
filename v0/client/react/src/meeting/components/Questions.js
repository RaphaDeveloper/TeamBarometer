import './styles/Questions.css';
import React, { Component } from 'react';
import Question from './Question';

export default class Questions extends Component {
    constructor(props) {
        super(props);
        this.questions = React.createRef();
    }

    scrollToQuestion = (questionElement) => {
        let questionsElementTop = this.questions.current.scrollTop;
        let questionsElementBottom = questionsElementTop + this.questions.current.offsetHeight;

        var questionElementTop = questionElement.offsetTop;
        var questionElementBottom = questionElementTop + questionElement.offsetHeight;

        if (questionElementTop < questionsElementTop || questionElementBottom > questionsElementBottom)
            this.questions.current.scrollTo(0, questionElementTop);
    }

    render() {
        return (
            <div ref={this.questions} className="questions col-sm-6">
                <ul>
                    {this.renderQuestions()}
                </ul>
            </div>
        );
    }

    renderQuestions() {
        return (
            this.props.meeting.questions.map(question =>                
                <Question key={question.description} meeting={this.props.meeting} question={question} selectedQuestion={this.props.selectedQuestion} onSelectQuestion={this.props.onSelectQuestion} onPlayQuestion={this.props.onPlayQuestion} onUpdateCurrentQuestion={this.scrollToQuestion} />
            )
        );
    }
}