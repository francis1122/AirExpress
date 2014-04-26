using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {


	public Texture2D deliveryDirection;
	public Texture2D cityDirection;
	public Texture2D enemyDirection;
	public ArrayList abilityArray =  new ArrayList();


	public bool Forward;
	public bool Backward;
	public bool Left;
	public bool Right;
	public bool Action;
	public bool isFire;
	public bool isBoost;
	public bool isOne;
	public bool isTwo;
	public bool isThree;
	public bool isFour;

	public bool isPackageOne;
	public bool isPackageTwo;
	public bool isPackageThree;
	public bool isPackageFour;

	void Start ()
	{
		foreach(GameObject ability in GameManager.playerManager.abilityArray){
			GameObject newAbility = (GameObject)GameObject.Instantiate(ability, transform.position, Quaternion.identity);
			newAbility.transform.parent = this.transform;
			abilityArray.Add (newAbility);
			//add weapons the ship should have
			//this.gameObject.AddComponent(ability);
		//	gameObject.AddComponent(ability);
		}

		//create interface for weapons
		GameObject planeInterface = GameObject.FindGameObjectWithTag ("PlaneInterface");
		Interface_Plane yeah = planeInterface.GetComponent<Interface_Plane> ();
		yeah.createInterface ();

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
		GameObject[] packageArray = GameManager.deliveryManager.activeDeliveryArray;
		//foreach (GameObject package in GameManager.deliveryManager.activeDeliveryArray) {
		for(int i = 0; i < packageArray.Length; i++){
			GameObject package = packageArray[i];
			if(package != null){
				PackageScript script = package.GetComponent<PackageScript>();
				GameObject dropoffObject = script.dropoffObject;
				Vector3 packagePosition = dropoffObject.transform.position;
				packagePosition.z = -9.0f;
				Vector3 direction = (packagePosition - transform.position );
				direction.y *= -1.0f;
				direction.Normalize();
				GUI.Box(new Rect(Screen.width/2 + direction.x * 350.0f, Screen.height/2 + direction.y * 250.0f,32,32), deliveryDirection);
			}
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

		//abilities
		if(Input.GetKeyDown(KeyCode.Alpha1)){ 
		 	isOne= true;
		}
		if(Input.GetKeyDown(KeyCode.Alpha2)){ 
			isTwo = true;
		}
		if(Input.GetKeyDown(KeyCode.Alpha3)){ 
			isThree= true;
		}
		if(Input.GetKeyDown(KeyCode.Alpha4)){ 
			isFour = true;
		}

		//packages

		if(Input.GetKeyDown(KeyCode.Q)){ 
			isPackageOne= true;
		}
		if(Input.GetKeyDown(KeyCode.W)){ 
			isPackageTwo = true;
		}
		if(Input.GetKeyDown(KeyCode.E)){ 
			isPackageThree = true;
		}
		if(Input.GetKeyDown(KeyCode.R)){ 
			isPackageFour = true;
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
		//abilities
		if(Input.GetKeyUp(KeyCode.Alpha1)){ 
			isOne = false;
		}
		if(Input.GetKeyUp(KeyCode.Alpha2)){ 
			isTwo = false;
		}
		if(Input.GetKeyUp(KeyCode.Alpha3)){ 
			isThree = false;
		}
		if(Input.GetKeyUp(KeyCode.Alpha4)){ 
			isFour = false;
		}
		//packages
		if(Input.GetKeyUp(KeyCode.Q)){ 
			isPackageOne = false;
		}
		if(Input.GetKeyUp(KeyCode.W)){ 
			isPackageTwo = false;
		}
		if(Input.GetKeyUp(KeyCode.E)){ 
			isPackageThree = false;
		}
		if(Input.GetKeyUp(KeyCode.R)){ 
			isPackageFour = false;
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
		int max = abilityArray.Count;
		for(int i = 0; i < max; i++){

			GameObject cat = (GameObject)abilityArray[i];
			Weapon_Base weapon = cat.GetComponent<Weapon_Base>();
			//weapon.WeaponUpdate();
			//Weapon_Base weapon = (Weapon_Base)GameManager.playerManager.abilityArray[i];
			if(i == 0){
				if(isOne)
					weapon.useAbility();
			}else if(i == 1){
				if(isTwo)
					weapon.useAbility();
			}else if(i == 2){
				if(isThree)
					weapon.useAbility();
			}else if(i == 3){
				if(isFour)
					weapon.useAbility();
			}
		}

		max = GameManager.deliveryManager.activeDeliveryArray.Length;
		for(int i = 0; i < max; i++){
			
			GameObject package = (GameObject)GameManager.deliveryManager.activeDeliveryArray[i];
			//Weapon_Base weapon = cat.GetComponent<Weapon_Base>();
			//weapon.WeaponUpdate();
			//Weapon_Base weapon = (Weapon_Base)GameManager.playerManager.abilityArray[i];
			if(package){
				if(i == 0){
					if(isPackageOne)
						shootPackage(package);
				}else if(i == 1){
					if(isPackageTwo)
						shootPackage(package);
				}else if(i == 2){
					if(isPackageThree)
						shootPackage(package);
				}else if(i == 3){
					if(isPackageFour)
						shootPackage(package);
				}
			}
		}
	}

	public void shootPackage(GameObject package){
		package.transform.position = transform.position;
		PackageScript script = package.GetComponent<PackageScript> ();
		script.beginActive ();
		package.SetActive (true);

		PlayerPlaneController playerControl = GetComponent<PlayerPlaneController>();
		float testY = Mathf.Sin (playerControl.player_angle);
		float testX = Mathf.Cos (playerControl.player_angle);
		package.transform.position += new Vector3 (testX * .5f, testY * .5f, 0f);
		Vector2 planeVelocity = rigidbody2D.velocity;
		package.rigidbody2D.velocity = (new Vector2 (testX * 4.0f, testY * 4.0f)) + planeVelocity;
		GameManager.deliveryManager.removePackage (package);

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
			PackageScript script = collision.gameObject.GetComponent<PackageScript>();
			if(script.isActive){
				GameManager.deliveryManager.collectPackage(collision.gameObject);
			}

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
