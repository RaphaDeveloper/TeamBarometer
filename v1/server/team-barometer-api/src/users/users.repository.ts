import { Injectable } from "@nestjs/common";
import { User } from "./user";
import { PrismaService } from "src/prisma/prisma.service";

export const USER_REPOSITORY = 'USER_REPOSITORY';
export interface IUserRepository {
    getByUsername(username: string): Promise<User | undefined>;
    getById(id: number): Promise<User | undefined>;
}

@Injectable()
export class InMemoryUserRepository implements IUserRepository {
    users: Array<User> = [];

    constructor() {
        const user = new User( { id: 1, username: 'raphael', password: '123456' });

        this.users.push(user);
    }

    getByUsername(username: string): Promise<User | undefined> {
        return Promise.resolve(this.users.find(u => u.username == username));
    }

    getById(id: number): Promise<User | undefined> {
        return Promise.resolve(this.users.find(u => u.id == id));
    }
}

@Injectable()
export class PrismaUserRepository implements IUserRepository {
    constructor(private prismaService: PrismaService) {}

    async getByUsername(username: string): Promise<User | undefined> {
        const userDto = await this.prismaService.user.findUnique({
            where: { username }
        });

        return new User(userDto);
    }

    async getById(id: number): Promise<User | undefined> {
        const userDto = await this.prismaService.user.findUnique({
            where: { id }
        });

        return new User(userDto);
    }
}