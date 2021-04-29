using UnityEngine;

public class Jukebox : MonoBehaviour
{
    private IJukeboxBehaviour[] behaviours;

    private void Awake()
    {
        behaviours = GetComponents<IJukeboxBehaviour>();
    }

    private void Start()
    {
        foreach (var behaviour in behaviours)
        {
            behaviour.Behave();
        }
    }

    public void PlayBehaviour(IJukeboxBehaviour behaviour)
    {
        behaviour.Behave();
    }
}
