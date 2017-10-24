using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu_Manager : MonoBehaviour {
    public void RestartGame()
    {
        PlayerPrefs.SetInt("AmountOfPokemon", PlayerPrefs.GetInt("AmountOfPokemon") + 1);
        Destroy(Pokemon_Collections.instance);
        Destroy(SM_Panels.instance);
        Destroy(Battle_Manager.instance);
        Time.timeScale = 1;
        SceneManager.LoadScene("pokemon_select");
    }
    public void QuitGame()
    {
        PlayerPrefs.SetInt("AmountOfPokemon", 3);
        Application.Quit();
    }

}
