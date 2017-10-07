using UnityEngine;
using Sergi.Pooling;

public class MonoPool : MonoBehaviour {
    public int Capacity;
    public IPool<GameObject> Pool { get; private set; }
    public GameObject Prototype;

    void Awake() {
        Pool = new QueuePool<GameObject>(() => Instantiate(Prototype), Capacity); 
    }

}
