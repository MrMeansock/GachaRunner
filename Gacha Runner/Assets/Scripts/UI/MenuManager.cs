using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private Stack<Menu> menuStack;
    private Menu ActiveMenu => menuStack.Peek();

    private void Awake()
    {
        menuStack = new Stack<Menu>();

        // Find all menus
        Menu[] menus = FindObjectsOfType<Menu>();
        List<Menu> menuList = new List<Menu>(menus);
        // Make first found active on hierarchy the active one
        menuStack.Push(menuList.Find(m => m.gameObject.activeInHierarchy));
        
        // Set all else to inactive
        menuList.ForEach(m => m.gameObject.SetActive(false));
        ActiveMenu.gameObject.SetActive(true);
    }

    public void SetActive(Menu menu)
    {
        ActiveMenu.gameObject.SetActive(false);
        menuStack.Push(menu);
        ActiveMenu.gameObject.SetActive(true);
    }

    public void Back()
    {
        ActiveMenu.gameObject.SetActive(false);
        menuStack.Pop();
        ActiveMenu.gameObject.SetActive(true);
    }
}
