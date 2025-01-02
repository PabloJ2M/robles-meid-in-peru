using UnityEngine;

[DefaultExecutionOrder(-100)]
public abstract class SimpleSingleton<T> : MonoBehaviour where T : Component
{
    public static T Instance { get; private set; }

    protected virtual void Awake() => Instance = this as T;
}

[DefaultExecutionOrder(-100)]
public abstract class Singleton<T> : MonoBehaviour where T : Component
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        //create singleton
        if (Instance) Destroy(gameObject);
        else
        {
            Instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
    }
}

[DefaultExecutionOrder(-100)]
public abstract class ComplexSingleton<T> : MonoBehaviour where T : Component
{
    private enum DestroyType { Destroy_New, Destroy_Old }

    [Header("Singleton"), SerializeField]
    private DestroyType _destroyType = DestroyType.Destroy_New;

    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        //destroy existing
        if (!Instance && _destroyType == DestroyType.Destroy_Old) Destroy(Instance.gameObject);
        
        //create singleton
        if (!Instance) { Instance = this as T; DontDestroyOnLoad(gameObject); }
        else Destroy(gameObject);
    }
}