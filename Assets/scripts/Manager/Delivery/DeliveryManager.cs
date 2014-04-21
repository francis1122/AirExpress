using UnityEngine;
using System.Collections;
public class DeliveryManager : MonoBehaviour {

	public GameObject[] activeDeliveryArray = new GameObject[4];
	public GameObject package;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void addPackage(){
		GameObject[] deliveryArray = GameObject.FindGameObjectsWithTag ("DeliveryLocation");
		int possible = Mathf.RoundToInt( Random.value * 100000.0f )%deliveryArray.Length;
		GameObject dropoffObject = deliveryArray [possible];
		Vector3 pos = dropoffObject.transform.position;



		for(int i = 0; i < activeDeliveryArray.Length; i++){
			if(activeDeliveryArray[i] == null){
				GameObject newPackage = (GameObject)Instantiate (package, pos, Quaternion.identity);
				PackageScript script = newPackage.GetComponent <PackageScript> ();
				if(i == 0){
					script.colorRep = Color.blue;
				}else if(i == 1){
					script.colorRep = Color.red;
				}else if (i == 2){
					script.colorRep = Color.yellow;
				}else if( i == 3){
					script.colorRep = Color.magenta;
				}

				script.dropoffObject = dropoffObject;
				script.dropoffLocation = pos;
				script.isActive = false;
				newPackage.SetActive (false);
				activeDeliveryArray[i] = newPackage;
				dropoffObject.renderer.enabled = true;
				//update interface
				GameObject planeInterface = GameObject.FindGameObjectWithTag ("PlaneInterface");
				Interface_Plane yeah = planeInterface.GetComponent<Interface_Plane> ();
				yeah.updateInterface ();

				break;
			}
		}

	}

	public void collectPackage(GameObject package){
		for(int i = 0; i < activeDeliveryArray.Length; i++){
			if(activeDeliveryArray[i] == null){
				activeDeliveryArray[i] = package;
				PackageScript script = package.GetComponent <PackageScript> ();
				script.isActive = false;
				package.SetActive (false);
				//activeDeliveryArray.Add (newPackage);
				GameObject planeInterface = GameObject.FindGameObjectWithTag ("PlaneInterface");
				Interface_Plane yeah = planeInterface.GetComponent<Interface_Plane> ();
				yeah.updateInterface ();
				break;
			}
		}
	}

	public void removePackage(GameObject packageToBeRemoved){
		for (int i = 0; i < activeDeliveryArray.Length; i++) {
			if(activeDeliveryArray[i] == packageToBeRemoved){
				//GameObject.DestroyObject (packageToBeRemoved);
				activeDeliveryArray[i] = null;
				break;
			}
		}
		//activeDeliveryArray.Add (newPackage);
		GameObject planeInterface = GameObject.FindGameObjectWithTag ("PlaneInterface");
		Interface_Plane yeah = planeInterface.GetComponent<Interface_Plane> ();
		yeah.updateInterface ();
		//activeDeliveryArray.Remove (packageToBeRemoved);


	}
}
