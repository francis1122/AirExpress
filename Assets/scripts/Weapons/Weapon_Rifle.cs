using UnityEngine;
using System.Collections;

public class Weapon_Rifle : MonoBehaviour {

	public GameObject objectBullet;
	public GameObject target;
	public float shootingDelay = 0.5f;
	public float bulletSpeed = 7.0f;
	public float shootAtDistance = 3.5f;
	public int bulletsInClip  = 3;
	public float reloadTime = 1.5f;

	private bool targetAcquired = false;
	private float bulletCooldown = 0.1f;
	private float reloadCooldown = 1.5f;
	private int bulletsRemaining = 0;


	// Use this for initialization
	void Start () {
		bulletsRemaining = bulletsInClip;
	}
	
	void Update () {
		
		bulletCooldown -= Time.deltaTime;
		reloadCooldown -= Time.deltaTime;
		if (GetComponent<EnemyScript> ().isAggro) {
						if (!targetAcquired) {
								if (reloadCooldown <= 0.0f) {
										GameObject player = GameObject.FindGameObjectWithTag ("Player");
										float distance = Vector3.Distance (player.transform.position, transform.position);
										if (distance < shootAtDistance) {
												//acquire target, is it in range and am i pointing towards it
												targetAcquired = true;
										}
								}
						}

						if (targetAcquired) {
								this.ShootBullet ();
						}
				}
	}
	
	void ShootBullet(){
		if (bulletCooldown <= 0.0f) {
			GameObject bulletTemp = (GameObject)Instantiate (objectBullet, transform.position, Quaternion.identity);
			bulletTemp.tag = "EnemyBullet";
			if(target){
				Vector3 direction = target.transform.position - transform.position;
				direction.Normalize ();
				bulletTemp.rigidbody2D.velocity = new Vector2 (direction.x * bulletSpeed, direction.y * bulletSpeed);
			}else{
				Vector2 direction = rigidbody2D.velocity;
				direction.Normalize();
				bulletTemp.rigidbody2D.velocity = new Vector2 (direction.x * bulletSpeed, direction.y * bulletSpeed);
			}
			bulletCooldown = shootingDelay;
			bulletsRemaining--;
			if(bulletsRemaining <= 0){
				reloadCooldown = reloadTime;
				bulletsRemaining = bulletsInClip;
				targetAcquired = false;
			}
		}
	}
}
