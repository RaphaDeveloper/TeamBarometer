import { Injectable } from "@nestjs/common";
import { Session } from "./session";
import { PrismaService } from "src/prisma/prisma.service";

export const SESSION_REPOSITORY = 'SESSION_REPOSITORY';
export interface ISessionRepository {
    save(session: Session): any;
    getAll(): Promise<Array<Session>>;
}

@Injectable()
export class InMemorySessionRepository implements ISessionRepository {
    getAll(): Promise<Array<Session>> {
        throw new Error("Method not implemented.");
    }
    sessions: Array<Session> = [];

    save(session: Session) {
        session.id = this.sessions.length + 1;
        this.sessions.push(session);
    }
}

@Injectable()
export class PrismaSessionRepository implements ISessionRepository {
    constructor(private prismaService: PrismaService) { }

    async save(session: Session) {
        await this.prismaService.session.create({
            data: { createdAt: session.createdAt, creatorId: session.creator.id }
        });
    }

    async getAll(): Promise<Array<Session>> {
        const sessions = await this.prismaService.session.findMany({
            include: {
                creator: true
            }
        });

        return sessions.map(s => new Session(s));
    }
}