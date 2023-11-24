import { Mock } from 'moq.ts';
import { CreateSession, IUserRepository, Session, User } from "./create-session";

describe('CreateSession', () => {
    const creatorId = 1;
    const creator = new User(creatorId);
    const userRepositoryMock = new Mock<IUserRepository>();
    userRepositoryMock.setup(u => u.getById(creatorId)).returns(creator);
    const createSession = new CreateSession(userRepositoryMock.object());

    it('Should be possible to create a session', () => {
        const creatorId = 1;

        const session = createSession.execute(creatorId);

        expect(session).toBeTruthy();
        expect(session).toBeInstanceOf(Session);
        expect(session.participants).toStrictEqual([]);
    });

    it('The created session should not has any participants', () => {
        const creatorId = 1;

        const session = createSession.execute(creatorId);

        expect(session.participants).toStrictEqual([]);
    });

    it('The created session date should be now', () => {
        const creatorId = 1;

        const session = createSession.execute(creatorId);

        expect(session.date.getTime()).toBeCloseTo(Date.now());
    });

    it('The created session should has a creator', () => {
        const creatorId = 1;

        const session = createSession.execute(creatorId);

        expect(session.creator).toBeTruthy();
        expect(session.creator).toBeInstanceOf(User);
    });
});