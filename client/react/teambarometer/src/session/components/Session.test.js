import React from 'react';
import { mount, render } from 'enzyme';
import Session from './Session';
import SessionAnswers from './SessionAnswers';
import Question from '../models/Question';
import SessionModel from '../models/SessionModel';

describe('when the session is loaded', () => {
  function getSession(userIsTheFacilitator, sessionFinished) {
    const questions = [
      new Question({
          id: 1,
          description: 'Confiança',
          redAnswer: 'Raramente dizemos o que pensamos. Preferimos evitar conflitos e não nos expor.',
          greenAnswer: 'Temos a coragem de ser honesto com os outros. Nos sentimos confortáveis participando de discussões e conflitos construtivos.',
          isTheCurrent: false,
          amountOfRedAnswers: 4,
          amountOfYellowAnswers: 2,
          amountOfGreenAnswers: 4
      }),

      new Question({
          id: 2,
          description: 'Feedback',
          redAnswer: 'Raramente nos elogiamos uns aos outros ou fazemos uma chamada de atenção quando alguém age de maneira irresponsável ou violando nossos princípios.',
          greenAnswer: 'Damos uns aos outros feedback regularmente sobre pontos positivos e a melhorar.',
          isTheCurrent: true && !sessionFinished
      }),

      new Question({id: 3, description: 'Autonomia', isTheCurrent: false}),
  ];

  return new SessionModel({ id: '123-456-789', questions, userIsTheFacilitator });
  }

  let session;

  beforeEach(() => {
    const userIsTheFacilitator = true;
    const sessionFinished = false;

    session = mount(<Session session={getSession(userIsTheFacilitator, sessionFinished)}/>);
  });

  afterEach(() => {
    session.unmount();
  });

  it('the questions should also be loaded', () => {
    expect(session.find('li div.question.d-flex').length).toBe(3);
  });

  it('should exist a current question', () => {
    const currentQuestion = session.find('li.current-question .question');

    expect(currentQuestion.find('.question').text()).toBe('Feedback');
  });

  it('the answers of the current question should also be loaded', () => {
    const answers = session.find('.answers');

    expect(answers.find('.red').text()).toBe('Raramente nos elogiamos uns aos outros ou fazemos uma chamada de atenção quando alguém age de maneira irresponsável ou violando nossos princípios.');
    expect(answers.find('.yellow').text()).toBe('');
    expect(answers.find('.green').text()).toBe('Damos uns aos outros feedback regularmente sobre pontos positivos e a melhorar.');
  });

  it('the answers of the current question should be disabled if the question is not up for answer', () => {
    const question = new Question({ isUpForAnswer: false });
    
    const answers = mount(<SessionAnswers question={question} userIsTheFacilitator={false}/>);

    expect(answers.find('.red[disabled=true]').length).toBe(1);
    expect(answers.find('.yellow[disabled=true]').length).toBe(1);
    expect(answers.find('.green[disabled=true]').length).toBe(1);
  });

  it('the answers of the current question should be disabled if the user is the facilitator', () => {
    const question = new Question({ isUpForAnswer: true });
    
    const answers = mount(<SessionAnswers question={question} userIsTheFacilitator={true}/>);

    expect(answers.find('.red[disabled=true]').length).toBe(1);
    expect(answers.find('.yellow[disabled=true]').length).toBe(1);
    expect(answers.find('.green[disabled=true]').length).toBe(1);
  });

  it('the answers of the current question should not be disabled if the question is up for answer and the user is not the facilitator', () => {
    const question = new Question({ isUpForAnswer: true });
    
    const answers = mount(<SessionAnswers question={question} userIsTheFacilitator={false}/>);

    expect(answers.find('.red[disabled=false]').length).toBe(1);
    expect(answers.find('.yellow[disabled=false]').length).toBe(1);
    expect(answers.find('.green[disabled=false]').length).toBe(1);
  });

  it('each question should has a description', () => {
    expect(session.find('.question-description').at(0).text()).toBe('Confiança');
    expect(session.find('.question-description').at(1).text()).toBe('Feedback');
    expect(session.find('.question-description').at(2).text()).toBe('Autonomia');
  });

  it('should be possible to select a question', () => {
    let firstQuestion = session.find('.questions li').at(0);
    
    firstQuestion = firstQuestion.simulate('click') && session.find('.questions li').at(0);

    expect(firstQuestion.hasClass('selected')).toBe(true);
  });
    
  it('should be possible to select an answer', () => {
    let redAnswer = session.find('.answers .red');
    
    redAnswer = redAnswer.simulate('click') && session.find('.answers .red');

    expect(redAnswer.hasClass('selected-answer')).toBe(true);
  });

  it('selected question should be the last question when the session is finished', () => {
    const userIsTheFacilitator = false;
    const sessionFinished = true;

    let session = mount(<Session session={getSession(userIsTheFacilitator, sessionFinished)}/>);

    let firstQuestion = session.find('.questions li').at(2);

    expect(firstQuestion.hasClass('selected')).toBe(true);
  });

  it('should update the answers of the selected question', () => {
    let firstQuestion = session.find('.questions li').at(0);
    
    firstQuestion.simulate('click');

    expect(session.find('.answers .red').text()).toBe('Raramente dizemos o que pensamos. Preferimos evitar conflitos e não nos expor.');
    expect(session.find('.answers .yellow').text()).toBe('');
    expect(session.find('.answers .green').text()).toBe('Temos a coragem de ser honesto com os outros. Nos sentimos confortáveis participando de discussões e conflitos construtivos.');
  });

  it('should not change the style of the current question when select it', () => {
    let currentQuestion = session.find('.questions .current-question');
    
    currentQuestion = currentQuestion.simulate('click') && session.find('.questions .current-question');

    expect(currentQuestion.hasClass('selected')).toBe(false);
  });

  it('each question should has red, yellow and green counters', () => {
    expect(session.find('li .question .count-red').length).toBe(3);
    expect(session.find('li .question .count-yellow').length).toBe(3);
    expect(session.find('li .question .count-green').length).toBe(3);
  });

  it('the counters should has the amount of each answer for the answered questions', () => {
    expect(session.find('li:first-child .question .count-red').text()).toBe('4');
    expect(session.find('li:first-child .question .count-yellow').text()).toBe('2');
    expect(session.find('li:first-child .question .count-green').text()).toBe('4');
  });

  it('the counters should be empty for the not answered questions', () => {
    expect(session.find('li .question .count-red').at(1).text()).toBe('');
    expect(session.find('li .question .count-yellow').at(1).text()).toBe('');
    expect(session.find('li .question .count-green').at(1).text()).toBe('');

    expect(session.find('li .question .count-red').at(2).text()).toBe('');
    expect(session.find('li .question .count-yellow').at(2).text()).toBe('');
    expect(session.find('li .question .count-green').at(2).text()).toBe('');
  });

  it('the play button should not be rendered for the not current question', () => {
    expect(session.find('li:not(.current-question) .question .play').length).toBe(0);
  });

  describe('if the user is the facilitator', () => {
    it('the play button should be rendered for the current question', () => {
      expect(session.find('.current-question .play').length).toBe(1);
    });
  });

  describe('if the user is not the facilitator', () => {
    it('the play button should not be rendered for the current question', () => {
      const userIsTheFacilitator = false;

      let session = mount(<Session session={getSession(userIsTheFacilitator)}/>);

      expect(session.find('.current-question .play').length).toBe(0);
    });
  });

});

//https://medium.com/capital-one-tech/unit-testing-behavior-of-react-components-with-test-driven-development-ae15b03a3689
//https://itnext.io/how-to-properly-test-react-components-9f090969cb6f
//https://medium.com/@mattmazzola/how-to-debug-jest-tests-with-vscode-48f003c7cb41
//https://jestjs.io/docs/en/es6-class-mocks