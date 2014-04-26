using UnityEngine;
using System.Collections;

public class PackageScript : MonoBehaviour {

	public Vector3 dropoffLocation;
	public GameObject dropoffObject;
	public bool isActive;
	public Color colorRep;
	public Sprite icon;
	public float maxSpeed = 1.0f;
	public float parachuteActivation = 2f;
	private bool parachuteActive = false;
	private float destroyPackageTimer;

	public GameObject indicatorObject;
	public GameObject indicatorPrefab;

	// Use this for initialization
	void Start () {
	}



	public void beginActive(){
		isActive = true;
		parachuteActive = false;
		destroyPackageTimer = 0.0f;
		GetComponent<SpriteRenderer>().enabled = true;
	}

	public void stopActive(){
		isActive = false;
		parachuteActive = false;
		GetComponent<SpriteRenderer>().enabled = false;
	}

	// Update is called once per frame
	void Update () {
		if (isActive) {

			destroyPackageTimer += Time.deltaTime;
			if(!parachuteActive){
				if(destroyPackageTimer > parachuteActivation){
					parachuteActive = true;
				}
			}

			if(destroyPackageTimer > 8.0f){
				//destroy package
				GameManager.deliveryManager.destroyPackage(gameObject);
			}

			//check for dropoff object
			if(Vector2.Distance((Vector2)transform.position,(Vector2)dropoffObject.transform.position) < 0.7f){
				GameManager.playerManager.money += 10;
				GameManager.deliveryManager.destroyPackage(gameObject);
			}
		}
	}

	void FixedUpdate(){
		if (isActive) {
			if (parachuteActive) {
				Vector2 rigidVel = rigidbody2D.velocity;
				// don't go faster than maxSpeed
				if (rigidVel.magnitude > maxSpeed) {
						//clamp
						rigidVel.Normalize ();
						rigidVel *= maxSpeed;
						rigidbody2D.velocity = rigidVel;
				}
			}
		}
	}
}
