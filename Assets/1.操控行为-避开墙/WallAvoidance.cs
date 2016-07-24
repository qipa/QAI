using UnityEngine;
using System.Collections;


/// <summary>
/// 避开墙行为
/// </summary>
public class WallAvoidance : MonoBehaviour {


	/// <summary>
	/// 速度
	/// </summary>
	public Vector3 velocity;

	/// <summary>
	/// 探针长度
	/// </summary>
	public float spinLength;

	void Start()
	{
		spinLength = 4;
	}

	void Update()
	{
		Vector3 steeringForce = new Vector3 (0, 0, 0);
		RaycastHit hitForward, hitLeft, hitRight;
		Vector3 offset = new Vector3 (0, 1, 0);
		Vector3 newPosition = transform.position + offset;
		Debug.DrawLine (newPosition, newPosition + spinLength * transform.forward, Color.green);
		Debug.DrawLine (newPosition, newPosition + spinLength * (transform.forward - transform.right), Color.green);
		Debug.DrawLine (newPosition, newPosition + spinLength * (transform.forward + transform.right), Color.green);

		if (Physics.Raycast (newPosition, transform.forward, out hitForward, spinLength, 1 << 8)) {
			steeringForce += (spinLength - (newPosition - hitForward.point).magnitude) * hitForward.normal;
		}
			
		if (Physics.Raycast (newPosition, transform.forward, out hitLeft, spinLength, 1 << 8)) {
			steeringForce += (spinLength - (newPosition - hitLeft.point).magnitude) * hitForward.normal;
		}

		if (Physics.Raycast (newPosition, transform.forward, out hitRight, spinLength, 1 << 8)) {
			steeringForce += (spinLength - (newPosition - hitRight.point).magnitude) * hitForward.normal;
		}

		Vector3 acc = steeringForce / 1;
		velocity += acc * Time.deltaTime;

		transform.position += velocity * Time.deltaTime;

		if (velocity.magnitude > 0.01f) {
			Vector3 newForward = Vector3.Slerp (transform.forward, velocity, Time.deltaTime);
			newForward.y = 0;
			transform.forward = newForward;
		}
	}
}
