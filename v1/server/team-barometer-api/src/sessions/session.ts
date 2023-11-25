import { User } from "src/users/user";

export class Session {
    id: number;
    creator: User;
    participants: Array<any> = [];
    createdAt: Date;

    constructor({ creator, ...data }: any) {
        this.creator = creator;
        Object.assign(this, data);
    }
}