using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Turn_Menu
{
    None,
    Attack,
    Pokemon,
    Item,
    Give_Up
}
public class B_Player_Interface : MonoBehaviour {
    public Turn_Menu Turn_Menu = Turn_Menu.None;
    void SetTurnMenu(Turn_Menu cur_menu)
    {
        switch (Turn_Menu)
        {
            case Turn_Menu.Attack:
                Debug.Log("Test");
                break;
            case Turn_Menu.Pokemon:
                Debug.Log("Gamble");
                break;
        }
        RefreshInterface();

    }

    public void Turnswitch(string _name)
    {
        Turn_Menu tm = (Turn_Menu)System.Enum.Parse(typeof(Turn_Menu), _name);
        SetTurnMenu(tm);
    }
    void RefreshInterface()
    {

    }
}
