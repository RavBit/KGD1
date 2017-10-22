using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public Image Cur_Pokemon_Back;
    public Turn_Menu Turn_Menu = Turn_Menu.None; 
    void Awake()
    {
        CurPokemon = Pokemon_Collections.instance.Pokemon[0];
        Cur_Pokemon_Back.sprite = CurPokemon.BackSprite;
        Event_Manager.SwitchTurnState += SetTurnMenu;
    }
    void SetTurnMenu(Turn_Menu cur_menu)
    {
        Turn_Menu = cur_menu;
        Debug.Log("Attack");
        switch (Turn_Menu)
        {
            case Turn_Menu.Attack:
                SetPanels();
                Battle_Manager.instance.ChangeState(Battle_State.Gamble);
                break;
            case Turn_Menu.Pokemon:
                break;
        }

    }

    void SetPanels() {
        foreach (PKM_Attack attack in CurPokemon.attacks) {
            SM_PanelData panel = new SM_PanelData();
            panel.name = attack.name;
            panel.spawnrate = ((attack.strength - 100) * -1);
            panel.image = attack.icon;
            panel.strength = attack.strength;
            panel.unique = true;
            SM_Panels.instance.AddPanel(panel);
        }
    }

    public void Turnswitch(string _name)
    {
        Turn_Menu tm = (Turn_Menu)System.Enum.Parse(typeof(Turn_Menu), _name);
        SetTurnMenu(tm);
    }
}