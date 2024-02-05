# temporary code just some string replacements

import asyncio
from GenerateControllers import generateControllers
from GenerateInterfaces import generateInterfaces

exceptions = ["Audit", "Quizz", "Pontaj"]

async def generate():
    await generateControllers(exceptions)
    await generateInterfaces(exceptions)

    print("\nComplet!\n")

if __name__ == "__main__":
    asyncio.run(generate())