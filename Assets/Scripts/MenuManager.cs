using UnityEngine;

public class MenuManager : Singleton<MenuManager>
{
    [SerializeField] private GameObject[] menus;
    private GameObject currentOpenedMenu;

    // Open menu by index (Enum)
    public void OpenMenu(MenuName menuName)
    {
        // Close current menu
        if (currentOpenedMenu != null)
        {
            CloseMenu(currentOpenedMenu);
        }

        // Open new menu
        menus[(int)menuName].SetActive(true);

        // Set to current menu
        currentOpenedMenu = menus[(int)menuName];
    }

    // Open menu by gameObject
    public void OpenMenu(GameObject menuObject)
    {
        // Close current menu
        if (currentOpenedMenu != null)
        {
            CloseMenu(currentOpenedMenu);
        }

        // Open new menu
        menuObject.SetActive(true);

        // Set to current menu
        currentOpenedMenu = menuObject;
    }

    // Close menu by index (Enum)
    public void CloseMenu(MenuName menuName)
    {
        menus[(int)menuName].SetActive(false);
    }

    // Close menu by gameObject
    public void CloseMenu(GameObject menuObject)
    {
        menuObject.SetActive(false);
    }
}

public enum MenuName
{
    LoadingMenu,
    TitleMenu,
    CreateRoomMenu,
    RoomMenu,
    RoomFailedMenu,
}
