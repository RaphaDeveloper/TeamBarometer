import MeetingRepository from '../../meeting/repositories/MeetingRepository';
import QuestionModel from '../../meeting/models/QuestionModel';
import MeetingModel from '../../meeting/models/MeetingModel';



export default function setupMeetingRepository() {
    MeetingRepository.mockImplementation(() => {
        return {
            createMeeting: async () => {
                const userIsTheFacilitator = true;

                const questions = [
                    new QuestionModel({
                        id: 1,
                        description: 'Confiança',
                        redAnswer: 'Raramente dizemos o que pensamos. Preferimos evitar conflitos e não nos expor.',
                        greenAnswer: 'Temos a coragem de ser honesto com os outros. Nos sentimos confortáveis participando de discussões e conflitos construtivos.',
                        isTheCurrent: false,
                        amountOfRedAnswers: 4,
                        amountOfYellowAnswers: 2,
                        amountOfGreenAnswers: 4
                    }),

                    new QuestionModel({
                        id: 2,
                        description: 'Feedback',
                        redAnswer: 'Raramente nos elogiamos uns aos outros ou fazemos uma chamada de atenção quando alguém age de maneira irresponsável ou violando nossos princípios.',
                        greenAnswer: 'Damos uns aos outros feedback regularmente sobre pontos positivos e a melhorar.',
                        isTheCurrent: true
                    }),

                    new QuestionModel({ id: 3, description: 'Autonomia', isTheCurrent: false }),
                ];

                const meeting = new MeetingModel({ id: '123-456-789', questions, userIsTheFacilitator });

                return Promise.resolve(meeting);
            },

            enterToTheMeeting: async (meetingId, userId) => {
                const userIsTheFacilitator = false;

                const questions = [
                    new QuestionModel({
                        id: 1,
                        description: 'Confiança',
                        redAnswer: 'Raramente dizemos o que pensamos. Preferimos evitar conflitos e não nos expor.',
                        greenAnswer: 'Temos a coragem de ser honesto com os outros. Nos sentimos confortáveis participando de discussões e conflitos construtivos.',
                        isTheCurrent: false,
                        amountOfRedAnswers: 4,
                        amountOfYellowAnswers: 2,
                        amountOfGreenAnswers: 4
                    }),

                    new QuestionModel({
                        id: 2,
                        description: 'Feedback',
                        redAnswer: 'Raramente nos elogiamos uns aos outros ou fazemos uma chamada de atenção quando alguém age de maneira irresponsável ou violando nossos princípios.',
                        greenAnswer: 'Damos uns aos outros feedback regularmente sobre pontos positivos e a melhorar.',
                        isTheCurrent: true
                    }),

                    new QuestionModel({ id: 3, description: 'Autonomia', isTheCurrent: false }),
                ];

                const meeting = meetingId == 'invalid' ? null : new MeetingModel({ id: meetingId, questions, userIsTheFacilitator });

                return Promise.resolve(meeting);
            }
        };
    });
}