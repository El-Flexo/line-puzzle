# Line puzzle (base gameplay)

Reference: [Jelly Splash](https://apps.facebook.com/jellysplash)

## Game features:
* 4 different gems colors;
* extra score for longer chains;
* minimum chain length - 4 gems;
* moves constrain for every level.

## Project features:
* clear and hight quality code;
* all in strange.ioc context for easy extending project
* every level data store in separate json files
* unit tests for all game logic
* best for 768x1024, but all UI elements stratched by uGUI

## Structure
* level defenitions contains at Assets/Resources/defs/levels
* element defenitions at Assets/Resources/defs/elements.json

Note: for extend number of gems you must add item here, add texture and add enum item to Elements.cs
* level prefabs contains at Assets/Resources/level/

Note: for extend number of levels or setup different level size you must add level prefab, setup camera and update your_best_new_level.json at Assets/Resources/defs/levels
* all gameplay models at Assets/Scripts/MVCS/model
* unit tests at tests/

Note: for run test on mac use tests/run.sh at terminal (be sure that you installed "xbuild")

All used graphic content form Kenney donation pack v33 (www.kenney.nl) - CC0 licence.