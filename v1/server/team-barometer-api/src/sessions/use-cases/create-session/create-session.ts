import { Inject, Injectable } from "@nestjs/common";
import { Session } from "src/sessions/session";
import { ISessionRepository, SESSION_REPOSITORY } from "src/sessions/sessions.repository";
import { IUserRepository, USER_REPOSITORY } from "src/users/users.repository";

@Injectable()
export class CreateSession {
    constructor(@Inject(USER_REPOSITORY) private userRepository: IUserRepository,
        @Inject(SESSION_REPOSITORY) private sessionRepository: ISessionRepository) {

    }

    execute(creatorId: number): Session {
        const creator = this.userRepository.getById(creatorId);

        const session = new Session(creator);

        this.sessionRepository.save(session);

        return session;
    }
}