using UnityEngine;
using System.Collections;

//circles around an area and attacks whoever gets close and follows for only a certain distance

public class DefendBehavior : PlaneBehavior {

	public float targetLocationUpdate = 0.0f;
	public Vector3 positionToDefend;
	public GameObject objectToDefend = null;
	public float radiusOfDefense = 5.0f;

	private Vector3 defenseTargetLocation;
	private float distanceForIntersection = 1.5f;
	private bool switchDefensiveSize = true;
	
	// Use this for initialization
	void Start () {

		chooseNewPatrolRoute ();

	}

	public void chooseNewPatrolRoute(){
		PlaneController planeController = this.GetComponent<PlaneController> ();
		
		defenseTargetLocation = positionToDefend;
		if (objectToDefend != null) {
			positionToDefend = objectToDefend.transform.position;
		}
		switchDefensiveSize = !switchDefensiveSize;
		Vector3 newPos;
		if(switchDefensiveSize){
			Vector3 test = new Vector3(radiusOfDefense, (Random.value - 0.5f) * radiusOfDefense, 0f);
			newPos = positionToDefend + test;
		}else{
			Vector3 test = new Vector3(-radiusOfDefense, (Random.value - 0.5f) * radiusOfDefense, 0f);
			newPos = positionToDefend + test;
		}
		
		defenseTargetLocation = newPos;
		planeController.targetLocation = defenseTargetLocation;
	}

	// Update is called once per frame
	public override void UpdateBehavior () {
		targetLocationUpdate += Time.deltaTime;
		if (objectToDefend != null) {
			positionToDefend = objectToDefend.transform.position;
		}

		//PlaneController planeController = this.GetComponent<PlaneController> ();
		// adjust targetLocation
		//if(targetLocationUpdate > (.2f + Random.value/2.0f)){

		float distance = Vector3.Distance(defenseTargetLocation, transform.position);
			
			//Vector3 newPosition = (Vector3)Random.insideUnitCircle * .5f;
			//Vector3 playerFuturePosition = player.transform.position + (Vector3)player.rigidbody2D.velocity * 0.06f;
		if(distance < distanceForIntersection){
				//acquire new defenseTargetLocation
			chooseNewPatrolRoute();
		}


				
		//}
	}
}
