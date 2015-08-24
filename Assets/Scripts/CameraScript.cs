using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

    public Transform target;
    public float offset = 5.0f;

    private const float MAX_HEIGHT = 100.0f;

	private void Update () {
        float y = target.position.y;
        Vector3 position = new Vector3(target.position.x - offset, y, transform.position.z);
        transform.position = position;
	}
}
