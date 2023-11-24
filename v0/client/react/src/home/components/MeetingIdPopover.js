import React, { Component } from 'react';
import { Popover, OverlayTrigger } from 'react-bootstrap';
import MeetingRepository from '../../meeting/repositories/MeetingRepository';

export default class MeetingIdPopover extends Component {
    constructor(props) {
        super(props);
        this.state = {            
            meetingId: props.meetingId,
            invalidCode: false
        };
        this.meetingRepository = new MeetingRepository();
        this.meetingIdInput = React.createRef();
    }

    componentDidUpdate() {
        if (this.meetingIdInput.current) {
            this.meetingIdInput.current.focus();
        }
    }

    render() {
        const popOver = 
            <Popover id="popover-enter-meeting">
                <Popover.Title as="h3">Join in a meeting</Popover.Title>
                <Popover.Content>
                    {this.renderInvalidCode()}                    
                    <div className="input-group mb-3">                        
                        <input onChange={this.updateMeetingId} ref={this.meetingIdInput} type="text" className="form-control" placeholder="Meeting Code" aria-label="Meeting Code" id="meeting-id-input" value={this.state.meetingId} />
                        <div className="input-group-append">
                            <button onClick={this.enterToTheMeeting} className="btn btn-outline-secondary" type="button" id="button-enter-meeting" disabled={!this.state.meetingId}>Join</button>
                        </div>
                    </div>
                </Popover.Content>
            </Popover>

        return (
            <OverlayTrigger trigger="click" placement="bottom" overlay={popOver}>
                <a id="enterToMeeting" href="javascript:void(0)" className="link" onClick={this.openPopover}>Join</a>
            </OverlayTrigger>
        );
    };

    renderInvalidCode() {
        return (
            this.state.invalidCode && 
            <div className="invalid-code-container">
                <span>Invalid Code</span>
            </div>
        );
    }

    updateMeetingId = (event) => {
        this.setState({ meetingId: event.target.value });
    }

    openPopover = () => {
        this.setState({ meetingId: '', invalidCode: false });
    }

    enterToTheMeeting = async () => {
        const meeting = await this.meetingRepository.enterToTheMeeting(this.state.meetingId, this.props.userId);

        if (meeting) {
            this.props.onEnterToTheMeeting(meeting);
        } else {
            this.setState({ invalidCode: true });
        }
    }
}