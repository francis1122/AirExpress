using UnityEngine;
using System.Collections;

public class Weapon_Player_Boost : MonoBehaviour {


	public float howLongBoostingLasts = 1.2f;
	public float timeBetweenBoosts = 4.5f;
	public float boostThrustForceScalar = .2f;
	public float boostMaxSpeedScalar = .3f;
	public float boostingTimer;
	public float boostingCooldown;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		PlayerControl playerControl = GetComponent<PlayerControl>();
		boostingCooldown -= Time.deltaTime;
		if (boostingTimer >= 0.0f) {
			boostingTimer -= Time.deltaTime;
			if(boostingTimer < 0.0f){
				PlayerPlaneController playerPlaneControl = GetComponent<PlayerPlaneController>();
				playerPlaneControl.maxSpeedScalar -= boostMaxSpeedScalar;
				playerPlaneControl.thrustForceScalar -= boostThrustForceScalar;
			}
		}
	
		if (playerControl.isBoost) {
			this.Boost();
		}

	}

	void Boost(){
		if(boostingCooldown < 0.0f){
			PlayerPlaneController playerPlaneControl = GetComponent<PlayerPlaneController>();
			playerPlaneControl.maxSpeedScalar += boostMaxSpeedScalar;
			playerPlaneControl.thrustForceScalar += boostThrustForceScalar;

			boostingCooldown = timeBetweenBoosts;
			boostingTimer = howLongBoostingLasts;
		}
	}

}
