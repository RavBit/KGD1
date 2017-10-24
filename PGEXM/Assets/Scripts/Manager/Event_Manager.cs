﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Manager : MonoBehaviour {

    //Start the slot machine wheel to gamble
    public delegate void StartGamble(bool toggle);
    public static event StartGamble InitGamble;

    //Set the current turn state in the Battle manager
    public delegate void SwitchTurn(Turn_Menu state);
    public static event SwitchTurn SwitchTurnState;

    //Set the current battle state in the Battle Manager
    public delegate void SwitchBattle(Battle_State state);
    public static event SwitchBattle SwitchBattleState;

    //Load in the pokemons from the databank
    public delegate void LoadDataBank();
    public static event LoadDataBank LoadingDataBank;

    //Update the player UI
    public delegate void UpatePlayerInterface();
    public static event UpatePlayerInterface PlayerUpdateInterface;

    //Reset Slot Machine and restart
    public delegate void ResetSlotMachine();
    public static event ResetSlotMachine ResetSM; 
    //Set the text in the Message Center
    public delegate void SetTextMessage(string text);
    public static event SetTextMessage SetMessageText;

    //Check if pokemon is alive or dead
    public delegate void PokemonAlive();
    public static event PokemonAlive PokemonAliveCheck;
    public static event PokemonAlive SetNewPokemon;

    //Let the pokemon die in the field
    public delegate void KillPokemon(PKM_Owner owner);
    public static event KillPokemon PokemonKill;

    public static void Start_Gamble(bool toggle) {
        InitGamble(toggle);
    }
    public static void Switch_State(Turn_Menu state) {
        SwitchTurnState(state);
    }
    public static void Switch_BattleState(Battle_State state) {
        SwitchBattleState(state);
    }
    public static void Loading_DataBank() {
        LoadingDataBank();
    }
    public static void Reset_SM() {
        if (ResetSM != null)
            ResetSM();
    }
    public static void Set_MessageText(string text) {
        SetMessageText(text);
    }
    public static void Pokemon_Kill(PKM_Owner owner)
    {
        PokemonKill(owner);
    }
    public static void Set_NewPokemon()
    {
        SetNewPokemon();
    }
    public static void Pokemon_AliveCheck()
    {
        PokemonAliveCheck();
    }
    public static void Update_PlayerInterface()
    {
        PlayerUpdateInterface();
    }
    public static PKM_Owner AliveCheck()
    {
        if (Battle_Manager.instance.Trainer_Manager.GetCurPokemon() == null)
            return PKM_Owner.Player;
        if(Battle_Manager.instance.Enemy_Manager.GetCurPokemon() == null)
            return PKM_Owner.Enemy;
        else
           return PKM_Owner.None;
    }
}
