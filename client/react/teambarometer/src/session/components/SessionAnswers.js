import React, { Component } from "react";

import './SessionAnswers.css';

export default class SessionAnswers extends Component {
    render() {
        return (
            <div className="answers col-sm">
                <button onClick={() => this.props.onSelectAnswer('Red')} className="btn-block red" disabled={this.disableAnswer()}>
                    {this.props.question && this.props.question.redAnswer}
                </button>
                <button onClick={() => this.props.onSelectAnswer('Yellow')} className="btn-block yellow" disabled={this.disableAnswer()}></button>
                <button onClick={() => this.props.onSelectAnswer('Green')} className="btn-block green" disabled={this.disableAnswer()}>
                    {this.props.question && this.props.question.greenAnswer}
                </button>
            </div>
        );
    }

    disableAnswer = () => {
        return !this.props.question.isUpForAnswer || this.props.userIsTheFacilitator;
    }
}