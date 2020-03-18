import React, { Component } from 'react';

import './Session.css';
import SessionQuestions from './SessionQuestions';
import SessionAnswers from './SessionAnswers';
import SessionRepository from '../repositories/SessionRepository';
import * as signalR from "@aspnet/signalr";

export default class Session extends Component {
    constructor(props) {
        super(props);
        this.sessionRepository = new SessionRepository();
        this.state = {
            session: this.props.session,
            selectedQuestion: this.props.session.getCurrentQuestion(),
            hubConnections: null
        };
    }

    componentDidMount() {
        const hubConnection = new signalR.HubConnectionBuilder().withUrl(`${process.env.REACT_APP_API_URL}/sessionHub/${this.props.session.id}`).build();

        hubConnection.on("RefreshSession", this.refreshSession);

        this.setState({ hubConnection }, () => {
            this.state.hubConnection
                .start({ withCredentials: false })
                .then(() => console.log('Connection started!'))
                .catch(err => console.log('Error while establishing connection :('));
        });
    }

    render() {
        return (
            <>
                <SessionQuestions session={this.state.session} selectedQuestion={this.state.selectedQuestion} onSelectQuestion={this.updateSelectedQuestion} onPlayQuestion={this.enableAnswersOfTheCurrentQuestion} />
                <SessionAnswers question={this.state.selectedQuestion} userIsTheFacilitator={this.state.session.userIsTheFacilitator} />
            </>
        );
    }

    updateSelectedQuestion = (question) => {
        this.setState({ selectedQuestion: question });
    }

    enableAnswersOfTheCurrentQuestion = () => {
        this.sessionRepository.enableAnswersOfTheCurrentQuestion(this.state.session.id, this.props.userId);
    }

    refreshSession = async () => {
        const session =  await this.sessionRepository.getSession(this.state.session.id, this.props.userId);

        this.setState({ session });
    }
}