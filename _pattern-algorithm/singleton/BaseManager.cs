using UnityEngine;

public abstract class BaseManager<T> : MonoBehaviour where T : BaseManager<T> {
    static T _instance;

    public static T Instance {
        get {
            // Lazy instantiation, in a situation that you need the singleton in runtime but it was not yet instantiated
            if (_instance == null) {
                GameObject itemClone = new GameObject(typeof(T).Name);
                T manager = itemClone.AddComponent<T>();
                _instance = manager;
            }
            return _instance;
        }
        set {
            if (_instance == null) {
                _instance = value;
                DontDestroyOnLoad(_instance.gameObject);
            } else if (_instance != value) {
                // if for some reason, you have duplication, destroy the duplicated instance
                Destroy(value.gameObject);
            }
        }
    }

    void Awake() {
        Instance = this as T;
    }
}