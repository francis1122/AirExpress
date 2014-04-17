using UnityEngine;
using System.Collections;

public class PlaneStrategy : MonoBehaviour {
	protected PlaneBehavior activeBehavior;

	// Use this for initialization
	void Start () {
		activeBehavior = GetComponent<FollowBehavior> ();
		GetComponent<EnemyScript> ().isAggro = true;
	}
	
	// Update is called once per frame
	void Update () {
		activeBehavior.UpdateBehavior ();
	}
}
