import React, { Component } from "react";

import './SessionAnswers.css';

export default class SessionAnswers extends Component {
    render() {
        return (
            <div className="answers col-sm">
                <button className="btn-block red">
                    {this.props.question && this.props.question.redAnswer}
                </button>
                <button className="btn-block yellow"></button>
                <button className="btn-block green">
                    {this.props.question && this.props.question.greenAnswer}
                </button>
            </div>
        );
    }
}