export interface IUserRepository {
    getById(id: number): User;

}

export class User {
    id: number;
    
    constructor(id: number) {
        this.id = id;
    }
}

export class Session {
    creator: any;
    participants: Array<any> = [];
    date: Date;
    
    constructor(creator: User) {
        this.creator = creator;
        this.date = new Date();
    }
}

export class CreateSession {
    constructor(private userRepository: IUserRepository) {

    }

    execute(creatorId: number): Session {
        const creator = this.userRepository.getById(creatorId);

        return new Session(creator);
    }    
}