using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



[System.Serializable]
public class Pokemon_BaseItem {
    [Header("Basic settings:")]
    public string Name;
    public Type Type;
    public int Health;
    public Sprite Sprite;
    [Space(3)]
    [Header("Attack settings:")]
    public List<PKM_Attack> attacks = new List<PKM_Attack>(4);
    [Header("Owner:")]
    public PKM_Owner PKM_Owner = PKM_Owner.None;
}

public enum Type {
    Fire,
    Grass,
    Water,
    Normal
}