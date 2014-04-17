using UnityEngine;
using System.Collections;
public class DeliveryManager : MonoBehaviour {

	public ArrayList activeDeliveryArray = new ArrayList();
	public GameObject package;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void addPackage(Vector3 pos){
		GameObject newPackage = (GameObject)Instantiate (package, pos, Quaternion.identity);
		activeDeliveryArray.Add (newPackage);
		}

	public void removePackage(GameObject packageToBeRemoved){
		activeDeliveryArray.Remove (packageToBeRemoved);
		GameObject.DestroyObject (packageToBeRemoved);
		GameManager.playerManager.money += 10;
	}
}
