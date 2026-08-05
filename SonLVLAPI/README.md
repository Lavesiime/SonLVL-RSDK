# SonLVLAPI

The API that powers SonLVL-RSDK, as well as the other handful of tools on the repo. This library contains the code for interacting with the files as well as other internal systems of the editor, while the [**SonLVL** folder](SonLVL) contains the actual GUI side of the program.

TODO: ig there should be more info here?

### Misc Information

The editor contains limits on things such as entity count and tile count, given that it targets the official version of the Retro Engine used in Sonic Origins and its standalone games. However, in order to change the limits, none of these actual limits are contained within SonLVL itself; instead, they're all a part of the [RSDKv3_4 library](Dependencies). As such, only that library needs to be changed in order to increase SonLVL's various limits; no editor code itself has to be modified. Once the library is recompiled, the resulting _RSDKv3_4.dll_ can simply be moved into the SonLVL folder to replace the existing version, at which point the new limits should be reflected in the editor.

#### Entity Limit

The maximum entity count is stored in _Scene.cs_, under the _ENTITY_LIST_SIZE_ constant. Normally, this value is 1024, or 0x400 in hexadecimal. Note that the file format for scenes stores the entity count within two bytes, so any value larger than 0xFFFF, or 65535 in decimal, will be unable to save properly without modifications to the file format.

#### Tile Limit

The number of 16x16 tiles in the stage is controlled in _Tileconfig.cs_, under the _TILE_LIST_SIZE_ constant. Normally, this number is 1024, or 0x400 in hexadecimal. Note that the file format for chunks stores the tile index in a space of 24 bits. As such, the largest tile index that can be used in a chunk (without altering the file format) is 0xFFF, or 4095 in decimal.

#### Chunk Limit

The chunk count is stored in _Tiles128x128.cs_, under the _CHUNK_LIST_SIZE_ constant. Normally, this value is 512, or 0x200 in hexadecimal. Note that the file format for scenes stores the chunk ID within two bytes, so any value larger than 0xFFFF, or 65535 in decimal, will be unable to save properly without modifications to the file format.
