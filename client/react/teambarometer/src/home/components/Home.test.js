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

                    return new SessionModel('123-456-789', questions);
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

    it('the link to subscribe to a session should be rendered', () => {
        expect(home.find('#subscribeToSession').length).toBe(1);
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
});