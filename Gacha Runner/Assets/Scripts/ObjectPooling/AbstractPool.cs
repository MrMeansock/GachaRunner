using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public abstract class AbstractPool : MonoBehaviour
{
    [Tooltip("What the objects will be parented to.")]
    public Transform parent = null;

    [Tooltip("How many objects will be pooled on Awake.")]
    [SerializeField] protected internal int startCount = 1;

    [Tooltip("Whether pool will grow to fit more objects if more than max requested.")]
    public bool isDynamic = true;

    [Tooltip("Maximum number of objects pooled allowed.")]
    public int max = 1; // Only serializes if isDynamic is set to false
}

/// <summary>
/// Custom editor for AbstractPools
/// Children's editors should inherit from this one
/// </summary>
[CustomEditor(typeof(AbstractPool))]
public class AbstractPoolEditor : Editor
{
    public SerializedProperty parentProp;
    public SerializedProperty startCountProp;
    public SerializedProperty isDynamicProp;
    public SerializedProperty maxProp;

    protected virtual void OnEnable()
    {
        parentProp = serializedObject.FindProperty("parent");
        startCountProp = serializedObject.FindProperty("startCount");
        isDynamicProp = serializedObject.FindProperty("isDynamic");
        maxProp = serializedObject.FindProperty("max");
    }

    public override void OnInspectorGUI()
    {
        AbstractPool poolScript = target as AbstractPool;

        EditorGUILayout.PropertyField(parentProp);
        poolScript.parent = (Transform)parentProp.objectReferenceValue;
        EditorGUILayout.PropertyField(startCountProp);
        poolScript.startCount = startCountProp.intValue;
        EditorGUILayout.PropertyField(isDynamicProp);
        poolScript.isDynamic = isDynamicProp.boolValue;

        // Only show max if ObjectPool is not dynamic
        if (!poolScript.isDynamic)
        {
            EditorGUILayout.PropertyField(maxProp);
            poolScript.max = maxProp.intValue;
        }
    }
}