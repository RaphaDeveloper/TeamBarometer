import React from 'react';
import { mount } from 'enzyme';
import Home from './Home';
import SessionRepository from '../../session/repositories/SessionRepository';
import Question from '../../session/models/Question';
import SessionModel from '../../session/models/SessionModel';

jest.mock('../../session/repositories/SessionRepository');

describe('when the home is loaded', () => {

    function configureSessionRepository() {
        SessionRepository.mockImplementation(() => {
            return {
                createSession: () => {
                    const teamMemberIsTheFacilitator = true;

                    const questions = [
                        new Question('Confiança',
                            'Raramente dizemos o que pensamos. Preferimos evitar conflitos e não nos expor.',
                            'Temos a coragem de ser honesto com os outros. Nos sentimos confortáveis participando de discussões e conflitos construtivos.',
                            false, 4, 2, 4),

                        new Question('Feedback',
                            'Raramente nos elogiamos uns aos outros ou fazemos uma chamada de atenção quando alguém age de maneira irresponsável ou violando nossos princípios.',
                            'Damos uns aos outros feedback regularmente sobre pontos positivos e a melhorar.',
                            true),

                        new Question('Autonomia', false),
                    ];

                    return new SessionModel('123-456-789', questions, teamMemberIsTheFacilitator);
                },

                enterToTheSession: (sessionId) => {
                    const teamMemberIsTheFacilitator = false;

                    const questions = [
                        new Question('Confiança',
                            'Raramente dizemos o que pensamos. Preferimos evitar conflitos e não nos expor.',
                            'Temos a coragem de ser honesto com os outros. Nos sentimos confortáveis participando de discussões e conflitos construtivos.',
                            false, 4, 2, 4),

                        new Question('Feedback',
                            'Raramente nos elogiamos uns aos outros ou fazemos uma chamada de atenção quando alguém age de maneira irresponsável ou violando nossos princípios.',
                            'Damos uns aos outros feedback regularmente sobre pontos positivos e a melhorar.',
                            true),

                        new Question('Autonomia', false),
                    ];

                    return new SessionModel(sessionId, questions, teamMemberIsTheFacilitator);
                }
            };
        });
    }

    let home;

    beforeAll(() => {
        configureSessionRepository();
    });

    beforeEach(() => {
        home = mount(<Home />);
    });

    afterEach(() => {
        home.unmount();
    });

    it('the link to create a session should be rendered', () => {
        expect(home.find('#createSession').length).toBe(1);
    });

    it('the link to enter to a session should be rendered', () => {
        expect(home.find('#enterToSession').length).toBe(1);
    });

    it('the session id should not be rendered', () => {
        expect(home.find('.questions').length).toBe(0);
        expect(home.find('.answers').length).toBe(0);
    });

    describe('and the session is created', () => {
        it('the session should be rendered', () => {
            let createSessionLink = home.find('#createSession');

            createSessionLink.simulate('click');

            expect(home.find('.questions').length).toBe(1);
            expect(home.find('.answers').length).toBe(1);
        });

        it('the session id should be rendered', () => {
            let createSessionLink = home.find('#createSession');

            createSessionLink.simulate('click');

            expect(home.find('#sessionId').text()).toBe('123-456-789');
        });
    });

    describe('and the user clicks to enter on a session', () => {
        it('the enter button should be disabled if not informe the session id', () => {
            let enterSessionLink = home.find('#enterToSession');
            enterSessionLink.simulate('click');

            let btnEnterSession = home.find('#button-enter-session')

            expect(btnEnterSession.is('[disabled]')).toBe(true);
        });

        it('session should not be rendered after user inform the session id, close the popover, open the popover and click to enter', () => {
            home.find('#enterToSession').simulate('click');
            home.find('#input-session-id').simulate('change', { target: { value: '789-456-123' } });
            home.find('#enterToSession').simulate('click');
            home.find('#enterToSession').simulate('click');

            home.find('#button-enter-session').simulate('click');

            expect(home.find('.questions').length).toBe(0);
            expect(home.find('.answers').length).toBe(0);
        });
    });

    describe('and the user enter on a session', () => {
        it('the session should be rendered', () => {
            let enterSessionLink = home.find('#enterToSession');
            enterSessionLink.simulate('click');
            let inputSessionId = home.find('#input-session-id');

            inputSessionId.simulate('change', { target: { value: '789-456-123' } });
            let btnEnterSession = home.find('#button-enter-session');
            btnEnterSession.simulate('click');

            expect(home.find('.questions').length).toBe(1);
            expect(home.find('.answers').length).toBe(1);
        });

        it('the session id should be rendered', () => {
            let enterSessionLink = home.find('#enterToSession');
            enterSessionLink.simulate('click');
            let inputSessionId = home.find('#input-session-id')

            inputSessionId.simulate('change', { target: { value: '789-456-123' } });
            let btnEnterSession = home.find('#button-enter-session')
            btnEnterSession.simulate('click');

            expect(home.find('#sessionId').text()).toBe('789-456-123');
        });
    });
});