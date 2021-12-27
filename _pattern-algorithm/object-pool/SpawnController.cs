using System.Collections;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField]
    GameObject spawnPrefab;

    [SerializeField]
    float spawnDelay;

    [SerializeField]
    int poolSize;

    GameObject[] poolObjs;

    int currentPoolSize;

    // TODO: when your level is loaded, start the process with this method
    void InitPool()
    {
        currentPoolSize = poolSize;
        poolObjs = new GameObject[currentPoolSize];

        for (int i = 0; i < poolObjs.Length; i++)
        {
            GameObject itemClone = Instantiate(spawnPrefab, transform);
            itemClone.SetActive(false);
            poolObjs[i] = itemClone;
        }
    }

    IEnumerator SpawnRoutine(Vector3 spawnPos, Quaternion spawnRot)
    {
        while (true)
        {
            GetObjFromPool(spawnPos, spawnRot);
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    GameObject GetObjFromPool(Vector3 spawnPos, Quaternion spawnRot)
    {
        // if there is no more available items in the pool, do nothing
        if (currentPoolSize == 0)
        {
            return null;
        }

        currentPoolSize--;

        GameObject obj = poolObjs[currentPoolSize];
        poolObjs[currentPoolSize] = null;

        obj.transform.position = spawnPos;
        obj.transform.rotation = spawnRot;
        obj.SetActive(true);

        return obj;
    }

    public void ReturnObjToPool(GameObject obj)
    {
        obj.SetActive(false);

        poolObjs[currentPoolSize] = obj;
        currentPoolSize++;
    }
}