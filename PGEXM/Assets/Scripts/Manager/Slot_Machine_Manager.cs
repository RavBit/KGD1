using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot_Machine_Manager : MonoBehaviour {
    public GameObject BaseObject;
    public void Start()
    {
        BaseObject.SetActive(false);
        Battle_Manager.InitGamble += ToggleSM;
    }
    
    public void ToggleSM(bool toggle)
    {
        BaseObject.SetActive(toggle);
        Debug.Log("SET ACTIVE SM");
    }
}
