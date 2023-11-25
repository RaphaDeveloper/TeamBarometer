import { Injectable } from '@nestjs/common';
import { PrismaClient } from '@prisma/client';

//Is this like a db context?
@Injectable()
export class PrismaService extends PrismaClient {}
