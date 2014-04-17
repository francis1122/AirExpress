using UnityEngine;
using System.Collections;

public class OpenCityMenu : ActionExecute {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void Execute(){
		if (!IsEnemyCloseToCity ()) {
			if(!IsPlayerMovingTooFast()){
				GameObject cityMenu = GameObject.FindGameObjectWithTag ("CityMenu");
				NGUITools.SetActiveChildren (cityMenu, true);
				Time.timeScale = 0.0f;
				//heal player
				GameObject player = GameObject.FindGameObjectWithTag ("Player");
				player.GetComponent<HealthScript> ().currentHealth = player.GetComponent<HealthScript> ().maxHealth;
			}
		} else {
			//TODO give player feedback as to why they can't enter the city
		}
		//NGUITools.SetActive (cityMenu, true);
	}
}
