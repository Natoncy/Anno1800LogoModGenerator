# Anno 1800 Logo Mod Generator

This mod takes one or more .pngs and outputs a ready to use mod for Anno 1800 that add the .pngs as selectable player logos.

This tool uses the [annotex](https://github.com/jakobharder/annotex/) tool to convert the .png files to .dds. A version is bundled in the release, if you want to use a different version you can just replace the .exe.

## How to use:
1. Download the latest release 
2. Unzip in any folder
3. Create your logo as a .png and save it inside the input subfolder
    1. The .png should be square and at least 512x512 pixels, otherwise it will be stretched or pixilated.
    2. Background needs to be transparent.
    3. Color of the logo does not matter, it will be colored in the player color. Multiple colors are not possible.
    4. You should leave some margin around the logo, othrewise it will overlap in some places.
    5. You can add multiple .pngs. The mod will contain all of them.
4. Run the program by double clicking the .exe or running it in the terminal
    1. You can pass any number as the first argument to use it as the guid of the first logo. Subsequent logos will auto-increment by 1.
    2. If no guid is specified it will start with the first personal range guid (2.001.000.000)
    3. Be aware that you should not publish a mod that uses the personal guid range and that you only use your own claimed guids if you publish it.
    4. For more information see [Community GUID Ranges](https://github.com/anno-mods/GuidRanges)
5. A console window should open with more information. 
6. A logo_mod.zip file was created if the program was successful.
7. Create a folder inside your mod folder (for example %userprofile%/Documents/Anno 1800/mods or wherever the game loads mods from) then unzip the generated .zip file inside this new folder.
8. Only if you want to publish it: Customize the modinfo.json with your information and make sure it only uses guids in your claimed guid range. Make sure the encding is just UTF-8, without BOM
9. Start or restart Anno 1800 and you can choose your logo(s) when starting a new game. The logos should appear at the end of the logo list.

## Warning on removing logo mods
The logo selection will break if you load a savegame with a modded logo from a removed mod. You will not be able to select ANY new logo.
You must select a vanilla logo and save before you can safely remove the mod.
If you removed the mod and the logo selection is already broken you can readd the mod, restart the game and then change to a vanilla logo.
If you do not have the mod anymore you can also create a new mod with a placeholder logo with the same guid that is selected in the savegame.