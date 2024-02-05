# temporary code just some string replacements

import os
import asyncio
import inflect

template_name = "Pontaj"
template_extension = "Store.ts"

local_path = os.path.dirname(os.path.abspath(__file__))
models_path = os.path.join(local_path, "WebAPI", "Models")
template_path = os.path.join(local_path, "UI", "src", "stores", template_name + template_extension)
output_path = os.path.join(local_path, "UI", "src", "stores", "generated")

async def generateInterfaces(exceptions: list[str]):
    exceptions.append(template_name)
    generated_files = []
    for model in os.listdir(models_path):
        if model.endswith("Model.cs"):
            model_name = model.rstrip("Model.cs")
            if model_name not in exceptions:

                model_plural_name = inflect.engine().plural(model_name)
                path_to_save = os.path.join(output_path, model_name + template_extension)
                
                if not os.path.exists(path_to_save):
                    with open(template_path, "r") as f:
                        content = f.read()

                    # Content processing
                    content = content.replace("import type { User } from '@/stores/generated/UserStore''", "")
                    content = content.replace("export const usePontajStore = defineStore('Pontaj', {", f"export const usePontajStore = defineStore('{model_plural_name}', " + "{")
                    content = content.replace(template_name, model_name)
                    #
                    
                    with open(path_to_save, "w") as f:
                        f.write(content)

                    generated_files.append(path_to_save)

    if(generated_files):
        print("\nGenerated files:")
        for file in generated_files:
            print(file)
        print()

if __name__ == "__main__":
    exceptions = ["Audit", "Quizz"]
    asyncio.run(generateInterfaces(exceptions))
