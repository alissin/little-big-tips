## _**Little Big Tips**_ ![Joystick](https://raw.githubusercontent.com/alissin/alissin.github.io/master/images/joystick.png) > General tips > land the rocket

> ![Boost the Rocket](./../../_images/boost_the_rocket/land-the-rocket.png)

Feel free to try this behaviour on the playable demonstration / prototype: [Boost the Rocket](https://simmer.io/@alissin/boost-the-rocket).

#### Problem description
How to have a more realistic and smoothly landing when close to the landing pad and avoid an explosion?

#### Solution suggestion
When the rocket player gets closer to the landing pad, it checks the angle of the rotation and if it's safe (green), it takes care of a smooth and nice rocket landing.

Create a C# script `Player.cs` and attach this script to the `Player` game object:

```csharp
public class Player : MonoBehaviour
{
    ...
```

Define the fields:

```csharp
const float maxRayGroundedDistance = 2.0f;
const int safeLandingAngle = 30;

bool isLandingCompleted = false;
```

Set the tag of your landing pad game object to `Landing Pad`.

Let's check if the `Player` is close to the landing pad and in a safe angle to land:<br/>
<sub>_Note:_ the landing pad uses the `IsSafeLandingAngle()` method to check if it's red or green when the `Player` gets close, as you can see on the image above.<br/>
_Note 2:_ the color change is out of scope of this _**Little Big Tip**_.</sub>

```csharp
void OnCollisionEnter(Collision collision)
{
    if (collision.gameObject.CompareTag("Landing Pad"))
    {
        if (!IsSafeLandingAngle())
        {
            // TODO: run player die / explosion method
        }
    }
    else
    {
        // TODO: run player die / explosion method
    }
}

public bool IsSafeLandingAngle()
{
    return Vector3.Angle(transform.up, Vector3.up) <= safeLandingAngle;
}
```

As you can see on the above snippet, if we collide with the landing pad in a safe landing angle, nothing will happen. In this case, we will use the `Physics.Raycast` to check if the `Player` is close to the landing pad to finally land it:

```csharp
void Update()
{
    Ray ray = new Ray(transform.position, -Vector3.up);
    RaycastHit hit;

    if (Physics.Raycast(ray, out hit, maxRayGroundedDistance))
    {
        if (hit.transform.gameObject.CompareTag("Landing Pad"))
        {
            Land(hit.transform);
        }
    }
    else
    {
        isLandingCompleted = false;
    }
}
```

Now, let's take care of our super smooth and stylish landing:<br/>
<sub>_Note:_ our rocket Player fits the right place above the landing pad smoothly.</sub>

```csharp
void Land(Transform groundTransform)
{
    if (!isLandingCompleted)
    {
        // TODO: check if this ground / landing offset position of the Player makes sense for you. In my case, I used 1.95f
        float landingYPos = groundTransform.position.y + 2.0f;

        Vector3 targetPos = new Vector3(groundTransform.position.x, landingYPos, groundTransform.position.z);

        if (transform.position != targetPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime);
        }
        else
        {
            isLandingCompleted = true;
            transform.rotation = Quaternion.identity;
        }
    }
}
```

Make sure to have a `Rigibody` (`gravity == true` / `kinematic == false`) and a `Collider` components attached to the `Player` game object. In this case, I used `mass == 15` and `drag == 5`.

Make sure to have a `Collider` component attached to your landing pad game object.

#### Scripts:
[Player.cs](./Player.cs)

Again, feel free to try the behaviour of this _**Little Big Tip**_ on [Boost the Rocket](https://simmer.io/@alissin/boost-the-rocket).

More _**Little Big Tips**_? Nice, [let's go](https://github.com/alissin/little-big-tips)!