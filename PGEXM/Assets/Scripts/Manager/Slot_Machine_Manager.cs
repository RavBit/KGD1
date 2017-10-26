using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot_Machine_Manager : MonoBehaviour {
    public GameObject baseObject;
    public void Start() {
        baseObject.SetActive(false);
        Event_Manager.InitGamble += ToggleSM;
    }

    //Toggle the gameObject when the event is called
    public void ToggleSM(bool toggle) {
        baseObject.SetActive(toggle);
    }
}
