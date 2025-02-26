readme is a heavy todo, don't mind the dust!!-

# SonLVL-RSDK

SonLVL-RSDK is a level editor for RSDKv3 (Sonic CD) and RSDKv4 (Sonic 1/2) games, forked from [the original SonLVL level editor](https://github.com/sonicretro/SonLVL).

## Releases

(not yet, sorry!)

## Building

0. In Visual Studio, ensure that you have the WinForms workload installed.
1. Download/clone this repository.
2. Open `SonLVL-RSDK.sln` in Visual Studio.
3. Build the solution.

## Projects

### [SonLVL-RSDK](SonLVL)

The main program.

### [Project Files](Project%20Files)

A complete set of project files and object definitions for *Sonic 1*, *Sonic 2*, and *Sonic CD*, as well as *Sonic Forever* and *Sonic 2 Absolute*. The *Sonic 1*, *Sonic 2*, and *Sonic CD* files are compatible with both the latest mobile releases as well as the versions of the games in *Sonic Origins* (2.0.2).

### [LevelConverter](LevelConverter)

A program to convert stages between Retro Engine versions (v3, v4, v5).

### [ObjDefGen](ObjDefGen)

A command line tool to generate basic object definitions for a game by using the first SpriteFrame in every object script. 

### [v5ImageImport](v5ImageImport)

A program to convert an image into a RSDKv5 level map.
