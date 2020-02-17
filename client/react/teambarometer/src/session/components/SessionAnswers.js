import React, { Component } from "react";

import './SessionAnswers.css';

export default class SessionAnswers extends Component {
    render() {
        return (
            
            <div className="answers col-sm">
                <div className="red">
                    Nós raramente dizemos o que pensamos. Preferimos evitar conflitos e não nos expor.
                </div>
                <div className="yellow"></div>
                <div className="green">
                    Temos a coragem de ser honesto com os outros. Nos sentimos confortáveis participando de discussões e conflitos construtivo.
                </div>
            </div>
        );
    }
}