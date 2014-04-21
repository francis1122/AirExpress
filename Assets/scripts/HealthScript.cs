using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour {
	public int currentHealth = 8;
	public int maxHealth = 8;

	// a float between 0.0 and 1.0
	private float playerEnergy = 1.0f; 

	public bool isPlayer;
	// Use this for initialization

	// background image that is 256 x 32
	public Texture2D bgImage; 
	
	// foreground image that is 256 x 32
	public Texture2D fgImage; 

	//flash damage
	private float flashCoolDown;

	void Start () {
	
	}

	void OnGUI () {
		if (isPlayer) {
						// Create one Group to contain both images
						// Adjust the first 2 coordinates to place it somewhere else on-screen
						GUI.BeginGroup (new Rect (20, 20, 256, 52));
		
						// Draw the background image
						GUI.Box (new Rect (00, 00, 256, 32), bgImage);
		
						// Create a second Group which will be clipped
						// We want to clip the image and not scale it, which is why we need the second Group
						GUI.BeginGroup (new Rect (00, 00, playerEnergy * 256, 32));
		
						// Draw the foreground image
						GUI.Box (new Rect (00, 00, 256, 32), fgImage);
		
						// End both Groups
						GUI.EndGroup ();
		
						GUI.EndGroup ();
				}
	}


	// Update is called once per frame
	void Update () {
		//get health percent
		playerEnergy = (float)currentHealth / (float)maxHealth;
		flashCoolDown -= Time.deltaTime;
		if (flashCoolDown < 0.0) {
			WingBodySpriteControl sprites = GetComponent<WingBodySpriteControl>();
			if(isPlayer){
			//	if(sprites.body)
			//		sprites.body.GetComponent<SpriteRenderer>().color = Color.grey;
			//	if(sprites.wings)
			//		sprites.wings.GetComponent<SpriteRenderer>().color = Color.grey;
			}else{
				if(sprites.body)
					sprites.body.GetComponent<SpriteRenderer>().color = Color.black;
				if(sprites.wings)
					sprites.wings.GetComponent<SpriteRenderer>().color = Color.black;
			}
		}

	}

	public void offsetHealth(int healthOffset){
		currentHealth += healthOffset;
		if (healthOffset < 0) {
			//WingBodySpriteControl sprites = GetComponent<WingBodySpriteControl>();
	//		if(sprites.body)
			//	sprites.body.GetComponent<SpriteRenderer>().color = Color.red;
		//	if(sprites.wings)
			//	sprites.wings.GetComponent<SpriteRenderer>().color = Color.red;
			
			flashCoolDown = .06f;
				}
		}
}
