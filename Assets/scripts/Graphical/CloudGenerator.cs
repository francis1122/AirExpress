using UnityEngine;
using System.Collections;

public class CloudGenerator : MonoBehaviour {

	public GameObject cloudPrefab;
	GameObject player;

	public float spawnCloudRange = 7.0f;

	public float deleteCloudRange = 15.0f;

	public int cloudAmount = 30;
	public ArrayList activeClouds = new ArrayList();

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		InitialAddClouds ();
	}
	
	// Update is called once per frame
	void Update () {
			RemoveOutOfRangeClouds ();
			AddClouds ();
	}

	void InitialAddClouds(){
		while (activeClouds.Count < cloudAmount) {
			Vector3 cloudPosition = (Vector3)Random.insideUnitCircle;
			cloudPosition *= deleteCloudRange;
			cloudPosition += player.transform.position;
			cloudPosition.z = 1;
			GameObject cloud = (GameObject)Instantiate (cloudPrefab, cloudPosition, Quaternion.identity);
			activeClouds.Add (cloud);
		}

	}

	void AddClouds(){

		while (activeClouds.Count < cloudAmount) {
			Vector3 cloudPosition = (Vector3)Random.insideUnitCircle;
			cloudPosition.Normalize();
			cloudPosition *= spawnCloudRange;
			cloudPosition += player.transform.position;
			cloudPosition.z = 4;
			GameObject cloud = (GameObject)Instantiate (cloudPrefab, cloudPosition, Quaternion.identity);
			activeClouds.Add (cloud);
		}
	}

	void RemoveOutOfRangeClouds(){
		for(int i = activeClouds.Count - 1; i >= 0; i--){
			GameObject cloud = (GameObject)activeClouds[i];
			Vector2 position2d = (Vector2)cloud.transform.position;
			Vector2 playerPosition2d = (Vector2)player.transform.position;
			float distance = Vector2.Distance(position2d, playerPosition2d);
			if(distance > deleteCloudRange){
				GameObject.DestroyObject(cloud);
				activeClouds.Remove(cloud);
			}
		}
	}
}
