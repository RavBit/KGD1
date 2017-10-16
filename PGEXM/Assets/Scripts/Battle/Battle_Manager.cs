using System.Collections;
using UnityEngine;


public enum Battle_State
{
    Turn,
    Gamble,
    Fight
}
public class Battle_Manager : MonoBehaviour {
    public static Battle_Manager instance;

    public Battle_State _currentstate = Battle_State.Turn;

    [SerializeField]
    private GameObject Turn_Menu;
    public bool TimerDone = false;
    void Awake()
    {
        if (instance != null)
            Debug.LogError("More than one Pokemon Collections in the scene");
        else
            instance = this;
        DontDestroyOnLoad(transform.gameObject);
        ChangeState(Battle_State.Turn);
    }

    void ChangeState(Battle_State _newstate)
    {
        _currentstate = _newstate;
        switch (_newstate)
        {
            case Battle_State.Turn:
                Debug.Log("Test");
                StartTurn();
                break;
            case Battle_State.Gamble:
                Debug.Log("Gamble");
                break;
        }
    }

    void StartTurn()
    {
        //StartCoroutine(Timer(3));
        Turn_Menu.SetActive(true);
    }

   /*IEnumerator Timer(int time)
        {
            while (time > 0)
            {
                Debug.Log("TIme: " + time);
                time--;
                yield return new WaitForSeconds(1f);
            }
            TimerDone = true;
        }
       */
    
}
