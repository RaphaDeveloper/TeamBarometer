import React from 'react';
import { mount, shallow } from 'enzyme';
import Home from '../../../home/components/Home';
import MeetingRepository from '../../../meeting/repositories/MeetingRepository/';
import QuestionModel from '../../../meeting/models/QuestionModel';
import MeetingModel from '../../../meeting/models/MeetingModel';
import { act } from "react-dom/test-utils";

jest.mock('../../../meeting/repositories/MeetingRepository/');

const setup = () => {
    const wrapper = mount(<Home />)

    return {
        wrapper,
        createMeeting: wrapper.find('#createMeeting'),
        enterToMeeting: wrapper.find('#enterToMeeting'),
        meetingId: wrapper.find('#meetingId'),
        title: wrapper.find('header > h1')
    }
}

describe('Home Component', () => {

    function configureMeetingRepository() {
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

    let home;

    beforeAll(() => {
        configureMeetingRepository();
    });

    beforeEach(() => {
        home = mount(<Home />);
        console.log(home);
    });

    afterEach(() => {
        home.unmount();
    });

    it('should be rendered', () => {
        const { wrapper, createMeeting, enterToMeeting, meetingId, title } = setup();

        expect(wrapper.exists()).toBe(true);
        expect(createMeeting.exists()).toBe(true);
        expect(enterToMeeting.exists()).toBe(true);
        expect(meetingId.exists()).toBe(false);
        expect(title.text()).toBe('Team Barometer');
    });

    describe('Create Meeting', () => {
        it('the meeting should be rendered', (done) => {
            const { wrapper, createMeeting } = setup();

            createMeeting.simulate('click');

            setImmediate(() => {
                wrapper.update();
                expect(wrapper.find('.questions').exists()).toBe(true);
                expect(wrapper.find('.answers').exists()).toBe(true);
                expect(wrapper.find('#meetingId').text()).toBe('123-456-789');
                done();
            });
        });
    });

    describe('Enter The Meeting', () => {
        it('the enter button should be disabled if meeting id is empty', () => {
            const { wrapper, enterToMeeting } = setup();

            enterToMeeting.simulate('click');

            expect(wrapper.find('#button-enter-meeting').prop('disabled')).toBe(true);
        });

        it('the enter button should be enabled if meeting id is not empty', () => {
            const { wrapper, enterToMeeting } = setup();
            enterToMeeting.simulate('click');

            wrapper.find('#meeting-id-input').simulate('change', { target: { value: '789-456-123' } });
            
            expect(wrapper.find('#button-enter-meeting').prop('disabled')).toBe(false);
        });

        it('meeting id should be clear when open', () => {
            const { wrapper, enterToMeeting } = setup();
            enterToMeeting.simulate('click');
            wrapper.find('#meeting-id-input').simulate('change', { target: { value: '789-456-123' } });
            enterToMeeting.simulate('click');

            enterToMeeting.simulate('click');
            
            expect(wrapper.find('#meeting-id-input').prop('value')).toBe('');
        });

        it('render invalid code when meeting id is invalid and try to enter the meeting', (done) => {
            const { wrapper, enterToMeeting } = setup();
            enterToMeeting.simulate('click');
            wrapper.find('#meeting-id-input').simulate('change', { target: { value: 'invalid' } });
            
            wrapper.find('#button-enter-meeting').simulate('click');
            
            setImmediate(() => {
                wrapper.update();
                expect(wrapper.find('.invalid-code-container > span').text()).toBe('Código Inválido');
                done();
            });
        });

        it('meeting should be rendered when enter a valid code', (done) => {
            const { wrapper, enterToMeeting } = setup();
            enterToMeeting.simulate('click');
            wrapper.find('#meeting-id-input').simulate('change', { target: { value: '123-456-789' } });
            
            wrapper.find('#button-enter-meeting').simulate('click');
            
            setImmediate(() => {
                wrapper.update();
                expect(wrapper.find('.questions').exists()).toBe(true);
                expect(wrapper.find('.answers').exists()).toBe(true);
                expect(wrapper.find('#meetingId').text()).toBe('123-456-789');
                done();
            });
        });
    });
});