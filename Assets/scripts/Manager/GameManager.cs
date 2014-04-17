
/*
(C) Your Happy Client
 
Grid - is a standard, general, utility used in the whole project (and every Unity project)
 
It is - essentially - pseudo-singletons for game engines.
 
it's this simple .. this is a handy class which allows you to "call other stuff easily"
That's all there is to it.  you can simply call anything like this:
 
    Grid.expertiseSystem.whatever()
 
it's that simple.  it is so simple you should be saying "this can't be right?!" Hah hah! But it is right!
 
For the stuff you have to call, it is almost as if they are static classes - BUT - 
they are normal game engine -style monobehaviors, in the scene, attached to a real
normal game object, which you can see and use in the correct normal ways with no confusion.
 
Philosophy:
 
(1) singletons are useless in game engines, so that's out
 
(2) generally in the real world in game engines, statics are out since you want stuff
to actually be "attached to a game object" so that you can enjoy critically three thingsm namely
(A) coroutines and (B) dragging-in-the-editor-variables and finally
(C) easy consistent serialization, persistence over scenes, yadda yadda
 
Note that this is, in fact, essential for crap like objects that connect to the internet,
perform "background processes" in Unity so to speak, and so on and on.
 
note for clarity, the word "Grid" is meaningless.  it could be any term like "Switchboard"
"HandyThing"  "MegaConnector" or some general exciting acronym from the project. "Grid" is short.
 
//          HOW TO USE
 
(1) Look at the constructor below. Do not hesitate to add in more "stuff" you want to access easily.
 
(2) If you have something that is a singleton. completely eliminate the singleton aspect: and then you
won't have any singletons. problem solved. make it a totally normal "thing" on a game object and use this.
 
(3) if you have something that is a static.  It's very hard to put in to words when things "are ok to be"
a static.  (Something like: "If it's a mathematical-like calculation" or "it almost never changes ever
and has nothing whatsoever to do with game data")  But if it's causing the slightest confusion as a static,
is hard to serialise, if it changes, if it needs coroutines, or anything else - just convert it as in (2).
 
(4) Notice you will now have a peaceful easy feeling and in particular that "It can't be this easy?" feeling.
Life is ridiculously simple with game engines! enjoy the paradigm!
 
 
 
(*) there's a question of whether to attach the items on __Operations, or, their "own" gameObjects.
in a word, either is perfectly OK.  Sometimes the scripts have other associated stuff, so perhaps
generally better with their own gameObjects.
Thus, for each of your "Grid items" (Brains.cs, Comms.cs, Mapping.cs) you will have one game objects
named like: __Brains, __Comms, __Mapping.
They will all be persistent, and all will be referenced in the script below, this script will use them.
Recall that once you actually hit play and the editor moves to your Scene1, you WILL STILL SEE THOSE
in the Hierarchy (ie, when the game is Play ing) - it's super handy and convenient.
 
(*) Just to be clear there's only one SCENE "__preEverythingScene"
That is the one and only "systems related" scene in the game. (and it MUST BE scene 0, and at the TOP
of the scene list in project settings)
 
 
(*) Note to be clear.  This script Grid.cs ... you DON'T attach this script itself to anything.  (It's not a
monobehaviour, so you would not be able to attach it to anything.)   All the items in question that you want
to gridize, eg ModelFeedTable, Brains, Mapping etc, would all be monobehaviors and you attach those to a game
object (see previous note.) This script Grid.cs takes care of your items on __preEverythingScene,
but it is not attached to a go, it is just a plain c# class.
 
 
version of 4/2013
*/


using UnityEngine;

static class GameManager
{
	
	
	// so, these items are available project-wide: Enjoy!
	
	public static SceneManager sceneManager;
	public static EventManager eventManager;
	public static DeliveryManager deliveryManager;
	public static PlayerManager playerManager;

	// when the program launches, Grid will check that all the needed elements are in place
	// that's exactly what you do in the static constructor here:
	static GameManager()
	{
		GameObject g;

		g = safeFind("__SceneManager");
		sceneManager = (SceneManager)SafeComponent( g, "SceneManager" );

		g = safeFind ("__EventManager");
		eventManager = (EventManager)SafeComponent (g, "EventManager");

		g = safeFind ("__DeliveryManager");
		deliveryManager = (DeliveryManager)SafeComponent (g, "DeliveryManager");

		g = safeFind ("__PlayerManager");
		playerManager = (PlayerManager)SafeComponent (g, "PlayerManager");

		//Application.LoadLevel("TestScene");

		// PS. annoying arcane technical note - remember that really, in c# static constructors do not run
		// until the FIRST TIME YOU USE THEM.  almost certainly in any large project like this, Grid
		// would be called zillions of times by all the Awake (etc etc) code everywhere, so it is
		// a non-issue. but if you're just testing or something, it may be confusing that (for example)
		// the wake-up alert only appears just before you happen to use Grid, rather than "when you hit play"
		// you may want to call "SayHello" from the GeneralOperations.cs, just to decisively start this script.
	}
	
	
	// SayHello() has no purpose other than for developers wondering HTF you use Grid.
	// just type Grid.SayHello() anywhere in the project.
	// it is useful to add a similar routine to (example) ExpertiseSystem.cs
	// then from anywhere in the project, you can type Grid.expertiseSystem.SayHello()
	// to check everything is hooked-up properly.
	public static void SayHello()
	{
		Debug.Log("Confirming to developer that the Grid is working fine.");
	}
	
	// when Grid wakes up, it checks everything is in place - it uses these routines to do so
	private static GameObject safeFind(string s)
	{
		GameObject g = GameObject.Find(s);
		if ( g == null ) BigProblem("The " +s+ " game object is not in this scene. You're stuffed.");
		return g;
	}
	private static Component SafeComponent(GameObject g, string s)
	{
		Component c = g.GetComponent(s);
		if ( c == null ) BigProblem("The " +s+ " component is not there. You're stuffed.");
		return c;
	}
	private static void BigProblem(string error)
	{
		for (int i=4;i>0;--i) Debug.LogError(" >>>>>>>>>>>> Cannot proceed... " +error);
		for (int i=4;i>0;--i) Debug.LogError(" !!! Is it possible you just forgot to launch from scene zero, the __preEverythingScene scene.");
		Debug.Break();
	}
}


/*
public class GameManager : MonoBehaviour{
	public bool gameIsActive;
	public GameObject enemyObject;
	private float spawnEnemyTimer;

	protected GameManager() {}
	private static GameManager _instance = null;
	
	public static GameManager Instance { 
		get {
			if (_instance == null)
			{
				Debug.Log("instantiate");
				GameObject go = new GameObject();
				_instance = go.AddComponent<GameManager>();
				go.name = "GameManager";
				//keeps the objects from getting destroyed between scenes
				DontDestroyOnLoad(go);

			}
			
			return _instance; 
		} 
	}
	
	void Update(){
		float dt = Time.fixedDeltaTime;

			if (gameIsActive) {
			Debug.Log ("aaaaaaaaaa");	
			spawnEnemyTimer += dt;
			if(spawnEnemyTimer > 1.0f){
				spawnEnemyTimer = 0.0f;
				Debug.Log ("enemy");
				//spawn an enemy
				GameObject player = GameObject.FindGameObjectWithTag("Player");
				Vector3 pos = player.transform.position;
				//+ new Vector3(1.0f,1.0f,0.0f)
				GameObject enemy = (GameObject)Instantiate(enemyObject, pos, Quaternion.identity);
			}
			}
	}

}*/