import React, { Component } from 'react';
import Session from '../../session/components/Session';
import './Home.css';
import SessionRepository from '../../session/repositories/SessionRepository';


export default class Home extends Component {
    constructor(props) {
        super(props);
        this.sessionRepository = new SessionRepository();
        this.state = {};
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
            <Session session={this.state.session}/>
        );
    }

    renderHomeContent() {
        return (
            <div className="main col-sm">
                <a id="createSession" onClick={() => this.createSession()} href="javascript:void(0)">Crie</a> uma sessão ou se <a id="subscribeToSession" href="">Inscreva</a> em uma existente.
            </div>
        );
    }

    createSession() {
        const session = this.sessionRepository.createSession();

        this.setState({ session: session });
    }
}