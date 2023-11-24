# Unity_Core
This Unity Core contains various helper classes and scripts, intended for use in the Unity Game Engine.
The goal of this core is to aid in more quickly and efficiently working on projects, while still staying organized in the process.
This core is heavily targeted towards 2D game development.

Created by TheGreatExpanse (https://www.youtube.com/@The_Great_Expanse)


# Dependencies:
- UltEvents - https://assetstore.unity.com/packages/tools/gui/ultevents-111307
- More Effective Coroutines - https://assetstore.unity.com/packages/tools/animation/more-effective-coroutines-free-54975



# CONTENTS:
/**Audio**  
 Contains Audio related scripts.  
- *AudioData.cs*  
 Intended to store audio data.  
- *AudioManager.cs*  
 To be used as a singleton and called statically.  
  

/**Game**  
Contains scripts designed for more specific game types.  
Still general enough to be applied across multiple 'similar' games.  
- /**AI**  
Contains scripts for more specific non-player behavior.  
  - *EnemyWalkAI.cs*  
  Makes a gameobject walk forward, until it hits a wall, in which case it turns around.  

- /**Entity**  
Contains scripts for creating gameobjects with health and the ability to take damage.  
  - *Entity.cs*  
  Makes a gameobject an entity, capable of taking damage.  
  - *EntityData.cs*  
  Scriptable Object holding various entity data.  

- /**Entity Effects**  
Contains scripts for interacting with entities.  
  - *ContinuousDamageCollider.cs*  
  Collider that damages the entity touching it continuously.  
  - *DamageCollider.cs*  
  Collider that damages the entity touching it.  
  - *DamageTrigger.cs*  
  Trigger that damages the entity touching it.  
  - *DeathTrigger.cs*  
  Trigger that instantly kills the entity that collides with it.  

- /**Other**  
Contains other miscellaneous scripts.  
  - *BreakOnTerrain.cs*  
  Causes the gameobject to self destruct when colliding with terrain.

- /**Player**  
Contains scripts for player inputs.  
  - *CursorController.cs*  
  Script that extends PlayerControllable, to control a gameobject to follow the mouse.  
  - *InputManager.cs*  
  Singleton script that manages all PlayerControllable gameobjects.  
  - *PlayerControllable.cs*  
  Abtract class that provides methods for various player inputs.  

/**General**  
Contains scripts for general purposes.  


/**Helpers**  
Contains helper classes to be called from within other scripts.  

/**Rendering**  
Contains various shaders, mainly aimed at 2D use.  


# Good Practices:
- Tag all Entities as 'Entity'
- Tag all terrain as 'Terrain'
