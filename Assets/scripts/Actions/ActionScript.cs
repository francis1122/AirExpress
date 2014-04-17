using UnityEngine;
using System.Collections;

public class ActionScript : MonoBehaviour {
	
	public string executeScriptName;
	public ActionExecute execute;
	public bool isActivatable;
	// Use this for initialization
	void Start () {
		//execute = gameObject.AddComponent<executeScriptName> ();
		execute = (ActionExecute)gameObject.AddComponent (executeScriptName);
	}

	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D collision){
		if (collision.gameObject.tag == "Player") {
			isActivatable = true;
				}
		}

	void OnTriggerExit2D(Collider2D collision){
		if (collision.gameObject.tag == "Player") {
			isActivatable = false;
		}
	}
}