import React, { Component } from 'react';
import Session from '../../session/components/Session';
import './Home.css';
import SessionRepository from '../../session/repositories/SessionRepository';
import { v4 as uuidv4 } from 'uuid';
import SessionIdPopover from './SessionIdPopover';


export default class Home extends Component {
    constructor(props) {
        super(props);
        this.sessionRepository = new SessionRepository();
        this.state = { session: null, userId: null };
    }

    componentDidMount() {
        this.setState({ userId: uuidv4() });
    }

    render() {
        return (
            <main className="container">
                <div className="row">
                    <header className="col-sm">
                        {this.state.session && <span id="sessionId">{this.state.session.id}</span>}
                        <h1>Team Barometer</h1>
                    </header>
                </div>
                <div className="row">
                    {this.renderMainContent()}
                </div>
            </main>
        );
    }

    renderMainContent() {
        return this.state.session ? this.renderSession() : this.renderHomeContent();
    }

    renderSession() {
        return (
            <Session session={this.state.session} userId={this.state.userId} />
        );
    }

    renderHomeContent() {
        return (
            <div className="main col-sm">
                <a id="createSession" onClick={this.createSession} href="javascript:void(0)">Crie</a> uma sessão ou {this.renderEnterSessionLink()} em uma existente.
            </div>
        );
    }

    createSession = async () => {
        const session = await this.sessionRepository.createSession(this.state.userId);

        this.setState({ session: session });
    }

    renderEnterSessionLink() {
        return (
            <SessionIdPopover onEnterToTheSession={this.enterToTheSession}></SessionIdPopover>
        );
    }

    enterToTheSession = async (sessionId) => {
        const session = await this.sessionRepository.enterToTheSession(sessionId, this.state.userId);

        this.setState({ session: session });
    }
}