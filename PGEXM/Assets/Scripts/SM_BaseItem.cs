using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Wheel { Wheel1, Wheel2, Wheel3 };

[RequireComponent(typeof(Image))]
public class SM_BaseItem : MonoBehaviour, SM_Items {
    [SerializeField]
    private static float _speed;
    [SerializeField]
    private Wheel baseWheel;
    private Image baseImage;
    void Start() {
        Init();
    }
    public void Init() {
        Menu_Manager.Test += Stop;
        _speed = 20;
        baseImage = GetComponent<Image>();
    }

    public void FixedUpdate() {
        Move(Vector3.down * _speed);
    }
    public void Move(Vector3 speed) {
        baseImage.rectTransform.Translate(speed);
        if (baseImage.rectTransform.localPosition.y <= -300)
            baseImage.transform.localPosition = new Vector3(0, 300, 0);
    }
    public void Stop(Wheel wheel) {
        Debug.Log("wheel " + wheel);
        if (wheel.ToString() != baseWheel.ToString())
            return;
        StartCoroutine("StopWheel", 10);
    }
    IEnumerator StopWheel(float time) {

        while(_speed > 0) {
            _speed = _speed - 1;
            yield return new WaitForSeconds(0.1f);
        }
        Debug.Log("STOPPED");
    }
}
