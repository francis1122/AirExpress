using UnityEngine;
using System.Collections;

public class PlayerPlaneController : MonoBehaviour {

	//Vector2
	public float speed;
	public float thrustForce = 3.0f;
	public float rotationRate = 0.12f;
	public float maxSpeed = 3.0f;

	public float maxSpeedScalar = 1.0f;
	public float thrustForceScalar = 1.0f;
	public float rotationRateScalar = 1.0f;
	
	
	// Use this for initialization
	public float player_angle;

	// Use this for initialization
	void Start () {
	
	}
	
	void FixedUpdate () {
		//	speed *= .95f;
		Vector2 direction = Vector2.zero;
		direction.x = Mathf.Cos (player_angle);
		direction.y = Mathf.Sin (player_angle);
		PlayerControl playerControl = GetComponent<PlayerControl>();
		if (playerControl.Forward)
		{
				direction *= thrustForce * thrustForceScalar;
				rigidbody2D.AddForce (direction);
			
		}
		if (playerControl.Right){
			player_angle -= rotationRate * rotationRateScalar;
		}
		if (playerControl.Left){
			player_angle += rotationRate * rotationRateScalar;
		}

		Vector2 rigidVel = rigidbody2D.velocity;
		float max = maxSpeed * maxSpeedScalar;
		
		if (rigidVel.magnitude > max) {
			//clamp
			rigidVel.Normalize();
			rigidVel *= max;
			rigidbody2D.velocity = rigidVel;
		}

		rigidbody2D.angularVelocity = 0.0f;
		
	}
}
