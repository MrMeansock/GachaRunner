using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;
    public static T Instance
    {
        get { return instance; }
        set 
        {
            if (instance == null)
            {
                instance = value;
                DontDestroyOnLoad(instance.gameObject);
            }
            else if (instance != value)
            {
                Destroy(value.gameObject);
            }
        }
    }

    protected virtual void Awake()
    {
        Instance = this as T;
    }

    protected virtual void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
}