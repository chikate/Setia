import os

if __name__ == "__main__":
    local_path = os.path.dirname(os.path.abspath(__file__))
    models_path = local_path + r"\WebAPI\Models"
    store_path = local_path + r"\UI\src\stores\PontajStore.ts"
    output_path = local_path + r"\UI\src\stores\generated" 
    for model_filename in os.listdir(models_path):
        if model_filename.endswith("Model.cs"):
            simple_model_filename = model_filename.rstrip('Model.cs')
            # for output_filename in os.listdir(models_path):
            #     if simple_model_filename != output_filename.rstrip('Store.ts'):
            with open(os.path.join(store_path), 'r') as f:
                content = f.read()

            content = content.replace('Pontaj', simple_model_filename)

            with open(os.path.join(output_path, simple_model_filename + "Store.ts"), 'w') as f:
                f.write(content)