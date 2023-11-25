export class User {
    id: number;

    username: string;
    password: string;

    constructor(data: any) {
        Object.assign(this, data);
    }
}