import { Module } from '@nestjs/common';
import { AppController } from './app.controller';
import { AppService } from './app.service';
import { AuthController } from './auth/auth.controller';
import { CreateSession } from './sessions/use-cases/create-session/create-session';
import { JwtModule } from '@nestjs/jwt';
import { jwtConstants } from './auth/auth.constants';
import { SessionsController } from './sessions/sessions.controller';
import { AuthService } from './auth/auth.service';
import { InMemoryUserRepository, USER_REPOSITORY } from './users/users.repository';
import { InMemorySessionRepository, SESSION_REPOSITORY } from './sessions/sessions.repository';

const userRepositoryProvider = {
  provide: USER_REPOSITORY,
  useClass: InMemoryUserRepository,
};

const sessionRepositoryProvider = {
  provide: SESSION_REPOSITORY,
  useClass: InMemorySessionRepository,
};

@Module({
  imports: [
    JwtModule.register({
      global: true,
      secret: jwtConstants.secret,
      signOptions: { expiresIn: '60s' },
    }),
  ],
  controllers: [AppController, AuthController, SessionsController],
  providers: [AppService, AuthService, CreateSession, userRepositoryProvider, sessionRepositoryProvider],
})
export class AppModule { }
