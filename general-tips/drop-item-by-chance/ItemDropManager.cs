using UnityEngine;

public class ItemDropManager : MonoBehaviour
{
    [SerializeField]
    ItemSO[] itemsSO;

    float totalDropChance;

    void Start()
    {
        foreach (var item in itemsSO)
        {
            totalDropChance += item.dropChance;
        }
    }

    public void DropItem(Vector3 dropPosition)
    {
        float random = Mathf.Round(Random.Range(0, totalDropChance) * 10.0f) / 10.0f;

        float whichItem = 0.0f;
        foreach (var item in itemsSO)
        {
            whichItem += item.dropChance;
            if (whichItem >= random)
            {
                Instantiate(item.prefab, dropPosition, item.prefab.transform.rotation);
                break;
            }
        }
    }
}