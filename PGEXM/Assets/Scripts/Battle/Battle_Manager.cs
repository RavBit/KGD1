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
    //Actions
    public delegate void StartGamble(bool toggle);
    public static event StartGamble InitGamble;
    public Battle_State _currentstate = Battle_State.Turn;
    public static System.Action<Battle_State> SwitchState;

    [SerializeField]
    private GameObject Turn_Menu;
    void Awake()
    {
        if (instance != null)
            Debug.LogError("More than one Pokemon Collections in the scene");
        else
            instance = this;
        DontDestroyOnLoad(transform.gameObject);
        SwitchState += ChangeState;
        SwitchState(Battle_State.Turn);
    }

    public void ChangeState(Battle_State _newstate)
    {
        _currentstate = _newstate;
        switch (_newstate)
        {
            case Battle_State.Turn:
                TurnToggle(true);
                break;
            case Battle_State.Gamble:
                TurnToggle(false);
                InitGamble(true);
                break;
            case Battle_State.Fight:
                InitGamble(false);
                break;
        }
    }

    void TurnToggle(bool toggle)
    {
        Turn_Menu.SetActive(toggle);
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
