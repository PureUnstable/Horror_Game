using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_spawn_manager : MonoBehaviour {

	public PlayerHealth playerHealth;
	public GameObject enemy;
	public float spawn_time = 3f;
	public Transform[] spawn_points;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("Spawn", spawn_time, spawn_time);
	}
	
	void Spawn()
	{
		if (playerHealth.current_health <= 0f) {
			return;
		}

		int spawnPointIndex = Random.Range (0, spawn_points.Length);

		Instantiate (enemy, spawn_points[spawnPointIndex].position, spawn_points[spawnPointIndex].rotation);
		Destroy (spawn_points [spawnPointIndex].gameObject);
	}

}
