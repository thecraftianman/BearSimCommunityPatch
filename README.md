# BearSimCommunityPatch
A [Bear Simulator](https://store.steampowered.com/app/395850/Bear_Simulator/) mod to fix various bugs within the game.

## Installing
Download the latest DLL version from the Releases page, then move it into your `../Bear Simulator/BepInEx/plugins` folder.

## Building
Clone this repository, then copy `../Bear Simulator/Bear Simulator (OS Version)_Data/Managed/Assembly-CSharp.dll` from your game files to `../BearSimCommunityPatch/lib/Assembly-CSharp.dll` and run `dotnet build` to generate the plugin DLL.