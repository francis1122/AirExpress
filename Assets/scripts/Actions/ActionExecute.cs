using UnityEngine;
using System.Collections;

public class ActionExecute : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public virtual void Execute(){
	}

	protected bool IsEnemyCloseToCity(){
	//	float minDistance = 10000.0f;
	//	Vector2 actionPos = (Vector2)transform.position;
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		bool isAggro = false;
		foreach (GameObject enemy in enemies) {
			//Vector2 enemyPos = (Vector2)enemy.transform.position;
			//float enemyDistance = Vector2.Distance(enemyPos, actionPos);
			//if(enemyDistance < minDistance){
			EnemyScript enemyScript =  enemy.GetComponent<EnemyScript>();
			if(enemyScript != null){
				if(enemyScript.isAggro){
					//minDistance = enemyDistance;
					isAggro = true;
					break;
				}
			}
		}

		return isAggro;
		//if (minDistance < 8.0f) {

	}

	protected bool IsPlayerMovingTooFast(){
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		Vector2 playerVel = player.rigidbody2D.velocity;
		float speed = playerVel.magnitude;
		if(speed > 1.0f){
			return true;
		}
		return false;
	}
}
