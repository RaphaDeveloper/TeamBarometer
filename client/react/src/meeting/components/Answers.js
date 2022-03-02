import './styles/Answers.css';
import React, { Component } from "react";

export default class Answers extends Component {
    constructor(props) {
        super(props);
        this.state = {
            answerByQuestion: null,
            annotationByQuestion: null
        };
    }

    render() {        
        return (
            <div className="answers col-sm">
                <button onClick={() => this.onSelectAnswer('Red')} className={this.getAnswerClasses("btn-block red", "Red")} disabled={!this.canAnswer()}>
                    {this.props.question && this.props.question.redAnswer}
                </button>
                <button onClick={() => this.onSelectAnswer('Yellow')} className={this.getAnswerClasses("btn-block yellow", "Yellow")} disabled={!this.canAnswer()}></button>
                <button onClick={() => this.onSelectAnswer('Green')} className={this.getAnswerClasses("btn-block green", "Green")} disabled={!this.canAnswer()}>
                    {this.props.question && this.props.question.greenAnswer}
                </button>
                {
                    !this.props.userIsTheFacilitator ? 
                        <div>
                            <label>Annotation</label>
                            <textarea 
                                value={this.getAnnotation()} 
                                onChange={event => this.handleChangeAnnotation(event.target.value)}
                                disabled={!this.canAnswer()}>
                            </textarea>
                            <button onClick={this.handleAnswerConfirmation} disabled={!this.canConfirmAnswer()}>Confirmar</button>
                        </div>
                    : null
                }
                
            </div>
        );
    }

    canConfirmAnswer() {
        return this.props.question.isUpForAnswer && 
            this.state.answerByQuestion != null && this.state.answerByQuestion[this.props.question.id.toString()] != null;
    }

    getAnnotation() {
        return this.state.annotationByQuestion == null || !this.state.annotationByQuestion[this.props.question.id.toString()] ? "" : this.state.annotationByQuestion[this.props.question.id.toString()];
    }

    getAnswerClasses(classes, answer) {
        if (this.state.answerByQuestion) {
            if (this.state.answerByQuestion[this.props.question.id.toString()] === answer) {
                classes += " selected-answer";
            }
        }

        return classes;
    }

    canAnswer = () => {
        return this.props.question.isUpForAnswer && !this.props.userIsTheFacilitator;
    }

    onSelectAnswer = (answer) => {   
        let answerByQuestion = this.state.answerByQuestion || {};
        
        answerByQuestion[this.props.question.id.toString()] = answer;

        this.setState({ answerByQuestion });
    }

    handleChangeAnnotation = (annotation) => {   
        let annotationByQuestion = this.state.annotationByQuestion || {};
        
        annotationByQuestion[this.props.question.id.toString()] = annotation;

        this.setState({ annotationByQuestion });
    }

    handleAnswerConfirmation = () => {
        let answer = this.state.answerByQuestion[this.props.question.id.toString()];
        let annotation = this.state.annotationByQuestion[this.props.question.id.toString()];

        this.props.onSelectAnswer(answer, annotation);
    }
}