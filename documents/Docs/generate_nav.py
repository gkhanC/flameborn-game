import os
import yaml

def generate_nav(directory):
    nav = [{'Home': 'index.md'}]
    for root, _, files in os.walk(directory):
        for file in files:
            if file.endswith('.md') and file != 'index.md':
                relative_path = os.path.relpath(os.path.join(root, file), directory)
                nav.append({file.replace('.md', ''): relative_path})
    return nav

if __name__ == "__main__":
    docs_dir = r'C:\Users\gkhan\Documents\Development\Games\flameborn\flameborn-game\documents\Docs'
    config_file = os.path.join(docs_dir, 'mkdocs.yml')

    with open(config_file, 'r') as f:
        config = yaml.safe_load(f)

    config['nav'] = generate_nav(docs_dir)

    with open(config_file, 'w') as f:
        yaml.safe_dump(config, f, sort_keys=False)

    print(f'Updated {config_file} with new navigation entries.')
