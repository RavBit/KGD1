using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SM_BaseItem : MonoBehaviour, SM_Items {
    [SerializeField]
    public bool stopping = false;
    [SerializeField]
    private static float _speed;
    [SerializeField]
    public SM_PanelData curPanel;
    public Image baseImage;
    void Start() {
        Init();
    }
    public void Init() {
        _speed = 10;
        baseImage = GetComponent<Image>();
    }

    public void FixedUpdate() {
        if(!stopping)
            Move(Vector3.down * _speed);
    }
    public void Move(Vector3 speed) {
        baseImage.rectTransform.Translate(speed);
        if (baseImage.rectTransform.localPosition.y <= -300)
        {
            curPanel = SM_Panels.instance.PanelSpawn(curPanel, GetComponentInParent<SM_Wheel>());
            baseImage.sprite = curPanel.image;
            baseImage.transform.localPosition = new Vector3(0, 300, 0);
        }
    }
    public void Stop() {

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
