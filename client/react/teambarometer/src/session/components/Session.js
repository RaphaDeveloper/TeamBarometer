import React, { Component } from 'react';

import './Session.css';
import SessionQuestions from './SessionQuestions';
import SessionAnswers from './SessionAnswers';
import SessionRepository from '../repositories/SessionRepository';

export default class Session extends Component {
    constructor(props) {
        super(props);
        this.sessionRepository = new SessionRepository();        
        this.state = {
            selectedQuestion: this.props.session.getCurrentQuestion()
        };
    }

    render() {
        return (
            <>
                <SessionQuestions session={this.props.session} selectedQuestion={this.state.selectedQuestion} onSelectQuestion={this.updateSelectedQuestion}/>
                <SessionAnswers question={this.state.selectedQuestion}/>
            </>
        );
    }

    updateSelectedQuestion = (question) => {
        this.setState({ selectedQuestion: question });
    }
}