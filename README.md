# Platformer Demo

All assets in this project made by me. (Except files in ./Assets/Textures and unity built-in features.)

*This is a developer demo, not a future game demo.*

Screenshoots
-
### HLSL Shaders

### `Background Shader`

Takes 3 texture and process pixels according to pixels alpha value, vertex's world position and layer's change speed. If you set layer's speed to 1, layer will  change same speed as world position. If you set 0, background won't change. 

* To the material work properly, link background object's position with camera position.
* You can assign any texture you wish.

![Background1](https://user-images.githubusercontent.com/63978053/165937020-4c0259cd-ac11-4a1c-b6be-4968b245d933.png)
![Background3](https://user-images.githubusercontent.com/63978053/165937024-5f436656-2693-473d-9e8f-9c4bf283dacd.png)

![Background2](https://user-images.githubusercontent.com/63978053/165960046-40d4170c-a5f8-4881-b4ab-c5f9003a142e.png)
![Background4](https://user-images.githubusercontent.com/63978053/165937026-3f216dde-63aa-4371-b566-c8376017832d.png)

### `Platform Shader`

Areas that look upward take edge texture's value, otherwise take fill texture's value.

* Made to work with '2D Closed Sprite Shape'
* Must check 'Enable Tangents'
* You can assign any texture you wish
* By default, PlatformEdge's Fill Texture is same with PlatformFill's Fill Texture

![Platformer1](https://user-images.githubusercontent.com/63978053/165940235-f1ab0446-6d4a-407f-8363-cd23884c4b8e.png)
![Platformer2](https://user-images.githubusercontent.com/63978053/165940241-f029639c-d420-4379-bf98-e3acb599e5f2.png)

### `Mace Shader`

It's a basic shader animation. Mace's color changes continously on runtime. 

* Only works with Mace's texture.

![Mace1](https://user-images.githubusercontent.com/63978053/165941086-596c38df-764a-4827-b1e2-42fcf0cdca77.png)
![Mace2](https://user-images.githubusercontent.com/63978053/165941091-446d4a46-4ee5-4465-9b06-331118b78c20.png)

 
 ### Triggers - Object Pool
  
`Trigger2D`

 In a plaformer game, there are a lot death, trap, checkpoint etc., Because of that I made a base class for common usage. 
 
 Sends command to GameManager based on its type.

![Trigger2D3](https://user-images.githubusercontent.com/63978053/165941819-ed50c2bb-e206-40a9-a9a8-77a244e06444.png)
![Trigger2D2](https://user-images.githubusercontent.com/63978053/165941826-324ecfd1-2d5f-4623-93f4-0bbaad27bf35.png)
![Trigger2D1](https://user-images.githubusercontent.com/63978053/165941823-ba7a4487-b451-4956-8683-86bdbd4d0714.png)

`Object Pool`

Simple Object Pool manager. 

* Provides object when needed. If there is no object in pool, instantiates new object and returns it.
* When an object returns pool, if pool is full, destroys objects.
 
![Objectpool4](https://user-images.githubusercontent.com/63978053/165955565-38cc97cd-91a5-4f70-86ce-c163a2722a18.png)
![Objectpool2](https://user-images.githubusercontent.com/63978053/165955570-77d067a5-b1b8-4c34-b2ba-ec243a6ea66b.png)
![Objectpool3](https://user-images.githubusercontent.com/63978053/165955574-71aac0a7-b0fc-4a5f-891d-d707ae3ad5a2.png)


`Trap Trigger`

Subclass of Trigger2D.

When triggered, Gets an object from pool and add BoobyTrap component to it.

* The Object goes back pool after when disable time is up.
* If disable time is set to 0, the object won't goes back.
* Can be triggered once
* When the game resets to a checkpoint, can be triggered again.
* When the game resets to a checkpoint, if pooled object is still active, returns the object to pool.
* Can assign different materials to same object type.

![TrapTrigger2](https://user-images.githubusercontent.com/63978053/165955723-61d0c102-2231-407b-88d6-37cd8b6884cd.png)
![TrapTrigger](https://user-images.githubusercontent.com/63978053/165955717-3ecef11a-4d52-4f1b-ab73-4797505152f6.png)
![TrapTrigger3](https://user-images.githubusercontent.com/63978053/165955712-472c9559-74a8-4d70-be3a-4b84b354728c.png)


`Decor Trigger`

When triggered, Gets an object from pool and add Decor component to it.

* Can be triggered once
* When the game resets to a checkpoint, can be triggered again.
* When the game resets to a checkpoint, if pooled object is still active, returns the object to pool.
* Can assign different materials to same object type.

![DecorTrigger](https://user-images.githubusercontent.com/63978053/165959023-1d3af7e4-c12a-4580-9a04-4e55484184af.png)
![DecorTrigger2](https://user-images.githubusercontent.com/63978053/165959029-b4d04198-21d8-4da0-b7f1-b493c32d736f.png)
![DecorTrigger3](https://user-images.githubusercontent.com/63978053/165959031-3bc932bc-d129-45c7-966f-74c4cd844acc.png)


### Others

`GameManager-UIManager-DefaultManager`

`Player-Camera-Platform Moves`

`Animation-Animator`


:collision: I will continously update the project and refactor it. :collision:

`What will`
- [ ] 2D Lighting
- [ ] Advanced data system (For each persistent and runtime data flow)
- [ ] Procedural meshes that work with custom shaders better
- [ ] Redesign trigger and pooling system. 
- [ ] Adding an advanced control system instead of this 
- [ ] NPCs and Enemies
- [ ] VFX-SFX
- [ ] Documentaiton&Comments
- [ ] New mechanics
- [ ] UI and HUD system
- [ ] New and longer levels


Credit
-
[Knight Sprite Sheet (Free) - Kin Ng](https://assetstore.unity.com/packages/2d/characters/knight-sprite-sheet-free-93897)
> * `./Assets/Textures/Knight/*` 

[Free Platform Game Assets - Bayat Games](https://assetstore.unity.com/packages/2d/environments/free-platform-game-assets-85838)
> * `./Assets/Textures` 
>   - `/Background/*`
>   - `/Enviroment/*`
>   - `/Platforms/*`

