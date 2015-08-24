using UnityEngine;
using System.Collections;

public class WarheadController : MonoBehaviour {
    public GameManager manager;
    public GameObject spawner;

    public GameObject explosionPrefab;

    public float maxLinearSpeed = 50.0f;
    public float maxLinearAcceleration = 0.1f;
    public float maxAngularSpeed = 100.0f;
    public float maxAngularAcceleration = 0.1f;
    public float angularDamping = 40.0f;

    private float currentLinearAcceleration = 0;
    private float currentAngularAcceleration = 0;
    public Vector3 linearVelocity;
    private int angularDirection = 0;
    private float angularVelocity = 0;

    private ParticleSystem particles;
    private bool inFlight = false;

	void Start () {
        particles = GetComponentInChildren<ParticleSystem>();
        if (particles == null) {
            Debug.Log("Cannot find particles for warhead");
        }
        particles.Stop();

        transform.rotation = Quaternion.Euler(0.0f, 0.0f, 15.0f);
	}

    private void Launch() {
        currentLinearAcceleration = maxLinearAcceleration;
        currentAngularAcceleration = maxAngularAcceleration;
        GetComponent<AudioSource>().Play();
        particles.Play();
        spawner.SetActive(true);
        manager.Begin();
    }

    private void UpdateInput() {
        if (!inFlight) {
            if (Input.GetKey(KeyCode.Space)) {
                inFlight = true;
                Launch();
            }
        }
        else {

            if (Input.GetKey(KeyCode.W))
                angularDirection = 1;
            else if (Input.GetKey(KeyCode.S))
                angularDirection = -1;
            else angularDirection = 0;


        }
    }

	// Update is called once per frame
	void FixedUpdate () {
        UpdateInput();

        Vector3 linearForce = new Vector3(Mathf.Cos(Mathf.Deg2Rad * transform.rotation.eulerAngles.z), Mathf.Sin(Mathf.Deg2Rad * transform.rotation.eulerAngles.z), 0.0f);
        linearForce .Normalize();
        linearForce *= currentLinearAcceleration;

        linearVelocity += linearForce;
        linearVelocity= Vector3.ClampMagnitude(linearVelocity, maxLinearSpeed);
        Vector3 position = transform.position;
        position = position + (linearVelocity* Time.fixedDeltaTime);
        transform.position = position;

        if (Mathf.Abs(angularVelocity) > angularDamping)
            angularVelocity += (angularVelocity > 0.0f) ? -angularDamping : angularDamping;
        else
            angularVelocity = 0;

        float angularForce = angularDirection * currentAngularAcceleration * Time.fixedDeltaTime;
        angularVelocity += angularForce;
        angularVelocity = Mathf.Clamp(angularVelocity, -maxAngularSpeed, maxAngularSpeed);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + (angularVelocity * Time.fixedDeltaTime));
    }

    void OnCollisionEnter(Collision collision) {
        gameObject.SetActive(false);
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        if (collision.collider.tag == "City") {
            manager.Victory();
        } else {
            manager.Defeat();
        }

        
    } 


}
