import Question from '../models/Question';
import SessionModel from '../models/SessionModel';

export default class SessionRepository {
    async createSession() {
        const response = await fetch(`http://localhost:58824/api/sessions/user/91E4AFD5-A3AF-40C1-8C54-A5829063BBCA`, { method: 'POST' });

        const session = await response.json();

        return new SessionModel(session);
    }

    enterToTheSession(sessionId) {
        return getSessionModel(sessionId, false);
    }
}

function getSessionModel(sessionId, teamMemberIsTheFacilitator) {
    const questions = [
        new Question('Confiança',
            'Raramente dizemos o que pensamos. Preferimos evitar conflitos e não nos expor.',
            'Temos a coragem de ser honesto com os outros. Nos sentimos confortáveis participando de discussões e conflitos construtivos.',
            true),

        new Question('Feedback',
            'Raramente nos elogiamos uns aos outros ou fazemos uma chamada de atenção quando alguém age de maneira irresponsável ou violando nossos princípios.',
            'Damos uns aos outros feedback regularmente sobre pontos positivos e a melhorar.',
            false),

        new Question('Autonomia', false),
    ];

    return new SessionModel(sessionId, questions, teamMemberIsTheFacilitator);
}