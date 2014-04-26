using UnityEngine;
using System.Collections;

public class FollowBehavior : PlaneBehavior {
	
	public float targetLocationUpdate = 0.0f;
	public Vector3 offsetLocation;

	// used to prevent perfect following and reduce bunching
	public float followCircleOffset = 1.55f;

	// distance till perfect following occurs
	public float perfectFollowDistance = 1.5f;

	
	// Use this for initialization
	void Start () {
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		//float distance = Vector3.Distance(player.transform.position, transform.position);
		// follow offset to reduce bunching
		Vector3 newPosition = (Vector3)Random.insideUnitCircle * followCircleOffset;
		offsetLocation = newPosition;
		//Vector3 playerFuturePosition = player.transform.position + (Vector3)player.rigidbody2D.velocity * 0.06f;
		PlaneController planeController = this.GetComponent<PlaneController> ();
		planeController.targetLocation = newPosition + player.transform.position ;
	}
	
	// Update is called once per frame
	public override void UpdateBehavior () {
		targetLocationUpdate += Time.deltaTime;
		PlaneController planeController = this.GetComponent<PlaneController> ();
		// adjust targetLocation
		if(targetLocationUpdate > (.2f + Random.value/2.0f)){
			GameObject player = GameObject.FindGameObjectWithTag ("Player");
			float distance = Vector3.Distance(player.transform.position, transform.position);
			
			//Vector3 newPosition = (Vector3)Random.insideUnitCircle * .5f;
			//Vector3 playerFuturePosition = player.transform.position + (Vector3)player.rigidbody2D.velocity * 0.06f;
			if(distance < perfectFollowDistance){
				planeController.targetLocation = player.transform.position ;
			}else{
				planeController.targetLocation = offsetLocation + player.transform.position ;
			}
		}
	}
}
