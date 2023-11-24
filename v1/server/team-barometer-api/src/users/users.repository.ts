import { Injectable } from "@nestjs/common";
import { User } from "./user";

export const USER_REPOSITORY = 'USER_REPOSITORY';
export interface IUserRepository {
    getbyUsername(username: string): User | undefined;
    getById(id: number): User | undefined;
}

@Injectable()
export class InMemoryUserRepository implements IUserRepository {
    users: Array<User> = [];

    constructor() {
        const user = new User(1);
        user.username = 'raphael';
        user.password = '123456';

        this.users.push(user);
    }

    getbyUsername(username: string): User | undefined {
        return this.users.find(u => u.username == username);
    }

    getById(id: number): User | undefined {
        return this.users.find(u => u.id == id);
    }
}