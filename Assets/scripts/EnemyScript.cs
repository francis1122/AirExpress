using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {
	
	public GameObject deathExplosion;
	public bool isAggro;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		//check if should be destroyed
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		float distance = Vector3.Distance (player.transform.position, transform.position);
		if (distance > 20.0f) {
			GameObject.DestroyObject (this.gameObject);
		}

	}

	void onDeath(){
		//deathExplosion
		GameObject.DestroyObject (this.gameObject);
		Instantiate (deathExplosion, transform.position, Quaternion.identity);
		//leave behind a coin
		Instantiate (GameManager.sceneManager.coinObject, transform.position, Quaternion.identity);
	}

	void OnTriggerEnter2D(Collider2D collision){
		if (collision.gameObject.tag == "Bullet") {
			//destroy self
			this.GetComponent<HealthScript>().offsetHealth(-1);
			if(this.GetComponent<HealthScript>().currentHealth <= 0){
				onDeath ();
				GameObject.DestroyObject (collision.gameObject);
			}
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "Player") {
			this.GetComponent<HealthScript>().offsetHealth(-1);
			if(this.GetComponent<HealthScript>().currentHealth <= 0){
				onDeath ();
			}
		}
	}
}