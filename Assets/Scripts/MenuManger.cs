using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManger : MonoBehaviour
{


    public static MenuManger menuManger;
    [SerializeField] Menu[] _menusList;


    private void Awake()
    {
        menuManger = this;
    }

    public void OpenMenu(string menuName)
    {
        for (int i = 0; i < _menusList.Length; i++)
        {
            if (_menusList[i].menuName == menuName)
            {
                //OpenMenu(_menusList[i]);
                _menusList[i].OpenMenu();
            }
            else if (_menusList[i].isOpen)
            {
                CloseMenu(_menusList[i]);
            }
        }

    }
  public  void OpenMenu(Menu menu)
    {
        for (int i = 0; i < _menusList.Length; i++)
        {
            if (_menusList[i].isOpen)
            {
                CloseMenu(_menusList[i]);
            }
        }

        menu.OpenMenu();

    }
  public  void CloseMenu(Menu menu)
    {

        menu.CloseMenu();
    }
}
