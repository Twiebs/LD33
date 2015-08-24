using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    public GameObject missilePrefab;

    public WarheadController target;
    
    float nextSpawnTime = 0.0f;
    float elapsedTime = -3.0f;

    private const float TARGET_X_OFFSET = 100.0f;
    private const float MIN_SPAWN_TIME = 0.0f;
    private const float MAX_SPAWN_TIME = 1.25f;
    private const float MAX_SPAWN_OFFSET = 8.0f;
    private const float MAX_SPAWN_ANGLE = 2.0f;

    void Start() {
        
    }

    private void SpawnMissile () {
        float yOffset =  Random.RandomRange(-MAX_SPAWN_OFFSET, MAX_SPAWN_OFFSET);
        float ypos = target.transform.position.y + yOffset;
        if (ypos < 1.0f) ypos = 1.0f;
        Vector3 missilePosition = new Vector3(target.transform.position.x + TARGET_X_OFFSET, ypos, target.transform.position.z);

        Vector3 estimatedPosition = target.transform.position + (target.linearVelocity * 7600.0f);
        float estimatedDist = Vector3.Distance(estimatedPosition, missilePosition);
        float angleOffset = Random.Range(-MAX_SPAWN_ANGLE, MAX_SPAWN_ANGLE);
        float height = missilePosition.y - estimatedPosition.y;
        float angle = (Mathf.Sin(height / estimatedDist) * Mathf.Rad2Deg) + angleOffset;

        GameObject missile = GameObject.Instantiate(missilePrefab);
        missile.transform.position = missilePosition;
        missile.transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
        MissileController controller = missile.GetComponent<MissileController>();
        controller.Fire(angle);
    }

	// Update is called once per frame
	void Update () {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > nextSpawnTime) {
            SpawnMissile();
            elapsedTime = 0;
            nextSpawnTime = Random.RandomRange(MIN_SPAWN_TIME, MAX_SPAWN_TIME);
        }
	}
}
