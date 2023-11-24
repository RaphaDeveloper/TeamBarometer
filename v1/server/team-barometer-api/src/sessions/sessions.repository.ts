import { Injectable } from "@nestjs/common";
import { Session } from "./session";

export const SESSION_REPOSITORY = 'SESSION_REPOSITORY';
export interface ISessionRepository {
    save(session: Session): any;
}

@Injectable()
export class InMemorySessionRepository implements ISessionRepository {
    sessions: Array<Session> = [];

    save(session: Session) {
        session.id = this.sessions.length + 1;
        this.sessions.push(session);
    }
}