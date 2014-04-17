using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke ("Death", 2.0f);
	}

	void Death(){
		GameObject.DestroyObject (this.gameObject);
	}
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D(Collider2D collision){
		if (tag == "EnemyBullet") {
						if (collision.gameObject.tag == "Player") {
								Death ();
						}
				}
		if (tag == "Bullet") {
				if(collision.gameObject.tag == "Enemy"){
					Death ();
					}
				}
		}

}
