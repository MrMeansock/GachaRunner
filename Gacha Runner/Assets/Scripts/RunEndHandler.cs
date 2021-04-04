using System;
using UnityEngine;

public class RunEndHandler : MonoBehaviour
{
    [SerializeField] Character player;

    public event Action OnRunEnd;

    private void Awake()
    {
        if (!player) player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
    }

    private void OnEnable()
    {
        player.OnDeath += FireRunEnd;
    }

    private void OnDisable()
    {
        player.OnDeath -= FireRunEnd;
    }

    private void FireRunEnd()
    {
        OnRunEnd?.Invoke();
    }
}
