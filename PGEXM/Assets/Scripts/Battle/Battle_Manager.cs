using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public enum Battle_State
{
    Turn,
    Gamble,
    Fight
}
public class Battle_Manager : MonoBehaviour {
    [SerializeField]
    public Enemy_Manager Enemy_Manager;
    public static Battle_Manager instance;
    //Actions
    public Battle_State _currentstate = Battle_State.Turn;
    [SerializeField]
    private GameObject Turn_Menu;

    void Awake()
    {
        if (instance != null)
            Debug.LogError("More than one Pokemon Collections in the scene");
        else
            instance = this;
        DontDestroyOnLoad(transform.gameObject);
        Event_Manager.SwitchBattleState += ChangeState;
        Event_Manager.Switch_BattleState(Battle_State.Turn);
        InitEnemy();
    }
    void InitEnemy()
    {
        Enemy_Manager.Pokemon = Pokemon_Collections.instance.Pokemon.ToList();
        int amount = 0;
        foreach(Pokemon pokemon in Enemy_Manager.Pokemon)
        {
            while (amount != Enemy_Manager.amount_pokemon)
            {
                int r = Random.Range(0, 2);
                if (r == 1 && amount != Enemy_Manager.amount_pokemon)
                {
                    pokemon.PKM_Owner = PKM_Owner.Enemy;
                    amount++;
                }
            }
            if (pokemon.PKM_Owner != PKM_Owner.Enemy)
                Enemy_Manager.Pokemon.Remove(pokemon);
        }
    }
    public void ChangeState(Battle_State _newstate)
    {
        _currentstate = _newstate;
        switch (_newstate)
        {
            case Battle_State.Turn:
                TurnToggle(true);
                break;
            case Battle_State.Gamble:
                TurnToggle(false);
                Event_Manager.Start_Gamble(true);
                break;
            case Battle_State.Fight:
                Event_Manager.Start_Gamble(false);
                break;
        }
    }

    void TurnToggle(bool toggle)
    {
        Turn_Menu.SetActive(toggle);
    }
}
