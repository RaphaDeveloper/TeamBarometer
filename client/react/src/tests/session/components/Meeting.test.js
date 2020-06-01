import React from 'react';
import { mount } from 'enzyme';
import Meeting from '../../../meeting/components/Meeting';
import Answers from '../../../meeting/components/Answers';
import QuestionModel from '../../../meeting/models/QuestionModel';
import MeetingModel from '../../../meeting/models/MeetingModel';

jest.mock('../../../meeting/repositories/MeetingRepository');

describe('when the meeting is loaded', () => {
  function getSession(userIsTheFacilitator, sessionFinished, isUpForAnswer) {
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
          isTheCurrent: true && !sessionFinished,
          isUpForAnswer: isUpForAnswer
      }),

      new QuestionModel({id: 3, description: 'Autonomia', isTheCurrent: false}),
  ];

  return new MeetingModel({ id: '123-456-789', questions, userIsTheFacilitator });
  }

  let meeting;

  beforeEach(() => {
    const userIsTheFacilitator = true;
    const sessionFinished = false;

    meeting = mount(<Meeting meeting={getSession(userIsTheFacilitator, sessionFinished)}/>);
  });

  afterEach(() => {
    meeting.unmount();
  });

  it('questions should be loaded', () => {
    expect(meeting.find('li div.question.d-flex').length).toBe(3);
  });

  it('current question should be loaded', () => {
    const currentQuestion = meeting.find('li.current-question .question');

    expect(currentQuestion.find('.question').text()).toBe('Feedback');
  });

  it('answers of current question should be loaded', () => {
    const answers = meeting.find('.answers');

    expect(answers.find('.red').text()).toBe('Raramente nos elogiamos uns aos outros ou fazemos uma chamada de atenção quando alguém age de maneira irresponsável ou violando nossos princípios.');
    expect(answers.find('.yellow').text()).toBe('');
    expect(answers.find('.green').text()).toBe('Damos uns aos outros feedback regularmente sobre pontos positivos e a melhorar.');
  });

  it('answers of current question should be disabled if the question is not up for answer', () => {
    const question = new QuestionModel({ isUpForAnswer: false });
    
    const answers = mount(<Answers question={question} userIsTheFacilitator={false}/>);

    expect(answers.find('.red[disabled=true]').length).toBe(1);
    expect(answers.find('.yellow[disabled=true]').length).toBe(1);
    expect(answers.find('.green[disabled=true]').length).toBe(1);
  });

  it('answers of current question should be disabled if user is the facilitator', () => {
    const question = new QuestionModel({ isUpForAnswer: true });
    
    const answers = mount(<Answers question={question} userIsTheFacilitator={true}/>);

    expect(answers.find('.red[disabled=true]').length).toBe(1);
    expect(answers.find('.yellow[disabled=true]').length).toBe(1);
    expect(answers.find('.green[disabled=true]').length).toBe(1);
  });

  it('answers of current question should be enabled if question is up for answer and user is not the facilitator', () => {
    const question = new QuestionModel({ isUpForAnswer: true });
    
    const answers = mount(<Answers question={question} userIsTheFacilitator={false}/>);

    expect(answers.find('.red[disabled=false]').length).toBe(1);
    expect(answers.find('.yellow[disabled=false]').length).toBe(1);
    expect(answers.find('.green[disabled=false]').length).toBe(1);
  });

  it('each question should has a description', () => {
    expect(meeting.find('.question-description').at(0).text()).toBe('Confiança');
    expect(meeting.find('.question-description').at(1).text()).toBe('Feedback');
    expect(meeting.find('.question-description').at(2).text()).toBe('Autonomia');
  });
  
  it('should be possible to select a question', () => {
    let firstQuestion = meeting.find('.questions li').at(0);    
    
    firstQuestion.simulate('click');

    expect(meeting.find('.questions li').at(0).hasClass('selected')).toBe(true);
  });    
  
  it('should be possible to select an answer', () => {
    const userIsTheFacilitator = false;
    const sessionFinished = false;
    const isUpForAnswer = true;
    let meeting = mount(<Meeting meeting={getSession(userIsTheFacilitator, sessionFinished, isUpForAnswer)}/>);
    let redAnswer = meeting.find('.answers button.red').at(0);

    redAnswer.simulate('click');
    
    expect(meeting.find('.answers .red').hasClass('selected-answer')).toBe(true);
  });
  
  it('should refresh the answers when select a question', () => {
    let firstQuestion = meeting.find('.questions li').at(0);
    
    firstQuestion.simulate('click');

    expect(meeting.find('.answers .red').text()).toBe('Raramente dizemos o que pensamos. Preferimos evitar conflitos e não nos expor.');
    expect(meeting.find('.answers .yellow').text()).toBe('');
    expect(meeting.find('.answers .green').text()).toBe('Temos a coragem de ser honesto com os outros. Nos sentimos confortáveis participando de discussões e conflitos construtivos.');
  });

  it('current question should not has its style changed when select it', () => {
    let currentQuestion = meeting.find('.questions .current-question');
    
    currentQuestion.simulate('click');

    expect(meeting.find('.questions .current-question').hasClass('selected')).toBe(false);
  });

  it('each question should has red, yellow and green counters', () => {
    expect(meeting.find('li .question .count-red').length).toBe(3);
    expect(meeting.find('li .question .count-yellow').length).toBe(3);
    expect(meeting.find('li .question .count-green').length).toBe(3);
  });

  it('answered questions should has the amount of each answer', () => {
    expect(meeting.find('li .question .count-red').at(0).text()).toBe('4');
    expect(meeting.find('li .question .count-yellow').at(0).text()).toBe('2');
    expect(meeting.find('li .question .count-green').at(0).text()).toBe('4');
  });

  it('not answered questions should not has the amount of each answer', () => {
    expect(meeting.find('li .question .count-red').at(1).text()).toBe('');
    expect(meeting.find('li .question .count-yellow').at(1).text()).toBe('');
    expect(meeting.find('li .question .count-green').at(1).text()).toBe('');

    expect(meeting.find('li .question .count-red').at(2).text()).toBe('');
    expect(meeting.find('li .question .count-yellow').at(2).text()).toBe('');
    expect(meeting.find('li .question .count-green').at(2).text()).toBe('');
  });

  it('play button should be rendered for current question if user is the facilitator', () => {
    expect(meeting.find('.current-question .question .button-play').length).toBe(1);
  });

  it('play button should not be rendered for not current question', () => {
    expect(meeting.find('li:not(.current-question) .question .button-play').length).toBe(0);
  });

  it('play button should not be rendered for current question if user is not the facilitator', () => {
    const userIsTheFacilitator = false;

    let meeting = mount(<Meeting meeting={getSession(userIsTheFacilitator)}/>);

    expect(meeting.find('.current-question .question .button-play').length).toBe(0);
  });
});

//https://medium.com/capital-one-tech/unit-testing-behavior-of-react-components-with-test-driven-development-ae15b03a3689
//https://itnext.io/how-to-properly-test-react-components-9f090969cb6f
//https://medium.com/@mattmazzola/how-to-debug-jest-tests-with-vscode-48f003c7cb41
//https://jestjs.io/docs/en/es6-class-mocks