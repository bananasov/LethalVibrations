set shell := ["sh", "-c"]
set windows-shell := ["pwsh.exe", "-c"]

alias b := build
alias c := copy

export COMPANY_DIRECTORY := "C:\\Program Files (x86)\\Steam\\steamapps\\common\\Lethal Company"
export FRAMEWORK := "net472"
export RELEASE_TARGET := "Debug"
export PROJECT_NAME := "LethalVibrations"

current_directory := invocation_directory()
binary_directory := current_directory / PROJECT_NAME / "bin"
built_files_directory := binary_directory / RELEASE_TARGET / FRAMEWORK
release_directory := binary_directory / "Release" / FRAMEWORK

dll_file := PROJECT_NAME + ".dll"

bepinex_plugin_directory := COMPANY_DIRECTORY / "BepInEx" / "plugins"

version := `git cliff --unreleased --bump --context | jq -r .[0].version`
unfucked_version := replace(version, "v", "")

# Build the project
build *FLAGS:
    dotnet build {{FLAGS}}

# Packages the files for Thunderstore
package: (build "-c Release")
    git cliff --bump --exclude-path "Thunderstore/*" -o "Thunderstore/CHANGELOG.md"

    mkdir "Thunderstore/BepInEx/plugins"
    cp "{{release_directory / dll_file}}" "Thunderstore/BepInEx/plugins/"
    jq --raw-output '.version_number = "{{trim_end(version)}}"' "Thunderstore/manifest.json" > "Thunderstore/manifest.json.tmp"
    rm "Thunderstore/manifest.json"
    mv "Thunderstore/manifest.json.tmp" "Thunderstore/manifest.json"
    7z a LethalVibrations.zip "./Thunderstore/*"
    rm "Thunderstore/BepInEx"

# Copies over the built DLLs over to the BepInEx install
copy:
    cp "{{built_files_directory / dll_file}}" "{{bepinex_plugin_directory / dll_file}}"

# Removes the DLL files from the BepInEx install
clean:
    rm "{{bepinex_plugin_directory / dll_file}}"
