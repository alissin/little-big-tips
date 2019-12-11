## _**Little Big Tips**_ ![Joystick](https://raw.githubusercontent.com/alissin/alissin.github.io/master/images/joystick.png) > VFX - Shaders

### sky rotation shader

Based on this demonstration / prototype: [Combat Wings](https://simmer.io/@alissin/combat-wings)

> ![Combat Wings](https://raw.githubusercontent.com/alissin/alissin.github.io/master/demonstration-projects/combat-wings.png)

#### Scenario
It would be really great to see the clouds moving on this amazing sky, on this amazing low poly environment.

#### Solution suggestion
Unity already has a very cool built-in shader for SkyBox that uses a cubemap for that. This _**Little Big Tip**_ will only add the constant rotation behaviour on it.

First, you can find and download all the Unity built-in shaders [here](https://unity3d.com/get-unity/download/archive).

Then, get the `Skybox-Cubed.shader` inside the `DefaultResourcesExtra` folder and put it in your Unity project `Assets` folder.

Step 1 - Open the shader file on a text editor and add these properties on `Properties` section of the shader code:

```
_RotationSpeed ("Rotation Speed", Range(1.0, 10.0)) = 2.0
[Toggle] _IsRotationEnabled ("Is Rotation Enabled?", Float) = 1
```

Step 2 - set the `#pragma shader_feature`, bellow the `CGPROGRAM` and anothers `#pragma` lines to use this toggle:

```
CGPROGRAM
#pragma ...
...
#pragma shader_feature _ISROTATIONENABLED_OFF _ISROTATIONENABLED_ON
```

Step 3 - in the `vert` function (vertex function), use this condition to toggle (on/off) the rotation:

```
...
#if _ISROTATIONENABLED_ON
    _Rotation += _Time.y % 359 * _RotationSpeed;
#endif
float3 rotated = RotateAroundYInDegrees(v.vertex, _Rotation);
...
```

Step 4 - create a material and change it to use this shader.

Step 5 - find a very cool skybox cubemap on Asset Store.

Step 6 - apply this material on Lighting Settings > Environment > Skybox Material.

Step 7 - enjoy the new sky!

More _**Little Big Tips**_? Nice, [follow me](https://github.com/alissin/little-big-tips)!