using UnityEngine;
using System.Collections;


//requires DefendBehavior and FollowBehavior
public class Strategy_DefendFollow : PlaneStrategy {

	DefendBehavior defendBehavior;
	FollowBehavior followBehavior;
	public float followLimit = 20.0f;
	public float aggroRadius = 4.0f;

	// Use this for initialization
	void Start () {
		GetComponent<EnemyScript> ().isAggro = false;
		activeBehavior = GetComponent<DefendBehavior> ();
		defendBehavior = GetComponent<DefendBehavior> ();
		followBehavior = GetComponent<FollowBehavior> ();
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log ("update");
		//use follow behavior if player is close
		if (activeBehavior == followBehavior) {
			//Debug.Log ("defend");
			GameObject player = GameObject.FindGameObjectWithTag ("Player");
			float distance = Vector2.Distance ((Vector2)player.transform.position, (Vector2)defendBehavior.positionToDefend);
			if (distance > followLimit) {
				defendBehavior.chooseNewPatrolRoute();
				activeBehavior = defendBehavior;
				GetComponent<EnemyScript> ().isAggro = false;
			}
		} else {
			//Debug.Log ("follow");
			// switch from follow behavior to defend behavior if far away from defending spot
			GameObject player = GameObject.FindGameObjectWithTag ("Player");
			float distance = Vector2.Distance ((Vector2)player.transform.position, (Vector2)this.transform.position);
			if (distance < aggroRadius) {
				activeBehavior = followBehavior;
				GetComponent<EnemyScript> ().isAggro = true;
			}
		}

		// switch from follow behavior to defend behavior if far away from defending spot
		activeBehavior.UpdateBehavior ();
	}
}
