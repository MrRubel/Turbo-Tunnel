# How to make a Turbo-tunnel effect in Unity using Particle System

Туториал на русском: https://dtf.ru/gamedev/109275-kak-sdelat-turbo-tonnel-effekt-v-unity-3d-s-pomoshchyu-particle-system

I don't know how to name this effect: hole, cave, turbo-tunnel, wormhole or, maybe, gullet, but the result looks like this:

![](1%20-%20WH%20result.gif)

Almost every game engine can be used for this, my first realization was made in Flash (Action Script 3). I'll use Unity here (Version 2019.2.4f1).

I added this effect to the opening cutscene and teaser of my game about the immune system and virus attacks, «[Listeria Wars](https://store.steampowered.com/app/1183010/Listeria_Wars/)».

[![](https://img.youtube.com/vi/FE37npMFFuQ/0.jpg)](https://www.youtube.com/watch?v=FE37npMFFuQ)

*[Click to watch teaser](https://www.youtube.com/watch?v=FE37npMFFuQ)*

## Step by step implementation
### Scene creation
I used the 2D template while creating the project, but you can use 3D. Anyway, you need orthographic projection at camera settings and solid black color.

![](https://leonardo.osnova.io/dd2a3130-3960-8bb6-75e7-ec3b72f6494f/-/resize/304/)

### Material creation
We need a material with a sprite of the tunnel for the main Particle System. I drew this sprite for a short time because the effect is dynamic and no one has time to appreciate your incredible details here. But it depends on you liking. Initially, the sprite was red but I turn it to grayscale to dynamic colorization.

![](https://leonardo.osnova.io/c4ddc1c7-c6b1-20b5-bbb7-d6befc8458aa/-/resize/600/)

Save our sprite to the Sprites folder in Assets and create new "Tunnel Wall" material (create the "Materials" folder, right mouse click → Create → Material). Select "Mobile/Particles/Alpha Blended" in shader settings (or Unity Particle/Additive old unity versions). Then select our wall sprite in "Particle Texture".

![](https://leonardo.osnova.io/a726fc6e-dd56-447b-2321-97dee8a61bff/-/resize/400/)

### Add the main Particle System
Add an empty object in the scene and rename it to "Tunnel". We will put our particles and controllers here. Click on it by right mouse button and select Effects → Particle System. Name it "Tunnel Wall". The most interesting part starts here! Settings and experiments.

The main point of these settings is appearing of tunnel walls step by step in order with increasing in size by lifetime. We will get the effect of moving forward through the tunnel.

Renderer
 - Material: Tunnel Wall;
 - Sort Mode: Oldest in Front;
 - Max Particle Size: 5 (by default (0.5) we can't increase the size of the sprites more than half of the screen).
 
Main (Appears by click on header of settings if they are hidden)
 - Start Lifetime 2.5 (Lifetime of a particle, also, it effects on the speed of increasing the size);
 - Start Speed: 0 (doesn't need the speed);
 - Start Size: 100 (Depends on sprite size, set with "Size over Lifetime");
 - Start Rotation: from 0 to 360 (Choose "Random Between Two Constants");
 - Start Color: Choose on your taste, if the Tunnel Wall sprite was grayscaled. Pay attention, light orange color will turn your turbo-tunnel to the rectum;
 - Gravity modifier: 0.02 (Add some gravity dynamic);
 - Simulation Space: World (We will move spawn point, set "world" to avoid the moving of the full construction);
 - Max Particles: 10 (See the total count of particles in Particle Effect panel while playing and set the same count).
 - Emission: Rate over Lifetime: 4 (on your liking)
 
Shape
 - Shape: Circle;
 - Radius: 0.05 (The less size the smoother walls).
 
Color over Lifetime

![](https://leonardo.osnova.io/925998fa-e833-d330-3a4c-f868156a7b6f/-/resize/400/)

*I set this for smooth appearing*

Size over Lifetime

![](https://leonardo.osnova.io/6a411d64-f94b-3602-0f78-9130cdcd3bf4/-/resize/308/)

*Size should be increased exponentially, but starts not from zero*

![](2%20WH%20walls.gif)

*The current result*
### Other particles
I added other cells and vessel particle systems by analogy. I will not describe it, there are small differences. Need to set greater "Order in Layer" in Renderer to appearing the particles in front of walls. Also, I used the "Velocity over Lifetime" setting and added vignette. And got this:

![](3%20WH%20other%20particles.gif)

### Add some dynamic
The simple moving of the Tunnel object turns it to non-linear. Add the Mover component to Tunnel and get wonder result 
```csharp
using UnityEngine;

public class Mover: MonoBehaviour {
    public float rangeX = 2;
    public float rangeY = 1.5f;
    public float timeDelimiterX = 4f;
    public float timeDelimiterY = 3f;

    void Update() {
        transform.position = new Vector3(
            Mathf.SmoothStep(-rangeX, rangeX, Mathf.PingPong(Time.time / timeDelimiterX, 1)),
            Mathf.SmoothStep(-rangeY, rangeY, Mathf.PingPong(Time.time / timeDelimiterY, 1)),
            0
        );
    }
}
```

![](4%20WH%20mover.gif)

Also, we can change the color of all particles in code. Color, rotation, speed, any stuff. There is an example of dynamic color. Add the component and add our particles to arguments.
```csharp
using UnityEngine;

public class Colorizer: MonoBehaviour {
    public ParticleSystem tunnelWall;
    public ParticleSystem cellBig;
    public ParticleSystem cellSmall;
    public ParticleSystem cellVessel;

    void Update() {
        Color color = new Color(
            Mathf.SmoothStep(1, 0.5f, Mathf.PingPong(Time.time / 10f, 1)),
            Mathf.SmoothStep(0, 1, Mathf.PingPong(Time.time / 15f, 1)),
            Mathf.SmoothStep(0, 1, Mathf.PingPong(Time.time / 5f, 1))
        );

        var tunnelWallMainSettings = tunnelWall.main;
        tunnelWallMainSettings.startColor = color;

        var cellBigMainSettings = cellBig.main;
        cellBigMainSettings.startColor = color;

        var cellSmallMainSettings = cellSmall.main;
        cellSmallMainSettings.startColor = color;

        var cellVesselMainSettings = cellVessel.main;
        cellVesselMainSettings.startColor = color;
    }
}
```

![](5%20WH%20colorized.gif)


Perhaps there are more simple and qualitative ways to implement it but I had fun with that and very like the process and the result!

You can follow the development of my game in [Twitter](https://twitter.com/Rubel_NMB) and [Steam Page](https://store.steampowered.com/app/1183010/Listeria_Wars/). Add the game to your wishlist!
