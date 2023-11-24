import { User } from "src/users/user";

export class Session {
    id: number;
    creator: any;
    participants: Array<any> = [];
    date: Date;

    constructor(creator: User | undefined) {
        this.creator = creator;
        this.date = new Date();
    }
}