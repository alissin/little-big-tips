using UnityEngine;

public abstract class BaseManager<T> : MonoBehaviour where T : BaseManager<T>
{
    static T instance;

    public static T Instance
    {
        get
        {
            // Lazy instantiation, in a situation that you need the singleton in runtime but it was not yet instantiated
            if (instance == null)
            {
                GameObject itemClone = new GameObject(typeof(T).Name);
                T manager = itemClone.AddComponent<T>();
                instance = manager;
            }

            return instance;
        }
        set
        {
            if (instance == null)
            {
                instance = value;
                DontDestroyOnLoad(instance.gameObject);
            }
            else if (instance != value)
            {
                // if for some reason, you have duplication, destroy the duplicated instance
                Destroy(value.gameObject);
            }
        }
    }

    void Awake()
    {
        Instance = this as T;
    }
}