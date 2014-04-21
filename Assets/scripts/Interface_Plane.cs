using UnityEngine;
using System.Collections;

public class Interface_Plane : MonoBehaviour {

	public GameObject abilityButton;
	private ArrayList abilityButtonArray = new ArrayList();
	private ArrayList deliveryButtonArray = new ArrayList();

	// Use this for initialization
	void Start () {
	//	Debug.Log ("screen" + Screen.width);
	//	Debug.Log ("screenheight" + Screen.height);
		//Debug.Log ("gameobject pos" + gameObject.transform.position);
		//Camera test = NGUITools.FindCameraForLayer ();
		//weapons

	}

	public void updateInterface(){
		deleteInterface ();
		createInterface ();
	}

	public void deleteInterface(){
	//	PlayerControl player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerControl> ();
		for (int i = abilityButtonArray.Count-1; i >= 0; i --) {
			GameObject button = (GameObject)abilityButtonArray[i];
			abilityButtonArray.Remove(button);
			GameObject.Destroy(button);
		}
		for (int i = deliveryButtonArray.Count-1; i >= 0; i --) {
			GameObject button = (GameObject)deliveryButtonArray[i];
			deliveryButtonArray.Remove(button);
			GameObject.Destroy(button);
		}
	}

	public void createInterface(){
		PlayerControl player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerControl> ();
		for (int i = 0; i < player.abilityArray.Count; i ++) {
			Vector3 pos = new Vector3(-.4f + .18f* i , -.85f, 0) + gameObject.transform.position;
			//GameObject button = (GameObject)Instantiate (abilityButton, pos, Quaternion.identity);
			//abilityButton.transform.position = pos;
			GameObject button = NGUITools.AddChild(this.gameObject, abilityButton);

			
			GameObject ability = (GameObject)player.abilityArray[i];
			Weapon_Base weapon = ability.GetComponent<Weapon_Base>();;
			UI2DSprite buttonSprite = button.GetComponentInChildren<UI2DSprite>();

			buttonSprite.sprite2D = weapon.icon;
			
			UILabel label = button.GetComponentInChildren<UILabel>();
			label.text = "" + (i + 1);
			button.transform.position = pos;
			abilityButtonArray.Add(button);
		}



		//create the package buttons
		//PlayerControl player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerControl> ();
		for (int i = 0; i < GameManager.deliveryManager.activeDeliveryArray.Length; i ++) {
			Vector3 pos = new Vector3(.4f + .18f* i , -.85f, 0) + gameObject.transform.position;
			//GameObject button = (GameObject)Instantiate (abilityButton, pos, Quaternion.identity);
			//abilityButton.transform.position = pos;
			GameObject package = GameManager.deliveryManager.activeDeliveryArray[i];

			if(package != null){
				GameObject button = NGUITools.AddChild(this.gameObject, abilityButton);
			
				//  GameObject ability = (GameObject)player.abilityArray[i];
				//	Weapon_Base weapon = ability.GetComponent<Weapon_Base>();;
				//	UI2DSprite buttonSprite = button.GetComponentInChildren<UI2DSprite>();
				//	buttonSprite.sprite2D = weapon.icon;
				UI2DSprite buttonSprite = button.GetComponentInChildren<UI2DSprite>();
				buttonSprite.color = package.GetComponent<PackageScript>().colorRep;
				buttonSprite.sprite2D = package.GetComponent<PackageScript>().icon;

				package.GetComponent<SpriteRenderer>().color = package.GetComponent<PackageScript>().colorRep;
				package.GetComponent<PackageScript>().dropoffObject.GetComponent<SpriteRenderer>().color = package.GetComponent<PackageScript>().colorRep;

				UILabel label = button.GetComponentInChildren<UILabel>();
				button.transform.position = pos;
				if(i == 0){
					label.text = "q";
				}else if( i == 1){
					label.text = "w";
				}else if( i == 2){
					label.text = "e";
				}else if( i == 3){
					label.text = "r";
				}

				deliveryButtonArray.Add(button);
			}
		}
	}


	
	// Update is called once per frame
	void Update () {
	
	}
	/*
	public static Vector3 GetObjectScreenDims(Transform trans)
	{
		var cam = FindCameraForLayer(trans.gameObject.layer);
		
		// the idea is to get the bounds of the object
		// which could be found in its collider, if it had one
		var collider = trans.collider;
		Bounds bounds;
		if (collider != null)
			bounds = collider.bounds;
		else
		{
			// if that fails, try the renderer
			var renderer = trans.renderer;
			if (renderer != null)
				bounds = renderer.bounds;
			else // if it didn't have a collider nor a renderer component, we fall back to:
				bounds = NGUIMath.CalculateAbsoluteWidgetBounds(trans);
		}
		
		// get the min and max in screen coords
		var sMax = cam.WorldToScreenPoint(bounds.max);
		var sMin = cam.WorldToScreenPoint(bounds.min);
		
		// subtract max from min to get what we want
		// now, (sMax - sMin).x is the width of the object,
		// (sMax - sMin).y is the height.
		return sMax - sMin;
	}*/
}
