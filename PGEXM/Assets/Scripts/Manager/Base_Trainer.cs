using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Base_Trainer : IBase_Item {
    [SerializeField]
    protected int amount_pokemon = 3;
    [SerializeField]
    protected pkm_owner pkm_owner;
    [SerializeField]
    protected List<Pokemon> pokemon;
    [SerializeField]
    protected Pokemon curPokemon;
    public void Start() {
        Event_Manager.LoadingDataBank += InitPokemon;
        Event_Manager.PokemonAliveCheck += CheckAlive;
    }
    //Set the owner of the pokemon and instantiate it
    public void InitPokemon() {
        pokemon = Pokemon_Collections.instance.Pokemon.ToList();
        int amount = 0;
        while (amount < amount_pokemon && pkm_owner == pkm_owner.Enemy) {
            System.Random rand = new System.Random();
            int r = rand.Next(pokemon.Count);
            if (pokemon[r].pkm_owner == pkm_owner.None) {
                pokemon[r].pkm_owner = pkm_owner;
                amount++;
            }
        }
        foreach (Pokemon _pokemon in pokemon.ToList())
            if (_pokemon.pkm_owner != pkm_owner)
                pokemon.Remove(_pokemon);
        InitCurPokemon(0);
    }
    //Setting the current pokemon
    public void InitCurPokemon(int n) {
        curPokemon = pokemon[n];
    }
    //Set the owner of the current pokemon
    public void SetOwner() {
        foreach (Pokemon _pokemon in pokemon)
            _pokemon.pkm_owner = pkm_owner;
    }
    //Adjusting the health of the pokemon
    public void AdjustHealth(int modifier) {
        curPokemon.health = curPokemon.health + modifier;
    }
    //Checking if the pokemon it's health is above 0
    protected void CheckAlive() {
        if (curPokemon.health <= 0)
            Die();
    }
    //Function that will trigger an event to kill the pokemon 
    private void Die() {
        Event_Manager.Pokemon_Kill(pkm_owner);
        curPokemon = null;
    }
    //Get the current pokemon (since it's protected
    public Pokemon GetCurPokemon() {
        return curPokemon;
    }
    //Set the current pokemon
    public void SetCurPokemon(Pokemon _pkm) {
        curPokemon = _pkm;
    }
    //Get an list of the pokemon the Trainer/Enemy has at the moment
    public List<Pokemon> GetCurPokemonList() {
        return pokemon;
    }
}

//Interface with an AdjustHealth that has to be added to the Base_Trainer class
public interface IBase_Item {
    void AdjustHealth(int modifier);
}