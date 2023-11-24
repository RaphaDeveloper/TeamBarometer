import { Controller, HttpCode, HttpStatus, Post, UseGuards, Request } from '@nestjs/common';
import { CreateSession } from 'src/sessions/use-cases/create-session/create-session';
import { AuthGuard } from '../auth/auth.guard';

@Controller('sessions')
export class SessionsController {
  constructor(private readonly createSession: CreateSession) {}

  @UseGuards(AuthGuard)
  @Post()
  @HttpCode(HttpStatus.CREATED)
  postSession(@Request() req: any) {
    return this.createSession.execute(req.user.sub);
  }
}