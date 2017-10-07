using System.Collections;
using UnityEngine;
namespace Sergi.Pooling {
    public class ObjectLauncher : MonoBehaviour {
        public Vector3 LaunchVector;
        public void Launch(Object obj) {
            var body = (obj as GameObject).GetComponent<Rigidbody>();
            (obj as GameObject).SetActive(true);
            body.transform.position = transform.position;
            body.transform.rotation = transform.rotation;
            body.velocity = Vector3.zero;
            Debug.Log("LaunchV: " + LaunchVector);
            body.AddForce(LaunchVector, ForceMode.VelocityChange);
        }
    }
}
