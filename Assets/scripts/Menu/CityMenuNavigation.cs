using UnityEngine;
using System.Collections;

public class CityMenuNavigation : MonoBehaviour {

	public string execute;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnClick(){
		Invoke (execute, 0.0f);
		}

	void CloseMenu(){
		GameObject cityMenu = GameObject.FindGameObjectWithTag ("CityMenu");
		NGUITools.SetActiveChildren (cityMenu, false);
		Time.timeScale = 1.0f;
	}

	void AddDelivery(){
		//create a delivery and add it to the Deilivery Manager
	//	DeliveryLocation delivery = new DeliveryLocation ();
		//DeliveryLocation bulletTemp = (DeliveryLocation)Instantiate (DeliveryLocation, transform.position, Quaternion.identity);
	//	GameManager.deliveryManager.activeDeliveryArray.Add (delivery);
	//	Debug.Log("added delivery" + GameManager.deliveryManager.activeDeliveryArray.Count);

		//find all delivery locations on map
		if (GameManager.deliveryManager.activeDeliveryArray.Count < 3) {
						GameObject[] deliveryArray = GameObject.FindGameObjectsWithTag ("DeliveryLocation");
			int possible = Mathf.RoundToInt( Random.value * 100000.0f )%deliveryArray.Length;
						GameManager.deliveryManager.addPackage (deliveryArray [possible].transform.position);
				}
		//	delivery.location = deliveryArray [0].transform.position;

		// choose one of those and spawn a package there
		}
}
