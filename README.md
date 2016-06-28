# Wundee
Specialisation 2 project

## Goal
The goal for this experiment was to dive head-first into creating a world simulation. Pulling from multiple resources and past experiences to create a setting where numerous settlements live out their lives and aspirations.

## Unity Project
Located in /WundeeUnity.  
Uses the most recent version of Unity as of the time of writing: 5.3.5f1  

### Project Structure
All gameplay code is framework-independant C# code and lives in the "Wundee" namespace. "Wundee" math modules were taken from the open-source MonoGame project. Unity specific code that interfaces with Wundee lives in the "WundeeUnity" namespace. 

All content is defined in YAML files, because few iterations into the project JSON proved to be too verbose.  
The main agents in this simulation are *Settlement*s. In order to satisfy their *Need*s, they look for a *Story* to follow which based on *Condition*s and *Effect*s may replenish these needs. Example content is currently in place, awaiting more comprehensive work...   
Any user is advised to install a linter plugin with YAML support to help when authoring content.
