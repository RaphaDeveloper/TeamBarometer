import React, { Component } from 'react';
import Session from '../../session/components/Session';
import './Home.css';
import SessionRepository from '../../session/repositories/SessionRepository';
import { Popover, Button, OverlayTrigger } from 'react-bootstrap';


export default class Home extends Component {
    constructor(props) {
        super(props);
        this.sessionRepository = new SessionRepository();
        this.sessionId = '';
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
            <Session session={this.state.session} />
        );
    }

    popover() {
        return (
            <Popover id="popover-enter-session">
                <Popover.Title as="h3">Entrar em uma sessão</Popover.Title>
                <Popover.Content>
                    <div className="input-group mb-3">
                        <input onChange={this.updateSessionId} type="text" className="form-control" placeholder="Código da Sessão" aria-label="Código da Sessão" id="button-session-id"/>
                        <div className="input-group-append">
                            <button onClick={this.enterToTheSession} className="btn btn-outline-secondary" type="button" id="button-enter-session">Entrar</button>
                        </div>
                    </div>
                </Popover.Content>
            </Popover>
        )
    };

    updateSessionId = (event) => {
        this.sessionId = event.target.value;
    }

    enterToTheSession = () => {
        const session = this.sessionRepository.enterToTheSession(this.sessionId);

        this.setState({ session: session });
    }

    renderHomeContent() {
        return (
            <div className="main col-sm">
                <a id="createSession" onClick={this.createSession} href="javascript:void(0)">Crie</a> uma sessão ou {this.renderEnterSessionLink()} em uma existente.
            </div>
        );
    }

    createSession = () => {
        const session = this.sessionRepository.createSession();

        this.setState({ session: session });
    }

    renderEnterSessionLink() {
        return (
            <OverlayTrigger trigger="click" placement="bottom" overlay={this.popover()}>
                <a id="enterToSession" href="javascript:void(0)">Entre</a>
            </OverlayTrigger>
        );
    }
}