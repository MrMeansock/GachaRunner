using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GameObjectPool : AbstractPool
{
    [Tooltip("Object to be pooled.")]
    [SerializeField] internal GameObject objectPrefab = null;

    List<GameObject> pool = new List<GameObject>();

    protected virtual void Awake()
    {
        if (!isDynamic) startCount = Mathf.Min(max, startCount); // startCount cannot be bigger than max if pool is not dynamic
        pool = new List<GameObject>(startCount);

        for (int i = 0; i < pool.Capacity; i++)
        {
            GameObject pooledObject = Instantiate(objectPrefab, parent);
            pooledObject.SetActive(false);
            pool.Add(pooledObject);
        }
    }

    /// <summary>
    /// Returns next available object and sets it to active.
    /// Returns null if none can be sent .
    /// </summary>
    /// <returns>(Possibly) Object fetched from pool.</returns>
    public virtual GameObject GetNextAvailable()
    {
        UpdatePoolCount();

        // Return the first non active object in pool
        // And set it to active
        for (int i = 0; i < pool.Count; i++)
        {
            if (pool[i].activeInHierarchy == false)
            {
                pool[i].SetActive(true);
                return pool[i];
            }
        }

        // Grow if dynamic
        if (isDynamic == true)
        {
            GameObject pooledObject = Instantiate(objectPrefab, parent);
            pooledObject.SetActive(true);
            pool.Add(pooledObject);
            return pooledObject;
        }

        return null;
    }

    /// <summary>
    /// Returns a requested object to the pool.
    /// Will not take objects that weren't initially in the pool.
    /// </summary>
    /// <param name="gameObject">Object returned.</param>
    public virtual void ReturnToPool(GameObject gameObject)
    {
        if (pool.Contains(gameObject))
        {
            gameObject.SetActive(false);
        }
    }

    // Updates pool count when it is not dynamic anymore
    private void UpdatePoolCount()
    {
        // Check if the pool is not dynamic anymore
        if (!isDynamic)
        {
            // Resize pool if greater than max
            if (pool.Count > max)
            {
                // Get resized pool
                List<GameObject> newPool = pool.GetRange(0, max);

                // Destroy objects that get left behind in the resize
                List<GameObject> objectsToDestroy = pool.GetRange(max, pool.Count - max);
                foreach (GameObject gameObject in objectsToDestroy)
                {
                    Destroy(gameObject);
                }

                // Update pool
                pool = newPool;
            }
        }
    }
}

// Inherit AbstractPoolEditor
[CustomEditor(typeof(GameObjectPool))]
public class GameObjectPoolEditor : AbstractPoolEditor {

    public SerializedProperty objectPrefabProp;

    protected override void OnEnable()
    {
        base.OnEnable();

        objectPrefabProp = serializedObject.FindProperty("objectPrefab");
    }

    public override void OnInspectorGUI()
    {
        GameObjectPool poolScript = target as GameObjectPool;

        EditorGUILayout.PropertyField(objectPrefabProp);
        poolScript.objectPrefab = (GameObject)objectPrefabProp.objectReferenceValue;

        base.OnInspectorGUI();
    }
}