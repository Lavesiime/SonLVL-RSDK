# SonLVL-RSDK

**A level editor for Retro Engine v3 (Sonic CD) and v4 (Sonic 1/2).**

This editor is based on the original [SonLVL](https://github.com/sonicretro/SonLVL) tool, but adapted for the Retro Engine. It supports level editing, from layer editing to object placement to more. Included are project files, for:

- _Sonic CD_ ([2011 Steam](https://store.steampowered.com/app/200940/Sonic_CD/) / [2011 Mobile](https://info.sonicretro.org/Sonic_the_Hedgehog_CD_(2011)) / [Origins](https://store.steampowered.com/app/1794960/Sonic_Origins/))
- _Sonic 1_ ([2013](https://info.sonicretro.org/Sonic_the_Hedgehog_(2013_game)) / [Origins](https://store.steampowered.com/app/1794960/Sonic_Origins/))
- _Sonic 2_ ([2013](https://info.sonicretro.org/Sonic_the_Hedgehog_2_(2013)) / [Origins](https://store.steampowered.com/app/1794960/Sonic_Origins/))
- [_Sonic Forever_](https://teamforeveronline.wixsite.com/home/sonic-1-forever)
- [_Sonic 2 Absolute_](https://teamforeveronline.wixsite.com/home/sonic-2-absolute)

<img width="3080" height="1904" alt="image" src="https://github.com/user-attachments/assets/c74380f6-a48e-4319-a3be-9a62408f949a" />

---

## Download

The latest stable build can always be found on the editor's [GameBanana page](https://gamebanana.com/tools/8369), as well as the [_Releases_ tab](https://github.com/Lavesiime/SonLVL-RSDK/releases). Alternatively, for more experimental "nightly" versions of the editor, the repository features autobuilds that can be downloaded from the [_Actions_ tab](https://github.com/Lavesiime/SonLVL-RSDK/actions). However, do note that these versions may contain bugs and incomplete/experimental features, so use them at your own risk!

## Documentation

See the [wiki](https://github.com/Lavesiime/SonLVL-RSDK/wiki) for more information on the editor, from setup to guides and more. (Work in progress!)

## Building

0. Install Visual Studio, with the **.NET desktop development** workload included.
1. Clone the repository.
2. Open `SonLVL-RSDK.sln` in Visual Studio.
3. Build the solution.

---

## Projects

In addition to the SonLVL-RSDK editor itself, this repository also features a complete set of project files and object definitions for *Sonic 1*, *Sonic 2*, *Sonic CD*, *Sonic Forever*, and *Sonic 2 Absolute*, located in the [`Project Files`](Project%20Files) folder.

### Additional Tools

Additional, smaller bonus tools included in the repository. For more information, refer to each tool's own README.

- [`LevelConverter`](LevelConverter): Converts levels between RSDK versions.
- [`ObjDefGen`](ObjDefGen): Generates basic SonLVL-RSDK object definitions from object scripts.
- [`v5ImageImport`](v5ImageImport): Converts image maps to RSDKv5 scenes.
