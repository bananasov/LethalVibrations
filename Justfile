set shell := ["sh", "-c"]
set windows-shell := ["pwsh.exe", "-c"]

alias b := build
alias c := copy

export COMPANY_DIRECTORY := "C:\\Program Files (x86)\\Steam\\steamapps\\common\\Lethal Company"
export FRAMEWORK := "net47"
export RELEASE_TARGET := "Debug"

current_directory := invocation_directory()
binary_directory := current_directory / "LethalVibrations" / "bin" / RELEASE_TARGET / FRAMEWORK

dll_file := "LethalVibrations.dll"
pdb_file := "LethalVibrations.pdb"

bepinex_plugin_directory := COMPANY_DIRECTORY / "BepInEx" / "plugins"

test:
    echo "{{bepinex_plugin_directory}}"

build:
    dotnet build -c {{RELEASE_TARGET}}

copy: build
    cp "{{binary_directory / dll_file}}" "{{bepinex_plugin_directory / dll_file}}"
    cp "{{binary_directory / pdb_file}}" "{{bepinex_plugin_directory / pdb_file}}"
    
