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
    public GameObject Battle_Menu;
    public Image Cur_Pokemon_Back;
    public Image Enemy_Pokemon_Front;
    public Turn_Menu Turn_Menu = Turn_Menu.None; 
    void Awake()
    {
        Event_Manager.SwitchTurnState += SetTurnMenu;
        Invoke("InitSprites", 0.1f);
    }

    void Update()
    {

    }
    public static void FlashSprite(PKM_Owner owner) {
        switch(owner) {
            case PKM_Owner.Enemy:

                break;
            case PKM_Owner.Player:
                break;
        }
    }

    void InitSprites()
    {
        Enemy_Pokemon_Front.sprite = Battle_Manager.instance.Enemy_Manager.GetCurPokemon().Sprite;
        Cur_Pokemon_Back.sprite = Battle_Manager.instance.Trainer_Manager.GetCurPokemon().BackSprite;
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
        Event_Manager.Reset_SM();
        foreach (PKM_Attack attack in Battle_Manager.instance.Trainer_Manager.GetCurPokemon().attacks) {
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