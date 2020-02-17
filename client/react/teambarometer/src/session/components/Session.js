
import React, { Component } from 'react';

import './Session.css';
import SessionQuestions from './SessionQuestions';
import SessionAnswers from './SessionAnswers';

export default class Session extends Component {
    constructor(props) {
        super(props);
        this.sessionRepository = props.sessionRepository;
        this.state = {
            session: { questions: [] }
        };
    }

    componentDidMount() {
        this.setState({ session: this.sessionRepository.getSession() });
    }

    userIsTheFacilitator() {
        return true;
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
                    <SessionQuestions questions={this.state.session.questions} userIsTheFacilitator={this.userIsTheFacilitator} />
                    <SessionAnswers />
                </div>
            </main>
        );
    }
}