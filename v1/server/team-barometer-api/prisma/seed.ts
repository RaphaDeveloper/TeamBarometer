import { PrismaClient } from "@prisma/client";

const prisma = new PrismaClient();

async function main() {
    await prisma.user.upsert({
        where: { username: 'raphael' },
        update: {},
        create: { username: 'raphael', password: '123456' }
    });
}

main()
    .catch((e) => {
        console.error(e);
        process.exit(1);
    })
    .finally(async () => {
        prisma.$disconnect();
    });