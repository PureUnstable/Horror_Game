using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class standard_roam_script : MonoBehaviour {

	public float wander_radius;
	public float wander_timer;

	private Transform target;
	private NavMeshAgent agent;
	private float timer;


	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		timer = wander_timer;
	}
	
	// Update is called once per frame
	void Update () {
		if (enabled) {
			timer += Time.deltaTime;

			if (timer >= wander_timer) {
				Vector3 newPos = RandomNavSphere (transform.position, wander_radius, -1);
				agent.SetDestination (newPos);
				timer = 0;
			}
		}
	}

	public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
	{
		Vector3 randDirection = Random.insideUnitSphere * dist;
		randDirection += origin;
		NavMeshHit navHit;

		NavMesh.SamplePosition (randDirection, out navHit, dist, layermask);
		return navHit.position;
	}

}
