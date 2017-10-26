using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu_Manager : MonoBehaviour {

    //Quit the game when you press the button
    public void QuitGame() {
        PlayerPrefs.SetInt("AmountOfPokemon", 3);
        Application.Quit();
    }

}
