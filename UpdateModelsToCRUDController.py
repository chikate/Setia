import os
import inflect

if __name__ == "__main__":
    local_path = os.path.dirname(os.path.abspath(__file__))
    models_path = os.path.join(local_path, "WebAPI", "Models")
    template_path = os.path.join(local_path, "WebAPI", "Controllers", "PontajController.cs")
    output_path = os.path.join(local_path, "WebAPI", "Controllers", "Generated")

    exceptions = ["Audit", "Quizz", "Pontaj"]

    for model_filename in os.listdir(models_path):
        if model_filename.endswith("Model.cs"):
            simple_model_filename = model_filename.rstrip("Model.cs")

            # Check if the current model filename is in the list of exceptions
            if simple_model_filename not in exceptions:
                with open(template_path, "r") as f:
                    content = f.read()

                content = content.replace(".Include(i => i.User)", "")
                content = content.replace("Pontaj", simple_model_filename)

                # Pluralize the simple_model_filename
                plural_model_filename = inflect.engine().plural(simple_model_filename)

                content = content.replace(f"_context.{simple_model_filename}", f"_context.{plural_model_filename}")

                with open(os.path.join(output_path, plural_model_filename + "Controller.cs"), "w") as f:
                    f.write(content)