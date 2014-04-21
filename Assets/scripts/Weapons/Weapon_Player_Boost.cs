using UnityEngine;
using System.Collections;

public class Weapon_Player_Boost : Weapon_Base {


	public float howLongBoostingLasts = 1.2f;
	public float timeBetweenBoosts = 2.4f;
	public float boostThrustForceScalar = .2f;
	public float boostMaxSpeedScalar = .3f;
	public float boostingTimer;
	public float boostingCooldown;


	// Use this for initialization
	void Start () {
	
	}

	public override void WeaponUpdate(){
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
	//	PlayerControl playerControl = player.GetComponent<PlayerControl>();
		boostingCooldown -= Time.deltaTime;
		if (boostingTimer >= 0.0f) {
			boostingTimer -= Time.deltaTime;
			if(boostingTimer < 0.0f){
				PlayerPlaneController playerPlaneControl = GetComponent<PlayerPlaneController>();
				playerPlaneControl.maxSpeedScalar -= boostMaxSpeedScalar;
				playerPlaneControl.thrustForceScalar -= boostThrustForceScalar;
			}
		}
		
		//if (playerControl.isBoost) {
		//	this.Boost();
		//}
		
		if (boostingTimer > 0.0f) {
			player.GetComponent<WingBodySpriteControl>().thrustSprite.transform.localScale =  new Vector3(2.0f, 2.5f, 1.0f);
		} else {
			player.GetComponent<WingBodySpriteControl>().thrustSprite.transform.localScale =  new Vector3(1.5f, 1.5f, 1.0f);
		}
	}

	// Update is called once per frame
	void Update () {
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
	//	PlayerControl playerControl = player.GetComponent<PlayerControl>();
		boostingCooldown -= Time.deltaTime;
		if (boostingTimer >= 0.0f) {
			boostingTimer -= Time.deltaTime;
			if(boostingTimer < 0.0f){
				PlayerPlaneController playerPlaneControl = player.GetComponent<PlayerPlaneController>();
				playerPlaneControl.maxSpeedScalar -= boostMaxSpeedScalar;
				playerPlaneControl.thrustForceScalar -= boostThrustForceScalar;
			}
		}
	
		//if (playerControl.isBoost) {
		//	this.Boost();
		//}

		if (boostingTimer > 0.0f) {
			player.GetComponent<WingBodySpriteControl>().thrustSprite.transform.localScale =  new Vector3(2.0f, 2.5f, 1.0f);
		} else {
			player.GetComponent<WingBodySpriteControl>().thrustSprite.transform.localScale =  new Vector3(1.5f, 1.5f, 1.0f);
		}

	}



	public override void useAbility(){
		this.Boost ();
	}

	void Boost(){
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		if(boostingCooldown < 0.0f){
			PlayerPlaneController playerPlaneControl = player.GetComponent<PlayerPlaneController>();
			playerPlaneControl.maxSpeedScalar += boostMaxSpeedScalar;
			playerPlaneControl.thrustForceScalar += boostThrustForceScalar;

			boostingCooldown = timeBetweenBoosts;
			boostingTimer = howLongBoostingLasts;
		}
	}

}
