using UnityEngine;
using System.Collections;

public class Item_Coin : MonoBehaviour {

	public int value = 1;

	void Start () {
		Invoke ("Death", 15.0f);
	}
	
	void Death(){
		GameObject.DestroyObject (this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		//go towards player if player is close
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		float distance = Vector3.Distance(player.transform.position, transform.position);
		if (distance < 0.95f) {
			Vector3 force = player.transform.position - transform.position;
			force.Normalize ();
			rigidbody2D.AddForce (force * 3.0f);
			Vector2 rigidVel = rigidbody2D.velocity;
			if (rigidVel.magnitude > 5.0f) {
				//clamp
				rigidVel.Normalize();
				rigidVel *= 5.0f;
				rigidbody2D.velocity = rigidVel;
			}

		} else {
			//reduce
			rigidbody2D.velocity *= 0.9f;

		}

	}

}
