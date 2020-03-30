import React, { Component } from 'react';
import { Popover, OverlayTrigger } from 'react-bootstrap';
import SessionRepository from '../../session/repositories/SessionRepository';

export default class SessionIdPopover extends Component {
    constructor(props) {
        super(props);
        this.state = {            
            sessionId: props.sessionId,
            invalidCode: false
        };
        this.sessionRepository = new SessionRepository();
        this.sessionIdInput = React.createRef();
    }

    componentDidUpdate() {
        if (this.sessionIdInput.current) {
            this.sessionIdInput.current.focus();
        }
    }

    render() {
        const popOver = 
            <Popover id="popover-enter-session">
                <Popover.Title as="h3">Entrar em uma sessão</Popover.Title>
                <Popover.Content>
                    {this.renderInvalidCode()}                    
                    <div className="input-group mb-3">                        
                        <input onChange={this.updateSessionId} ref={this.sessionIdInput} type="text" className="form-control" placeholder="Código da Sessão" aria-label="Código da Sessão" id="session-id-input" />
                        <div className="input-group-append">
                            <button onClick={this.enterToTheSession} className="btn btn-outline-secondary" type="button" id="button-enter-session" disabled={!this.state.sessionId}>Entrar</button>
                        </div>
                    </div>
                </Popover.Content>
            </Popover>

        return (
            <OverlayTrigger trigger="click" placement="bottom" overlay={popOver}>
                <a href="javascript:void(0)" className="link" onClick={this.openPopover}>Entre</a>
            </OverlayTrigger>
        );
    };

    renderInvalidCode() {
        return (
            this.state.invalidCode && 
            <div className="invalid-code-container">
                <span>Código Inválido</span>
            </div>
        );
    }

    updateSessionId = (event) => {
        this.setState({ sessionId: event.target.value });
    }

    openPopover = () => {
        this.setState({ sessionId: '', invalidCode: false });
    }

    enterToTheSession = async () => {
        const session = await this.sessionRepository.enterToTheSession(this.state.sessionId, this.props.userId);

        if (session) {
            this.props.onEnterToTheSession(session);
        } else {
            this.setState({ invalidCode: true });
        }
    }
}