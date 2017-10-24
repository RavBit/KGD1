using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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
    public GameObject Interface_Enemy;
    public GameObject Interface_Trainer;
    public GameObject Game_Over;
    public Turn_Menu Turn_Menu = Turn_Menu.None; 
    void Awake()
    {
        Event_Manager.SwitchTurnState += SetTurnMenu;
        Event_Manager.PokemonKill += RemovePanels;
        Event_Manager.PokemonKill += Die;
        Event_Manager.SetNewPokemon += NewPokemon;
        Event_Manager.SetNewPokemon += SetPanels;
        Invoke("InitSprites", 1);
    }

    public static void FlashSprite(PKM_Owner owner) {
        switch(owner) {
            case PKM_Owner.Enemy:

                break;
            case PKM_Owner.Player:
                break;
        }
    }

    public void InitSprites()
    {
        Event_Manager.Update_PlayerInterface();
        Enemy_Pokemon_Front.sprite = Battle_Manager.instance.Enemy_Manager.GetCurPokemon().Sprite;
        Cur_Pokemon_Back.sprite = Battle_Manager.instance.Trainer_Manager.GetCurPokemon().BackSprite;
    }
    void SetTurnMenu(Turn_Menu cur_menu)
    {
        Turn_Menu = cur_menu;
        switch (Turn_Menu)
        {
            case Turn_Menu.Attack:
                RemovePanels(PKM_Owner.Player);
                SetPanels();
                Battle_Manager.instance.ChangeState(Battle_State.Gamble);
                break;
            case Turn_Menu.Pokemon:
                Battle_Manager.instance.SwitchPokemon(PKM_Owner.Player);
                Battle_Manager.instance.ChangeState(Battle_State.Fight);
                Battle_Manager.instance.StartAttack(0, "");
                break;
        }

    }

    public void SetPanels() {
        InitSprites();
        Event_Manager.Reset_SM();
        foreach (PKM_Attack attack in Battle_Manager.instance.Trainer_Manager.GetCurPokemon().attacks) {
            SM_PanelData panel = new SM_PanelData();
            panel.name = attack.name;
            panel.spawnrate = ((attack.strength - 100) * -1);
            panel.image = attack.icon;
            panel.strength = attack.strength;
            SM_Panels.instance.AddPanel(panel);
        }
    }
    public void RemovePanels(PKM_Owner owner)
    {
        foreach (PKM_Attack attack in Battle_Manager.instance.Trainer_Manager.GetCurPokemon().attacks.ToList())
            foreach (SM_PanelData panel in SM_Panels.instance.paneldata.ToList())
                if (attack.name == panel.name)
                    SM_Panels.instance.RemovePanel(panel);
    }
    
    void Die(PKM_Owner owner)
    {
        switch(owner)
        {
            case PKM_Owner.Player:
                Cur_Pokemon_Back.DOFade(0, 2);
                Interface_Trainer.SetActive(false);
                break;
            case PKM_Owner.Enemy:
                Enemy_Pokemon_Front.DOFade(0, 2);
                Interface_Enemy.SetActive(false);
                break;
        }
    }
    void NewPokemon()
    {
        Cur_Pokemon_Back.DOFade(1, 2);
        Enemy_Pokemon_Front.DOFade(1, 2);
        Interface_Trainer.SetActive(true);
        Interface_Enemy.SetActive(true);

    }
    public void Turnswitch(string _name)
    {
        Turn_Menu tm = (Turn_Menu)System.Enum.Parse(typeof(Turn_Menu), _name);
        SetTurnMenu(tm);
    }
}