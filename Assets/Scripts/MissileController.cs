using UnityEngine;
using System.Collections;

public class MissileController : MonoBehaviour {

    public GameObject explosionPrefab;

    public float maxLinearVelocity = 20.0f;
    private float angle;

    private Vector3 velocity;
    private Vector3 directionVector;

    public LineRenderer lineRenderer;

    private const int FLASH_COUNT = 4;
    private const float FLASH_INTERVAL = 0.05f;
    private const float DECAY_TIME = 6.0f;

    private float decayTime = 0.0f;
    private float flashTime = 0.0f;
    private int flashCount = 0;
    private bool flashThisTick = false;

    public void Fire (float angle) {
        this.angle = angle;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, angle);
        directionVector = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle), 0.0f);
        directionVector.Normalize();
        velocity = directionVector * -maxLinearVelocity;
    }
    
    void Update () {
        decayTime += Time.deltaTime;
        if (decayTime > DECAY_TIME) {
            Destroy(gameObject);
        }

        if (flashCount < FLASH_COUNT) {
            flashTime += Time.deltaTime;
            if (flashTime > FLASH_INTERVAL) {
                if (flashThisTick) {
                    flashCount++;
                    if (flashCount == FLASH_COUNT) {
                        lineRenderer.enabled = false;
                    }
                    else {
                        lineRenderer.enabled = true;
                        lineRenderer.SetPosition(0, transform.position);
                        lineRenderer.SetPosition(1, transform.position + (-directionVector * 200));
                    }
                    
                } else {
                    lineRenderer.enabled = false;
                }
                
                flashThisTick = !flashThisTick;
                flashTime = 0;
            }
        }
    }

	void FixedUpdate () {
        Vector3 position = transform.position;
        position += velocity * Time.fixedDeltaTime;
        transform.position = position;
	}

    void OnCollisionEnter (Collision collision) {
        GameObject explosion = Instantiate(explosionPrefab);
        explosion.transform.position = transform.position;
        Destroy(gameObject);
    }
}
