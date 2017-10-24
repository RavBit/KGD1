using System.Collections;
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
    [SerializeField]
    public Battle_Menu_Manager BMM;
    public static Battle_Manager instance;
    //Actions
    public Battle_State _currentstate = Battle_State.Turn;
    [SerializeField]
    private GameObject Turn_Menu;

    private string trainer_attack;


    public bool PokemonDied;
    void Awake()
    {
        if (instance != null)
            Debug.LogError("More than one Pokemon Collections in the scene");
        else
            instance = this;
        Event_Manager.SwitchBattleState += ChangeState;
        Event_Manager.PokemonKill += Die;
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
                TurnToggle(false);
                Event_Manager.Start_Gamble(false);
                break;
        }
    }
    public void StartAttack(int score, string attack) {

        trainer_attack = attack;
        StartCoroutine("ATTACKMngr", score);
    }
    public IEnumerator ATTACKMngr(int score)
    {
        Event_Manager.Pokemon_AliveCheck();
        int random = 0;
        if (Trainer_Manager.GetCurPokemonList().Count == 0 || Enemy_Manager.GetCurPokemonList().Count == 0)
        {
            Time.timeScale = 0;
            BMM.Game_Over.SetActive(true);
        }
        while (random != 2) {
            Event_Manager.Update_PlayerInterface();
            if (random == 0 && Event_Manager.AliveCheck() != PKM_Owner.Player && score != 0)
            {
                Attack(PKM_Owner.Player, (-1 * (int)score));
                yield return new WaitForSeconds(4);
            }
            if (random == 1 && Event_Manager.AliveCheck() != PKM_Owner.Enemy) {
                System.Random rand = new System.Random();
                int r = rand.Next(Enemy_Manager.GetCurPokemon().attacks.Count);
                float attack = Enemy_Manager.GetCurPokemon().attacks[r].strength * Random.Range(1, 3);
                Event_Manager.Set_MessageText("Foe's " + Enemy_Manager.GetCurPokemon().Name + " used " + Enemy_Manager.GetCurPokemon().attacks[r].name);
                Attack(PKM_Owner.Enemy, (-1 * (int)attack));
                yield return new WaitForSeconds(4);
            }
            random++;
        }
        if (Event_Manager.AliveCheck() == PKM_Owner.None)
        {
            Event_Manager.Update_PlayerInterface();
            Event_Manager.Set_MessageText("");
            ChangeState(Battle_State.Turn);
        }
    }
    public void Attack(PKM_Owner _curattacker, int damage)
    {
        switch (_curattacker)
        {
            case PKM_Owner.Player:
                Enemy_Manager.AdjustHealth(damage);
                Event_Manager.Set_MessageText(Trainer_Manager.GetCurPokemon().Name + " used " + trainer_attack);
                break;
            case PKM_Owner.Enemy:
                Trainer_Manager.AdjustHealth(damage);
                break;
        }
        Event_Manager.Pokemon_AliveCheck();
        new WaitForSeconds(1);
        if (Event_Manager.AliveCheck() != PKM_Owner.None)
        {
            Invoke("SendInNewPokemon", 3);
        }
    }
    void SendInNewPokemon()
    {
        Event_Manager.Set_MessageText("Pokemon Fainted");
        StopCoroutine("ATTACKMngr");
        StartCoroutine("SetNewPokemon", Event_Manager.AliveCheck());
    }
    public IEnumerator SetNewPokemon(PKM_Owner owner)
    {
        if (Trainer_Manager.GetCurPokemonList().Count == 0 || Enemy_Manager.GetCurPokemonList().Count == 0)
        {
            Time.timeScale = 0;
            BMM.Game_Over.SetActive(true);
        }
        SwitchPokemon(owner);
        yield return new WaitForSeconds(3);
        Event_Manager.Set_NewPokemon();
        yield return new WaitForSeconds(2F);
        Event_Manager.Set_MessageText("");
        ChangeState(Battle_State.Turn);

    }

    public void SwitchPokemon(PKM_Owner owner)
    {
        Debug.Log("SWITCH POKEMON OWNER : " + owner);
        switch (owner)
        {
            case PKM_Owner.Player:
                System.Random rand1 = new System.Random();
                int r1 = rand1.Next(Trainer_Manager.GetCurPokemonList().Count);
                Trainer_Manager.SetCurPokemon(Trainer_Manager.GetCurPokemonList()[r1]);
                Debug.Log("Setcur pokemon : " + Trainer_Manager.GetCurPokemonList()[r1]);
                Event_Manager.Set_MessageText("You send out " + Trainer_Manager.GetCurPokemonList()[r1].Name + "!");
                Event_Manager.Set_NewPokemon();
                break;
            case PKM_Owner.Enemy:
                System.Random rand = new System.Random();
                int r = rand.Next(Enemy_Manager.GetCurPokemonList().Count);
                Enemy_Manager.SetCurPokemon(Enemy_Manager.GetCurPokemonList()[r]);
                Debug.Log("CUR POKEMON ENEMY " + Enemy_Manager.GetCurPokemon().Name);
                Event_Manager.Set_MessageText("Enemy send out " + Enemy_Manager.GetCurPokemonList()[r].Name + "!");
                Event_Manager.Set_NewPokemon();
                break;
        }
        BMM.RemovePanels(PKM_Owner.Player);
        BMM.SetPanels();

    }

    public void Die(PKM_Owner player)
    {
        switch (player)
        {
            case PKM_Owner.Player:
                Debug.Log("Player ");
                Trainer_Manager.GetCurPokemonList().Remove(Trainer_Manager.GetCurPokemon());
                break;
            case PKM_Owner.Enemy:
                Debug.Log("Enemy ");
                Enemy_Manager.GetCurPokemonList().Remove(Enemy_Manager.GetCurPokemon());
                break;
        }
    }
    void FlashSprite(Sprite sprite) {
     //sprite
    }

    void TurnToggle(bool toggle)
    {
        Turn_Menu.SetActive(toggle);
    }
}
