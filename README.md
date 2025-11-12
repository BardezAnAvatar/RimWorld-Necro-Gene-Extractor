# Necro Genepack Extractor Tiers

# About
This repository is a mod for the game [RimWorld](https://rimworldgame.com/). Its purpose is to extract genetic material off of
dead bodies in the game (inspired behavior by the mod [Necrogene](https://steamcommunity.com/sharedfiles/filedetails/?id=2981697023), but I don't _quite_ like its behavior).
This mod is an extension of [Gene Extractor Tiers](https://github.com/RedMattis/Gene-Extractor/).

# Features
- New buildings, to extract genetic material from corpses:
  - No Tier I Extractor (presume that is the building from **Necrogene**, above).
  - Tier II Extractor 
    - Plop a dead body into the extractor
	- Pulls random non-archite genetics from the corpse
	- Ejects the corpse when the user chooses to cancel operations
  - Tier III (Archite I) Extractor 
    - Plop a dead body into the extractor
	- Pulls random genetics from the corpse
	    - Archite genes are extracted as well
	- Ejects the corpse when the user chooses to cancel operations
	- Works faster than the Tier II model
  - Tier IV (Archite II) Extractor
    - Plop a dead body into the extractor
	- Pulls random genetics from the corpse
	    - Archite genes are extracted as well
	- Ejects the corpse when the user chooses to cancel operations
	- Works faster than the Tier III model
	- Allows targeting of specific genes
- Uses neutroamine rather than nutrition, since the bodies are... dead.
  - Bodies that are fresh require less neutroamine than rotting corpses
- Mod settings menu, where setting
  - Building cost and stats
  - Neutroamine required for fresh corpse
  - Neutroamine cost multiplier for rotting corpse
  - Neutroamine cost multiplier for dessicated corpse
  - Time modifiers for above
  - Whether the above corpse states are allowed


# Changelog
All notable changes to this project will be documented in this file.