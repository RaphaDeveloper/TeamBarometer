import './styles/Home.css';
import React, { Component } from 'react';
import Meeting from '../../meeting/components/Meeting';
import MeetingRepository from '../../meeting/repositories/MeetingRepository';
import { v4 as uuidv4 } from 'uuid';
import MeetingIdPopover from './MeetingIdPopover';


export default class Home extends Component {
    constructor(props) {
        super(props);
        this.meetingRepository = new MeetingRepository();        
        this.state = { meeting: null, userId: null };
    }

    componentDidMount() {
        this.setState({ userId: uuidv4() });
    }

    render() {
        return (
            <>
                <div className="background">
                    <div className="row-background-header"></div>
                    <div className="row-background-body"></div>
                </div>
                <main className="container">
                    <div className="row">
                        <header className="col-sm">
                            {this.state.meeting && <span id="meetingId">{this.state.meeting.id}</span>}
                            <h1>Team Barometer</h1>
                        </header>
                    </div>
                    <div className="row">
                        {this.renderMainContent()}
                    </div>
                </main>
            </>
        );
    }

    renderMainContent() {
        return this.state.meeting ? this.renderMeeting() : this.renderHomeContent();
    }

    renderMeeting() {
        return (
            <Meeting meeting={this.state.meeting} userId={this.state.userId} />
        );
    }

    renderHomeContent() {
        return (
            <div className="main col-sm">
                <a id="createMeeting" onClick={this.createMeeting} className="link" href="javascript:void(0)">Create</a> a meeting or {this.renderEnterMeetingLink()} an existing one.
            </div>
        );
    }

    createMeeting = async () => {
        const meeting = await this.meetingRepository.createMeeting(this.state.userId);

        this.setState({ meeting });
    }

    renderEnterMeetingLink() {
        return (
            <MeetingIdPopover onEnterToTheMeeting={this.enterToTheMeeting} userId={this.state.userId}></MeetingIdPopover>
        );
    }

    enterToTheMeeting = async (meeting) => {
        this.setState({ meeting });
    }
}