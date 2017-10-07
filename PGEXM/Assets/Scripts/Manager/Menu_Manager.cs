using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Menu_Manager : MonoBehaviour {

    public delegate void ClickAction(Wheel time);
    public static event ClickAction Test;
    // Use this for initialization

    public void StopWheel(string Wheel_string) {
        Wheel wheel = (Wheel)System.Enum.Parse(typeof(Wheel), Wheel_string);
        Test(wheel);
    }
}
