using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PKM_Attack {
    [Header("Attack Settings: ")]
    [Tooltip("Name of the panel")]
    public string name;
    [Tooltip("Description of the attack")]
    [TextArea]
    public string description;
    [Range(0, 100)]
    [Tooltip("Spawnrate of how much the panel will spawn")]
    public float strength = 50;
    [Tooltip("Icon of the attack")]
    public Sprite icon;
    [Tooltip("Type of attack:")]
    public Type typeattack;
}