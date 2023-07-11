using UnityEngine;

public class MenuManager : Singleton<MenuManager>
{
    [SerializeField] private GameObject[] menus;

    public void OpenMenu(MenuName menuName)
    {
        menus[(int)menuName].SetActive(true);
    }

    public void CloseMenu(MenuName menuName)
    {
        menus[(int)menuName].SetActive(false);
    }
}

public enum MenuName
{
    LoadingMenu,
    TitleMenu,
}
