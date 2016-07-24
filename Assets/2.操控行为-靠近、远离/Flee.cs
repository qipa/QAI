using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// 逃离
/// </summary>
public class Flee : MonoBehaviour {

	public float max_velocity;
	public Vector3 velocity;
	public Transform target;
	// Use this for initialization

	private List<Vector3> pathList;
	private float timer;

	void Start()
	{
		max_velocity = 8;
		timer = 0;
		pathList = new List<Vector3> ();

		pathList.Add (transform.position);
	}

	// Update this called once per frame
	void Update()
	{
		timer += Time.deltaTime;
		if (timer > 0.2f) {
			timer = 0;
			pathList.Add (transform.position);
		}



		// disired_velocity = normalize(target.position - transform.position) * max_velocity
		// steering = desired_velocity - currentVelocity
		Vector3 desiredVelocity = ( transform.position - target.position).normalized * max_velocity;
		Vector3 steergingForce = desiredVelocity - velocity;

		Vector3 acc = steergingForce / 1;
		velocity += acc * Time.deltaTime;
		transform.position += velocity * Time.deltaTime;

		if (velocity.magnitude > 0.01f) {
			Vector3 newForward = Vector3.Slerp (transform.forward, velocity, Time.deltaTime);

			newForward.y = 0;
			transform.forward = newForward;
		}

		for (int i = 0; i < pathList.Count - 1; i++) {
			Debug.DrawLine (pathList[i],pathList[i+1],Color.red);
		}
	}


}
