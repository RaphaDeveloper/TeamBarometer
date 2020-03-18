import SessionModel from '../models/SessionModel';

export default class SessionRepository {
    async getSession(sessionId, userId) {
        const response = await fetch(`${process.env.REACT_APP_API_URL}/api/sessions/${sessionId}/user/${userId}`, { method: 'GET' });

        const session = await response.json();

        return new SessionModel(session);
    }

    async createSession(userId) {
        const response = await fetch(`${process.env.REACT_APP_API_URL}/api/sessions/createsession/user/${userId}`, { method: 'POST' });

        const session = await response.json();

        return new SessionModel(session);
    }

    async enterToTheSession(sessionId, userId) {
        const response = await fetch(`${process.env.REACT_APP_API_URL}/api/sessions/jointhesession/${sessionId}/user/${userId}`, { method: 'PUT' });

        const session = await response.json();

        return new SessionModel(session);
    }

    enableAnswersOfTheCurrentQuestion(sessionId, userId) {
        fetch(`${process.env.REACT_APP_API_URL}/api/sessions/EnableAnswersOfTheCurrentQuestion/${sessionId}/user/${userId}`, { method: 'PUT' });
    }
}