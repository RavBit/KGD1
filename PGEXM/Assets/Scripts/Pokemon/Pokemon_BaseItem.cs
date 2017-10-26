using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



[System.Serializable]
public class Pokemon_BaseItem {
    [Header("Basic settings:")]
    public string name;
    public Type Type;
    public int health = 100;
    public Sprite sprite;
    public Sprite backSprite;
    [Space(3)]
    [Header("Attack settings:")]
    public List<PKM_Attack> attacks = new List<PKM_Attack>(4);
    [Header("Owner:")]
    public pkm_owner pkm_owner = pkm_owner.None;
}



//The types that are used in the game
public enum Type {
    Fire,
    Grass,
    Water,
    Normal
}