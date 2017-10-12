using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokemon_Collections : MonoBehaviour {
    public static Pokemon_Collections instance;
    public List<Pokemon> Pokemon = new List<Pokemon>();
    void Awake() {
        if (instance != null)
            Debug.LogError("More than one Pokemon Collections in the scene");
        else
            instance = this;
        DontDestroyOnLoad(transform.gameObject);
    }
}

public enum PKM_Owner {
    Player,
    None,
    Enemy
}