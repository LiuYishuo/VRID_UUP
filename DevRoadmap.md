# Development Roadmap for Team VRID

## Current Week - 20170206

### Everyone >> Preparing the tools
1. Clone this repo
2. Download and Install Unity 5.6.0b6, version matters
3. Download Oculus SDK (url: https://developer3.oculus.com/downloads/game-engines/1.10.0/Oculus_Utilities_for_Unity_5/ )
4. Run and test the Unity project on your machine
5. Apply for the VR Lab access

### Development
1. Unity >> Test Project Settings
  * the basic settings have been done, test using Oculus to see if there's any issue
2. Unity >> Character Movement
  * use teleport for major movement in the virtual space
  * use location tracking data for near field movement
3. Voice Command >> Feasibility
  * finish a primary analysis of available voice command platforms and their compatibility with Unity (with Oculus),

### Design
1. Support Interaction Design
  Collaborate with Unity development member and support designing:
  * interaction for character movement: use what buttons and how
2. Model & Rendering
  * fix irregularities of the 5 apartment models included in the Unity project
3. Competitor Analysis - Interaction
   Try out and summary interaction designs used by other interior design tools/platforms, including:
   * IKEA Home Planner
   * AutoDesk HomeStyler
   * Adornably
   * Houzz
   * Lowe for HoloLens (prototype)
   * others if related


## --Future--

## Week - 20170213

### Development
  1. Unity >> Menu System
    * implement a menu system using Canvas and Panels in Unity
    * implement interaction with the menu system, in collaboration with designer
    * represent each item using sprite in the menu
    * should be able to change categories, select from the menu and initiate the furniture model in the apartment
    * a functional prototype is the top priority, no need to make it fancy, leave the optimization of UX for later weeks
  2. Voice Command >> Implementation
    * discuss voice command integration with Unity development and designer
    * implement voice command for interacting with the menu system

### Design
  1. Support Interaction Design
    Collaborate with Unity development member and support designing:
    * furniture categories
    * menu system layout
    * interaction with the menu system
  2. Model & Rendering
    * fix irregularities of all the furniture models
    * learn the illumination (lights) settings and make the change in Unity


## Week - 20170220

### Development
  1. Unity >> Furniture Manipulation
    * implement furniture manipulation, in collaboration with designer; all direct manipulation for this week
    * should be capable of: translation, rotation, and deletion
  2. Voice Command >> Furniture Search
    * prototype fuzzy furniture search (rather than step-by-step selection) for the menu system, using voice command
    * could use an alternative Panel
    * implement search by name, and by category for this week

### Design
  1. Support Interaction Design
    Collaborate with Unity development member and support designing:
    * interaction for furniture manipulation, keep a focus on key mapping -- how to reduce the conflicts and confusion when character movement, menu system, and manipulation are all happening with only ~16 keys on the game controller
  2. Model & Rendering
    * improve the rendering parameters in Unity, in collaboration with Unity development


## Week - 20170227

### Development
  1. Unity >> Enhanced Manipulation
    Provide assistance in furniture manipulation
    * implement angular auto-alignment when placing furniture near a wall or corner; pay attention to when to trigger
  2. Voice Command >> Improve Menu Integration
    * provide more alternative ways of searching furniture, such as by colour, by material, etc.

### Design
  1. Support Interaction Design
    * auto-alignment triggering mechanism
    * help improve voice command search
  2. Model & Rendering
    * search and import more furniture models into the system
    * optimize materials used in the project: reduce the amount if possible; improve the texture and rendering parameters
    * analyze online furniture stores' searching mechanism design
    * add additional and useful information to all the furniture models, could in form of tags; discuss this with voice command development


<!-- ## Week - 20170306


## Week - 20170313


## Week - 20170320


## Week - 20170327


## Week - 20170403


## Week - 20170410


## Week - 20170417


## Week - 20170424


## Week - 20170501


## Week - 20170508 -->
