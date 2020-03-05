import SessionModel from '../models/SessionModel';

export default class SessionRepository {
    async createSession() {
        const response = await fetch(`${process.env.REACT_APP_API_URL}/api/sessions/createsession/user/91E4AFD5-A3AF-40C1-8C54-A5829063BBCA`, { method: 'POST' });

        const session = await response.json();

        return new SessionModel(session);
    }

    async enterToTheSession(sessionId) {
        const response = await fetch(`${process.env.REACT_APP_API_URL}/api/sessions/jointhesession/${sessionId}/user/14907CD3-531F-4807-A458-FD6668FFFD19`, { method: 'PUT' });

        const session = await response.json();

        return new SessionModel(session);
    }
}