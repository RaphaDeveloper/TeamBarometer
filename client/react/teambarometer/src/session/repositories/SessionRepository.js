import Question from '../models/Question';
import SessionModel from '../models/SessionModel';

export default class SessionRepository {
    createSession() {
        const questions = [
            new Question('Confiança',
                'Raramente dizemos o que pensamos. Preferimos evitar conflitos e não nos expor.',
                'Temos a coragem de ser honesto com os outros. Nos sentimos confortáveis participando de discussões e conflitos construtivos.',
                false, 2, 2, 4),

            new Question('Feedback',
                'Raramente nos elogiamos uns aos outros ou fazemos uma chamada de atenção quando alguém age de maneira irresponsável ou violando nossos princípios.',
                'Damos uns aos outros feedback regularmente sobre pontos positivos e a melhorar.',
                true),

            new Question('Autonomia', false),
        ];

        return new SessionModel('123-456-789', questions);
    }
}