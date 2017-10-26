using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Battle_State {
    Turn,
    Gamble,
    Fight
}
public class Battle_Manager : MonoBehaviour {
    [SerializeField]
    public Enemy_Manager enemy_Manager;
    [SerializeField]
    public Trainer_Manager trainer_Manager;
    [SerializeField]
    public Battle_Menu_Manager bMM;
    public static Battle_Manager instance;
    //Actions
    public Battle_State _currentstate = Battle_State.Turn;
    [SerializeField]
    private GameObject Turn_Menu;

    public AudioSource Attack_Sound;

    private string trainer_attack;


    private void Awake() {
        Event_Manager.SwitchBattleState += ChangeState;
        Event_Manager.PokemonKill += Die;
        Event_Manager.Switch_BattleState(Battle_State.Turn);
        InitEnemy();
        if (instance != null)
            Debug.LogError("More than one Pokemon Collections in the scene");
        else
            instance = this;
    }
    //Instantiate the Enemy and load in the pokemon databank
    private void InitEnemy() {
        trainer_Manager.Start();
        enemy_Manager.Start();
        Event_Manager.Loading_DataBank();
    }

    //Called when you want to change the battle state. Think of from Fighting to the menu where the player can select the attack again.
    public void ChangeState(Battle_State _newstate) {
        _currentstate = _newstate;
        switch (_newstate) {
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
    //Start the attack with the score and attack name given from the SM_Calculator. That script calculates and 
    public void StartAttack(int score, string attack) {
        trainer_attack = attack;
        StartCoroutine("AttackMngr", score);
    }
    //Manages the attack and for the player as the enemy I use the same function and in that order I let them attack
    public IEnumerator AttackMngr(int score) {
        Event_Manager.Pokemon_AliveCheck();
        int random = 0;
        //If one of the pokemon isn't alive it will trigger this if statement and the enemy/player will send in a new pokemon. If the pokemon count is zero it will trigger the game over screen.
        if (trainer_Manager.GetCurPokemonList().Count == 0 || enemy_Manager.GetCurPokemonList().Count == 0) {
            Time.timeScale = 0;
            bMM.game_over.SetActive(true);
        }
        while (random != 2) {
            Event_Manager.Update_PlayerInterface();
            if (random == 0 && Event_Manager.AliveCheck() != pkm_owner.Player && score != 0) {
                Attack(pkm_owner.Player, (-1 * (int)score));
                yield return new WaitForSeconds(4);
            }
            if (random == 1 && Event_Manager.AliveCheck() != pkm_owner.Enemy) {
                System.Random rand = new System.Random();
                int r = rand.Next(enemy_Manager.GetCurPokemon().attacks.Count);
                float attack = enemy_Manager.GetCurPokemon().attacks[r].strength * Random.Range(1, 3);
                Event_Manager.Set_MessageText("Foe's " + enemy_Manager.GetCurPokemon().name + " used " + enemy_Manager.GetCurPokemon().attacks[r].name);
                Attack(pkm_owner.Enemy, (-1 * (int)attack));
                yield return new WaitForSeconds(4);
            }
            random++;
        }
        //Check if the pokemons are still alive. If they are it continues this if statement.
        if (Event_Manager.AliveCheck() == pkm_owner.None) {
            Event_Manager.Update_PlayerInterface();
            Event_Manager.Set_MessageText("");
            ChangeState(Battle_State.Turn);
        }
    }
    //The function that comes after the enumerator. To adjust the health of the attacks
    public void Attack(pkm_owner _curattacker, int damage) {
        switch (_curattacker) {
            case pkm_owner.Player:
                enemy_Manager.AdjustHealth(damage);
                StartCoroutine("BlinkSprite", _curattacker);
                Attack_Sound.Play();
                Event_Manager.Set_MessageText(trainer_Manager.GetCurPokemon().name + " used " + trainer_attack);
                break;
            case pkm_owner.Enemy:
                StartCoroutine("BlinkSprite", _curattacker);
                Attack_Sound.Play();
                trainer_Manager.AdjustHealth(damage);
                break;
        }
        Event_Manager.Pokemon_AliveCheck();
        new WaitForSeconds(1);
        if (Event_Manager.AliveCheck() != pkm_owner.None) {
            Invoke("SendInNewPokemon", 3);
        }
    }
    //If the player get's attacked the sprite will blink. On that way you can visually see the player has been attacked
    public IEnumerator BlinkSprite(pkm_owner owner) {
        switch (owner) {
            case pkm_owner.Player:
                bMM.enemy_pokemon_front.color = Color.black;
                yield return new WaitForSeconds(0.05f);
                bMM.enemy_pokemon_front.color = Color.white;
                break;
            case pkm_owner.Enemy:
                bMM.cur_pokemon_back.color = Color.black;
                yield return new WaitForSeconds(0.05f);
                bMM.cur_pokemon_back.color = Color.white;
                break;
        }
    }
    //This function is when your pokemon dies and a new pokemon needs to be set in
    private void SendInNewPokemon() {
        Event_Manager.Set_MessageText("Pokemon Fainted");
        StopCoroutine("ATTACKMngr");
        StartCoroutine("SetNewPokemon", Event_Manager.AliveCheck());
    }
    //An enumarator to time correctly when the new pokemon needs to be set in. It also changes the text and continues to an next function
    public IEnumerator SetNewPokemon(pkm_owner owner) {
        if (trainer_Manager.GetCurPokemonList().Count == 0 || enemy_Manager.GetCurPokemonList().Count == 0) {
            Time.timeScale = 0;
            bMM.game_over.SetActive(true);
        }
        SwitchPokemon(owner);
        yield return new WaitForSeconds(3);
        Event_Manager.Set_NewPokemon();
        yield return new WaitForSeconds(2F);
        Event_Manager.Set_MessageText("");
        ChangeState(Battle_State.Turn);

    }

    //This function adjusts the current pokemon to the new pokemon and chooses out a random pokemon
    public void SwitchPokemon(pkm_owner owner) {
        switch (owner) {
            case pkm_owner.Player:
                System.Random rand1 = new System.Random();
                int r1 = rand1.Next(trainer_Manager.GetCurPokemonList().Count);
                trainer_Manager.SetCurPokemon(trainer_Manager.GetCurPokemonList()[r1]);
                Event_Manager.Set_MessageText("You send out " + trainer_Manager.GetCurPokemonList()[r1].name + "!");
                Event_Manager.Set_NewPokemon();
                break;
            case pkm_owner.Enemy:
                System.Random rand = new System.Random();
                int r = rand.Next(enemy_Manager.GetCurPokemonList().Count);
                enemy_Manager.SetCurPokemon(enemy_Manager.GetCurPokemonList()[r]);
                Event_Manager.Set_MessageText("Enemy send out " + enemy_Manager.GetCurPokemonList()[r].name + "!");
                Event_Manager.Set_NewPokemon();
                break;
        }
        bMM.RemovePanels(pkm_owner.Player);
        bMM.SetPanels();

    }

    //This function is called when the pokemon dies. Refering to the Trainer or Enemy manager and triggering their current pokemon
    public void Die(pkm_owner player) {
        switch (player) {
            case pkm_owner.Player:
                trainer_Manager.GetCurPokemonList().Remove(trainer_Manager.GetCurPokemon());
                break;
            case pkm_owner.Enemy:
                enemy_Manager.GetCurPokemonList().Remove(enemy_Manager.GetCurPokemon());
                break;
        }
    }
    //To switch turnes you use this small function
    private void TurnToggle(bool toggle) {
        Turn_Menu.SetActive(toggle);
    }
}
