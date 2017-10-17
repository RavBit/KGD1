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
public class Battle_Menu_Manager : MonoBehaviour
{
    public Pokemon CurPokemon;
    public GameObject Battle_Menu;
    public Turn_Menu Turn_Menu = Turn_Menu.None;
    
    void Awake()
    {
        //TODO MAKE POKEMON DRAW SYSTEM
        CurPokemon = Pokemon_Collections.instance.Pokemon[0];
    }
    void SetTurnMenu(Turn_Menu cur_menu)
    {
        Turn_Menu = cur_menu;
        Debug.Log("Attack");
        switch (Turn_Menu)
        {
            case Turn_Menu.Attack:
                Battle_Manager.instance.ChangeState(Battle_State.Gamble);
                break;
            case Turn_Menu.Pokemon:
                Debug.Log("Gamble");
                break;
        }

    }

    public void Turnswitch(string _name)
    {
        Turn_Menu tm = (Turn_Menu)System.Enum.Parse(typeof(Turn_Menu), _name);
        SetTurnMenu(tm);
    }
}