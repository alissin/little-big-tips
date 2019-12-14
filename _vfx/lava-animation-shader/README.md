## _**Little Big Tips**_ ![Joystick](https://raw.githubusercontent.com/alissin/alissin.github.io/master/images/joystick.png) > VFX - Shaders

### lava animation shader

See this VFX in action [here](https://youtu.be/EtYslfd8Nog).

> [![lava animation shader](./lava-animation-shader_small.png)](https://youtu.be/EtYslfd8Nog)

#### Scenario
It would be really great to see some movement on our lava river.

#### Solution suggestion
This _**Little Big Tip**_ will add some animation on the surface of the mesh.

And the best part of this _**Little Big Tip**_: we are going to write our own shader using cg language!<br/>
_Note:_ as this shader animates the normal vertices, make sure to have a high poly mesh.

Create an empty file and name it `LavaAnimation.shader`.

Step 1 - let's create the base structure of our shader file:

```
Shader "MyShader/LavaAnimation" {

    Properties {

    }

    SubShader {

        Pass {

            CGPROGRAM
            
            ENDCG
        }
    }
}
```

As you can see, we have all the base code blocks for a basic cg language shader.

Step 2 - let's define our `Properties`:

```
Properties {    
    _Tex ("Texture", 2D) = "white" {}
    _Speed ("Speed", Float) = 1.0
    _Frequency ("Frequency", Float) = 1.0
    _Amplitude ("Amplitude", Float) = 1.0
}
```

Step 3 - now, inside the `CGPROGRAM` block, let's declare our main functions and properties:

```
CGPROGRAM

#pragma vertex vert
#pragma fragment frag

uniform sampler2D _Tex;
uniform float4 _Tex_ST;
uniform float _Speed;
uniform float _Frequency;
uniform float _Amplitude;

ENDCG
```

Step 4 - our input and output:<br/>
_Note:_ below the code of step 3.

```
CGPROGRAM

...

struct input {
    float4 position : POSITION;
    float4 normal: NORMAL;
    float4 texcoord : TEXCOORD0;
};

struct output {
    float4 position : SV_POSITION;
    float4 texcoord : TEXCOORD0;
};

ENDCG
```

Step 5 - finally, the vertex shader and fragment shader functions. We will use the math sin function to animate and simulate the "bounce" of the vertices:<br/>
_Note:_ below the code of step 4.

```
CGPROGRAM

...

output vert(input i) {
    i.position += sin((i.normal - _Time.y * _Speed) * _Frequency) * (i.normal * _Amplitude);

    output o;
    UNITY_INITIALIZE_OUTPUT(output, o);
    o.position = UnityObjectToClipPos(i.position);
    o.texcoord.xy = (i.texcoord.xy * _Tex_ST.xy + _Tex_ST.zw);
    return o;
}

half4 frag(output o) : COLOR {
    return tex2D(_Tex, o.texcoord);
}

ENDCG
```

Step 6 - find a very cool 2D texture in the Asset Store and don't forget to set the texture shape as `2D` and apply:

> ![2d-texture](../_common-images/2d-texture.png)

Step 7 - find an "irregular" surface high poly mesh in the Asset Store.

Step 8 - create a material, change it to use our shader, select your texture and finally attach this material to the mesh:

> ![material](./material.png)

Step 9 - hit play!

Again, feel free to see this VFX in action [here](https://youtu.be/EtYslfd8Nog).

More _**Little Big Tips**_? Nice, [let's go](https://github.com/alissin/little-big-tips)!