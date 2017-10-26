using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public enum Turn_Menu {
    None,
    Attack,
    Pokemon,
    Item,
    Give_Up
}
public class Battle_Menu_Manager : MonoBehaviour {
    public Image cur_pokemon_back;
    public Image enemy_pokemon_front;
    public GameObject interface_enemy;
    public GameObject interface_trainer;
    public GameObject game_over;
    public Turn_Menu Turn_Menu = Turn_Menu.None;
    void Awake() {
        //Set the events when the manager awakes.
        Event_Manager.SwitchTurnState += SetTurnMenu;
        Event_Manager.PokemonKill += RemovePanels;
        Event_Manager.PokemonKill += Die;
        Event_Manager.SetNewPokemon += NewPokemon;
        Event_Manager.SetNewPokemon += SetPanels;

        //Set the first sprites after 1 second
        Invoke("InitSprites", 1);
    }
    //Flash the sprites of the pokemon (checking their owner)
    public static void FlashSprite(pkm_owner owner) {
        switch (owner) {
            case pkm_owner.Enemy:

                break;
            case pkm_owner.Player:
                break;
        }
    }

    //Instantiates the sprite
    public void InitSprites() {
        Event_Manager.Update_PlayerInterface();
        enemy_pokemon_front.sprite = Battle_Manager.instance.enemy_Manager.GetCurPokemon().sprite;
        cur_pokemon_back.sprite = Battle_Manager.instance.trainer_Manager.GetCurPokemon().backSprite;
    }

    //Set the turn menu to the current turn and what the actions of the player are. Disabling/Enabling UI elements
    void SetTurnMenu(Turn_Menu cur_menu) {
        Turn_Menu = cur_menu;
        switch (Turn_Menu) {
            case Turn_Menu.Attack:
                RemovePanels(pkm_owner.Player);
                SetPanels();
                Battle_Manager.instance.ChangeState(Battle_State.Gamble);
                break;
            case Turn_Menu.Pokemon:
                Battle_Manager.instance.SwitchPokemon(pkm_owner.Player);
                Battle_Manager.instance.ChangeState(Battle_State.Fight);
                Battle_Manager.instance.StartAttack(0, "");
                break;
        }

    }

    //Set the panels of the Slot machine correctly when spawning a new pokemon
    public void SetPanels() {
        InitSprites();
        Event_Manager.Reset_SM();
        foreach (PKM_Attack attack in Battle_Manager.instance.trainer_Manager.GetCurPokemon().attacks) {
            SM_PanelData panel = new SM_PanelData();
            panel.name = attack.name;
            panel.spawnrate = ((attack.strength - 100) * -1);
            panel.image = attack.icon;
            panel.strength = attack.strength;
            SM_Panels.instance.AddPanel(panel);
        }
    }
    //Removing the panels of the old pokemon or panels that are instantiated incorrectly
    public void RemovePanels(pkm_owner owner) {
        foreach (PKM_Attack attack in Battle_Manager.instance.trainer_Manager.GetCurPokemon().attacks.ToList())
            foreach (SM_PanelData panel in SM_Panels.instance.paneldata.ToList())
                if (attack.name == panel.name)
                    SM_Panels.instance.RemovePanel(panel);
    }

    //Trigger and removing the UI when the pokemon are dying (also checking if it kills the correct sprite (enemy/player))
    void Die(pkm_owner owner) {
        switch (owner) {
            case pkm_owner.Player:
                cur_pokemon_back.DOFade(0, 2);
                interface_trainer.SetActive(false);
                break;
            case pkm_owner.Enemy:
                enemy_pokemon_front.DOFade(0, 2);
                interface_enemy.SetActive(false);
                break;
        }
    }

    //Setting in the new pokemon and fading it to be visible again
    void NewPokemon() {
        cur_pokemon_back.DOFade(1, 2);
        enemy_pokemon_front.DOFade(1, 2);
        interface_trainer.SetActive(true);
        interface_enemy.SetActive(true);

    }
    //A function called to call a function with an enumrator. This was not possible in MonoBehaviour
    public void Turnswitch(string _name) {
        Turn_Menu tm = (Turn_Menu)System.Enum.Parse(typeof(Turn_Menu), _name);
        SetTurnMenu(tm);
    }
}