## _**Little Big Tips**_ ![Joystick](https://raw.githubusercontent.com/alissin/alissin.github.io/master/images/joystick.png) > VFX - Particle System > smoke particle

> [![smoke particle](./smoke-particle_small.png)](https://youtu.be/2QzYuhXA2_w)

Click to see this VFX in action.<br/>
<sub>_Note:_ The purpose of this demonstration is to evaluate this VFX. The scenario and the props are free assets from the Asset Store.</sub>

#### Problem description
How to see some smoke coming out of the window or chimney?

#### Solution suggestion
This is a very simple but really nice and handy `Particle System` effect to show a smoke. Both smokes have almost the same settings. We will create the black smoke example and then you can duplicate it and play with the values by yourself.

In the hierarchy, create a game object and name it as `Smoke`:

```
Hierarchy:
- Smoke
```

Don't forget to add a `Particle System` component to the `Smoke` game object.

Find a very cool cloud / smoke sprite. As suggestion, you can use the `SplatAlbedo.tif` from the `Unity Particle Pack` from the Asset Store. Don't forget to set the texture shape as `2D` and apply:

> ![2d-texture](../../_images/2d-texture.png)

Create a `Material` and name it as `Smoke`:
* for _Universal RP_, use `Particles/Unlit`, set the surface to `Transparent`, color mode to `Additive`, select the `Base Map` (select your smoke sprite) and a grey color with alpha 128 for the `Tint Color`:

> ![material](./material.png)

* for _Legacy_, use `Particles/Alpha Blended`, select the `Texture` (select your smoke sprite) and a grey color with alpha 128 for the `Tint Color`.

Select the `Smoke` game object and let's work on our `Particle System`:
* renderer module (last module):
    * material: select the `Material` we've created in step 2
* main module:
    * start lifetime: 2
    * start speed: 0
    * start size: 2
    * start rotation: 0 - 360 (two constants)
    * simulation speed: 0.5
* emission module:
    * rate over time: 10
* shape module (uncheck it)
* velocity over lifetime module (check it):
    * linear: change it to curve, change the Z to a curve starting with 2 and finishing with 0:
> ![velocity over lifetime](./velocity-over-lifetime.png)
* color over lifetime module (check it):
    * color: select a sequence of colors of your choice
* size over lifetime module (check it):
    * size: select the curve of your choice

As you can see, by default, the main module > play on awake is checked. It means the `Particle System` will start its emission immediately when you hit play. Just uncheck it if you don't want it.

To start (or stop) the emission programmatically:

```csharp
ParticleSystem particleSystem = GetComponent<ParticleSystem>();
particleSystem.Play(); // starts the Particle System
particleSystem.Stop(); // stops the Particle System
```

Again, feel free to see this VFX in action [here](https://youtu.be/2QzYuhXA2_w).

More _**Little Big Tips**_? Nice, [let's go](https://github.com/alissin/little-big-tips)!