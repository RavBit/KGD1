using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_Manager : MonoBehaviour {
    
    public void StartGame()
    {
        SceneManager.LoadScene("pokemon_select");
    }
}
