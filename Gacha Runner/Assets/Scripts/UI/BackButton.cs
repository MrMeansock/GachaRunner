using UnityEngine;

public class BackButton : MonoBehaviour
{
    private MenuManager menuManager;

    private void Awake()
    {
        menuManager = FindObjectOfType<MenuManager>();
    }

    public void Back()
    {
        menuManager.Back();
    }
}
