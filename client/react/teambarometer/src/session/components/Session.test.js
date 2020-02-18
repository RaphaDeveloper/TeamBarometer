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
          new Question('Confiança', true),
          new Question('Feedback'),
          new Question('Autonomia'),
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

    expect(currentQuestion.find('.question').text()).toBe('Confiança');
  });

  it('each question should has a description', () => {
    const { getByText } = render(<Session />);

    expect(getByText(/Confiança/i)).toBeInTheDocument();
    expect(getByText(/Feedback/i)).toBeInTheDocument();
    expect(getByText(/Autonomia/i)).toBeInTheDocument();
  });

  it('each question should has red, yellow and green counters', () => {
    const session = mount(<Session />);

    expect(session.find('li div.question div.cont-red').length).toBe(3);
    expect(session.find('li div.question div.cont-yellow').length).toBe(3);
    expect(session.find('li div.question div.cont-green').length).toBe(3);
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

  describe('and the user is not the facilitator', () => {
    it('the play button should not be rendered', () => {
      const session = shallow(<Session />);
      session.instance().userIsTheFacilitator = () => false;

      expect(session.contains(<div className="play"></div>)).toBe(false);
    });
  });

});

//https://medium.com/capital-one-tech/unit-testing-behavior-of-react-components-with-test-driven-development-ae15b03a3689
//https://itnext.io/how-to-properly-test-react-components-9f090969cb6f
//https://medium.com/@mattmazzola/how-to-debug-jest-tests-with-vscode-48f003c7cb41
//https://jestjs.io/docs/en/es6-class-mocks