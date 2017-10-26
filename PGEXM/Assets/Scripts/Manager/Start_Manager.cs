using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_Manager : MonoBehaviour {

    //Start the game when this function is called and loading a new scene
    public void StartGame() {
        PlayerPrefs.SetInt("AmountOfPokemon", 3);
        SceneManager.LoadScene("pokemon_select");
    }
}
