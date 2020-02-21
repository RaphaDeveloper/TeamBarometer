import React, { Component } from 'react';

import './Session.css';
import SessionQuestions from './SessionQuestions';
import SessionAnswers from './SessionAnswers';
import SessionRepository from '../repositories/SessionRepository';
import SessionModel from '../models/SessionModel';
import Question from '../models/Question';

export default class Session extends Component {
    constructor(props) {
        super(props);
        this.sessionRepository = new SessionRepository();
        this.state = {
            session: new SessionModel(),
            selectedQuestion: new Question()
        };
    }

    componentDidMount() {
        const session = this.sessionRepository.getSession();

        this.setState({ session: session, selectedQuestion: session.getCurrentQuestion() });
    }

    render() {
        return (
            <main className="container">
                <div className="row">
                    <header className="col-sm">
                        <h1>Team Barometer</h1>
                    </header>
                </div>
                <div className="row">
                    <SessionQuestions questions={this.state.session.questions} selectedQuestion={this.state.selectedQuestion} onSelectQuestion={this.updateSelectedQuestion}/>
                    <SessionAnswers question={this.state.selectedQuestion}/>
                </div>
            </main>
        );
    }

    updateSelectedQuestion = (question) => {
        this.setState({ selectedQuestion: question });
    }
}