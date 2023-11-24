import './styles/Meeting.css';
import React, { Component } from 'react';
import Questions from './Questions';
import Answers from './Answers';
import MeetingRepository from '../repositories/MeetingRepository';
import * as signalR from "@aspnet/signalr";

export default class Meeting extends Component {
    constructor(props) {
        super(props);
        this.meetingRepository = new MeetingRepository();
        this.state = {
            meeting: this.props.meeting,
            selectedQuestion: this.props.meeting.getCurrentQuestion(),
            hubConnections: null
        };
    }

    componentDidMount() {
        const hubConnection = new signalR.HubConnectionBuilder().withUrl(`${process.env.REACT_APP_API_URL}/meetingHub/${this.props.meeting.id}`).build();

        hubConnection.on("RefreshMeeting", this.refreshMeeting);

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
                <Questions meeting={this.state.meeting} selectedQuestion={this.state.selectedQuestion} onSelectQuestion={this.updateSelectedQuestion} onPlayQuestion={this.enableAnswersOfTheCurrentQuestion} />
                <Answers question={this.state.selectedQuestion} userIsTheFacilitator={this.state.meeting.userIsTheFacilitator} onSelectAnswer={this.answerTheCurrentQuestion} />
            </>
        );
    }

    updateSelectedQuestion = (question) => {
        this.setState({ selectedQuestion: question });
    }

    enableAnswersOfTheCurrentQuestion = () => {
        this.meetingRepository.enableAnswersOfTheCurrentQuestion(this.state.meeting.id, this.props.userId);
    }

    refreshMeeting = async () => {
        const meeting =  await this.meetingRepository.getMeeting(this.state.meeting.id, this.props.userId);

        this.setState({ meeting, selectedQuestion: meeting.getCurrentQuestion() });
    }

    answerTheCurrentQuestion = (answer, annotation) => {
        this.meetingRepository.answerTheCurrentQuestion(this.props.userId, answer, this.state.meeting.id, annotation);
    }
}