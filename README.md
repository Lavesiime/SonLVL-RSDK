# SonLVL-RSDK

**A level editor for Retro Engine v3 (Sonic CD 2011/Origins) and v4 (Sonic 1/2 2013/Origins).**

This editor is based on the original [SonLVL](https://github.com/sonicretro/SonLVL) tool, but adapted for the Retro Engine. It supports level editing, from layer editing to object placement to more. Included are project files, for:

- _Sonic CD_ (2011 / Origins)
- _Sonic 1_ (2013 / Origins)
- _Sonic 2_ (2013 / Origins)
- _Sonic Forever_
- _Sonic 2 Absolute_

---

## Download

The latest stable build can always be found in the [Releases tab](https://github.com/Lavesiime/SonLVL-RSDK/releases). Alternatively, for more experimental "nightly" versions of the editor, the repository features autobuilds that can be downloaded from the [_Actions_ tab](https://github.com/Lavesiime/SonLVL-RSDK/actions). However, do note that these versions may contain unfixed bugs or incomplete features, so use them at your own risk!

## Documentation

See the [wiki](https://github.com/Lavesiime/SonLVL-RSDK/wiki) for more information on the editor, from setup to guides and more. (Work in progress!)

## Building

0. Install the **WinForms** workload in Visual Studio.
1. Clone the repository.
2. Open `SonLVL-RSDK.sln`.
3. Build the solution.

---

## Projects

In addition to the SonLVL-RSDK editor itself, this repository also features a complete set of project files and object definitions for *Sonic 1*, *Sonic 2*, *Sonic CD*, *Sonic Forever*, and *Sonic 2 Absolute*, located in the [`Project Files`](Project%20Files) folder.

### Additional Tools

Additional, smaller bonus tools included in the repository. For more information, refer to each tool's own README.

- [`LevelConverter`](LevelConverter): Converts levels between RSDK versions.
- [`ObjDefGen`](ObjDefGen): Generates basic SonLVL-RSDK object definitions from object scripts.
- [`v5ImageImport`](v5ImageImport): Converts image maps to RSDKv5 scenes.
