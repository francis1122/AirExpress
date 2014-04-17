using UnityEngine;
using System.Collections;

public class PlaneController : MonoBehaviour {

	public float thrustForce = 30.0f;
//	public float rotationRate = 0.12f;
	public float maxSpeed = 50.0f;
	public float minSpeed = 15.0f;

	public Vector3 targetLocation;

	// Use this for initialization
	void Start () {
		thrustForce *= (Random.value / 3.0f) + .75f;
		maxSpeed *= (Random.value / 3.0f) + .75f;
		minSpeed *= (Random.value / 3.0f) + 0.75f;
		//targetLocation = transform.position;
	}
	
	void FixedUpdate () {
		// go after target
		Vector3 force = targetLocation - transform.position;
		force.Normalize ();
		rigidbody2D.AddForce (force * thrustForce);
		Vector2 rigidVel = rigidbody2D.velocity;

		// don't go faster than maxSpeed
		if (rigidVel.magnitude > maxSpeed) {
			//clamp
			rigidVel.Normalize();
			rigidVel *= maxSpeed;
			rigidbody2D.velocity = rigidVel;
		}

		// don't go slower than min speed
		if (rigidVel.magnitude < minSpeed) {
			rigidVel.Normalize ();
			rigidVel *= minSpeed;
			rigidbody2D.velocity = rigidVel;
		}
		rigidbody2D.angularVelocity = 0.0f;
	}
}
