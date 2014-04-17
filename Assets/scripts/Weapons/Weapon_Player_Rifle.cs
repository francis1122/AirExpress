using UnityEngine;
using System.Collections;

public class Weapon_Player_Rifle : MonoBehaviour {

	public GameObject objectBullet;
	public GameObject target;
	public float shootingDelay = 1.5f;
	public float bulletSpeed = 7.0f;
	
	private float bulletCooldown = 0.1f;
	
	// Use this for initialization
	void Start () {
		
	}
	
	void Update () {
		PlayerControl playerControl = GetComponent<PlayerControl>();
		bulletCooldown -= Time.deltaTime;
		
		if (bulletCooldown <= 0.0f) {
			GameObject player = GameObject.FindGameObjectWithTag ("Player");
			if(playerControl.isFire){
				//acquire target, is it in range and am i pointing towards it
				this.ShootBullet ();
			}
		}
	}
	
	void ShootBullet(){
		if (bulletCooldown <= 0.0f) {
			GameObject bulletTemp = (GameObject)Instantiate (objectBullet, transform.position, Quaternion.identity);
			bulletTemp.tag = "Bullet";
			if(target){
				Vector3 direction = target.transform.position - transform.position;
				direction.Normalize ();
				bulletTemp.rigidbody2D.velocity = new Vector2 (direction.x * bulletSpeed, direction.y * bulletSpeed);
			}else{
				PlayerPlaneController playerControl = GetComponent<PlayerPlaneController>();
				float testY = Mathf.Sin (playerControl.player_angle);
				float testX = Mathf.Cos (playerControl.player_angle);
				bulletTemp.rigidbody2D.velocity = new Vector2 (testX * bulletSpeed, testY * bulletSpeed);
			}
			bulletCooldown = shootingDelay;
		}
	}
}