using UnityEngine;
using System.Collections;

public class Seek : MonoBehaviour {

	public float max_velocity;
	public Vector3 velocity;
	public Transform target;
	// Use this for initialization
	void Start()
	{
		max_velocity = 8;
	}

	// Update this called once per frame
	void Update()
	{
		// disired_velocity = normalize(target.position - transform.position) * max_velocity
		// steering = desired_velocity - currentVelocity
		Vector3 desiredVelocity = (target.position - transform.position).normalized * max_velocity;
		Vector3 steergingForce = desiredVelocity - velocity;

		Vector3 acc = steergingForce / 1;
		velocity += acc * Time.deltaTime;
		transform.position += velocity * Time.deltaTime;

		if (velocity.magnitude > 0.01f) {
			Vector3 newForward = Vector3.Slerp (transform.forward, velocity, Time.deltaTime);

			newForward.y = 0;
			transform.forward = newForward;
		}
	}


}
