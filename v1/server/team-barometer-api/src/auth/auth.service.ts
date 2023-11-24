import { Inject, Injectable, UnauthorizedException } from "@nestjs/common";
import { JwtService } from "@nestjs/jwt";
import { IUserRepository, USER_REPOSITORY } from "src/users/users.repository";

@Injectable()
export class AuthService {
    constructor(@Inject(USER_REPOSITORY) private userRepository: IUserRepository, private jwtService: JwtService) {
        console.log('AuthService');
    }

    async signIn(username: string, pass: string): Promise<any> {
        const user = this.userRepository.getbyUsername(username);

        if (user?.password !== pass)
            throw new UnauthorizedException();

        const payload = { sub: user.id, username: user.username };

        return {
            access_token: await this.jwtService.signAsync(payload),
        };
    }
}