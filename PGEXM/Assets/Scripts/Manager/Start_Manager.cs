using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_Manager : MonoBehaviour {
    
    public void StartGame()
    {
        PlayerPrefs.SetInt("AmountOfPokemon", 3);
        SceneManager.LoadScene("pokemon_select");
    }
}
