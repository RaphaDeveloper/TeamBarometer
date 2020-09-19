import MeetingModel from '../models/MeetingModel';

export default class MeetingRepository {
    async getMeeting(meetingId, userId) {
        const response = await fetch(`${process.env.REACT_APP_API_URL}/api/Meetings/${meetingId}/User/${userId}`, { method: 'GET' });

        const meeting = await response.json();

        return new MeetingModel(meeting);
    }

    async createMeeting(userId) {
        const response = await fetch(`${process.env.REACT_APP_API_URL}/api/Meetings/CreateMeeting/User/${userId}`, { method: 'POST' });

        const meeting = await response.json();

        return new MeetingModel(meeting);
    }

    async enterToTheMeeting(meetingId, userId) {
        const response = await fetch(`${process.env.REACT_APP_API_URL}/api/Meetings/JoinTheMeeting/${meetingId}/User/${userId}`, { method: 'PUT' });

        if (!response.ok) {
            return null;
        }

        const meeting = await response.json();

        return new MeetingModel(meeting);
    }

    enableAnswersOfTheCurrentQuestion(meetingId, userId) {
        fetch(`${process.env.REACT_APP_API_URL}/api/Meetings/EnableAnswersOfTheCurrentQuestion/${meetingId}/User/${userId}`, { method: 'PUT' });
    }

    answerTheCurrentQuestion(userId, answer, meetingId) {
        fetch(`${process.env.REACT_APP_API_URL}/api/Meetings/AnswerTheCurrentQuestion/${meetingId}/User/${userId}/Answer/${answer}`, { method: 'PUT' });
    }
}