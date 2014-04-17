using UnityEngine;
using System.Collections;

public class WingBodySpriteControl : MonoBehaviour {

	public bool rotateShipByVelocity = true;
	public GameObject thrustSprite;
	public bool isPlayer = false;
	public GameObject wings;
	public GameObject body;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		float rotation;


		if (isPlayer) {
			PlayerPlaneController control = GetComponent<PlayerPlaneController>();
			rotation = control.player_angle;
			rigidbody2D.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, rotation * Mathf.Rad2Deg));

		} else {
			Vector2 normalizedVelocity = rigidbody2D.velocity.normalized;
			rotation = Mathf.Atan2 (normalizedVelocity.y, normalizedVelocity.x);
			if (rotateShipByVelocity) {
				rigidbody2D.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, rotation * Mathf.Rad2Deg));
			}
		}

		//wing graphics

		float testY = Mathf.Abs (Mathf.Sin (rotation));
		float testX = Mathf.Abs (Mathf.Cos (rotation));
		if (testY < 0.1f) {
			testY = 0.0f;
		}
		if (testY > 0.7f) {
			testY = 0.7f;
		}
		if (wings) {
			wings.GetComponent<SpriteRenderer> ().transform.localScale = new Vector3 (testY, 0.7f, 1.0f);
		}


		//animate thruster
		if (isPlayer) {
			if (GetComponent<PlayerControl> ().Forward) {
					thrustSprite.GetComponent<SpriteRenderer> ().enabled = true;
				} else {
					thrustSprite.GetComponent<SpriteRenderer> ().enabled = false;
				}

			if (GetComponent<Weapon_Player_Boost> ().boostingTimer > 0.0f) {
						thrustSprite.transform.localScale =  new Vector3(2.0f, 2.5f, 1.0f);
				} else {
						thrustSprite.transform.localScale =  new Vector3(1.5f, 1.5f, 1.0f);
				}
			}

	}
}
