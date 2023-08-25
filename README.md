# BizHawk External Tool for ShinyHunting in Pokemon 3rd Gen

This is a BizHawk External Tool that works with the Pokebot found here [40Cakes/pokebot-bizhawk](https://github.com/40Cakes/pokebot-bizhawk)

It replaces the luascript to be loaded into BizHawk

by default it uses the `bot_instance_id` of `pokebot_cs`. it can be changed in the Tool Window, but you need to Restart the Emulation core afterwards to initialize the new InstanceID correctly.

To Build from Source copy BizHawk into a folder called `BizHawk` and open the Visual Studio Project. The Build dll wil be copied automatically into the `ExternalTools` Folder of BizHawk.

