set shell := ["sh", "-c"]
set windows-shell := ["pwsh.exe", "-c"]

alias b := build
alias c := copy

export COMPANY_DIRECTORY := "C:\\Program Files (x86)\\Steam\\steamapps\\common\\Lethal Company"
export FRAMEWORK := "net47"
export RELEASE_TARGET := "Debug"
export PROJECT_NAME := "LethalVibrations"

current_directory := invocation_directory()
binary_directory := current_directory / "LethalVibrations" / "bin" / RELEASE_TARGET / FRAMEWORK

dll_file := PROJECT_NAME + ".dll"
pdb_file := PROJECT_NAME + ".pdb"

bepinex_plugin_directory := COMPANY_DIRECTORY / "BepInEx" / "plugins"

# Build the project
build:
    dotnet build -c {{RELEASE_TARGET}}

# Copies over the built DLLs over to the BepInEx install
copy:
    cp "{{binary_directory / dll_file}}" "{{bepinex_plugin_directory / dll_file}}"
    cp "{{binary_directory / pdb_file}}" "{{bepinex_plugin_directory / pdb_file}}"

# Removes the DLL files from the BepInEx install
clean:
    rm "{{bepinex_plugin_directory / dll_file}}"
    rm "{{bepinex_plugin_directory / pdb_file}}"
