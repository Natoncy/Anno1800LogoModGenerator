# Anno 1800 Logo Mod Generator

This mod takes one or more pngs and outputs a ready to use mod for Anno 1800 that add the .pngs as selectable player logos.

## How to use:
1. Download the latest release 
2. Unzip in any folder
3. Create your logo as a .png and save it inside the input subfolder
    1. The .png should be square and at least 512x512, otherwise it will be stretched or pixilated.
    2. Background needs to be transparent.
    3. Color of the logo does not matter, it will be colored in the player color.
    4. You can add multiple pngs. The mod will then contain all of them.
4. Run the program by double clicking the .exe or running it in the terminal
    1. You can pass any number as the first argument to use it as the guid of the first logo. Subsequent logos will auto-increment by 1.
    2. If no guid is specified it will start with the first personal range guid (2.001.000.000)
    3. Be aware that you should not publish a mod that uses the personal guid range and that you only use your own claimed guids if you publish it.
    4. For more information see [Community GUID Ranges](https://github.com/anno-mods/GuidRanges)
5. A console window should open with more information. 
6. A .zip file was created if the program was successful
7. Unzip the folder in you mod folder (for example %userprofile%/Documents/Anno 1800/mods or wherever the game loads mods from)
8. Only if you want to publish it: Customize the modinfo.json with your information and make sure it only uses guids in your claimed guid range
9. Start or restart Anno 1800 and you can choose your logo(s) when starting a new game. The logos should appear at the end of the logo list
