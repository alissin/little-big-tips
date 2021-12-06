using UnityEngine;
using UnityEngine.UI;

public class Cooldown : MonoBehaviour
{
    [SerializeField]
    float cooldown = 5.0f;

    Slider cooldownSlider;

    int availableItems = 3;
    int maxItems = 3;
    bool isCooldownOn = false;

    void Start()
    {
        cooldownSlider = GetComponent<Slider>();
    }

    void Update()
    {
        if (isCooldownOn)
        {
            cooldownSlider.value += Time.deltaTime / cooldown;

            if (cooldownSlider.value >= cooldownSlider.maxValue)
            {
                availableItems++;
                cooldownSlider.value = 0.0f;
            }

            isCooldownOn = availableItems < maxItems;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            availableItems = 0;
            isCooldownOn = true;
        }
    }
}