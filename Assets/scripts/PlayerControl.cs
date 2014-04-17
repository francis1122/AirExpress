using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {


	public Texture2D deliveryDirection;
	public Texture2D cityDirection;
	public Texture2D enemyDirection;

	public bool Forward;
	public bool Backward;
	public bool Left;
	public bool Right;
	public bool Action;
	public bool isFire;
	public bool isBoost;

	void Start ()
	{
		GameObject cityMenu = GameObject.FindGameObjectWithTag ("CityMenu");
		NGUITools.SetActiveChildren (cityMenu, false);
	}
	
	void OnGUI () {

		//closest city
		GameObject closestCity = null;
		float minDistance = 1000000.0f; 
		foreach (GameObject city in GameObject.FindGameObjectsWithTag("City")) {
			Vector2 playerPos = (Vector2)transform.position;
			Vector2 cityPos = (Vector2)city.transform.position;
			float distance = Vector2.Distance(playerPos, cityPos);
			if(distance < minDistance){
				minDistance = distance;
				closestCity = city;
			}
		}
		if(closestCity){
			if(minDistance < 4.0f){

			}else{
				Vector2 cityPosition = closestCity.transform.position;
				Vector2 direction = (cityPosition - (Vector2)transform.position );
				direction.y *= -1.0f;
				direction.Normalize();
				GUI.Box(new Rect(Screen.width/2 + direction.x * 350.0f, Screen.height/2 + direction.y * 250.0f,32,32), cityDirection);
			}
		}


		//direciton to packages
		int i = 0;
		foreach (GameObject package in GameManager.deliveryManager.activeDeliveryArray) {
			Vector3 packagePosition = package.transform.position;
			packagePosition.z = -9.0f;
			Vector3 direction = (packagePosition - transform.position );
			direction.y *= -1.0f;
			direction.Normalize();
			GUI.Box(new Rect(Screen.width/2 + direction.x * 350.0f, Screen.height/2 + direction.y * 250.0f,32,32), deliveryDirection);
			i++;
		}

		foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")) {
			Vector3 enemyPosition = enemy.transform.position;
			enemyPosition.z = -9.0f;
			float distance = Vector3.Distance(enemyPosition, transform.position);
			if(distance < 4.0f){
				
			}else if(distance > 10.0f){

			}else{
				Vector3 direction = (enemyPosition - transform.position );
				direction.y *= -1.0f;
				direction.Normalize();
				GUI.Box(new Rect(Screen.width/2 + direction.x * 350.0f, Screen.height/2 + direction.y * 250.0f,16,16), enemyDirection);

			}
		}
	}

	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.UpArrow)){ 
			Forward = true;
		}
		if(Input.GetKeyDown(KeyCode.DownArrow)){ 
			Backward = true;
		}
		if(Input.GetKeyDown(KeyCode.RightArrow)){ 
			Right = true;
		}
		if(Input.GetKeyDown(KeyCode.LeftArrow)){ 
			Left = true;
		}
		if(Input.GetKeyDown (KeyCode.X)){
			isBoost= true;
		}

		if (Input.GetKeyDown (KeyCode.C)) {
			Action = true;
		}

		if(Input.GetKeyUp(KeyCode.UpArrow)){ 
			Forward = false;
		}
		if(Input.GetKeyUp(KeyCode.DownArrow)){ 
			Backward = false;
		}
		if(Input.GetKeyUp(KeyCode.RightArrow)){ 
			Right = false;
		}
		if(Input.GetKeyUp(KeyCode.LeftArrow)){ 
			Left = false;
		}

		if(Input.GetKeyUp (KeyCode.X)){
			isBoost= false;
		}

		if (Input.GetKeyUp (KeyCode.C)) {
			Action = false;
		}

		if(Input.GetKeyDown(KeyCode.Space)){ 
			isFire = true;
		}

		if (Input.GetKeyUp (KeyCode.Space)) { 
			isFire = false;
		}
	

		if (Action) {
			this.ActionCheck();
		}

	}

	void ActionCheck(){
		//cycle through action objects and check for proximity
		ActionScript[] actionArray = GameObject.FindObjectsOfType<ActionScript>();
		foreach (ActionScript actionScript in actionArray) {
			//check for collision
			if(actionScript.isActivatable){
				//activate script
				ActionExecute actionExecute = (ActionExecute)actionScript.execute;
				actionExecute.Execute();
				//AtionExecute test = (ActionExecute) ScriptableObject.CreateInstance(script.execute.GetClass ());
				//ActionExecute test = (ActionExecute) System.Activator.CreateInstance(script.execute.GetClass());

				//test.Execute();
				Action = false;
				return;
			}


				}
		}


	void FixedUpdate () {

	}

	void OnTriggerEnter2D(Collider2D collision){
		if (collision.gameObject.tag == "package") {
			GameManager.deliveryManager.removePackage(collision.gameObject);

			//should reward the player here eventually
		}

		if (collision.gameObject.tag == "Enemy") {
			//Application.LoadLevel("StartMenuScene");
			this.GetComponent<HealthScript>().offsetHealth(-1);
			if(this.GetComponent<HealthScript>().currentHealth <= 0){
				GameManager.eventManager.gameIsActive = false;
				Application.LoadLevel("StartMenuScene");
			}
		}
		if (collision.gameObject.tag == "EnemyBullet") {
			//Application.LoadLevel("StartMenuScene");
			this.GetComponent<HealthScript>().offsetHealth(-1);
			if(this.GetComponent<HealthScript>().currentHealth <= 0){
				GameManager.eventManager.gameIsActive = false;
				Application.LoadLevel("StartMenuScene");
			}
		}
		if (collision.gameObject.tag == "Item") {
			GameManager.playerManager.money += 1;
			//TODO: should be in a coin/item script
			GameObject.DestroyObject(collision.gameObject);
		}

	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "Enemy") {
			//Application.LoadLevel("StartMenuScene");
			this.GetComponent<HealthScript>().offsetHealth(-1);
			if(this.GetComponent<HealthScript>().currentHealth <= 0){
				GameManager.eventManager.gameIsActive = false;
				Application.LoadLevel("StartMenuScene");
			}
		}

	}
}
