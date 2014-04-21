using UnityEngine;
using System.Collections;

public class PackageScript : MonoBehaviour {

	public Vector3 dropoffLocation;
	public GameObject dropoffObject;
	public bool isActive;
	public Color colorRep;
	public Sprite icon;

	// Use this for initialization
	void Start () {
	}


	
	// Update is called once per frame
	void Update () {
		if (isActive) {
			//check for dropoff object
			if(Vector2.Distance((Vector2)transform.position,(Vector2)dropoffObject.transform.position) < 0.7f){
				GameManager.playerManager.money += 10;
				dropoffObject.renderer.enabled = false;
				GameObject.Destroy(gameObject);
			}
		}
	}
}
