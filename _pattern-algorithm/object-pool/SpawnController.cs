using System.Collections;
using UnityEngine;

public class SpawnController : MonoBehaviour {

    [SerializeField]
    GameObject _spawnPrefab;

    [SerializeField]
    float _spawnDelay;

    [SerializeField]
    int _poolSize;

    GameObject[] _poolObjs;

    int _currentPoolSize;

    // TODO: when your level is loaded, start the process with this method
    void InitPool() {
        _currentPoolSize = _poolSize;
        _poolObjs = new GameObject[_currentPoolSize];

        for (int i = 0; i < _poolObjs.Length; i++) {
            GameObject itemClone = Instantiate(_spawnPrefab, transform);
            itemClone.SetActive(false);
            _poolObjs[i] = itemClone;
        }
    }

    IEnumerator SpawnRoutine(Vector3 spawnPos, Quaternion spawnRot) {
        while (true) {
            GetObjFromPool(spawnPos, spawnRot);
            yield return new WaitForSeconds(_spawnDelay);
        }
    }

    GameObject GetObjFromPool(Vector3 spawnPos, Quaternion spawnRot) {
        // if there is no more available items in the pool, do nothing
        if (_currentPoolSize == 0) {
            return null;
        }

        _currentPoolSize--;

        GameObject obj = _poolObjs[_currentPoolSize];
        _poolObjs[_currentPoolSize] = null;

        obj.transform.position = spawnPos;
        obj.transform.rotation = spawnRot;
        obj.SetActive(true);

        return obj;
    }

    public void ReturnObjToPool(GameObject obj) {
        obj.SetActive(false);

        _poolObjs[_currentPoolSize] = obj;
        _currentPoolSize++;
    }
}