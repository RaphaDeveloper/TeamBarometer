import { It, Mock, Times } from 'moq.ts';
import { CreateSession } from "./create-session";
import { IUserRepository } from 'src/users/users.repository';
import { ISessionRepository } from 'src/sessions/sessions.repository';
import { User } from 'src/users/user';
import { Session } from 'src/sessions/session';

describe('CreateSession', () => {
    const creatorId = 1;
    const creator = new User(creatorId);
    const userRepositoryMock = new Mock<IUserRepository>();
    userRepositoryMock.setup(u => u.getById(creatorId)).returnsAsync(creator);
    const sessionRepositoryMock = new Mock<ISessionRepository>();
    sessionRepositoryMock.setup(r => r.save(It.IsAny())).returns(It.IsAny());
    const createSession = new CreateSession(userRepositoryMock.object(), sessionRepositoryMock.object());

    it('Should be possible to create a session', async () => {
        const creatorId = 1;

        const session = await createSession.execute(creatorId);

        expect(session).toBeTruthy();
        expect(session).toBeInstanceOf(Session);
        expect(session.participants).toStrictEqual([]);
    });

    it('The created session should not has any participants', async () => {
        const creatorId = 1;

        const session = await createSession.execute(creatorId);

        expect(session.participants).toStrictEqual([]);
    });

    it('The created session date should be now', async () => {
        const creatorId = 1;

        const session = await createSession.execute(creatorId);

        expect(session.createdAt.getTime()).toBeCloseTo(Date.now());
    });

    it('The created session should has a creator', async () => {
        const creatorId = 1;

        const session = await createSession.execute(creatorId);

        expect(session.creator).toBeTruthy();
        expect(session.creator).toBeInstanceOf(User);
    });

    it('The created session should has a creator', async () => {
        const creatorId = 1;

        const session = await createSession.execute(creatorId);

        expect(session.creator).toBeTruthy();
        expect(session.creator).toBeInstanceOf(User);
    });

    it('The created session should be saved', async () => {
        const creatorId = 1;

        const session = await createSession.execute(creatorId);

        sessionRepositoryMock.verify(r => r.save(session), Times.Once());
    });
});