import React from 'react';
import { render } from '@testing-library/react';
import { shallow, mount } from 'enzyme';
import Session from './Session';
import SessionRepository from '../repositories/SessionRepository';
import Question from '../models/Question';
import SessionModel from '../models/SessionModel';

jest.mock('../repositories/SessionRepository');

function configureSessionRepository() {
  SessionRepository.mockImplementation(() => {
    return {
      getSession: () => {
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

        return new SessionModel(questions);
      }
    };
  });
}

describe('when the session is loaded', () => {
  beforeAll(() => {
    configureSessionRepository();
  });

  it('the questions should also be loaded', () => {
    const session = mount(<Session />);

    expect(session.find('li div.question.d-flex').length).toBe(3);
  });

  it('should exist a current question', () => {
    const session = mount(<Session />);

    const currentQuestion = session.find('li.current-question');

    expect(currentQuestion.find('.question').text()).toBe('Feedback');
  });

  it('the answers of the current question should also be loaded', () => {
    const session = mount(<Session />);

    const answers = session.find('.answers');

    expect(answers.find('.red').text()).toBe('Raramente nos elogiamos uns aos outros ou fazemos uma chamada de atenção quando alguém age de maneira irresponsável ou violando nossos princípios.');
    expect(answers.find('.yellow').text()).toBe('');
    expect(answers.find('.green').text()).toBe('Damos uns aos outros feedback regularmente sobre pontos positivos e a melhorar.');
  });

  it('each question should has a description', () => {
    const { getByText } = render(<Session />);

    expect(getByText('Confiança')).toBeInTheDocument();
    expect(getByText('Feedback')).toBeInTheDocument();
    expect(getByText('Autonomia')).toBeInTheDocument();
  });

  it('should be possible to select a question', () => {
    const session = mount(<Session />);

    const firstQuestion = session.find('.questions li').at(0);
    
    firstQuestion.prop('onClick')();

    expect(firstQuestion.hasClass('selected')).toBe(true);
  });

  it('each question should has red, yellow and green counters', () => {
    const session = mount(<Session />);

    expect(session.find('li .question .count-red').length).toBe(3);
    expect(session.find('li .question .count-yellow').length).toBe(3);
    expect(session.find('li .question .count-green').length).toBe(3);
  });

  it('the counters should has the amount of each answer for the answered questions', () => {
    const session = mount(<Session />);

    expect(session.find('li:first-child .question .count-red').text()).toBe('4');
    expect(session.find('li:first-child .question .count-yellow').text()).toBe('2');
    expect(session.find('li:first-child .question .count-green').text()).toBe('4');
  });

  it('the counters should be empty for the not answered questions', () => {
    const session = mount(<Session />);

    expect(session.find('li .question .count-red').at(1).text()).toBe('');
    expect(session.find('li .question .count-yellow').at(1).text()).toBe('');
    expect(session.find('li .question .count-green').at(1).text()).toBe('');

    expect(session.find('li .question .count-red').at(2).text()).toBe('');
    expect(session.find('li .question .count-yellow').at(2).text()).toBe('');
    expect(session.find('li .question .count-green').at(2).text()).toBe('');
  });

  describe('and the user is the facilitator', () => {
    it('the play button should be rendered for the current question', () => {
      const session = mount(<Session />);

      expect(session.find('.current-question .play img').length).toBe(1);
    });

    it('the play button should not be rendered for the not current question', () => {
      const session = mount(<Session />);

      expect(session.find('li:not(.current-question) .question .play img').length).toBe(0);
    });
  });

});

//https://medium.com/capital-one-tech/unit-testing-behavior-of-react-components-with-test-driven-development-ae15b03a3689
//https://itnext.io/how-to-properly-test-react-components-9f090969cb6f
//https://medium.com/@mattmazzola/how-to-debug-jest-tests-with-vscode-48f003c7cb41
//https://jestjs.io/docs/en/es6-class-mocks