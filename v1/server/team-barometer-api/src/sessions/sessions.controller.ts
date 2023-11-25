import { Controller, HttpCode, HttpStatus, Post, UseGuards, Request, Get, Inject } from '@nestjs/common';
import { CreateSession } from 'src/sessions/use-cases/create-session/create-session';
import { AuthGuard } from '../auth/auth.guard';
import { PrismaService } from 'src/prisma/prisma.service';
import { ISessionRepository, SESSION_REPOSITORY } from './sessions.repository';

@Controller('sessions')
export class SessionsController {
  constructor(private readonly createSession: CreateSession, 
    @Inject(SESSION_REPOSITORY) private sessionRepository: ISessionRepository) { }

  @UseGuards(AuthGuard)
  @Post()
  @HttpCode(HttpStatus.CREATED)
  postSession(@Request() req: any) {
    return this.createSession.execute(req.user.sub);
  }

  @Get()
  @HttpCode(HttpStatus.OK)
  getSessions() {
    return this.sessionRepository.getAll();
  }
}