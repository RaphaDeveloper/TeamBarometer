import React from 'react';
import { mount } from 'enzyme';
import Home from '../../../home/components/Home';
import setupMeetingRepository from '../../doubles/repository.mock';

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

    beforeAll(() => {
        setupMeetingRepository();
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