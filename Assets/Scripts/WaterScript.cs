using UnityEngine;
using System.Collections;

public class WaterScript : MonoBehaviour {

    public Transform target;

    void Start() {

    }

    void Update() {
        Transform trans = GetComponent<Transform>();
        trans.position = new Vector3(target.position.x, transform.position.y, transform.position.z);
    }

}
