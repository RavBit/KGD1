using System.Collections;
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

    public delegate void EnemyAttack(int damage);
    public static event EnemyAttack EnemyAttacked;
    public static void Start_Gamble(bool toggle)
    {
        InitGamble(toggle);
    }
    public static void Switch_State(Turn_Menu state)
    {
        SwitchTurnState(state);
    }
    public static void Switch_BattleState(Battle_State state)
    {
        SwitchBattleState(state);
    }

}
