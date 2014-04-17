using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour {
	public bool gameIsActive;
	public GameObject enemyObject;
	public GameObject enemyDefendObject;
	private float spawnEnemyTimer;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		float dt = Time.deltaTime;
		if (gameIsActive) {
			spawnEnemyTimer += dt;
			if(spawnEnemyTimer > 15.0f){
				if(!IsPlayerCloseToCity()){
					DefenderEvent();
				}else{
					spawnEnemyTimer = 10.0f;
				}
			}
		}
	}

	void FollowerEvent(){
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		Vector3 pos = player.transform.position;
		for( int i = 0; i < 5; i++){
			spawnEnemyTimer = 0.0f;
			//spawn an enemy
			Vector3 newPosition = (Vector3)Random.insideUnitCircle * 5f;
			Vector2 norm = player.rigidbody2D.velocity.normalized;
			pos += (Vector3)norm * 30.0f + newPosition;
			pos.z = -9;
			//+ new Vector3(1.0f,1.0f,0.0f)
			GameObject enemy = (GameObject)Instantiate(enemyObject, pos, Quaternion.identity);
		}
	}

	void DefenderEvent(){
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		Vector3 pos = player.transform.position;
		Vector2 normVel = player.rigidbody2D.velocity.normalized;
		pos += (Vector3)normVel * 13.0f;
		pos.z = -9;

		Vector3 defensePosition = (Vector3)Random.insideUnitCircle * 4f;
		defensePosition = pos + defensePosition;
		defensePosition.z = -9;
		for( int i = 0; i < 8; i++){
			spawnEnemyTimer = 0.0f;
			//spawn an enemy

			Vector3 newPosition = (Vector3)Random.insideUnitCircle * 4f;
			newPosition += pos;

			//+ new Vector3(1.0f,1.0f,0.0f)
			GameObject enemy = (GameObject)Instantiate(enemyDefendObject, newPosition, Quaternion.identity);
			enemyDefendObject.GetComponent<DefendBehavior>().positionToDefend = defensePosition;
		}
	}

	bool IsPlayerCloseToCity(){
		float minDistance = 10000.0f;
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		Vector2 playerPos = (Vector2)player.transform.position;
		GameObject[] cities = GameObject.FindGameObjectsWithTag ("City");
		foreach (GameObject city in cities) {
			Vector2 cityPos = (Vector2)city.transform.position;
			float cityDistance = Vector2.Distance(cityPos, playerPos);
			if(cityDistance < minDistance){
				minDistance = cityDistance;
			}
		}

		if (minDistance < 8.0f) {
			return true;
		}
		return false;

	}
}
