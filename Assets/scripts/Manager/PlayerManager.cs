using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

	public int money;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);
	}

	void OnGUI () {
			// Create one Group to contain both images
			// Adjust the first 2 coordinates to place it somewhere else on-screen

		GUI.Label (new Rect (20, 60, 100, 50), "money: " + money);
		GUI.BeginGroup (new Rect (20, 200, 256, 52));
			
		// Draw the background imagee);
			
		GUI.EndGroup ();
		}


	// Update is called once per frame
	void Update () {
	
	}
}
