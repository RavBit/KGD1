﻿using System.Collections;
using System.Collections.Generic;
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
    [SerializeField]
    public Trainer_Manager Trainer_Manager;
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
        Trainer_Manager.Start();
        Enemy_Manager.Start();
        Event_Manager.Loading_DataBank();
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
    public void ATTACKMngr(int score)
    {
        int attacked = 0;
        while (attacked != 2)
        {
            int random = Random.Range(0, 2);
            if (random == 0)
            {
                attacked++;
                Attack(PKM_Owner.Enemy, (-1 * (int)score));
            }
            if (random == 1)
            {
                attacked++;
                System.Random rand = new System.Random();
                int r = rand.Next(Enemy_Manager.GetCurPokemon().attacks.Count);
                float attack = Enemy_Manager.GetCurPokemon().attacks[r].strength * Random.Range(0, 2) * Random.Range(-1, 1);
                Debug.Log("Attack: " + attack);
                Attack(PKM_Owner.Player, (-1 * (int)attack));
            }
        }
    }
    public void Attack(PKM_Owner _curattacker, int damage)
    {
        switch (_curattacker)
        {
            case PKM_Owner.Player:
                Enemy_Manager.AdjustHealth(damage);
                break;
            case PKM_Owner.Enemy:
                Trainer_Manager.AdjustHealth(damage);
                break;
        }
    }

    void TurnToggle(bool toggle)
    {
        Turn_Menu.SetActive(toggle);
    }
}
