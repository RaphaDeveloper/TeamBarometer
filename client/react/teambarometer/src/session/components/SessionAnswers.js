import React, { Component } from "react";

import './SessionAnswers.css';

export default class SessionAnswers extends Component {
    render() {
        return (
            <div className="answers col-sm">
                <div className="red">
                    {this.props.question && this.props.question.redAnswer}
                </div>
                <div className="yellow"></div>
                <div className="green">
                    {this.props.question && this.props.question.greenAnswer}
                </div>
            </div>
        );
    }
}